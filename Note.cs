using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Note
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool? State { get; set; }
        public int? TagId { get; set; }
        [Required]
        public int PriorityLevelId { get; set; }
        public PriorityLevel PriorityLevel { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
