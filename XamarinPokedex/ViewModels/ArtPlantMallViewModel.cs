using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using XamarinPokedex.Models;
using XamarinPokedex.Services;

namespace XamarinPokedex.ViewModels
{
    public class ArtPlantMallViewModel : BindableObject
    {
        public ArtPlantMallViewModel()
        {
            LoadData();
        }
        private ObservableCollection<Plant> _plants;
        public ObservableCollection<Plant> Plants
        {
            get { return _plants; }
            set
            {
                _plants = value;
                OnPropertyChanged(nameof(Plants));
            }
        }

        private void LoadData()
        {
            Plants = new ObservableCollection<Plant>(FakePlantService.Instance.GetPlants());
        }
    }
}
