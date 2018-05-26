using System;
using System.Collections.Generic;

namespace Spice.Models
{
    public partial class CatCell
    {
        public int CellId { get; set; }
        public int? Row { get; set; }
        public int? Col { get; set; }
        public int? Value { get; set; }
        public int? Cat1Id { get; set; }
        public int? Cat2Id { get; set; }
        public int? CatMatrixId { get; set; }

        public CatMatrix CatMatrix { get; set; }
    }
}
