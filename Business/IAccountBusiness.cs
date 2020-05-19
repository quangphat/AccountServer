using CoreObject.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountServer.Business
{
    public interface IAccountBusiness
    {
        Task<MongoAccount> Login(string email, string passwordPlainText);
    }
}
