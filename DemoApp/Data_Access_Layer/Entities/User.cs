﻿using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
