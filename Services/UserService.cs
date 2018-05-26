using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Spice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Services
{
    public class UserService
    {
        private SpiceDBContext _context;

        public UserService(SpiceDBContext context)
        {
            _context = context;
        }

        public void Registry()
        {

        }
    }
}
