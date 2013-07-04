using System.Collections.Generic;

namespace MovieLink.Model
{
    public class MovieInfo : Movie
    {
        public int Star { get; set; }
        public Character Director { get; set; }
        public List<Character> Actors { get; set; }
        public List<Type> Types { get; set; }
        public List<Language> Languages { get; set; }
        public List<Country> Countrys { get; set; }
        public string Era { get; set; }
    }
}
