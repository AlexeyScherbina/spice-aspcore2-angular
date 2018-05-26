using System;
using System.Collections.Generic;

namespace Spice.Models
{
    public partial class Process
    {
        public Process()
        {
            Practice = new HashSet<Practice>();
        }

        public int ProcessId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<Practice> Practice { get; set; }
    }
}
