using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DB.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
        public string Path { get; set; }

        [NotMapped]
        public bool IsAdmin { get; set; }

        public IList<string> ImagePaths { get; set; }
        public ICollection<EventImage> Images { get; set; }
        public ICollection<EventRegistration> EventRegistrations { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }

        public Event()
        {
            ImagePaths = new List<string>();
        }
    }
}
