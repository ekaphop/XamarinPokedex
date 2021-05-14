using System;
using System.Collections.Generic;
using XamarinPokedex.Models;

namespace XamarinPokedex.Services
{
    public class FakePlantService
    {
        private static FakePlantService _instance;
        public static FakePlantService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FakePlantService();
                return _instance;
            }
        }

        public List<Plant> GetPlants()
        {
            return new List<Plant>
            {
                new Plant { Name = "bulbasaur",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/1.png",Price = 12 ,Description=""},
                new Plant { Name = "ivysaur",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/2.png",Price = 26 ,Description=""},
                new Plant { Name = "venusaur",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/3.png",Price = 13 ,Description=""},
                new Plant { Name = "charmander",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/4.png",Price = 5 ,Description=""},
                new Plant { Name = "charmeleon",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/5.png",Price = 78 ,Description=""},
                new Plant { Name = "charizard",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/6.png",Price = 67 ,Description=""},
                new Plant { Name = "squirtle",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/7.png",Price = 16 ,Description=""},
                new Plant { Name = "blastoise",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/8.png",Price = 85 ,Description=""},
                new Plant { Name = "Angelica",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/9.png",Price = 12 ,Description=""},
                new Plant { Name = "Jennifer",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/10.png",Price = 26 ,Description=""},
                new Plant { Name = "Samantha",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/11.png",Price = 13 ,Description=""},
                new Plant { Name = "Setena",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/12.png",Price = 5 ,Description=""},
                new Plant { Name = "Bezane",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/13.png",Price = 78 ,Description=""},
                new Plant { Name = "Fassobo",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/14.png",Price = 67 ,Description=""},
                new Plant { Name = "Sebastian",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/15.png",Price = 16 ,Description=""},
                new Plant { Name = "Veron",Image="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/16.png",Price = 85 ,Description=""}
            };
        }
    }
}
