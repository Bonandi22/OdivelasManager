using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Member>? Members { get; set; }
    }
}
