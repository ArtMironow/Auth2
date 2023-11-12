namespace DAL.Auth.Models
{
    public class Like
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }

        public Guid? ReviewId { get; set; }
        public Review? Review { get; set; }
    }
}
