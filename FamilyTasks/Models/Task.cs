using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTasks.Models
{
    public class Task
    {
        [Key]  
        public Guid Id { get; set; }
        [StringLength(50)] public string Subject { get; set; }
        public bool IsComplete { get; set; }  
        public Guid? AssignedMemberId { get; set; }
        public Member AssignedMember { get; set; }
    }
}
