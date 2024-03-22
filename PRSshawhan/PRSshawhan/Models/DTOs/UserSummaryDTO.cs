namespace PRSshawhan.Models.DTOs
{
    public class UserSummaryDTO
    {
        public string FullName { get; set; }
        public int CountOfRejectedRequests { get; set; }
        public int CountOfApprovedRequests { get; set; }
        public int CountOfPendingRequests { get; set; }
        public decimal ApprovedTotal { get; set; }
        public decimal RejectedTotal { get; set; }

        public UserSummaryDTO(string firstName, string lastName, int countOfRejectedRequests, int countOfApprovedRequests,
            int countOfPendingRequests, decimal approvedTotal, decimal rejectedTotal)
        {
            this.FullName = firstName + " " + lastName;
            this.CountOfRejectedRequests = countOfRejectedRequests;
            this.CountOfApprovedRequests = countOfApprovedRequests;
            this.CountOfPendingRequests = countOfPendingRequests;
            this.ApprovedTotal = approvedTotal;
            this.RejectedTotal = rejectedTotal;
        }
    }
}
