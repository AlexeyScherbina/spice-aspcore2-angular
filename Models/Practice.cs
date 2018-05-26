using System;
using System.Collections.Generic;

namespace Spice.Models
{
    public partial class Practice
    {
        public int PracticeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ProcessId { get; set; }

        public Process Process { get; set; }
    }
}
