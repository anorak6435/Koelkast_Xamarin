using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koelkast_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : ContentPage
    {
        public static bool closing = false;
        public static Model.User loggedInUser; // Refered to by RootPage.loggedInUser
        public static string msg; // the message given to the homescreen welcoming the new user
        public RootPage()
        {
            InitializeComponent();
            // when first opening the app check if a user wants to be logged in automatically
            (loggedInUser, msg) = Services.UserService.AutoLogin();
        }

        //hacky solution: when you come back from the HomePage will want to close the app
        protected override void OnAppearing()
        {
            if (!closing)
            {
                closing = true;
                Navigation.PushAsync(new View.HomePage());
            } else
            {
                var closer = DependencyService.Get<ICloseApplication>();
                closer?.closeApplication();
            }
            
        }
    }
}