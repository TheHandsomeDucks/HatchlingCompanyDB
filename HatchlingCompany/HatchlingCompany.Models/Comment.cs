using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatchlingCompany.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public virtual Employee Author { get; set; }

        public int RecipientId { get; set; }
        public virtual Employee Recipient { get; set; }

        public string Content { get; set; }
    }
}
