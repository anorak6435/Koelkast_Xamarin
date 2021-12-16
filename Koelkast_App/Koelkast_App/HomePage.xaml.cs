﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koelkast_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        private Model.User currentUser;
        public HomePage(Model.User usr)
        {
            // get the user from the model from the 
            InitializeComponent();
            currentUser = usr;
        }
    }
}