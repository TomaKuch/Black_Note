using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Note
{
    public class PriorityLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Note> Notes { get; set; }
        public PriorityLevel()
        {
            Notes = new List<Note>();
        }
    }
}
