using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinPokedex.ViewModels;

namespace XamarinPokedex.Views.TestPage
{
    public partial class ArtPlantMallView : ContentPage
    {
        public ArtPlantMallView()
        {
            InitializeComponent();

            BindingContext = new ArtPlantMallViewModel();
        }
    }
}
