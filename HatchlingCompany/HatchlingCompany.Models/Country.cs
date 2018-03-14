using System.Collections.Generic;

namespace HatchlingCompany.Models
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }

    }
}