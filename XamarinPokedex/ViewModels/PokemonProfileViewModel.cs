using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using XamarinPokedex.Models;
using XamarinPokedex.Services;

namespace XamarinPokedex.ViewModels
{
    public class PokemonProfileViewModel : INotifyPropertyChanged
    {
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

        private ObservableCollection<PokemonProfileEntity> _profile;
        public ObservableCollection<PokemonProfileEntity> Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                OnPropertyChanged(nameof(Profile));
            }
        }


        public PokemonProfileViewModel(PokemonProfileEntity pokemonProfile, PokemonSpeciesEntity pokemonSpecies)
        {
            if (pokemonProfile == null)
                return;

            PokemonProfile = pokemonProfile;
            PokemonSpecies = pokemonSpecies;

            _profile = new ObservableCollection<PokemonProfileEntity>();
            Profile.Add(PokemonProfile);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}