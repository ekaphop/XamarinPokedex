using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XamarinPokedex.Models;
using XamarinPokedex.Services;

namespace XamarinPokedex.ViewModels
{
    public class PokemonProfileViewModel : INotifyPropertyChanged
    {
        private string _imageUrl = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{0}.png";
        private string _pokemonUrl = "https://pokeapi.co/api/v2/pokemon/{0}/";

        private int _movesListViewHeight;
        public int MovesListViewHeight
        {
            get { return _movesListViewHeight; }
            set
            {
                _movesListViewHeight = value;
                OnPropertyChanged(nameof(MovesListViewHeight));
            }
        }

        private int _abilityListViewHeight;
        public int AbilityListViewHeight
        {
            get { return _abilityListViewHeight; }
            set
            {
                _abilityListViewHeight = value;
                OnPropertyChanged(nameof(AbilityListViewHeight));
            }
        }

        private List<string> _spritesImage;
        public List<string> SpritesImage
        {
            get { return _spritesImage; }
            set
            {
                _spritesImage = value;
                OnPropertyChanged(nameof(SpritesImage));
            }
        }

        private List<ItemEntity> _chainSource;
        public List<ItemEntity> ChainSource
        {
            get { return _chainSource; }
            set
            {
                _chainSource = value;
                OnPropertyChanged(nameof(ChainSource));
            }
        }

        private ObservableCollection<ItemEntity> _pokemonChainData;
        public ObservableCollection<ItemEntity> PokemonChainData
        {
            get { return _pokemonChainData; }
            set
            {
                _pokemonChainData = value;
                OnPropertyChanged(nameof(PokemonChainData));
            }
        }

        private PokemonSpeciesEntity _pokemonSpecies;
        public PokemonSpeciesEntity PokemonSpecies
        {
            get { return _pokemonSpecies; }
            set
            {
                _pokemonSpecies = value;
                OnPropertyChanged(nameof(PokemonSpecies));
            }
        }

        private PokemonProfileEntity _pokemonProfile;
        public PokemonProfileEntity PokemonProfile
        {
            get { return _pokemonProfile; }
            set
            {
                _pokemonProfile = value;

                if(PokemonProfile != null)
                {
                    _spritesImage = new List<string>();

                    AbilityListViewHeight = PokemonProfile.Abilities.Length * 30;
                    MovesListViewHeight = PokemonProfile.Moves.Length * 30;

                    if(!string.IsNullOrWhiteSpace(PokemonProfile.Sprites.BackDefault))
                        SpritesImage.Add(PokemonProfile.Sprites.BackDefault);
                    if (!string.IsNullOrWhiteSpace(PokemonProfile.Sprites.BackFemale))
                        SpritesImage.Add(PokemonProfile.Sprites.BackFemale);
                    if (!string.IsNullOrWhiteSpace(PokemonProfile.Sprites.BackShiny))
                        SpritesImage.Add(PokemonProfile.Sprites.BackShiny);
                    if (!string.IsNullOrWhiteSpace(PokemonProfile.Sprites.BackShinyFemale))
                        SpritesImage.Add(PokemonProfile.Sprites.BackShinyFemale);
                    if (!string.IsNullOrWhiteSpace(PokemonProfile.Sprites.FrontDefault))
                        SpritesImage.Add(PokemonProfile.Sprites.FrontDefault);
                    if (!string.IsNullOrWhiteSpace(PokemonProfile.Sprites.FrontFemale))
                        SpritesImage.Add(PokemonProfile.Sprites.FrontFemale);
                    if (!string.IsNullOrWhiteSpace(PokemonProfile.Sprites.FrontShiny))
                        SpritesImage.Add(PokemonProfile.Sprites.FrontShiny);
                    if (!string.IsNullOrWhiteSpace(PokemonProfile.Sprites.FrontShinyFemale))
                        SpritesImage.Add(PokemonProfile.Sprites.FrontShinyFemale);

                }
                else
                {
                    AbilityListViewHeight = 0;
                    MovesListViewHeight = 0; 
                }

                OnPropertyChanged(nameof(AbilityListViewHeight));
                OnPropertyChanged(nameof(MovesListViewHeight));
                OnPropertyChanged(nameof(PokemonProfile));
            }
        }


        public Command SelectChainItemCommand { get; }

        private bool _isShowListView = true;
        public bool IsShowListView
        {
            get { return _isShowListView; }
            set { _isShowListView = value; OnPropertyChanged(nameof(IsShowListView)); }
        }

        private bool _isIndicatorRunning = false;
        public bool IsIndicatorRunning
        {
            get { return _isIndicatorRunning; }
            set
            {
                _isIndicatorRunning = value;
                OnPropertyChanged(nameof(IsIndicatorRunning));
            }
        }

        public PokemonProfileViewModel(PokemonProfileEntity pokemonProfile, PokemonSpeciesEntity pokemonSpecies, List<ItemEntity> pokemonChain)
        {
            if (pokemonProfile == null)
                return;

            SelectChainItemCommand = new Command(SelectChainItemMethod);

            PokemonProfile = pokemonProfile;
            PokemonSpecies = pokemonSpecies;
            ChainSource = pokemonChain;

            PokemonChainData = new ObservableCollection<ItemEntity>();

            if(ChainSource != null)
            {
                BindingPokemonChain();
            }  
        }

        private bool _isSelectDoing = false;
        
        private async void SelectChainItemMethod(object obj)
        {
            if (_isSelectDoing)
                return;

            _isSelectDoing = true;

            var content = obj as ItemEntity;

            if(PokemonProfile.Id == content.Id)
            {
                await Application.Current.MainPage.DisplayAlert("Current Pokemon", "This is a " + content.Name, "OK");
            }            
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsIndicatorRunning = true;
                    IsShowListView = false;
                });

                var pokemonProfile = await PokeApiService.Instance.GetPokemonProfile(content.Id);
                var pokemonSpecies = await PokeApiService.Instance.GetPokemonSpecies(content.Id);

                PokemonProfile = pokemonProfile;
                PokemonSpecies = pokemonSpecies;

                Device.BeginInvokeOnMainThread(() =>
                { 
                    IsIndicatorRunning = false;
                    IsShowListView = true;
                });
            }

            _isSelectDoing = false;
        }

        private void BindingPokemonChain()
        {
            foreach (var item in ChainSource)
            {
                PokemonChainData.Add(item);
            }
        }

        public int FindIdByUrl(string url)
        {
            string[] words = url.Split('/');

            string idStr = "";

            foreach (var word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                    idStr = word;
            }

            return Convert.ToInt32(idStr);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}