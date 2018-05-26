using System;
using System.Collections.Generic;

namespace Spice.Models
{
    public partial class ProcCell
    {
        public int CellId { get; set; }
        public int? Row { get; set; }
        public int? Col { get; set; }
        public int? Value { get; set; }
        public int? Proc1Id { get; set; }
        public int? Proc2Id { get; set; }
        public int? ProcMatrixId { get; set; }

        public ProcMatrix ProcMatrix { get; set; }
    }
}
