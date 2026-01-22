using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Model
{
    public class CompetencyContext : DbContext
    {
        public CompetencyContext(DbContextOptions<CompetencyContext> options) : base(options) { }

        public DbSet<CompetencyDetails> Competencies{ get; set; }
    }
}
