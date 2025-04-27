using Newtonsoft.Json;

namespace CinemaTicketApp
{
    public class MovieManager
    {
        private const string MoviesFile = "movies.json";
        public List<Movie> Movies { get; private set; } = new List<Movie>();

        public MovieManager()
        {
            LoadMovies();
        }

        public void LoadMovies()
        {
            if (File.Exists(MoviesFile))
            {
                string json = File.ReadAllText(MoviesFile);
                Movies = JsonConvert.DeserializeObject<List<Movie>>(json) ?? new List<Movie>();
            }
        }

        public void SaveMovies()
        {
            string json = JsonConvert.SerializeObject(Movies, Formatting.Indented);
            File.WriteAllText(MoviesFile, json);
        }

        public void AddMovie(Movie movie)
        {
            Movies.Add(movie);
            SaveMovies();
        }

        public void RemoveMovie(Movie movie)
        {
            Movies.Remove(movie);
            SaveMovies();
        }
    }
}
