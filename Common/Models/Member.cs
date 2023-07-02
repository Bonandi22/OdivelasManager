using Common.Models;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Member
    {
        public Member()
        {
            Functions = new List<Function>();
        }
        public Member(int id, 
                      string name, 
                      string? phoneNumber,
                      string? address,
                      string? city,
                      string? region, 
                      string? nacionality, 
                      bool isbaptized, 
                      DateTime? birthDate)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            Address = address;
            City = city;
            Region = region;
            Isbaptized = isbaptized;
            Nacionality = nacionality;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public bool Isbaptized { get; set; }
        public string? Nacionality { get; set; }
        public Category? Category { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Group? Group { get; set; }
        [Display(Name = "Group")]
        public int GroupId { get; set; }
        public Situation? Situation { get; set; }
        [Display(Name = "Situation")]
        public int SituationId { get; set; }
        [Display(Name = "Function")]
        public List<Function>? Functions { get; set; }
        public ICollection<MemberFunction>? MemberFunctions { get; set; }
    }
}
