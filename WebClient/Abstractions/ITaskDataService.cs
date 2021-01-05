using System;
using System.Collections.Generic;
using Domain.ViewModel;

namespace WebClient.Abstractions
{
    /// <summary>
    /// This Service is currently using the TaskModel Class, and will need to use a shared view
    /// model after the model has been created.  For the moment, this pattern facilitates a client
    /// side storage mechanism to view functionality.  See work completed for the MemberDataService
    /// for an example of expectations.
    /// </summary>
    public interface ITaskDataService
    {
        IEnumerable<TaskVm> Tasks { get; }
        TaskVm SelectedTask { get; set; }

        public event EventHandler TasksChanged;
        public event EventHandler<string> UpdateTaskFailed;
        public event EventHandler<string> CreateTaskFailed;
        public event EventHandler SelectedTaskChanged;

        void SelectTask(Guid id);
        void ToggleTaskAsync(Guid id);
        void AddTaskAsync(TaskVm model);
        void AssignMember(TaskVm model);
    }
}