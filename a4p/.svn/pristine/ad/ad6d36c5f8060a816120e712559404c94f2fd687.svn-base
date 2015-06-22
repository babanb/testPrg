using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Infrastructure.Extensions;
using Model;
using Model.Interfaces;

namespace Infrastructure
{
    public partial class ADOPetsEntities
    {
        public int UserId { get; set; }
        public string IpAddress { get; set; }

        public override int SaveChanges()
        {
            var newItems = new List<DbEntityEntry>();

            foreach (var dbEntityEntry in ChangeTracker.Entries().Where(e => e.Entity is IAuditable))
            {
                if (dbEntityEntry.State == EntityState.Added)
                {
                    newItems.Add(dbEntityEntry);
                }
                else
                {
                    var state = dbEntityEntry.State == EntityState.Modified ? AuditModificationTypeEnum.Modified : AuditModificationTypeEnum.Deleted;
                    var auditLog = dbEntityEntry.GetAuditLog(UserId, IpAddress, state);
                    AuditLogs.Add(auditLog);
                }
            }

            foreach (var deletableEntity in ChangeTracker.Entries<ISoftDelete>())
            {
                if (deletableEntity.State == EntityState.Deleted)
                {
                    deletableEntity.State = EntityState.Unchanged;
                    deletableEntity.Entity.IsDeleted = true; 
                }
            }

            if (newItems.Any())
            {
                base.SaveChanges();

                foreach (var dbEntityEntry in newItems)
                {
                    var auditLog = dbEntityEntry.GetAuditLog(UserId, IpAddress, AuditModificationTypeEnum.Added);
                    AuditLogs.Add(auditLog);
                }
            }

            return base.SaveChanges();
        }
    }
}
