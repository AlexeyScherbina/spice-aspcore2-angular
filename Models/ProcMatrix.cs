using System;
using System.Collections.Generic;

namespace Spice.Models
{
    public partial class ProcMatrix
    {
        public ProcMatrix()
        {
            ProcCell = new HashSet<ProcCell>();
        }

        public int ProcMatrixId { get; set; }
        public string UserId { get; set; }
        public int? CategoryId { get; set; }

        public Category Category { get; set; }
        public User User { get; set; }
        public ICollection<ProcCell> ProcCell { get; set; }
    }
}
