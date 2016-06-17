using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍


namespace CommunistApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            contentFrame.Navigate(typeof(HomePage));
        }
        //忘了咋用blend 暂时这么着吧
        //Panel_Tap后类似于切换状态
        void CanelColorEvent()
        {
            MainPageIcon.Foreground = new SolidColorBrush(Colors.DarkGray);
            MainPageWord.Foreground = new SolidColorBrush(Colors.DarkGray);
            NewsPageIcon.Foreground = new SolidColorBrush(Colors.DarkGray);
            NewsPageWord.Foreground = new SolidColorBrush(Colors.DarkGray);
            CommunityPageIcon.Foreground = new SolidColorBrush(Colors.DarkGray);
            CommunityPageWord.Foreground = new SolidColorBrush(Colors.DarkGray);
            PersonalPageIcon.Foreground = new SolidColorBrush(Colors.DarkGray);
            PersonalPageWord.Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void MainPagePanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CanelColorEvent();
            MainPageIcon.Foreground = new SolidColorBrush(Colors.Black);
            MainPageWord.Foreground = new SolidColorBrush(Colors.Black);
            contentFrame.Navigate(typeof(HomePage));
        }

        private void NewsPagePanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CanelColorEvent();
            NewsPageIcon.Foreground = new SolidColorBrush(Colors.Black);
            NewsPageWord.Foreground = new SolidColorBrush(Colors.Black);
            contentFrame.Navigate(typeof(NewsPage));
        }

        private void CommunityPagePanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CanelColorEvent();
            CommunityPageIcon.Foreground = new SolidColorBrush(Colors.Black);
            CommunityPageWord.Foreground = new SolidColorBrush(Colors.Black);
            contentFrame.Navigate(typeof(CommunityPage));
        }

        private void PersonalPagePanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CanelColorEvent();
            PersonalPageIcon.Foreground = new SolidColorBrush(Colors.Black);
            PersonalPageWord.Foreground = new SolidColorBrush(Colors.Black);
            contentFrame.Navigate(typeof(PersonalPage));
        }
    }
}
