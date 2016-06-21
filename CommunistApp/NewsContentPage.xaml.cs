using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static CommunistApp.NewsPage;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace CommunistApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NewsContentPage : Page
    {
        public NewsContentPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {

            if (e.Handled == false && Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            var itemId = (NewsContent)e.Parameter;
            TitleTextBlock.Text = itemId.title;
            TimeTextBlock.Text = itemId.time;
            List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
            paramList.Add(new KeyValuePair<string, string>("id", itemId.id));
            string uri = "http://202.202.43.42/lxyz/index.php?m=Home&c=Article&a=mobilearticle";
            String tempString = Utils.ConvertUnicodeStringToChinese(await NetWork.getHttpWebRequest(uri, paramList, fulluri: true));

            JObject newsContentobj = JObject.Parse(tempString);

            string content = (JObject.Parse(newsContentobj["data"].ToString()))["content"].ToString();
            Debug.WriteLine(content);
            //content = content.Replace("&lt;p;", "<p");
            //content = content.Replace("&lt;/p&gt;", "</p>");
            //content = content.Replace("&lt;span", "<span");
            //content = content.Replace("&lt;/span&gt;", "</span>");
            //content = content.Replace("&lt;strong", "<strong");
            //content = content.Replace("&lt;/strong&gt;", "</strong>");

            content = content.Replace("&lt;", "<");
            content = content.Replace("&gt;", ">");

            content = content.Replace("&amp;nbsp;", " ");
            content = content.Replace("&amp;ldquo;", "\"");
            content = content.Replace("&amp;rdquo;", "\"");
            content = content.Replace("&quot;", "\"");
            Debug.WriteLine(content);
            ContentWebView.NavigateToString(content);


            //JObject jArray2 = (JObject)JsonConvert.DeserializeObject(tempString);
            //string json2 = jArray2["data"].ToString();

            //JObject jArray = (JObject)JsonConvert.DeserializeObject(json2);

            //ContentWebView.NavigateToString(jArray["content"].ToString());
            //UmengSDK.UmengAnalytics.TrackPageStart("NewsContentPage");
        }
    }

    public class News
    {
        public string title { get; set; }
        public string content { get; set; }
        public string time { get; set; }
        public string author { get; set; }
    }
}
