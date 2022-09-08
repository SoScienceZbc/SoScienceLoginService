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
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace SoScienceLoginService.LoginClasses
{
    class LoginManager
    {
        public async Task<LoginRepley> LoginAD(string username, string password)
        {
            //Ldaps Primary Port: 636, Ldaps Secondary Primary Port: 3269, Ldap Primary Port: 389

            //Console.WriteLine(username, password);
            LoginRepley loginRepley = new LoginRepley();

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://ws01.efif.dk");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var credentials = new
            {
                Identity = "zbc-soscience-ws",
                Password = "****************"
            };
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("/api/Authentication/Authenticate", httpContent);
            var tokenInfo = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            if (tokenInfo.ToString() != "" || tokenInfo != null)
            {
                Console.WriteLine("Token: " +tokenInfo);
            }

            ////LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier("dc01.efif.dk", 389);
            //Console.WriteLine("Before Ldap");
            ////string[] pw = File.ReadAllLines("/home/soscience/Desktop/Services/PassPhrase.txt");
            ////string pwToCheck = string.Concat(pw);
            ////var cer = new X509Certificate("/home/soscience/Desktop/Services/soscience.dk.pfx", pwToCheck);
            //X509Certificate cert = X509Certificate.CreateFromCertFile("/home/soscience/Desktop/Services/LoginService/LDAPS_Certs/EFIF - Root CA.crl");
            //LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier("10.255.1.1", 636); 
            //Console.WriteLine("After Ldap Identifier");
            //X509Certificate2 x509 = new X509Certificate2();
            //byte[] rawData = ReadFile("/hom/soscience/Desktop/Services/LoginService/LDAPS_Certs/EFIF - Issuing CA+.crl");
            //x509.Import(rawData);
            //username = username.Split('@')[0];
            //NetworkCredential credential = new NetworkCredential(username + "@zbc.dk", password);
            //Console.WriteLine("Before Ldap Connection");
            //LdapConnection connection = new LdapConnection(identifier, credential, AuthType.External);
            //connection.SessionOptions.SecureSocketLayer = true;
            //connection.ClientCertificates.Add(x509);
            ////connection.SessionOptions.ProtocolVersion = 3;



            //connection.ClientCertificates


            //Console.WriteLine("just before DirectoryEntry connection");
            //DirectoryEntry connection = new DirectoryEntry("LDAP://10.255.1.1:636", username + "@zbc.dk", password);
            //try
            //{
            //    var tmp = connection.NativeObject;
            //    var temp = connection.Guid;
            //    Console.WriteLine("conn works");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("exception: " + ex.Message);
            //    Console.WriteLine("conn fails");
            //    throw;
            //}

            //Console.WriteLine("After Ldap Connection");
            //try
            //{
            //    connection.Bind();
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
        internal static byte[] ReadFile(string fileName)
        {
            FileStream f = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            int size = (int)f.Length;
            byte[] data = new byte[size];
            size = f.Read(data, 0, size);
            f.Close();
            return data;
        }
    }
}
