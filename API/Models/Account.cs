﻿namespace API.Models
{
    public class Account
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
        public long Role { get; set; }
    }
}