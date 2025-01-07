using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(18, 60)]
        public int Age { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
