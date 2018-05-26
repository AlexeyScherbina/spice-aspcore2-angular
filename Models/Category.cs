using System;
using System.Collections.Generic;

namespace Spice.Models
{
    public partial class Category
    {
        public Category()
        {
            ProcMatrix = new HashSet<ProcMatrix>();
            Process = new HashSet<Process>();
        }

        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<ProcMatrix> ProcMatrix { get; set; }
        public ICollection<Process> Process { get; set; }
    }
}
