namespace HRR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SkillSet
    {
        public int Id { get; set; }

        public short SkillId { get; set; }

        public short RoleId { get; set; }

        public byte? Years { get; set; }

        public virtual Role Role { get; set; }

        public virtual Skill Skill { get; set; }
    }
}
