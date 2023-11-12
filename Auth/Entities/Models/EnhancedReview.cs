﻿namespace Auth.Entities.Models
{
    public class EnhancedReview
    {
        public Guid Id { get; set; }
        public string? Nickname { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ReviewText { get; set; }
        public string? Theme { get; set; }
        public string? Image { get; set; }
        public DateTime? Created { get; set; }
        public string? Link { get; set; }
        public string? UserId { get; set; }
        public double? Rating { get; set; }
        public int? LikesCount { get; set; }
    }

    //public record EnhancedReview(
    //    Guid Id, 
    //    string? Nickname, 
    //    string? Title, 
    //    string? Description, 
    //    string? ReviewText,
    //    string? Theme,
    //    string? Image,
    //    DateTime? Created,
    //    string? Link,
    //    string? UserId,
    //    double? Rating,
    //    int? LikesCount
    //);
}
