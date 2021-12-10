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
    public partial class AddDrinkPage : ContentPage
    {
        public AddDrinkPage()
        {
            InitializeComponent();
        }

        private void AddDrinkButton_Clicked(object sender, EventArgs e)
        {
            // TODO: apply the add drink logic
            Navigation.PopAsync();
        }
    }
}