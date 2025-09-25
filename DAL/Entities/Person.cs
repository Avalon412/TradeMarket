using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Person : EntityBase
    {
        [StringLength(50)]
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Surname { get; set; }
        public DateTime BirthDate { get; set; }
        [StringLength(30)]
        public string? Email { get; set; }
    }
}
