using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRR.Models
{
    public class SkillModel
    {
        public SkillModel()
        {
            SkillSetModels = new HashSet<SkillSetModel>();
        }

        public short Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<SkillSetModel> SkillSetModels { get; set; }
    }
}