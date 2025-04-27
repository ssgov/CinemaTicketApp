using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace CinemaTicketApp
{
    public partial class AddMovieForm : Form
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Movie? CreatedMovie { get; private set; }

        private TextBox titleBox = null!;
        private TextBox genreBox = null!;
        private TextBox descriptionBox = null!;
        private DateTimePicker screeningDatePicker = null!;
        private Button saveButton = null!;

        public AddMovieForm()
        {
            InitializeComponent();
            CreatedMovie = new Movie();
        }

        private void InitializeComponent()
        {
            this.Text = "Dodaj film";
            this.Size = new Size(300, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label titleLabel = new Label();
            titleLabel.Text = "Tytuł:";
            titleLabel.Location = new Point(20, 20);
            titleLabel.Size = new Size(100, 20);
            this.Controls.Add(titleLabel);

            titleBox = new TextBox();
            titleBox.Location = new Point(20, 45);
            titleBox.Width = 240;
            this.Controls.Add(titleBox);

            Label genreLabel = new Label();
            genreLabel.Text = "Gatunek:";
            genreLabel.Location = new Point(20, 80);
            genreLabel.Size = new Size(100, 20);
            this.Controls.Add(genreLabel);

            genreBox = new TextBox();
            genreBox.Location = new Point(20, 105);
            genreBox.Width = 240;
            this.Controls.Add(genreBox);

            Label descriptionLabel = new Label();
            descriptionLabel.Text = "Opis:";
            descriptionLabel.Location = new Point(20, 140);
            descriptionLabel.Size = new Size(100, 20);
            this.Controls.Add(descriptionLabel);

            descriptionBox = new TextBox();
            descriptionBox.Location = new Point(20, 165);
            descriptionBox.Size = new Size(240, 60);
            descriptionBox.Multiline = true;
            this.Controls.Add(descriptionBox);

            Label dateLabel = new Label();
            dateLabel.Text = "Data seansu:";
            dateLabel.Location = new Point(20, 235);
            dateLabel.Size = new Size(100, 20);
            this.Controls.Add(dateLabel);

            screeningDatePicker = new DateTimePicker();
            screeningDatePicker.Location = new Point(20, 260);
            screeningDatePicker.Width = 240;
            this.Controls.Add(screeningDatePicker);

            saveButton = new Button();
            saveButton.Text = "Zapisz";
            saveButton.Size = new Size(240, 40);
            saveButton.Location = new Point(20, 310);
            saveButton.Click += SaveButton_Click;
            this.Controls.Add(saveButton);
        }

        private void SaveButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleBox.Text) || string.IsNullOrWhiteSpace(genreBox.Text))
            {
                MessageBox.Show("Tytuł i gatunek są wymagane!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CreatedMovie = new Movie
            {
                Title = titleBox.Text,
                Genre = genreBox.Text,
                Description = descriptionBox.Text,
                ScreeningDate = screeningDatePicker.Value
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
