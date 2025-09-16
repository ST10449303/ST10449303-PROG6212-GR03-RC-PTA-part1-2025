using System.Collections.ObjectModel;
using ContractMonthlyClaimSystemPart1.Models;

namespace ContractMonthlyClaimSystemPart1
{
    public static class ClaimStore
    {
       
        public static ObservableCollection<Claim> LecturerClaims { get; } = new ObservableCollection<Claim>();
        public static ObservableCollection<Claim> VerifiedClaims { get; } = new ObservableCollection<Claim>();
        public static ObservableCollection<Claim> ApprovedClaims { get; } = new ObservableCollection<Claim>();

        
    }
}
