using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace CinemaTicketApp
{
    public partial class ShowRepertoireForm : Form
    {
        private ListBox movieListBox;

        public ShowRepertoireForm()
        {
            movieListBox = new ListBox();
            InitializeComponent();
            LoadMoviesFromJson();
        }

        private void InitializeComponent()
        {
            this.Text = "Dostępny Repertuar";
            this.Size = new System.Drawing.Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            movieListBox = new ListBox();
            movieListBox.Dock = DockStyle.Fill;
            movieListBox.Font = new System.Drawing.Font("Segoe UI", 12);
            this.Controls.Add(movieListBox);
        }

        private void LoadMoviesFromJson()
        {
            string filePath = "movies.json";

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Brak dostępnych filmów.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                List<Movie> movies = JsonSerializer.Deserialize<List<Movie>>(json) ?? new List<Movie>();

                if (movies == null || movies.Count == 0)
                {
                    MessageBox.Show("Brak dostępnych filmów.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var movie in movies)
                {
                    movieListBox.Items.Add($"{movie.Title} ({movie.Genre}) - {movie.ScreeningDate:dd.MM.yyyy}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wczytywania repertuaru: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
