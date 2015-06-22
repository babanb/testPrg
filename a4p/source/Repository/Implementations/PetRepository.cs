using System.Data;
using System.Data.Entity;
using System.Linq;
using Model;

namespace Repository.Implementations
{
    public class PetRepository : SoftDeleteRepository<Pet>
    {
        public PetRepository(DbContext context)
            : base(context)
        {
        }

        public override void Update(Pet item)
        {
            if (item.Farmer != null)
            {
                context.Entry(item.Farmer).State = item.FarmerId == null
                    ? EntityState.Added
                    : EntityState.Modified;
            }

            dbSet.Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }

        public int GetPetCountByUser(int userId)
        {
            return context.Set<Pet>().Count(p => !p.IsDeleted && p.Users.Any(u => u.Id == userId));
        }
    }
}
