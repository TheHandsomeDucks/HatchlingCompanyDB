using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HatchlingCompany.Models
{
    public class Country
    {
        public Country()
        {
            this.Towns = new HashSet<Town>();
        }
        
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; } = new HashSet<Town>();
    }
}