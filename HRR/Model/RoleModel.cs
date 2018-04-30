using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRR.Models
{
    public class RoleModel
    {
        public RoleModel()
        {
            SkillSetModels = new HashSet<SkillSetModel>();
        }

        public short Id { get; set; }

        public short ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public byte? Years { get; set; }

        public virtual ProjectModel ProjectModel { get; set; }

        public virtual ICollection<SkillSetModel> SkillSetModels { get; set; }
    }
}