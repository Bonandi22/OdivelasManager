using Common.Models;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Function
    {
        [Key]

        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Member>? Members { get; set; }
        public ICollection<MemberFunction>? MemberFunctions { get; set; }
    }
}
