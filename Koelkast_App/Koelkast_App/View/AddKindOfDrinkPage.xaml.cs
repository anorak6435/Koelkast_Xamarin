using Koelkast_App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koelkast_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddKindOfDrinkPage : ContentPage
    {
        private string imagePath;
        public AddKindOfDrinkPage()
        {
            InitializeComponent();
            imagePath = "no-image";
        }

        /// <summary>
        /// The media picker function invoked to get an image from the gallery of the phone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddPicture_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Kies uw foto"
            });

            if (result.FullPath is null)
            {
                _ = DisplayAlert("Waarschuwing!", "Geen afbeelding ontvangen!", "terug");
            } else
            {
                imagePath = result.FullPath;

                var stream = await result.OpenReadAsync();
                LoadedImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        /// <summary>
        /// Submit the filled form into the inner database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnAddDrinkKind_Clicked(object sender, EventArgs e)
        {
            Model.Drink DrinkInsert = new Model.Drink();

            if (string.IsNullOrEmpty(DrinkEntry.Text))
            {
                _ = DisplayAlert("Naam waarschuwing!", "Er is geen naam gegeven aan de onderdelen van de code!", "vul in");
            } else
            {
                int ins_rows = DrinkService.Insert(DrinkEntry.Text, imagePath);

                if (ins_rows > 0)
                {
                    _ = DisplayAlert("Succes", "Nieuwe soort drank toegevoegd aan de database.", "Oke");
                }
                else
                {
                    _ = DisplayAlert("Waarschuwing!", "Toevoegen mislukt!", "Oke");
                }

                await Navigation.PopAsync();
            }
        }
    }
}