using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Koelkast_App.Services;

namespace Koelkast_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListKindOfDrinkPage : ContentPage
    {
        List<Model.Drink> drinks;
        public ListKindOfDrinkPage()
        {
            InitializeComponent();
            //get the drinks from the database
            drinks = DrinkService.GetAllDrinks();
        }

        protected override void OnAppearing()
        {   
            foreach (Model.Drink drink in drinks)
            {
                // keep track of a 
                DrinkStack.Children.Add(
                    SingleRow(drink.ID, drink.Name, drink.ImagePath)
                );
            }
        }

        public Grid SingleRow(int DrinkId, string name, string imgsrc)
        {
            // setup the image of the item in the list
            Image img = new Image();
            if (imgsrc == "no-image")
            {
                //hier gebruiken we de embedded resource van no-image
                img.Source = ImageSource.FromResource("Koelkast_App.images.no-image.png");
            } else
            {
                // laad een afbeelding uit de gallerij
                img.Source = ImageSource.FromFile(imgsrc);
            }
            img.HeightRequest = 100;
            img.WidthRequest = 100;
            img.HorizontalOptions = LayoutOptions.End;
            Grid.SetColumn(img, 0);

            // setup the label with the text from the item in the list
            Label lblName = new Label
            {
                Text = name
            };
            // position the label in the grid for return
            Grid.SetColumn(lblName, 1);

            // an edit button
            ImageButton editBtn = new ImageButton();
            editBtn.ClassId = string.Format("Edit_{0}", DrinkId);
            editBtn.Source = ImageSource.FromResource("Koelkast_App.images.pencil_img.png");
            editBtn.WidthRequest = 40;
            editBtn.HeightRequest = 40;
            editBtn.BackgroundColor = Color.Transparent;

            editBtn.Clicked += EditBtn_Clicked;

            Grid.SetColumn(editBtn, 2);
            
            // an delete button
            ImageButton deleteBtn = new ImageButton();
            deleteBtn.ClassId = string.Format("Delete_{0}", DrinkId);
            deleteBtn.Source = ImageSource.FromResource("Koelkast_App.images.trash_img.png");
            deleteBtn.WidthRequest = 40;
            deleteBtn.HeightRequest = 40;
            deleteBtn.BackgroundColor = Color.Transparent;

            deleteBtn.Clicked += DeleteBtn_Clicked;

            Grid.SetColumn(deleteBtn, 4);

            Grid DrinkKindRow = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition
                    {
                        Height = GridLength.Auto
                    }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition
                    {   // the image column
                        Width=GridLength.Auto
                    },
                    new ColumnDefinition
                    {   // the name column
                        Width=GridLength.Star
                    },
                    new ColumnDefinition
                    {   // An edit column
                        Width=GridLength.Auto
                    },
                    new ColumnDefinition
                    {   // A spacing column
                        Width=15
                    },
                    new ColumnDefinition
                    {   // An delete column
                        Width=GridLength.Auto
                    }
                },
                Children = {
                    img,
                    lblName,
                    editBtn,
                    deleteBtn
                }
            };

            return DrinkKindRow;
        }

        private void EditBtn_Clicked(object sender, EventArgs e)
        {
            //TODO Call an update/edit page for changing the data. from ID given by className
            var button = (ImageButton)sender;
            var idName = button.ClassId.Remove(0, 5); // keeping only the id
            _ = DisplayAlert(string.Format("Edit:{0}", idName), "Edit button!", "Edit info");
        }

        private void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            //TODO Call an confirmation page before removing the data. from ID given by className
            var button = (ImageButton)sender;
            var id = button.ClassId.Remove(0, 7); // keeping only the id for the drink service
            _ = DisplayAlert(string.Format("Delete:{0}", id), "Delete button!", "Delete info");
            DrinkService.DeleteDrink(Convert.ToInt32(id));
        }
    }
}