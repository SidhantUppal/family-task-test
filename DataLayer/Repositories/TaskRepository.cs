using Core.Abstractions.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using Domain.DataModels;
using ThreadingTask = System.Threading.Tasks;

namespace DataLayer
{
    public class TaskRepository : BaseRepository<Guid, Task, TaskRepository>, ITaskRepository
    {
        public TaskRepository(FamilyTaskContext context) : base(context)
        {
        }

        ITaskRepository IBaseRepository<Guid, Task, ITaskRepository>.NoTrack()
        {
            return base.NoTrack();
        }

        ITaskRepository IBaseRepository<Guid, Task, ITaskRepository>.Reset()
        {
            return base.Reset();
        }

        IQueryable<Task> IBaseRepository<Guid, Task, ITaskRepository>.GetAll(
            params Expression<Func<Task, object>>[] including)
        {
            return base.GetAll(including);
        }

    }
}
