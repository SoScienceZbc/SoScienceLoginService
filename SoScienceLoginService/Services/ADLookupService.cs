using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using LoginService_Grpc;
using SoScienceLoginService.LoginClasses;

namespace SoScienceLoginService.Services
{
    class ADLookupService : LoginService.LoginServiceBase
    {
        LoginManager loginManager = new LoginManager();

        public override Task<LoginRepley> LoginAD(LoginRequset request, ServerCallContext context)
        {
            return loginManager.LoginAD(request.Username, request.Password);
        }

        //public override Task<LoginRepley> LoginAD(LoginRequset request, ServerCallContext context)
        //{
        //    Console.WriteLine($"Host:{context.Host}\nMethod: {context.Method}");
        //    return Task.FromResult(loginManager.LoginAD(request.Username, request.Password));
        //}
    }
}
