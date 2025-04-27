using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace CinemaTicketApp
{
    public partial class Form1 : Form
    {
        private User? loggedInUser = null;
        private ReservationManager reservationManager = new();

        public Form1()
        {
            InitializeComponent();
        }

        public void SetLoggedInUser(User user)
        {
            loggedInUser = user;
            MessageBox.Show($"Witaj, {user.Username}!", "Zalogowano", MessageBoxButtons.OK, MessageBoxIcon.Information);

            showRepertoireButton.Visible = true;
            createReservationButton.Visible = true;
            viewReservationsButton.Visible = true;
            changePasswordButton.Visible = true;
            logoutButton.Visible = true;
            loginButton.Visible = false;
            registerButton.Visible = false;
            closeButton.Visible = false;

            if (user.Role == "Admin")
            {
                manageRepertoireButton.Visible = true;
                createReservationButton.Visible = false;
                viewReservationsButton.Visible = false;
            }
            else
            {
                manageRepertoireButton.Visible = false;
            }

            UpdateButtonsLayout();
        }

        public void AddReservation(TicketReservation reservation)
        {
            reservationManager.AddReservation(reservation);
            MessageBox.Show("Rezerwacja została dodana!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowRepertoireButton_Click(object sender, EventArgs e)
        {
            ShowRepertoireForm repertoireForm = new ShowRepertoireForm();
            repertoireForm.ShowDialog();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(this);
            loginForm.ShowDialog();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        private void CreateReservationButton_Click(object sender, EventArgs e)
        {
            TicketReservation.CreateReservation(loggedInUser!.Username);
        }

        private void ViewReservationsButton_Click(object sender, EventArgs e)
        {
            if (loggedInUser == null)
            {
                MessageBox.Show("Musisz być zalogowany, aby zobaczyć swoje rezerwacje.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ViewReservationsForm reservationsForm = new ViewReservationsForm(loggedInUser);
            reservationsForm.ShowDialog();
        }


        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            if (loggedInUser == null)
            {
                MessageBox.Show("Musisz być zalogowany, aby zmienić hasło.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChangePasswordForm form = new ChangePasswordForm(new UserManager(), loggedInUser);
            form.ShowDialog();
        }

        private void ManageRepertoireButton_Click(object sender, EventArgs e)
        {
            ManageRepertoireForm form = new ManageRepertoireForm();
            form.ShowDialog();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            loggedInUser = null;

            loginButton.Visible = true;
            registerButton.Visible = true;
            closeButton.Visible = true;

            showRepertoireButton.Visible = true;
            createReservationButton.Visible = false;
            viewReservationsButton.Visible = false;
            changePasswordButton.Visible = false;
            manageRepertoireButton.Visible = false;
            logoutButton.Visible = false;

            MessageBox.Show("Zostałeś wylogowany.", "Wylogowano", MessageBoxButtons.OK, MessageBoxIcon.Information);

            UpdateButtonsLayout();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateButtonsLayout()
        {
            int centerX = (this.ClientSize.Width - 200) / 2;
            int startY = 100;
            int spacing = 60;
            int currentY = startY;

             titleLabel.Location = new Point((this.ClientSize.Width - titleLabel.Width) / 2, 20);

            Button[] buttons = new Button[]
            {
                showRepertoireButton,
                loginButton,
                registerButton,
                createReservationButton,
                viewReservationsButton,
                changePasswordButton,
                manageRepertoireButton,
                logoutButton,
                closeButton
            };

            foreach (Button btn in buttons)
            {
                if (btn.Visible)
                {
                    btn.Location = new Point(centerX, currentY);
                    currentY += spacing;
                }
            }
        }
    }
}