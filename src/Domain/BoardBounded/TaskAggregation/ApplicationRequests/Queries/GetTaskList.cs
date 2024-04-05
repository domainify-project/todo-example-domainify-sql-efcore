﻿using Domainify.Domain;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Domain.TaskAggregation
{
    public class GetTaskList :
        QueryListRequest<Task, PaginatedList<TaskViewModel>>
    {
        [Required]
        public Guid ProjectId { get; private set; }
        public Guid? SprintId { get; set; }
        public TaskStatus? Status { get; set; }
        public string? DescriptionSearchKey { get; set; }

        public GetTaskList()
        {
            ValidationState.Validate();
        }
        public GetTaskList SetProjectId(Guid value)
        {
            ProjectId = value;
            return this;
        }

        public override async System.Threading.Tasks.Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
