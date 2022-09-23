namespace FilmLibrary
{
    public class Film
    {
        // public properties of each film in the library
        public Int64 filmId { get; set; }
        public string title { get; set; }
        public List<string> director { get; set;}
        public List<string> genres { get; set; }

        // public constructor method
        public Film()
        {
            genres = new List<string>();
            director = new List<string>();
            
        }

        // public method to display the the films in the library
        public string Display()
        {
            return $"ID: {filmId}\nTitle: {title}\nDirector: {director}\nGenres: {string.Join(", ", genres)}\n";
        }
    }
}
