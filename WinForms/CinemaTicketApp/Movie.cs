namespace CinemaTicketApp
{
    public class Movie
    {
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ScreeningDate { get; set; }

        public Movie()
        {
        }

        public Movie(string title, string genre, string description, DateTime screeningDate)
        {
            Title = title;
            Genre = genre;
            Description = description;
            ScreeningDate = screeningDate;
        }

        public override string ToString()
        {
            return $"{Title} ({Genre}) - {ScreeningDate:dd.MM.yyyy HH:mm}";
        }
    }
}
