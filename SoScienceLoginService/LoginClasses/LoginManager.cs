using LoginService_Grpc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SoScienceLoginService.LoginClasses
{
    class LoginManager
    {
        public LoginRepley LoginAD(string username, string password)
        {
            LoginRepley loginRepley = new LoginRepley();
            //  LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier("dc01.efif.dk", 389);
            LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier("10.255.1.1", 389);

            LdapConnection connection = new LdapConnection(identifier)
            {
                Credential = new NetworkCredential(username, password)
            };
            try
            {
                connection.Bind();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{username} have been login...");
                Console.ForegroundColor = ConsoleColor.White;
                loginRepley.LoginSucsefull = true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{username} cant login...");
                Console.ForegroundColor = ConsoleColor.White;
                loginRepley.LoginSucsefull = false;
            }
            finally
            {
                connection.Dispose();
            }

            return loginRepley;
        }
    }
}
