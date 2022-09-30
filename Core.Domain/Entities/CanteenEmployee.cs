namespace Core.Domain.Entities
{
    public class CanteenEmployee
    {
        public int CanteenEmployeeId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? EmployeeNumber { get; set; }
        [Required]
        public string? Location { get; set; }
    }
}
