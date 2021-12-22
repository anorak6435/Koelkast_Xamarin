using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koelkast_App
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new RootPage());
        }

        public App(string dbloc)
        {
            InitializeComponent();
            DatabaseLocation = dbloc;
            MainPage = new NavigationPage(new RootPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
