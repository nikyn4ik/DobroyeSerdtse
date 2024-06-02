using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DB.Models
{
    public class EventImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Path { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}