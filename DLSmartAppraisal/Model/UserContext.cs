using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Model
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options):base(options){ }
        public DbSet<UserDetails> Users { get; set; }

    }
}
