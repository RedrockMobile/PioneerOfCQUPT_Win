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
using static CommunistApp.CommunityPage;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace CommunistApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class StudyContentPage : Page
    {
        public StudyContentPage()
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
        List<fileInNews> fin = new List<fileInNews>();

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            var itemId = (NewsContent1)e.Parameter;
            TitleTextBlock.Text = itemId.title;
            TimeTextBlock.Text = itemId.time;
            List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
            paramList.Add(new KeyValuePair<string, string>("id", itemId.id));
            string uri = "http://202.202.43.42/lxyz/index.php?m=Home&c=Article&a=mobilearticle";
            String tempString = Utils.ConvertUnicodeStringToChinese(await NetWork.getHttpWebRequest(uri, paramList, fulluri: true));

            JObject jArray2 = (JObject)JsonConvert.DeserializeObject(tempString);
            string json2 = jArray2["data"].ToString();
            JObject jArray3 = (JObject)JsonConvert.DeserializeObject(json2);
            string json3 = jArray3["file"].ToString();

            JArray jArray4 = (JArray)JsonConvert.DeserializeObject(json3);

            fin = JsonConvert.DeserializeObject<List<fileInNews>>(jArray4.ToString());

            if (tempString != "")
            {
                try
                {
                    JObject newsContentobj = JObject.Parse(tempString);
                    if (newsContentobj["status"].ToString() == "200")
                    {
                        string content = (JObject.Parse(newsContentobj["data"].ToString()))["content"].ToString();
                        Debug.WriteLine(content);

                        ContentWebView.NavigateToString(content);
                    }
                }
                catch (Exception) { }
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
}
