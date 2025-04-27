using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace CinemaTicketApp
{
    public class TicketReservation
    {
        public Guid ReservationId { get; set; } 
        public string MovieTitle { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public int SeatNumber { get; set; }
        public DateTime ReservationDate { get; set; }

        public TicketReservation()
        {
            
        }

        public TicketReservation(string movieTitle, string customerName, int seatNumber, DateTime reservationDate)
        {
            ReservationId = Guid.NewGuid();
            MovieTitle = movieTitle;
            CustomerName = customerName;
            SeatNumber = seatNumber;
            ReservationDate = reservationDate;
        }

        public override string ToString()
        {
            return $"{MovieTitle} - {CustomerName} (Miejsce {SeatNumber}) - {ReservationDate:dd/MM/yyyy HH:mm}";
        }

        // Tworzenie nowej rezerwacji
        public static void CreateReservation(string customerName)
        {
            if (!File.Exists("movies.json"))
            {
                MessageBox.Show("Brak dostępnych filmów.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string json = File.ReadAllText("movies.json");
            List<Movie>? movies = JsonSerializer.Deserialize<List<Movie>>(json);

            if (movies == null || movies.Count == 0)
            {
                MessageBox.Show("Brak dostępnych filmów.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string filmList = string.Join(Environment.NewLine, movies.Select((m, index) => $"{index + 1}. {m.Title}"));
            string input = Microsoft.VisualBasic.Interaction.InputBox($"Wybierz numer filmu:\n{filmList}", "Wybierz Film", "1");

            if (!int.TryParse(input, out int movieIndex) || movieIndex < 1 || movieIndex > movies.Count)
            {
                MessageBox.Show("Nieprawidłowy wybór.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedMovie = movies[movieIndex - 1].Title;

            string seatInput = Microsoft.VisualBasic.Interaction.InputBox("Podaj numer miejsca:", "Numer Miejsca", "1");

            if (!int.TryParse(seatInput, out int seatNumber) || seatNumber <= 0)
            {
                MessageBox.Show("Nieprawidłowy numer miejsca.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TicketReservation reservation = new TicketReservation(selectedMovie, customerName, seatNumber, DateTime.Now);

            List<TicketReservation> reservations = new();

            if (File.Exists("reservations.json"))
            {
                string reservationsJson = File.ReadAllText("reservations.json");
                reservations = JsonSerializer.Deserialize<List<TicketReservation>>(reservationsJson) ?? new List<TicketReservation>();
            }

            reservations.Add(reservation);

            File.WriteAllText("reservations.json", JsonSerializer.Serialize(reservations, new JsonSerializerOptions { WriteIndented = true }));

            MessageBox.Show("Rezerwacja została zapisana!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Usuwanie rezerwacji
        public static void DeleteReservation(string customerName)
        {
            if (!File.Exists("reservations.json"))
            {
                MessageBox.Show("Brak rezerwacji do usunięcia.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string json = File.ReadAllText("reservations.json");
            List<TicketReservation>? reservations = JsonSerializer.Deserialize<List<TicketReservation>>(json);

            if (reservations == null || reservations.Count == 0)
            {
                MessageBox.Show("Brak rezerwacji.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var userReservations = reservations.Where(r => r.CustomerName == customerName).ToList();

            if (userReservations.Count == 0)
            {
                MessageBox.Show("Nie masz żadnych rezerwacji.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string reservationList = string.Join(Environment.NewLine, userReservations.Select((r, index) => $"{index + 1}. {r.MovieTitle} (Miejsce {r.SeatNumber})"));
            string input = Microsoft.VisualBasic.Interaction.InputBox($"Wybierz numer rezerwacji do usunięcia:\n{reservationList}", "Usuń Rezerwację", "1");

            if (!int.TryParse(input, out int reservationIndex) || reservationIndex < 1 || reservationIndex > userReservations.Count)
            {
                MessageBox.Show("Nieprawidłowy wybór.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TicketReservation selectedReservation = userReservations[reservationIndex - 1];

            reservations.RemoveAll(r => r.ReservationId == selectedReservation.ReservationId);

            File.WriteAllText("reservations.json", JsonSerializer.Serialize(reservations, new JsonSerializerOptions { WriteIndented = true }));

            MessageBox.Show("Rezerwacja została usunięta.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
