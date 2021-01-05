using AutoMapper;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.Queries;
using Domain.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Domain.DataModels;
using ThreadingTasks = System.Threading.Tasks;

namespace Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async ThreadingTasks.Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command)
        {
             var task = _mapper.Map<Task>(command);
            var persistedTask = await _taskRepository.CreateRecordAsync(task);

            var vm = _mapper.Map<TaskVm>(persistedTask);

            return new CreateTaskCommandResult()
            {
                Payload = vm
            };
        }
        
        public async ThreadingTasks.Task<UpdateTaskCommandResult> UpdateTaskCommandHandler(UpdateTaskCommand command)
        {
            var isSucceed = true;
            var task = await _taskRepository.ByIdAsync(command.Id);

            _mapper.Map<UpdateTaskCommand, Task>(command, task);
            
            var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);

            if (affectedRecordsCount < 1)
                isSucceed = false;

            return new UpdateTaskCommandResult() { 
               Succeed = isSucceed
            };
        }

        public async ThreadingTasks.Task<GetAllTasksQueryResult> GetAllTasksQueryHandler()
        {
            IEnumerable<TaskVm> vm = new List<TaskVm>();

            var tasks =  _taskRepository.GetAll(x=>x.Member).ToList();

            if (tasks != null && tasks.Any())
                vm = _mapper.Map<IEnumerable<TaskVm>>(tasks);
            return new GetAllTasksQueryResult() {Payload = vm
            };
        }

    }
}
