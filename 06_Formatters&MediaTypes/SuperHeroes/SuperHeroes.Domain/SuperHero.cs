using System.ComponentModel.DataAnnotations;

namespace SuperHeroes.Domain
{
    public class SuperHero
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string Nickname { get; set; }
        [Required]
        public string Powers { get; set; }
        public bool HasCape { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
    }
}
