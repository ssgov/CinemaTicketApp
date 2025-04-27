using System;
using System.Windows.Forms;

namespace CinemaTicketApp
{
    public partial class LoginForm : Form
    {
        private UserManager userManager;
        private Form1 mainForm;

        public LoginForm(Form1 mainForm)
        {
            this.mainForm = mainForm;
            userManager = new UserManager();
            InitializeComponent();
        }

        private void ManageRepertoireButton_Click(object sender, EventArgs e)
        {
            ManageRepertoireForm form = new ManageRepertoireForm();
            form.ShowDialog();
        }
        
        private void InitializeComponent()
        {
            this.Text = "Logowanie";
            this.Size = new System.Drawing.Size(300, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label usernameLabel = new Label { Text = "Nazwa użytkownika:", Location = new System.Drawing.Point(20, 20) };
            this.Controls.Add(usernameLabel);

            TextBox usernameBox = new TextBox { Location = new System.Drawing.Point(20, 40), Width = 200 };
            this.Controls.Add(usernameBox);

            Label passwordLabel = new Label { Text = "Hasło:", Location = new System.Drawing.Point(20, 70) };
            this.Controls.Add(passwordLabel);

            TextBox passwordBox = new TextBox { Location = new System.Drawing.Point(20, 90), Width = 200, PasswordChar = '*' };
            this.Controls.Add(passwordBox);

            Button loginButton = new Button { Text = "Zaloguj", Location = new System.Drawing.Point(20, 120),  Size = new System.Drawing.Size(180, 40) };
            loginButton.Click += (sender, e) =>
            {
                User? user = userManager.AuthenticateUser(usernameBox.Text, passwordBox.Text);
                if (user != null)
                {
                    MessageBox.Show($"Zalogowano jako {user.Username} ({user.Role})!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mainForm.SetLoggedInUser(user);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nieprawidłowa nazwa użytkownika lub hasło!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            this.Controls.Add(loginButton);

            Button registerButton = new Button() { Text = "Zarejestruj się", Location = new System.Drawing.Point(20, 180),  Size = new System.Drawing.Size(180, 40) };
            registerButton.Click += (sender, e) =>
            {
                RegisterForm registerForm = new RegisterForm();
                registerForm.ShowDialog();
            };
            this.Controls.Add(registerButton);

        }
    }
}
