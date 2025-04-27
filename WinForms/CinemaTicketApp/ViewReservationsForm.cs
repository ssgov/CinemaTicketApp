using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace CinemaTicketApp
{
    public partial class ViewReservationsForm : Form
    {
        private ListBox reservationsListBox = null!;
        private Button deleteButton = null!;
        private User currentUser;
        private List<TicketReservation> allReservations = new();

        public ViewReservationsForm(User user)
        {
            currentUser = user;
            InitializeComponent();
            LoadReservations();
        }

        private void InitializeComponent()
        {
            this.Text = "Moje Rezerwacje";
            this.Size = new System.Drawing.Size(400, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            reservationsListBox = new ListBox();
            reservationsListBox.Size = new System.Drawing.Size(350, 250);
            reservationsListBox.Location = new System.Drawing.Point(20, 20);
            this.Controls.Add(reservationsListBox);

            deleteButton = new Button();
            deleteButton.Text = "Usuń Rezerwację";
            deleteButton.Size = new System.Drawing.Size(180, 40);
            deleteButton.Location = new System.Drawing.Point(100, 290);
            deleteButton.Click += DeleteButton_Click;
            this.Controls.Add(deleteButton);
        }

        private void LoadReservations()
        {
            if (File.Exists("reservations.json"))
            {
                string json = File.ReadAllText("reservations.json");
                allReservations = JsonSerializer.Deserialize<List<TicketReservation>>(json) ?? new List<TicketReservation>();

                foreach (var reservation in allReservations)
                {
                    if (reservation.CustomerName == currentUser.Username)
                    {
                        reservationsListBox.Items.Add($"Film: {reservation.MovieTitle} (Rezerwacja ID: {reservation.ReservationId})");
                    }
                }
            }
        }

        private void DeleteButton_Click(object? sender, EventArgs e)
        {
            if (reservationsListBox.SelectedIndex >= 0)
            {
                var selectedItem = reservationsListBox.SelectedItem?.ToString();
                if (selectedItem == null)
                {
                    MessageBox.Show("Wybrany element jest nieprawidłowy.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
        
                string reservationIdText = selectedItem.Split("(Rezerwacja ID: ")[1].Replace(")", "").Trim();
        
                if (Guid.TryParse(reservationIdText, out Guid reservationId))
                {
                    allReservations.RemoveAll(r => r.ReservationId == reservationId);
                    File.WriteAllText("reservations.json", JsonSerializer.Serialize(allReservations));
                    MessageBox.Show("Rezerwacja została usunięta.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reservationsListBox.Items.RemoveAt(reservationsListBox.SelectedIndex);
                }
                else
                {
                    MessageBox.Show("Nie udało się rozpoznać rezerwacji.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Wybierz rezerwację do usunięcia.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
