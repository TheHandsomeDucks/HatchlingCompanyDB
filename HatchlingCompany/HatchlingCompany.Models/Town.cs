using System.ComponentModel.DataAnnotations;

namespace HatchlingCompany.Models
{
    public class Town
    {
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}