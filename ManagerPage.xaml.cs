using ContractMonthlyClaimSystemPart1.Models;
using System.Windows;

namespace ContractMonthlyClaimSystemPart1
{
    public partial class ManagerPage : Window
    {
        private User CurrentUser;

        public ManagerPage(User user) 
        {
            InitializeComponent();
            CurrentUser = user;

            lstVerified.ItemsSource = ClaimStore.VerifiedClaims;
            lstVerified.SelectionChanged += LstVerified_SelectionChanged;
        }

        private void LstVerified_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstVerified.SelectedItem is Claim c)
            {
                txtDetails.Text = $"Lecturer: {c.LecturerName} ({c.StaffNumber})\n" +
                                  $"Hours: {c.Hours}\nRate: {c.HourlyRate:C}\nAmount: {c.Amount:C}\n" +
                                  $"Submitted: {c.SubmittedAt:f}\nNotes: {c.Notes}\n" +
                                  $"Verified by: {c.VerifiedBy ?? "(pending)"}";
            }
            else
            {
                txtDetails.Text = "(select a claim)";
            }
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (lstVerified.SelectedItem is Claim c)
            {
                c.Status = ClaimStatus.Approved;
                c.ApprovedBy = CurrentUser.FullName; 
                ClaimStore.ApprovedClaims.Add(c);
                ClaimStore.VerifiedClaims.Remove(c);
                MessageBox.Show("Claim approved.", "Approved", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a verified claim to approve.", "Selection required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            if (lstVerified.SelectedItem is Claim c)
            {
                var ans = MessageBox.Show("Are you sure you want to reject this claim?", "Confirm reject", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (ans == MessageBoxResult.Yes)
                {
                    c.Status = ClaimStatus.Rejected;
                    c.ApprovedBy = CurrentUser.FullName; 
                    ClaimStore.VerifiedClaims.Remove(c);
                    MessageBox.Show("Claim rejected.", "Rejected", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a verified claim to reject.", "Selection required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            
            var main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    }
}
