#nullable disable
using System.Windows.Forms;
using System.Drawing;

namespace CinemaTicketApp
{
    public partial class Form1
    {
        private Label titleLabel = null!;
        private Button showRepertoireButton = null!;
        private Button loginButton = null!;
        private Button registerButton = null!;
        private Button closeButton = null!;
        private Button changePasswordButton = null!;
        private Button logoutButton = null!;
        private Button createReservationButton = null!;
        private Button viewReservationsButton = null!;
        private Button manageRepertoireButton = null!;

        private void InitializeComponent()
        {
            this.Text = "Cinema Ticket App";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            titleLabel = new Label();
            titleLabel.Text = "🎬 Witaj w Cinema Ticket App!";
            titleLabel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Location = new Point((this.ClientSize.Width - titleLabel.Width) / 10, 20);
            this.Controls.Add(titleLabel);

            showRepertoireButton = CreateButton("Wyświetl Repertuar", new Point(160, 80), ShowRepertoireButton_Click);
            loginButton = CreateButton("Zaloguj się", new Point(160, 130), LoginButton_Click);
            registerButton = CreateButton("Stwórz nowe konto", new Point(160, 180), RegisterButton_Click);
            createReservationButton = CreateButton("Stwórz Rezerwację", new Point(160, 230), CreateReservationButton_Click, false);
            viewReservationsButton = CreateButton("Moje Rezerwacje", new Point(160, 280), ViewReservationsButton_Click, false);
            changePasswordButton = CreateButton("Zmień hasło", new Point(160, 330), ChangePasswordButton_Click, false);
            manageRepertoireButton = CreateButton("Zarządzaj Repertuarem", new Point(160, 380), ManageRepertoireButton_Click, false);
            logoutButton = CreateButton("Wyloguj się", new Point(160, 430), LogoutButton_Click, false);
            closeButton = CreateButton("Zamknij", new Point(160, 230), CloseButton_Click);
        }

        private Button CreateButton(string text, Point location, EventHandler clickHandler, bool visible = true)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(180, 40);
            btn.Location = location;
            btn.Visible = visible;
            btn.Click += clickHandler;
            this.Controls.Add(btn);
            return btn;
        }
    }
}
