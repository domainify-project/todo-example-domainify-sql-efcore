﻿using Domainify.Domain;
using System.ComponentModel.DataAnnotations;
using Domain.Properties;

namespace Domain.ProjectSettingAggregation
{
    public class Project : Entity<Project, string>, IAggregateRoot
    {
        public double Version { get; set; }

        [MinLengthShouldBe(1)]
        [MaxLengthShouldBe(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; protected set; } = string.Empty;

        public List<Sprint> Sprints { get; set; } = new List<Sprint>();

        public override ConditionProperty<Project>? Uniqueness()
        {
            return new ConditionProperty<Project>()
            {
                Condition = x => x.Name == Name,
                Description = Resource.AProjectWithThisNameHasAlreadyExisted
            };
        }

        public Project ()
        {
            // The version field is used for persisting in a nosql database like MongoDB
            //Version = 1.0;
        }

        public static Project NewInstance()
        {
            return new Project();
        }

        public Project SetName(string value)
        {
            Name = value;

            return this;
        }
        public ProjectViewModel ToViewModel()
        {
            var viewModel = new ProjectViewModel()
            {
                ModifiedDate = ModifiedDate,
                IsDeleted = IsDeleted,
                Sprints = Sprints.Select(i => i.ToViewModel(Name)).ToList(),
                Id = Id!,
                Name = Name,
            };

            return viewModel;
        }
    }
}