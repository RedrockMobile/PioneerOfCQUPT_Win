using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CommunistApp
{
    public class NetWork
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="api">api地址</param>
        /// <param name="paramList">post时的参数</param>
        /// <param name="PostORGet">0POST 1GET</param>
        /// <param name="fulluri">api是否为完整的地址</param>
        /// <returns></returns>
        public static async Task<string> getHttpWebRequest(string api, List<KeyValuePair<String, String>> paramList = null, int PostORGet = 0, bool fulluri = false)
        {
            string content = "";
            return await Task.Run(() =>
            {
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    try
                    {
                        HttpClient httpClient = new HttpClient();
                        string uri;
                        if (!fulluri)
                            uri = "http://hongyan.cqupt.edu.cn/" + api;
                        else
                            uri = api;
                        HttpRequestMessage requst;
                        System.Net.Http.HttpResponseMessage response;
                        if (PostORGet == 0)
                        {
                            requst = new HttpRequestMessage(HttpMethod.Post, new Uri(uri));
                            response = httpClient.PostAsync(new Uri(uri), new FormUrlEncodedContent(paramList)).Result;
                        }
                        else
                        {
                            requst = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));
                            response = httpClient.GetAsync(new Uri(uri)).Result;
                        }
                        if (response.StatusCode == HttpStatusCode.OK)
                            content = response.Content.ReadAsStringAsync().Result;

                    }
                    catch (Exception e)
                    {
                    }
                }
                else
                {
                }
                //if (content.IndexOf("{") != 0)
                //    return "";
                //else
                return content;

            });
        }
    }
}
