using Core.Abstractions.Repositories;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MemberRepository : BaseRepository<Guid, Member, MemberRepository>, IMemberRepository
    {
        public MemberRepository(FamilyTaskContext context) : base(context)
        { }

       

        IMemberRepository IBaseRepository<Guid, Member, IMemberRepository>.NoTrack()
        {
            return base.NoTrack();
        }

        IMemberRepository IBaseRepository<Guid, Member, IMemberRepository>.Reset()
        {
            return base.Reset();
        }

        IQueryable<Member> IBaseRepository<Guid, Member, IMemberRepository>.GetAll(params Expression<Func<Member, object>>[] including)
        {
            return base.GetAll(including);
        }

    }
}
