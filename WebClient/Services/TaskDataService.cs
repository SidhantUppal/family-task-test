using Domain.Commands;
using Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using ThreadingTasks = System.Threading.Tasks;
using WebClient.Abstractions;
using Domain.ViewModel;
using Core.Extensions.ModelConversion;
using Microsoft.AspNetCore.Components;

namespace WebClient.Services
{
    public class TaskDataService: ITaskDataService
    {
        private readonly HttpClient _httpClient;
       
        public TaskDataService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            tasks = new List<TaskVm>();
            LoadTasks();
        }

        private IEnumerable<TaskVm> tasks;

        public IEnumerable<TaskVm> Tasks => tasks;


        public TaskVm SelectedTask { get; set; }

        public event EventHandler TasksChanged;
        public event EventHandler<string> UpdateTaskFailed;
        public event EventHandler<string> CreateTaskFailed;
        public event EventHandler SelectedTaskChanged;

        private async void LoadTasks()
        {
            tasks =(await GetAllTasks()).Payload;
            TasksChanged?.Invoke(this, null);
        }

        public void SelectTask(Guid id)
        {
            SelectedTask = tasks.SingleOrDefault(t => t.Id == id);
            SelectedTaskChanged?.Invoke(this, null);
        }
        private ThreadingTasks.Task<CreateTaskCommandResult> Create(CreateTaskCommand command)
        {
            var response= _httpClient.PostJsonAsync<CreateTaskCommandResult>("tasks", command);
            return response;
        }

        private async ThreadingTasks.Task<UpdateTaskCommandResult> Update(UpdateTaskCommand command)
        {
            var response = await _httpClient.PutJsonAsync<UpdateTaskCommandResult>($"tasks/{command.Id}", command);
            return response;
        }

        private async ThreadingTasks.Task<GetAllTasksQueryResult> GetAllTasks()
        {
            return await _httpClient.GetJsonAsync<GetAllTasksQueryResult>("tasks");
        }

        public async void ToggleTaskAsync(Guid id)
        {
            foreach (var taskModel in tasks)
            {
                if (taskModel.Id == id)
                {
                    taskModel.IsComplete = !taskModel.IsComplete;

                    var result = await Update(taskModel.ToUpdateTaskCommand());

                    Console.WriteLine(JsonSerializer.Serialize(result));

                    if (result != null)
                    {
                        var updatedList = (await GetAllTasks()).Payload;

                        if (updatedList != null)
                        {
                            tasks = updatedList.ToList();
                            TasksChanged?.Invoke(this, null);
                            return;
                        }

                        UpdateTaskFailed?.Invoke(this,
                            "The save was successful, but we can no longer get an updated list of tasks from the server.");
                    }

                    UpdateTaskFailed?.Invoke(this, "Unable to save changes.");
                }
            }

            //TasksUpdated?.Invoke(this, null);
        }

        public async void AddTaskAsync(TaskVm model)
        {
            var result = await Create(model.ToCreateTaskCommand());
            if (result != null)
            {
                var updatedList = (await GetAllTasks()).Payload;

                if (updatedList != null)
                {
                    tasks = updatedList.ToList();
                    TasksChanged?.Invoke(this, null);
                    return;
                }
                UpdateTaskFailed?.Invoke(this, "The creation was successful, but we can no longer get an updated list of tasks from the server.");
            }

            UpdateTaskFailed?.Invoke(this, "Unable to create record.");
        }

        public async void AssignMember(TaskVm model)
        {
            var result = await Update(model.ToUpdateTaskCommand());
            if (result != null)
            {
                var updatedList = (await GetAllTasks()).Payload;

                if (updatedList != null)
                {
                    tasks = updatedList.ToList();
                    TasksChanged?.Invoke(this, null);
                    return;
                }
                UpdateTaskFailed?.Invoke(this, "The updation was successful, but we can no longer get an updated list of tasks from the server.");
            }

            UpdateTaskFailed?.Invoke(this, "Unable to create record.");
        }
    }
}