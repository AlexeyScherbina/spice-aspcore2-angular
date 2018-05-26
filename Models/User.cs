using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Spice.Models
{
    public partial class User : IdentityUser
    {

        public User() : base()
        {
            CatMatrix = new HashSet<CatMatrix>();
            ProcMatrix = new HashSet<ProcMatrix>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<CatMatrix> CatMatrix { get; set; }
        public ICollection<ProcMatrix> ProcMatrix { get; set; }

    }
}
