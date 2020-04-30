#pragma checksum "D:\Development\my8\AccountServer\Views\Account\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9237ea46a1a4170dc5929f7b82dee755a643b2a3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Login), @"mvc.1.0.view", @"/Views/Account/Login.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9237ea46a1a4170dc5929f7b82dee755a643b2a3", @"/Views/Account/Login.cshtml")]
    public class Views_Account_Login : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AccountServer.Models.LoginViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"login-page\">\r\n    <div class=\"page-header\">\r\n        <h1>Login</h1>\r\n    </div>\r\n\r\n    <partial name=\"_ValidationSummary\" />\r\n\r\n    <div class=\"row\">\r\n\r\n");
#nullable restore
#line 12 "D:\Development\my8\AccountServer\Views\Account\Login.cshtml"
         if (Model.EnableLocalLogin)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""col-sm-6"">
                <div class=""panel panel-default"">
                    <div class=""panel-heading"">
                        <h3 class=""panel-title"">Local Login</h3>
                    </div>
                    <div class=""panel-body"">

                        <form asp-route=""Login"">
                            <input type=""hidden"" asp-for=""ReturnUrl"" />

                            <fieldset>
                                <div class=""form-group"">
                                    <label asp-for=""Username""></label>
                                    <input class=""form-control"" placeholder=""Username"" asp-for=""Username"" autofocus>
                                </div>
                                <div class=""form-group"">
                                    <label asp-for=""Password""></label>
                                    <input type=""password"" class=""form-control"" placeholder=""Password"" asp-for=""Password"" autocomplete=""off"">
                    ");
            WriteLiteral("            </div>\r\n");
#nullable restore
#line 33 "D:\Development\my8\AccountServer\Views\Account\Login.cshtml"
                                 if (Model.AllowRememberLogin)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                    <div class=""form-group login-remember"">
                                        <label asp-for=""RememberLogin"">
                                            <input asp-for=""RememberLogin"">
                                            <strong>Remember My Login</strong>
                                        </label>
                                    </div>
");
#nullable restore
#line 41 "D:\Development\my8\AccountServer\Views\Account\Login.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <div class=""form-group"">
                                    <button class=""btn btn-primary"" name=""button"" value=""login"">Login</button>
                                    <button class=""btn btn-default"" name=""button"" value=""cancel"">Cancel</button>
                                </div>
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
");
#nullable restore
#line 51 "D:\Development\my8\AccountServer\Views\Account\Login.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 53 "D:\Development\my8\AccountServer\Views\Account\Login.cshtml"
         if (Model.VisibleExternalProviders.Any())
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""col-md-6 col-sm-6 external-providers"">
                <div class=""panel panel-default"">
                    <div class=""panel-heading"">
                        <h3 class=""panel-title"">External Login</h3>
                    </div>
                    <div class=""panel-body"">
                        <ul class=""list-inline"">
");
#nullable restore
#line 62 "D:\Development\my8\AccountServer\Views\Account\Login.cshtml"
                             foreach (var provider in Model.VisibleExternalProviders)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <li>\r\n                                    <a class=\"btn btn-default\"\r\n                                       asp-controller=\"External\"\r\n                                       asp-action=\"Challenge\"");
            BeginWriteAttribute("asp-route-provider", "\r\n                                       asp-route-provider=\"", 3089, "\"", 3180, 1);
#nullable restore
#line 68 "D:\Development\my8\AccountServer\Views\Account\Login.cshtml"
WriteAttributeValue("", 3150, provider.AuthenticationScheme, 3150, 30, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("asp-route-returnUrl", "\r\n                                       asp-route-returnUrl=\"", 3181, "\"", 3259, 1);
#nullable restore
#line 69 "D:\Development\my8\AccountServer\Views\Account\Login.cshtml"
WriteAttributeValue("", 3243, Model.ReturnUrl, 3243, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                        ");
#nullable restore
#line 70 "D:\Development\my8\AccountServer\Views\Account\Login.cshtml"
                                   Write(provider.DisplayName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </a>\r\n                                </li>\r\n");
#nullable restore
#line 73 "D:\Development\my8\AccountServer\Views\Account\Login.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </ul>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 78 "D:\Development\my8\AccountServer\Views\Account\Login.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 80 "D:\Development\my8\AccountServer\Views\Account\Login.cshtml"
         if (!Model.EnableLocalLogin && !Model.VisibleExternalProviders.Any())
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"alert alert-warning\">\r\n                <strong>Invalid login request</strong>\r\n                There are no login schemes configured for this client.\r\n            </div>\r\n");
#nullable restore
#line 86 "D:\Development\my8\AccountServer\Views\Account\Login.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AccountServer.Models.LoginViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
