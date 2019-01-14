using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System.Net;

using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;
using System.IO;

namespace TextMe_Final
{
    public class ApiConnector
    {
        private static ApiConnector Instance { get; set; }

        public static ApiConnector GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ApiConnector();
            }
            return Instance;
        }

        public string Token { get; set; }
        Client client = new Client();
        public ComplexUser LoggedUser { get; set; }
        public int LoggedUserID { get; set; }

        private ApiConnector()
        {

        }

        public void AddUser(User user)
        {
            client.EndPoint = @"http://10.0.2.2:3030/user";
            string json = JsonConvert.SerializeObject(user);
            HttpWebResponse response = client.Send("POST", json);
            System.Diagnostics.Debug.WriteLine("Request completed with status code: " + response.StatusCode);
        }

        public void Authenticate(string email, string password)
        {
            client.EndPoint = @"http://10.0.2.2:3030/authentication";

            JObject json = new JObject();
            json.Add("strategy", "local");
            json.Add("email", email);
            json.Add("password", password);

            HttpWebResponse response = client.Send("POST", json.ToString());

            string resp = GetResponse(response);
            var respToken = JObject.Parse(resp)["accessToken"];
            string stream = respToken.ToString();
            Token = stream;

            var handler = new JwtSecurityTokenHandler();

            var jsonToken = (JwtSecurityToken)handler.ReadToken(stream);

            LoggedUserID = Convert.ToInt32(jsonToken.Claims.First(claim => claim.Type == "userId").Value);
        }

        public ComplexUser GetUserByID(int id)
        {
            client.EndPoint = @"http://10.0.2.2:3030/user-details/" + id;
            HttpWebResponse response = client.Send("GET", "", true);
            string json = GetResponse(response);
            ComplexUser user = new ComplexUser()
            {
                name = JObject.Parse(json)["name"].ToString(),
                ID = id,
                email = JObject.Parse(json)["email"].ToString()
            };

            JObject obj = JObject.Parse(json);
            List<ComplexUser> list = obj["contacts"].Select(p => new ComplexUser
            {
                name = (string)p["name"],
                ID = (int)p["id"],
                email = (string)p["email"]
            }).ToList();

            user.Friends = list;

            return user;
        }

        public string GetResponse(HttpWebResponse resp)
        {
            string Response = "";

            using (HttpWebResponse response = (HttpWebResponse)resp)
            {
                //if (response.StatusCode != HttpStatusCode.OK)
                //{
                //    throw new Exception(response.StatusDescription + " > " + response.StatusCode);
                //}

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            Response = reader.ReadToEnd();
                        }
                    }
                }

            }
            return Response;
        }
    }
}