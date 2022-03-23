using System.ComponentModel.DataAnnotations;

namespace SYC.Core.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Description { get; set; }
        public DateTime Birthday { get; set; }
        public string Photo { get; set; }
        public string[] Phones { get; set; }
        public string[] Emails { get; set; }
        public string[] BankCards { get; set; }
        public string[] CarNumbers { get; set; }
    }
}