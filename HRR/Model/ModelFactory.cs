using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRR.Models
{
    public class ModelFactory
    {
        private Model model;
        public ModelFactory()
        {
            this.model = new Model();
        }

        public ICollection<ProjectModel> Map(IEnumerable<Project> projects)
        {
            ICollection<ProjectModel> projectModels = new List<ProjectModel>();

            foreach (Project project in projects)
            {
                projectModels.Add(Map(project));
            }

            return projectModels;
        }

        public ProjectModel Map(Project project)
        {
            return new ProjectModel
            {
                CreatedDate = project.CreatedDate,
                Id = project.Id,
                Name = project.Name,
                RoleModels = Map(project.Roles)
            };
        }

        public ICollection<RoleModel> Map(IEnumerable<Role> roles)
        {
            ICollection<RoleModel> roleModels = new List<RoleModel>();

            foreach (Role role in roles)
            {
                roleModels.Add(Map(role));
            }

            return roleModels;
        }

        public RoleModel Map(Role role)
        {
            return new RoleModel
            {
                Id = role.Id,
                Name = role.Name,
                ProjectId = role.ProjectId,
                //ProjectModel = Map(role.Project),
                SkillSetModels = Map(role.SkillSets)
            };
        }

        public ICollection<SkillModel> Map(IEnumerable<Skill> skills)
        {
            ICollection<SkillModel> skillModels = new List<SkillModel>();

            foreach (Skill skill in skills)
            {
                skillModels.Add(Map(skill));
            }

            return skillModels;
        }

        public SkillModel Map(Skill skill)
        {
            return new SkillModel
            {
                Id = skill.Id,
                Name = skill.Name,
                SkillSetModels = Map(skill.SkillSets)
            };
        }

        public ICollection<SkillSetModel> Map(IEnumerable<SkillSet> skillSets)
        {
            ICollection<SkillSetModel> skillSetModels = new List<SkillSetModel>();

            foreach (SkillSet skillSet in skillSets)
            {
                skillSetModels.Add(Map(skillSet));
            }

            return skillSetModels;
        }

        public SkillSetModel Map(SkillSet skillSet)
        {
            return new SkillSetModel
            {
                Id = skillSet.Id,
                Name = model.Skills.Where(x => x.Id == skillSet.SkillId).Select(x => x.Name).First(),
                Years = skillSet.Years,
                //RoleId = skillSet.RoleId,
                //SkillId = skillSet.SkillId,
                //RoleModel = Map(skillSet.Role),
                //SkillModel = Map(skillSet.Skill)
            };
        }
    }
}