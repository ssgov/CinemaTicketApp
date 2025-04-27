using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace CinemaTicketApp
{
    public partial class ManageRepertoireForm : Form
    {
        private List<Movie> movies = new();
        private ListBox movieListBox = null!;
        private Button addButton = null!;
        private Button deleteButton = null!;

        private readonly string MoviesFilePath = "movies.json";

        public ManageRepertoireForm()
        {
            InitializeComponent();
            LoadMovies();
            UpdateMovieList();
        }

        private void InitializeComponent()
        {
            this.Text = "Zarządzanie Repertuarem";
            this.Size = new System.Drawing.Size(400, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Lista filmów
            movieListBox = new ListBox();
            movieListBox.Size = new System.Drawing.Size(340, 280);
            movieListBox.Location = new System.Drawing.Point(20, 20);
            this.Controls.Add(movieListBox);

            // Dodaj film
            addButton = new Button();
            addButton.Text = "Dodaj Film";
            addButton.Size = new System.Drawing.Size(150, 40);
            addButton.Location = new System.Drawing.Point(20, 320);
            addButton.Click += AddButton_Click;
            this.Controls.Add(addButton);

            // Usuń film
            deleteButton = new Button();
            deleteButton.Text = "Usuń Wybrany Film";
            deleteButton.Size = new System.Drawing.Size(150, 40);
            deleteButton.Location = new System.Drawing.Point(210, 320);
            deleteButton.Click += DeleteButton_Click;
            this.Controls.Add(deleteButton);
        }

        private void LoadMovies()
        {
            if (File.Exists(MoviesFilePath))
            {
                string json = File.ReadAllText(MoviesFilePath);
                var loadedMovies = JsonSerializer.Deserialize<List<Movie>>(json);
                if (loadedMovies != null)
                    movies = loadedMovies;
            }
        }

        private void SaveMovies()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(movies, options);
            File.WriteAllText(MoviesFilePath, json);
        }

        private void UpdateMovieList()
        {
            movieListBox.Items.Clear();
            foreach (var movie in movies)
            {
                movieListBox.Items.Add($"{movie.Title} ({movie.ScreeningDate:yyyy-MM-dd})");
            }
        }

        private void AddButton_Click(object? sender, EventArgs e)
        {
            AddMovieForm addForm = new AddMovieForm();
            if (addForm.ShowDialog() == DialogResult.OK && addForm.CreatedMovie != null)
            {
                movies.Add(addForm.CreatedMovie);
                SaveMovies();
                UpdateMovieList();
            }
        }

        private void DeleteButton_Click(object? sender, EventArgs e)
        {
            if (movieListBox.SelectedIndex >= 0)
            {
                movies.RemoveAt(movieListBox.SelectedIndex);
                SaveMovies();
                UpdateMovieList();
            }
            else
            {
                MessageBox.Show("Wybierz film do usunięcia.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
