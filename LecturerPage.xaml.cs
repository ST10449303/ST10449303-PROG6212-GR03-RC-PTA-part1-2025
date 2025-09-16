using ContractMonthlyClaimSystemPart1.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace ContractMonthlyClaimSystemPart1
{
    public partial class LecturerPage : Window
    {
        private User CurrentUser;

        public LecturerPage(User user)
        {
            InitializeComponent();
            CurrentUser = user;

            
            txtLecturerName.Text = CurrentUser.FullName;
            txtStaffNumber.Text = CurrentUser.StaffNumber;

            

            lstMyClaims.ItemsSource = ClaimStore.LecturerClaims;
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { Multiselect = true, Title = "Select supporting document(s)" };
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    var upDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploads");
                    Directory.CreateDirectory(upDir);
                    foreach (var f in dlg.FileNames)
                    {
                        var dest = Path.Combine(upDir, Path.GetFileName(f));
                        File.Copy(f, dest, true);
                    }
                    MessageBox.Show("Files copied to local 'uploads' folder (prototype).", "Upload", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Upload failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLecturerName.Text) ||
                string.IsNullOrWhiteSpace(txtStaffNumber.Text) ||
                string.IsNullOrWhiteSpace(txtHours.Text) ||
                string.IsNullOrWhiteSpace(txtRate.Text))
            {
                MessageBox.Show("Please complete all required fields.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtHours.Text, out decimal hours) || hours <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for hours.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtRate.Text, out decimal rate) || rate < 0)
            {
                MessageBox.Show("Please enter a valid number for rate.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var claim = new Claim
            {
                LecturerName = txtLecturerName.Text.Trim(),
                StaffNumber = txtStaffNumber.Text.Trim(),
                Hours = hours,
                HourlyRate = rate,
                Notes = txtNotes.Text?.Trim(),
                Status = ClaimStatus.PendingVerification,
                SubmittedAt = DateTime.Now
            };

            ClaimStore.LecturerClaims.Add(claim);
            MessageBox.Show("Claim submitted and is pending verification.", "Submitted", MessageBoxButton.OK, MessageBoxImage.Information);

            
            txtHours.Clear();
            txtRate.Clear();
            txtNotes.Clear();
        }

        private void SaveDraft_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Draft saved locally (prototype).", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
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
