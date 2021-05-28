using System;
using System.Collections.Generic;
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
        private string _pokemonUrl = "https://pokeapi.co/api/v2/pokemon/{0}/";

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

        private bool _isSelectDoing = false;

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
            if (_isSelectDoing)
                return;

            _isSelectDoing = true;

            var pokemonProfile = await PokeApiService.Instance.GetPokemonProfile(id);
            var pokemonSpecies = await PokeApiService.Instance.GetPokemonSpecies(id);
            var pokemonChain = await PokeApiService.Instance.GetPokemonChain(pokemonSpecies.EvolutionChain.Url);

            List<ItemEntity> chainItem = new List<ItemEntity>();

            if(pokemonChain != null && pokemonChain.Chain != null)
            {
                if (!string.IsNullOrWhiteSpace(pokemonChain.Chain.Species.Url))
                {
                    var entity = new ItemEntity
                    {
                        Name = pokemonChain.Chain.Species.Name,
                        Id = FindIdByUrl(pokemonChain.Chain.Species.Url)
                    };
                    entity.Url = string.Format(_pokemonUrl, entity.Id);
                    entity.Image = string.Format(_imageUrl, entity.Id);

                    chainItem.Add(entity);
                }

                if(pokemonChain.Chain.EvolvesTo != null)
                {
                    var bodyGen2 = pokemonChain.Chain.EvolvesTo;

                    foreach(var item2 in bodyGen2)
                    {
                        if(item2 != null)
                        {
                            if (!string.IsNullOrWhiteSpace(item2.Species.Url))
                            {
                                var entity = new ItemEntity
                                {
                                    Name = item2.Species.Name,
                                    Id = FindIdByUrl(item2.Species.Url)
                                };
                                entity.Url = string.Format(_pokemonUrl, entity.Id);
                                entity.Image = string.Format(_imageUrl, entity.Id);

                                chainItem.Add(entity);
                            }

                            if(item2.EvolvesTo != null)
                            {
                                var bodyGen3 = item2.EvolvesTo;

                                foreach (var item3 in bodyGen3)
                                {
                                    if (item3 != null)
                                    {
                                        if (!string.IsNullOrWhiteSpace(item3.Species.Url))
                                        {
                                            var entity = new ItemEntity
                                            {
                                                Name = item3.Species.Name,
                                                Id = FindIdByUrl(item3.Species.Url)
                                            };
                                            entity.Url = string.Format(_pokemonUrl, entity.Id);
                                            entity.Image = string.Format(_imageUrl, entity.Id);

                                            chainItem.Add(entity);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                var entity = new ItemEntity
                {
                    Name = pokemonProfile.Name,
                    Id = pokemonProfile.Id
                };
                entity.Url = string.Format(_pokemonUrl, entity.Id);
                entity.Image = string.Format(_imageUrl, entity.Id);

                chainItem.Add(entity);
            }

            _isSelectDoing = false;

            await Navigation.PushAsync(new PokemonProfilePage(pokemonProfile, pokemonSpecies, chainItem));
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
                    entity = new DoubleGridItem
                    {
                        Name = item.Name,
                        Url = item.Url
                    };

                    if (!string.IsNullOrWhiteSpace(entity.Url))
                    {
                        entity.Id = FindIdByUrl(entity.Url);
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
                        entity.Id2 = FindIdByUrl(entity.Url2);
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