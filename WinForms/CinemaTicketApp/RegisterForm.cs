using System;
using System.Windows.Forms;

namespace CinemaTicketApp
{
    public partial class RegisterForm : Form
    {
        private UserManager userManager = new UserManager();

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Rejestracja użytkownika";
            this.Size = new System.Drawing.Size(300, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label usernameLabel = new Label() { Text = "Nazwa użytkownika:", Location = new System.Drawing.Point(20, 20) };
            this.Controls.Add(usernameLabel);

            TextBox usernameBox = new TextBox() { Location = new System.Drawing.Point(20, 40), Width = 200 };
            this.Controls.Add(usernameBox);

            Label passwordLabel = new Label() { Text = "Hasło:", Location = new System.Drawing.Point(20, 70) };
            this.Controls.Add(passwordLabel);

            TextBox passwordBox = new TextBox() { Location = new System.Drawing.Point(20, 90), Width = 200, PasswordChar = '*' };
            this.Controls.Add(passwordBox);

            Button registerButton = new Button() { Text = "Zarejestruj", Location = new System.Drawing.Point(20, 130),  Size = new System.Drawing.Size(180, 40) };
            registerButton.Click += (sender, e) =>
            {
                string username = usernameBox.Text;
                string password = passwordBox.Text;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Wprowadź poprawne dane!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = userManager.RegisterUser(username, password, "User");
                if (success)
                {
                    MessageBox.Show("Rejestracja udana!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Użytkownik już istnieje!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            this.Controls.Add(registerButton);
        }
    }
}
