using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinPokedex.Models;
using XamarinPokedex.Services;
using XamarinPokedex.Views;

namespace XamarinPokedex.ViewModels
{
    public class PokemonMainViewModel : INotifyPropertyChanged
    {
        private bool _isOddItem = true;
        private int _offset = 0;
        private int _limit = 40;
        private int _offsetPlus = 40;
        private string _imageUrl = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{0}.png";

        private PokemonMainEntity _pokemonMainEntity;

        private INavigation _navigation;
        public INavigation Navigation
        {
            get { return _navigation; }
            set { _navigation = value; }
        }

        public ICommand MyCommand { get; set; }

        private ObservableCollection<DoubleGridItem> _pokemonData;
        public ObservableCollection<DoubleGridItem> PokemonData
        {
            get { return _pokemonData; }
            set
            {
                _pokemonData = value;
                OnPropertyChanged(nameof(PokemonData));
            }
        }

        public Command SelectOddItemCommand { get; }
        public Command SelectEventItemCommand { get; }

        public PokemonMainViewModel(INavigation navigation)
        {
            _navigation = navigation;

            _pokemonMainEntity = new PokemonMainEntity();
            PokemonData = new ObservableCollection<DoubleGridItem>();
            SelectOddItemCommand = new Command(SelectOddItemMethod);
            SelectEventItemCommand = new Command(SelectEventItemMethod);

            LoadData();
        }

        private void SelectOddItemMethod(object obj)
        {
            var content = obj as DoubleGridItem;
            GoToProfilePage(content.Id);
        }

        private void SelectEventItemMethod(object obj)
        {
            var content = obj as DoubleGridItem;
            GoToProfilePage(content.Id2);
        }

        private async void GoToProfilePage(int id)
        {
            var pokemonProfile = await PokeApiService.Instance.GetPokemonProfile(id);
            var pokemonSpecies = await PokeApiService.Instance.GetPokemonSpecies(id);
            await Navigation.PushAsync(new PokemonProfilePage(pokemonProfile, pokemonSpecies));
        }

        private async void LoadData()
        {
            var pokemonMainEntity = await PokeApiService.Instance.GetPokemonLists(_offset, _limit);
            _pokemonMainEntity = pokemonMainEntity;

            BindingPokemonMainList();

            //Next of OffSet
            _offset += _offsetPlus;
        }

        public async void LoadMoreItems(DoubleGridItem currentItem)
        {
            int itemIndex = PokemonData.IndexOf(currentItem);

            _offset = PokemonData.Count;

            if (PokemonData.Count - 10 == itemIndex)
            {
                var pokemonMainEntity = await PokeApiService.Instance.GetPokemonLists(_offset, _limit);
                _pokemonMainEntity = new PokemonMainEntity();
                _pokemonMainEntity = pokemonMainEntity;

                BindingPokemonMainList();

                //Next of OffSet
                _offset += _offsetPlus;
            }
        }

        private void BindingPokemonMainList()
        {
            _isOddItem = true;
            DoubleGridItem entity = new DoubleGridItem();

            foreach (var item in _pokemonMainEntity.Results)
            {
                if (_isOddItem)
                {
                    entity = new DoubleGridItem();
                    entity.Name = item.Name;
                    entity.Url = item.Url;

                    if (!string.IsNullOrWhiteSpace(entity.Url))
                    {
                        string[] words = entity.Url.Split('/');

                        string idStr = "";

                        foreach (var word in words)
                        {
                            if (!string.IsNullOrWhiteSpace(word))
                                idStr = word;
                        }

                        entity.Id = Convert.ToInt32(idStr);
                        entity.Image = string.Format(_imageUrl, entity.Id);
                    }

                    _isOddItem = false;
                }
                else
                {
                    entity.Name2 = item.Name;
                    entity.Url2 = item.Url;

                    if (!string.IsNullOrWhiteSpace(entity.Url2))
                    {
                        string[] words = entity.Url2.Split('/');

                        string idStr = "";

                        foreach (var word in words)
                        {
                            if (!string.IsNullOrWhiteSpace(word))
                                idStr = word;
                        }

                        entity.Id2 = Convert.ToInt32(idStr);
                        entity.Image2 = string.Format(_imageUrl, entity.Id2);
                    }

                    _isOddItem = true;

                    PokemonData.Add(entity);
                }
            }

            if (!_isOddItem)
            {
                PokemonData.Add(entity);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}