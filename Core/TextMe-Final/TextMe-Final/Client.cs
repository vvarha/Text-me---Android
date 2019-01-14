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
using System.Net;
using System.IO;

namespace TextMe_Final
{
    public class Client
    {
        public string EndPoint { get; set; }

        public string Response { get; set; }

        public HttpWebResponse Send(string attribute, string message, bool withToken = false)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(EndPoint);

            request.Method = attribute;

            byte[] postBytes = Encoding.UTF8.GetBytes(message);

            if (withToken)
                request.Headers.Add("Authorization", "Bearer " + ApiConnector.GetInstance().Token);

            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";
            request.ContentLength = postBytes.Length;
            if (request.Method != "GET")
            {
                Stream requestStream = request.GetRequestStream();


                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();


            return response;
        }
    }
}