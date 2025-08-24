namespace DAL.Entities
{
    public class Person : EntityBase
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
