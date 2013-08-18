using GenericDatatables.Core.Domain.Models;
using GenericDatatables.Core.Domain.Repositories;
using GenericDatatables.Default.Base.Repositories;
using GenericDatatables.Default.Database;

namespace GenericDatatables.Default.Persistence.Repositories
{
    public class GymMemberRepository: Repository<GymMember, GymContext>, IGymMemberRepository
    {
        public GymMemberRepository(GymContext context) : base(context)
        {
        }
    }
}
