#nullable disable
using System;
using System.Windows.Forms;

namespace CinemaTicketApp
{
    public class ChangePasswordForm : Form
    {
        private TextBox oldPasswordBox;
        private TextBox newPasswordBox;
        private TextBox confirmPasswordBox;
        private Button changeButton;

        private UserManager userManager;
        private User currentUser;

        public ChangePasswordForm(UserManager userManager, User currentUser)
        {
            this.userManager = userManager;
            this.currentUser = currentUser;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Zmiana hasła";
            this.Size = new System.Drawing.Size(300, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label oldLabel = new Label { Text = "Stare hasło:", Location = new System.Drawing.Point(20, 20) };
            oldPasswordBox = new TextBox { Location = new System.Drawing.Point(20, 40), Width = 240, UseSystemPasswordChar = true };

            Label newLabel = new Label { Text = "Nowe hasło:", Location = new System.Drawing.Point(20, 70) };
            newPasswordBox = new TextBox { Location = new System.Drawing.Point(20, 90), Width = 240, UseSystemPasswordChar = true };

            Label confirmLabel = new Label { Text = "Powtórz nowe hasło:", Location = new System.Drawing.Point(20, 120) };
            confirmPasswordBox = new TextBox { Location = new System.Drawing.Point(20, 140), Width = 240, UseSystemPasswordChar = true };

            changeButton = new Button { Text = "Zmień hasło", Location = new System.Drawing.Point(20, 180), Width = 240 };
            changeButton.Click += ChangeButton_Click;

            Controls.Add(oldLabel);
            Controls.Add(oldPasswordBox);
            Controls.Add(newLabel);
            Controls.Add(newPasswordBox);
            Controls.Add(confirmLabel);
            Controls.Add(confirmPasswordBox);
            Controls.Add(changeButton);
        }

        private void ChangeButton_Click(object? sender, EventArgs e)
        {
            string oldPass = oldPasswordBox.Text;
            string newPass = newPasswordBox.Text;
            string confirmPass = confirmPasswordBox.Text;

            if (string.IsNullOrWhiteSpace(oldPass) || string.IsNullOrWhiteSpace(newPass) || string.IsNullOrWhiteSpace(confirmPass))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           if (newPass != confirmPass)
            {
                MessageBox.Show("Nowe hasła nie są identyczne.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (oldPass == newPass)
            {
                MessageBox.Show("Nowe hasło musi różnić się od starego.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            bool success = userManager.ChangePassword(currentUser.Username, oldPass, newPass);

            if (success)
            {
                MessageBox.Show("Hasło zostało zmienione!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Nieprawidłowe stare hasło.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
