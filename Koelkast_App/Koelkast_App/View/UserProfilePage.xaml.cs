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
    public partial class UserProfilePage : ContentPage
    {
        TabbedPage tabParent;
        public UserProfilePage(TabbedPage parent)
        {
            InitializeComponent();
            tabParent = parent;
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            bool logout = await DisplayAlert("Uitloggen", "Wilt u uitloggen?", "Ja", "Nee");
            if (logout)
            {
                RootPage.loggedInUser = null;
                RootPage.msg = "Welkom gast!";
                Navigation.InsertPageBefore(new HomePage(), this.tabParent);
                await Navigation.PopAsync();
            }
        }
    }
}