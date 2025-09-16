using System.Windows;
using ContractMonthlyClaimSystemPart1.Models;

namespace ContractMonthlyClaimSystemPart1
{
    public partial class LoginWindow : Window
    {
        public User CurrentUser { get; private set; }
        public string Role { get; private set; }

        public LoginWindow(string role)
        {
            InitializeComponent();
            Role = role;
            this.DataContext = this;
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password.Trim();

            
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Enter email and password.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            CurrentUser = new User
            {
                FullName = Role + " User",
                StaffNumber = Role.Substring(0, 3).ToUpper() + "001",
                Email = email,
                Password = password,
                Role = Role
            };

            this.DialogResult = true;
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
