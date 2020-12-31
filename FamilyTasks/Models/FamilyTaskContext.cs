using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTasks.Models
{
    public class FamilyTaskContext:DbContext
    {
        public FamilyTaskContext(DbContextOptions<FamilyTaskContext> options) : base(options)
        { }
        public DbSet<Member> Members { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
    
}
