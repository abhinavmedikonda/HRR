using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRR.Models
{
    public class ProjectModel
    {
        public ProjectModel()
        {
            RoleModels = new HashSet<RoleModel>();
        }

        public short Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<RoleModel> RoleModels { get; set; }
    }
}