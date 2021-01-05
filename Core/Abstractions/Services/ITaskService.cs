using Domain.Commands;
using Domain.Queries;
using ThreadingTasks = System.Threading.Tasks;

namespace Core.Abstractions.Services
{
    public interface ITaskService
    {
        ThreadingTasks.Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command);
        ThreadingTasks.Task<UpdateTaskCommandResult> UpdateTaskCommandHandler(UpdateTaskCommand command);
        ThreadingTasks.Task<GetAllTasksQueryResult> GetAllTasksQueryHandler();
    }
}