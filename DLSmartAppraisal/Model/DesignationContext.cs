using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Model
{
    public class DesignationContext:DbContext
    {
        public DesignationContext(DbContextOptions<DesignationContext> options):base(options){ }
        public DbSet<DesignationDetails> designations { get; set; }

    }
}
