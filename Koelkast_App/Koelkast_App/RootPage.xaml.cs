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
        public RootPage()
        {
            InitializeComponent();
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