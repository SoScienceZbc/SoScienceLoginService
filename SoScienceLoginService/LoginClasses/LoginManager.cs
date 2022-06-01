using Grpc.Core;
using LoginService_Grpc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace SoScienceLoginService.LoginClasses
{
    class LoginManager
    {
        public LoginRepley LoginAD(string username, string password)
        {
            Console.WriteLine(username, password);
            LoginRepley loginRepley = new LoginRepley();
            //LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier("dc01.efif.dk", 389);
            Console.WriteLine("Before Ldap");
            //LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier("10.255.1.1", 636);
            Console.WriteLine("After Ldap Identifier");
           
            username = username.Split('@')[0];
            Console.WriteLine("Before Ldap Connection");
            //LdapConnection connection = new LdapConnection(identifier)
            //{
            //    Credential = new NetworkCredential(username + "@zbc.dk", password)
            //};

            Console.WriteLine("just before DirectoryEntry connection");
            DirectoryEntry connection = new DirectoryEntry("LDAP://10.255.1.1:389", username, password);
            try
            {
                var tmp = connection.NativeObject;
                var temp = connection.Guid;
                Console.WriteLine("conn works");
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception: " + ex.Message);
                Console.WriteLine("conn fails");
                throw;
            }
            
            //Console.WriteLine("After Ldap Connection");
            //try
            //{
            //    //connection.Bind();
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    Console.WriteLine($"{username} have been login...");
            //    Console.ForegroundColor = ConsoleColor.White;
            //    loginRepley.LoginSucsefull = true;
            //}
            //catch
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine($"{username} cant login...");
            //    Console.ForegroundColor = ConsoleColor.White;
            //    loginRepley.LoginSucsefull = false;
            //}
            //finally
            //{
            //    connection.Dispose();
            //}



            // Fix before full release
            if ((username == "Test" || username == "CoTest") && password == "KageMand")
            {
                loginRepley.LoginSucsefull = true;
                loginRepley.Admin = true;
            }





            return loginRepley;
        }
    }
}
