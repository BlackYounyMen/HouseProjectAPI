using System.ComponentModel.DataAnnotations;

namespace House.Model
{
    public class Test
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }
    }
}