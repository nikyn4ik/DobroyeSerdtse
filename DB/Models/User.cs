using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DB.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string? Achievements { get; set; }
        public string? Description { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<EventRegistration> EventRegistrations { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}