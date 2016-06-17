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
using Windows.System;
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
            new Item {ItemName="学党章党规" ,ItemURL="http://lxyz.12371.cn/dzdg/"},
            new Item {ItemName="学系列讲话" ,ItemURL="http://www.12371.cn/special/xjpzyls/xxxjpzyls/" },
            new Item {ItemName="做合格党员" ,ItemURL="http://lxyz.12371.cn/xjdx/" },
            new Item {ItemName="网络活动" ,ItemURL="https://redrock.cqupt.edu.cn/lxyz_activity/" },
            new Item {ItemName="先进典型" ,ItemURL="http://lxyz.12371.cn/xjdx/" },
            new Item {ItemName="经典影像" ,ItemURL="http://lxyz.12371.cn/jdyx/" }
        };
        string tempString;
        //TODO:FlipView的自动播放+循环播放，参考约。
        ObservableCollection<Pic> PicData = new ObservableCollection<Pic>();
        //List<Pic> PicData = new List<Pic>()
        //{
        //    new Pic {imgurl ="http://202.202.43.42/lxyz/Public/uploads/2016-06-07/575676790d7db.jpg",link="http://202.202.43.42/lxyz/index.php?m=Home&amp;c=Article&amp;a=index&amp;id=19",title="学校召开“两学一做”学习教育动员部署会" },
        //    new Pic {imgurl ="http://202.202.43.42/lxyz/Public/uploads/2016-06-07/575676790d7db.jpg",link="http://202.202.43.42/lxyz/index.php?m=Home&amp;c=Article&amp;a=index&amp;id=19",title="学校召开“两学一做”学习教育动员部署会" },
        //    new Pic {imgurl ="http://202.202.43.42/lxyz/Public/uploads/2016-06-07/575676790d7db.jpg",link="http://202.202.43.42/lxyz/index.php?m=Home&amp;c=Article&amp;a=index&amp;id=19",title="学校召开“两学一做”学习教育动员部署会" },
        //};


        public HomePage()
        {
            this.InitializeComponent();
            SixItemGridView.ItemsSource = sixItem;
            GetPic();

        }

        async void GetPic()
        {
            HttpClient httpClient1 = new HttpClient();
            string uri = "http://202.202.43.42/lxyz/index.php?m=Home&c=index&a=mobilepic";
            tempString = await NetWork.getHttpWebRequest(uri, PostORGet: 1, fulluri: true);

            JObject jArray2 = (JObject)JsonConvert.DeserializeObject(tempString);
            string json2 = jArray2["data"].ToString();
            JArray jArray = (JArray)JsonConvert.DeserializeObject(json2);

            PicData = JsonConvert.DeserializeObject<ObservableCollection<Pic>>(jArray.ToString());
            this.fvCenter.ItemsSource = PicData;

        }

        private async void SixItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool success = await Launcher.LaunchUriAsync(new Uri(((Item)e.ClickedItem).ItemURL));
        }
        public class Item
        {
            public string ItemName { get; set; }
            public string ItemURL { get; set; }

        }
        public class Pic
        {
            public string title { get; set; }
            public string link { get; set; }
            public string imgurl { get; set; }
        }


    }
}

