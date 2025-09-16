using System;
using System.ComponentModel;

namespace ContractMonthlyClaimSystemPart1.Models
{
    public enum ClaimStatus
    {
        PendingVerification,
        Verified,
        Approved,
        Rejected
    }

    public class Claim : INotifyPropertyChanged
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        
        private string _lecturerName = "";
        public string LecturerName
        {
            get => _lecturerName;
            set { _lecturerName = value; OnPropertyChanged(nameof(LecturerName)); }
        }

        private string _staffNumber = "";
        public string StaffNumber
        {
            get => _staffNumber;
            set { _staffNumber = value; OnPropertyChanged(nameof(StaffNumber)); }
        }

        private decimal _hours;
        public decimal Hours
        {
            get => _hours;
            set { _hours = value; OnPropertyChanged(nameof(Hours)); UpdateAmount(); }
        }

        private decimal _hourlyRate;
        public decimal HourlyRate
        {
            get => _hourlyRate;
            set { _hourlyRate = value; OnPropertyChanged(nameof(HourlyRate)); UpdateAmount(); }
        }

        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            private set { _amount = value; OnPropertyChanged(nameof(Amount)); }
        }

        private ClaimStatus _status = ClaimStatus.PendingVerification;
        public ClaimStatus Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        public string Notes { get; set; }

        
        public string SubmittedBy { get; set; }
        public string VerifiedBy { get; set; }
        public string ApprovedBy { get; set; }

        
        public string DisplayTitle => $"{LecturerName} [{StaffNumber}] — {Hours}h @ {HourlyRate:C}";

        private void UpdateAmount()
        {
            Amount = Math.Round(Hours * HourlyRate, 2);
            OnPropertyChanged(nameof(DisplayTitle));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
