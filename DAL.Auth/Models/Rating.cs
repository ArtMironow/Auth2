namespace DAL.Auth.Models
{
    public class Rating
    {
        public Guid Id { get; set; }
        public int? Value { get; set; }
        public string? UserId { get; set; }

        public Guid? ReviewId { get; set; }
        public Review? Review { get; set; }
    }
}
