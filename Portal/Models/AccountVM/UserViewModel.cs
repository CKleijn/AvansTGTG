namespace Portal.Models.AccountVM
{
    public class UserViewModel
    {
        public string? UserName { get; set; }
        public bool IsStudent { get; set; } = false;
        public bool IsCanteenEmployee { get; set; } = false;
        public bool IsCanteenLocation { get; set; } = false;
    }
}
