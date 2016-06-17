using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace CommunistApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NewsPage : Page
    {
        List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
        public NewsPage()
        {
            this.InitializeComponent();
            GetAllListData();
        }
        string tempString;
        void GetAllListData()
        {
            GetNewsData("2");
            GetNewsData("3");
            GetNewsData("4");
            GetNewsData("5");
        }
        void GetNewsData(string id)
        {
            paramList.Add(new KeyValuePair<string, string>("id", id));
            HttpClient httpClient = new HttpClient();
            string uri = $"http://202.202.43.42/lxyz/index.php?m=Home&c=index&a=mobilearticlelist";
            System.Net.Http.HttpResponseMessage response;
            response = httpClient.PostAsync(new Uri(uri), new FormUrlEncodedContent(paramList)).Result;
            if (response.StatusCode == HttpStatusCode.OK)
                tempString = response.Content.ReadAsStringAsync().Result;

            tempString.Replace("\r\n\r\n", " ");
            tempString.Replace("&ldquo;", " ");
            tempString.Replace("&rdquo;", " ");
            tempString.Replace("&rdq", " ");

            JObject jArray2 = (JObject)JsonConvert.DeserializeObject(tempString);
            string json2 = jArray2["data"].ToString();
            JArray jArray = (JArray)JsonConvert.DeserializeObject(json2);

            List<NewsContent> newsContent = JsonConvert.DeserializeObject<List<NewsContent>>(jArray.ToString());

            switch (id)
            {
                case "2":
                    NewsList.ItemsSource = newsContent;
                    break;
                case "3":
                    WorkList.ItemsSource = newsContent;
                    break;
                case "4":
                    MoveList.ItemsSource = newsContent;
                    break;
                case "5":
                    StudyList.ItemsSource = newsContent;
                    break;
            }

        }

        private void NewsList_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void WorkList_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void MoveList_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void StudyList_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        public class NewsContent
        {
            public string id { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public string time { get; set; }
        }

    }
}
