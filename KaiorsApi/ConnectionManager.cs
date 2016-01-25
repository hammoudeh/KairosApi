using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KaiorsApi
{
    public class ConnectionManager
    {


        public Dictionary<string ,string> Headers { set; get; }


        public string doHttpPost(string url, string body) {

            var request = (HttpWebRequest)WebRequest.Create(url);

            request.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };

            var data = Encoding.UTF8.GetBytes(body);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            if (Headers != null) {
                foreach (string k in Headers.Keys) {
                    request.Headers.Add(k, Headers[k]);
                }
            }


            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }
        


    }
}
