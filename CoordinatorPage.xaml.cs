using ContractMonthlyClaimSystemPart1.Models;
using System.Windows;

namespace ContractMonthlyClaimSystemPart1
{
    public partial class CoordinatorPage : Window
    {
        private User CurrentUser;

        public CoordinatorPage(User user)
        {
            InitializeComponent();
            CurrentUser = user;

            lstPending.ItemsSource = ClaimStore.LecturerClaims;
            lstPending.SelectionChanged += LstPending_SelectionChanged;
        }

        private void LstPending_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstPending.SelectedItem is Claim c)
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

        private void Verify_Click(object sender, RoutedEventArgs e)
        {
            if (lstPending.SelectedItem is Claim c)
            {
                c.Status = ClaimStatus.Verified;
                c.VerifiedBy = CurrentUser.FullName; // Track who verified
                ClaimStore.VerifiedClaims.Add(c);
                ClaimStore.LecturerClaims.Remove(c);
                MessageBox.Show("Claim verified and forwarded to Manager.", "Verified", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a pending claim to verify.", "Selection required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            if (lstPending.SelectedItem is Claim c)
            {
                var ans = MessageBox.Show("Are you sure you want to reject this claim?", "Confirm reject", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (ans == MessageBoxResult.Yes)
                {
                    c.Status = ClaimStatus.Rejected;
                    c.VerifiedBy = CurrentUser.FullName; 
                    ClaimStore.LecturerClaims.Remove(c);
                    MessageBox.Show("Claim rejected and removed from queue.", "Rejected", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a pending claim to reject.", "Selection required", MessageBoxButton.OK, MessageBoxImage.Warning);
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
