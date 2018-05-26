using System;
using System.Collections.Generic;

namespace Spice.Models
{
    public partial class CatMatrix
    {
        public CatMatrix()
        {
            CatCell = new HashSet<CatCell>();
        }

        public int CatMatrixId { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }
        public ICollection<CatCell> CatCell { get; set; }
    }
}
