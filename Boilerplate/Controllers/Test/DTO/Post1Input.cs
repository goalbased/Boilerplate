using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Controllers.Test.DTO
{
    public class Post1Input
    {
        [Required]
        public string Name { get; set; }

        public int Test1 { get; set; }
        public bool Test2 { get; set; }
    }
}