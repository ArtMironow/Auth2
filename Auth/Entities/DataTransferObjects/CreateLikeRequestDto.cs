﻿using System.ComponentModel.DataAnnotations;

namespace Auth.Entities.DataTransferObjects
{
    public class CreateLikeRequestDto
    {
        //[Required]
        public string? Email { get; set; }

        //[Required]
        public string? ReviewId { get; set; }
    }
}
