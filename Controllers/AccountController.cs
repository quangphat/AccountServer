using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountServer.Business;
using AccountServer.Infrastructures;
using AccountServer.Models;
using CoreBusiness;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountServer.Controllers
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IClientStore _clientStore;
        private readonly IAccountBusiness _bizAccount;
        private readonly IEventService _events;
        public AccountController(IAuthenticationSchemeProvider schemeProvider,
            IClientStore clientStore,
            IAccountBusiness accountBusiness,
            CurrentProcess process
            , IEventService eventService
            , IIdentityServerInteractionService interaction) //: base(process)
        {
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _bizAccount = accountBusiness;
            _events = eventService;
        }
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // build a model so we know what to show on the login page
            var vm = await BuildLoginViewModelAsync(returnUrl);

            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return RedirectToAction("Challenge", "External", new { provider = vm.ExternalLoginScheme, returnUrl });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string button)
        {
            var user = await _bizAccount.Login(model.Email, model.Password);
            if (user != null)
            {
                var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
                //if (!string.IsNullOrWhiteSpace(obj.Avatar))
                //    obj.Avatar = Utils.GetAvatarFullPathByAMZ(_blobService.awsService.Host, obj.Avatar);
                await _events.RaiseAsync(new UserLoginSuccessEvent(user.Email, user.PersonId, user.Email, clientId: context?.ClientId));
                AuthenticationProperties props = null;
                if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                {
                    props = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                    };
                }
                var isuser = new IdentityServerUser(user.PersonId)
                {
                    DisplayName = user.Email
                };
                await HttpContext.SignInAsync(isuser, props);

                //if (context != null)
                //{
                //    if (await _clientStore.IsPkceClientAsync(context.ClientId))
                //    {
                //        // if the client is PKCE then we assume it's native, so this change in how to
                //        // return the response is for better UX for the end user.
                //        return this.LoadingPage("Redirect", model.ReturnUrl);
                //    }

                //    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                //    return Redirect(model.ReturnUrl);
                //}

            }
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            var user = model;
            return Ok();
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new LoginViewModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Username = context?.LoginHint,
                };

                if (!local)
                {
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null ||
                            (x.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName, StringComparison.OrdinalIgnoreCase))
                )
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;
            if (context?.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }
    }
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}