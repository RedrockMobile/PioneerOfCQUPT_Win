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
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace CommunistApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class HomePage : Page
    {
        List<Item> sixItem = new List<Item>()
        {
            new Item {ItemName="党章党规" },
            new Item {ItemName="系列讲话" },
            new Item {ItemName="合格党员" },
            new Item {ItemName="网络活动" },
            new Item {ItemName="先进典范" },
            new Item {ItemName="经典影像" }
        };
        public HomePage()
        {
            this.InitializeComponent();
            SixItem.ItemsSource = sixItem;
            GetPic();
            this.fvCenter.ItemsSource = PicData;
        }
        string tempString;
        List<Pic> PicData = new List<Pic>();
        
        void GetPic()
        {
            HttpClient httpClient1 = new HttpClient();
            string uri = "http://202.202.43.42/lxyz/index.php?m=Home&c=index&a=mobilepic";            
            System.Net.Http.HttpResponseMessage response;
            response = httpClient1.GetAsync(new Uri(uri)).Result;
            if (response.StatusCode == HttpStatusCode.OK)
                tempString = response.Content.ReadAsStringAsync().Result;

            JObject jArray2 = (JObject)JsonConvert.DeserializeObject(tempString);
            string json2 = jArray2["data"].ToString();
            JArray jArray = (JArray)JsonConvert.DeserializeObject(json2);

            PicData = JsonConvert.DeserializeObject<List<Pic>>(jArray.ToString());
            
        }
        
        private void SixItem_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        public class Item
        {
            public string ItemName { get; set; }
        }
        public class Pic
        {
            public string title { get; set; }
            public string link { get; set; }
            public string imgurl { get; set; }
        }
    }
}

