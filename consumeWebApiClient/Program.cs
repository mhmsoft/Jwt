using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace consumeWebApiClient
{
  
    class userCred
    {
        public string uname { get; set; }
        public string passw { get; set; }
    }
    class Program
    {
        static string getToken()
        {
            userCred Credential = new userCred();
            Console.WriteLine("Kullanıcı Adı girin");
            Credential.uname = Console.ReadLine();
            Console.WriteLine("Parola girin");
            Credential.passw = Console.ReadLine();

            var client = new WebClient();
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");
                var result=client.UploadData("https://localhost:44372/weatherforecast/Authenticate", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Credential,Formatting.None)));

           string token= Encoding.ASCII.GetString(result);
           return token;
        }

        static void Main(string[] args)
        {
            string token=getToken();
            var client = new WebClient();
            client.Headers.Add("Content-Type:application/json"); //Content-Type  
            client.Headers.Add("Accept:application/json");
            client.Headers.Add(HttpRequestHeader.Authorization,"Bearer " + token);
            var result = client.DownloadString("https://localhost:44372/weatherforecast");

            
            
            Console.ReadKey();
        }
    }
}
