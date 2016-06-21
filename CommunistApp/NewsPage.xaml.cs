using CommunistApp.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class NewsPage : Page
    {
        public NewsPage()
        {
            this.InitializeComponent();
            GetAllListData();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            SystemNavigationManager.GetForCurrentView().BackRequested -= App_BackRequested;
        }
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {

            if (e.Handled == false && Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }

        }
        void GetAllListData()
        {
            GetNewsData("2");
            GetNewsData("3");
            GetNewsData("4");
        }
        int NewPage = 1;
        int WorkPage = 1;
        int MovePage = 1;
        async void GetNewsData(string id)
        {
            List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
            paramList.Add(new KeyValuePair<string, string>("id", id));
            //paramList.Add(new KeyValuePair<string, string>("page", page.ToString()));
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

                        newsContent = JsonConvert.DeserializeObject<ObservableCollection<NewsContent>>(jArray.ToString());


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
                        }
                    }
                }
                catch (Exception) { }
            }
        }
        ObservableCollection<NewsContent> newsContent = new ObservableCollection<NewsContent>();
        async Task<ObservableCollection<NewsContent>> ReFreshList(string id, int page)
        {
            List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
            paramList.Add(new KeyValuePair<string, string>("id", id));
            paramList.Add(new KeyValuePair<string, string>("page", page.ToString()));
            string uri = "http://202.202.43.42/lxyz/index.php?m=Home&c=index&a=mobilearticlelist";
            String tempString = Utils.ConvertUnicodeStringToChinese(await NetWork.getHttpWebRequest(uri, paramList, fulluri: true));
            ObservableCollection<NewsContent> newsContentNew = new ObservableCollection<NewsContent>();

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


                        JObject jArray2 = (JObject)JsonConvert.DeserializeObject(tempString);
                        string json2 = jArray2["data"].ToString();
                        JArray jArray = (JArray)JsonConvert.DeserializeObject(json2);
                        newsContentNew = JsonConvert.DeserializeObject<ObservableCollection<NewsContent>>(jArray.ToString());
                        if (newsContentNew.Count != 0)
                        {
                            HasNoNewItem = false;
                        }
                    }
                }
                catch (Exception) { }
            }
            return newsContentNew;
        }
        bool HasNoNewItem = true;
        ObservableCollection<NewsContent> tempList = new ObservableCollection<NewsContent>();
        private void NewsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemId = (NewsContent)e.ClickedItem;
            this.Frame.Navigate(typeof(NewsContentPage), itemId);
        }
        double NewsOldScrollableHeight = 0;
        double WorkOldScrollableHeight = 0;
        double MoveOldScrollableHeight = 0;


        public class NewsContent
        {
            public string id { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public string time { get; set; }
        }

        private async void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (News.VerticalOffset > (News.ScrollableHeight - 500) && News.ScrollableHeight != NewsOldScrollableHeight)
            {
                NewsOldScrollableHeight = News.ScrollableHeight;
                NewPage++;
                tempList = await ReFreshList("2", NewPage);
                foreach (var item in tempList)
                {
                    newsContent.Add(item);
                }
                tempList.Clear();
            }
            if (News.VerticalOffset == News.ScrollableHeight && HasNoNewItem)
            {
                NotifyPopup notifyPopup = new NotifyPopup("没有更多内容了哦~");
                notifyPopup.Show();
            }
        }

        private async void Work_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (Work.VerticalOffset > (Work.ScrollableHeight - 500) && Work.ScrollableHeight != WorkOldScrollableHeight)
            {
                WorkOldScrollableHeight = Work.ScrollableHeight;
                WorkPage++;
                tempList = await ReFreshList("3", WorkPage);
                foreach (var item in tempList)
                {
                    newsContent.Add(item);
                }
                tempList.Clear();
            }
            if (News.VerticalOffset == News.ScrollableHeight && HasNoNewItem)
            {
                NotifyPopup notifyPopup = new NotifyPopup("没有更多内容了哦~");
                notifyPopup.Show();
            }
        }

        private async void Move_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (Move.VerticalOffset > (Move.ScrollableHeight - 500) && Move.ScrollableHeight != MoveOldScrollableHeight)
            {
                MoveOldScrollableHeight = Move.ScrollableHeight;
                MovePage++;
                tempList = await ReFreshList("4", MovePage);
                foreach (var item in tempList)
                {
                    newsContent.Add(item);
                }
                tempList.Clear();
            }
            if (News.VerticalOffset == News.ScrollableHeight && HasNoNewItem)
            {
                NotifyPopup notifyPopup = new NotifyPopup("没有更多内容了哦~");
                notifyPopup.Show();
            }
        }
    }
}
