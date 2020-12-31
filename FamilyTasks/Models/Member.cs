using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTasks.Models
{
    public class Member
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(50)] public string FirstName { get; set; }
        [StringLength(50)] public string LastName { get; set; }
        [StringLength(50)] public string EmailAddress { get; set; }
        [Required] [StringLength(50)] public string Roles { get; set; }
        [Required] [StringLength(10)] public string Avatar { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
