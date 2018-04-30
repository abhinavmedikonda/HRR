using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRR.Models
{
    public class SkillSetModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte? Years { get; set; }

        //public short SkillId { get; set; }

        //public short RoleId { get; set; }

        //public virtual RoleModel RoleModel { get; set; }

        //public virtual SkillModel SkillModel { get; set; }
    }
}