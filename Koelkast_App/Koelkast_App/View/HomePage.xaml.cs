using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koelkast_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
            if (RootPage.loggedInUser != null)
            {
                TabbedHomePage.Children.Add(new UserProfilePage(this));
                TabbedHomePage.Children[2].Title = "Profiel";
            }
            else
            {
                TabbedHomePage.Children.Add(new LoginPage(this));
                TabbedHomePage.Children[2].Title = "inloggen";
            }
        }
    }
}