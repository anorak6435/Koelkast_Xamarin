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
    public partial class GeneralSettingsPage : ContentPage
    {
        string language;
        public GeneralSettingsPage()
        {
            InitializeComponent();
            // TODO: Load the language from the database
            language = "NL";
        }

        private void ChangeLanguageButton_Clicked(object sender, EventArgs e)
        {
            if (language == "NL")
            {
                language = "EN";
                ChangeLanguageButton.Text = "Taal: Engels";
            } else
            {
                language = "NL";
                ChangeLanguageButton.Text = "Taal: Nederlands";
            }
        }
    }
}