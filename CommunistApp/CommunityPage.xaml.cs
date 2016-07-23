using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace CommunistApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CommunityPage : Page
    {
        public class NewsContent1
        {
            public string id { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public string time { get; set; }
        }
        public CommunityPage()
        {
            this.InitializeComponent();
            GetNewsData();
        }
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= App_BackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
           
            if (e.Handled == false && Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }
        ObservableCollection<NewsContent1> newsContent = new ObservableCollection<NewsContent1>();
        async void GetNewsData()
        {
            List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
            paramList.Add(new KeyValuePair<string, string>("id", "5"));
            string uri = "http://202.202.43.42/lxyz/index.php?m=Home&c=index&a=mobilearticlelist";
            String tempString = Utils.ConvertUnicodeStringToChinese(await NetWork.getHttpWebRequest(uri, paramList, fulluri: true));
            if (tempString != "")
            {
                try
                {
                    JObject job = JObject.Parse(tempString);
                    if (job["status"].ToString() == "200")
                    {
                        tempString = tempString.Replace("\\r", "");
                        tempString = tempString.Replace("\\n", "");
                        tempString = tempString.Replace("\\t", "");
                        tempString = tempString.Replace("&ldquo;", "");
                        tempString = tempString.Replace("&ldqu;", "");
                        tempString = tempString.Replace("&ldq;", "");
                        tempString = tempString.Replace("&ld;", "");
                        tempString = tempString.Replace("&l;", "");
                        tempString = tempString.Replace("&rdquo", " ");
                        tempString = tempString.Replace("&rdqu", " ");
                        tempString = tempString.Replace("&rdq", " ");
                        tempString = tempString.Replace("&rd", " ");
                        tempString = tempString.Replace("&r", " ");

                        Debug.WriteLine(tempString);

                        JObject jArray2 = (JObject)JsonConvert.DeserializeObject(tempString);
                        string json2 = jArray2["data"].ToString();
                        JArray jArray = (JArray)JsonConvert.DeserializeObject(json2);

                        newsContent = JsonConvert.DeserializeObject<ObservableCollection<NewsContent1>>(jArray.ToString());
                        NewsList.ItemsSource = newsContent;
                    }
                }
                catch (Exception) { }
            }
        }

        private void NewsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemId = (NewsContent1)e.ClickedItem;
            
            //if (Window.Current.Bounds.Width > 550)
            //{
            //    this.NewsContentFrame.Navigate(typeof(NewsContentPage), itemId);
            //    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            //    SystemNavigationManager.GetForCurrentView().BackRequested -= App_BackRequested;
            //}
            //else
                this.Frame.Navigate(typeof(StudyContentPage), itemId);
        }
    }
}
