using AccountServer.Models;
using CoreBusiness;
using CoreObject.Mongo;
using CoreRepository.Interfaces;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using my8ViewObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountServer.Business
{
    public class AccountBusiness : BaseBusiness, IAccountBusiness
    {
        protected readonly IAccountRepository _rpAccount;
        
        private readonly IIdentityServerInteractionService _interaction;
        public AccountBusiness(
             IAccountRepository accountRepository
            , CurrentProcess process) : base(process)
        {
            _rpAccount = accountRepository;
        }
        public async Task<MongoAccount> Login(string email, string passwordPlainText)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(passwordPlainText)) return null;
            MongoAccount user = await _rpAccount.GetByEmail(email);
            if (user == null)
            {
                AddError(nameof(ErrorRsx.invalid_password));
                return null;
            }
            if (string.IsNullOrWhiteSpace(user.PersonId)
                || string.IsNullOrWhiteSpace(user.Role)
                || string.IsNullOrWhiteSpace(user.ProjectId)
                || string.IsNullOrWhiteSpace(user.Password)
                )
            {
                AddError(ErrorRsx.invalid_password);
                return null;
            }
            //if (!string.IsNullOrWhiteSpace(obj.Avatar))
            //    obj.Avatar = Utils.GetAvatarFullPathByAMZ(_blobService.awsService.Host, obj.Avatar);
            if (checkAccountWithSalt(passwordPlainText, user))
            {
                return user;
            }
            AddError(nameof(ErrorRsx.invalid_password));
            return null;
        }
        private bool checkAccountWithSalt(string passwordPlainText, MongoAccount account)
        {
            //string userPass = Utils.GetSHA256Hash(passwordPlainText + account.Salt);
            //if (userPass == account.Password)
            //    return true;
            //return false;
            return true;
        }

    }
}
