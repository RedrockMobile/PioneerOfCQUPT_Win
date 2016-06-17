using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
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
            this.SizeChanged += (s, e) =>
            {
                var state = "VisualState000";
                if (e.NewSize.Width > 550)
                {
                    state = "VisualState550";
                }
                VisualStateManager.GoToState(this, state, true);
            };
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                Utils.ShowSystemTrayAsync(Color.FromArgb(255, 247, 59, 53), Colors.White);
            }s
            else
            {
                var view = ApplicationView.GetForCurrentView();
                view.TitleBar.BackgroundColor = Color.FromArgb(255, 247, 59, 53);
                view.TitleBar.ButtonBackgroundColor = Color.FromArgb(255, 247, 59, 53);
                view.TitleBar.ButtonHoverBackgroundColor = Color.FromArgb(255, 234, 53, 48);
                view.TitleBar.ButtonPressedBackgroundColor = Color.FromArgb(255, 206, 47, 42);
            }
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

            LMainPageIcon.Foreground = new SolidColorBrush(Colors.DarkGray);
            LNewsPageIcon.Foreground = new SolidColorBrush(Colors.DarkGray);
            LCommunityPageIcon.Foreground = new SolidColorBrush(Colors.DarkGray);
            LPersonalPageIcon.Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void MainPagePanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CanelColorEvent();
            MainPageIcon.Foreground = new SolidColorBrush(Colors.Black);
            LMainPageIcon.Foreground = new SolidColorBrush(Colors.Black);
            MainPageWord.Foreground = new SolidColorBrush(Colors.Black);
            contentFrame.Navigate(typeof(HomePage));
        }

        private void NewsPagePanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CanelColorEvent();
            NewsPageIcon.Foreground = new SolidColorBrush(Colors.Black);
            LNewsPageIcon.Foreground = new SolidColorBrush(Colors.Black);
            NewsPageWord.Foreground = new SolidColorBrush(Colors.Black);
            contentFrame.Navigate(typeof(NewsPage));
        }

        private void CommunityPagePanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CanelColorEvent();
            CommunityPageIcon.Foreground = new SolidColorBrush(Colors.Black);
            LCommunityPageIcon.Foreground = new SolidColorBrush(Colors.Black);
            CommunityPageWord.Foreground = new SolidColorBrush(Colors.Black);
            contentFrame.Navigate(typeof(CommunityPage));
        }

        private void PersonalPagePanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CanelColorEvent();
            PersonalPageIcon.Foreground = new SolidColorBrush(Colors.Black);
            LPersonalPageIcon.Foreground = new SolidColorBrush(Colors.Black);
            PersonalPageWord.Foreground = new SolidColorBrush(Colors.Black);
            contentFrame.Navigate(typeof(PersonalPage));
        }
    }
}
