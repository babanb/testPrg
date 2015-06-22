using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using Infrastructure.Helpers;
using Model;
using Model.Interfaces;

namespace Infrastructure.Extensions
{
    public static class DBEntityEntryExtensions
    {
        public static object ToObject(this DbEntityEntry dbEntityEntry)
        {
            var type = ObjectContext.GetObjectType(dbEntityEntry.Entity.GetType());

            var instance = Activator.CreateInstance(type);

            var values = dbEntityEntry.State == EntityState.Deleted ? dbEntityEntry.GetDatabaseValues() : dbEntityEntry.CurrentValues;

            foreach (var name in values.PropertyNames)
            {
                var property = type.GetProperty(name);
                property.SetValue(instance, values.GetValue<object>(name));
            }

            return instance;
        }

        public static string GetTableName(this DbEntityEntry dbEntityEntry)
        {
            return ObjectContext.GetObjectType(dbEntityEntry.Entity.GetType()).Name;
        }

        public static AuditLog GetAuditLog(this DbEntityEntry dbEntityEntry, int userId, string ipAddress, AuditModificationTypeEnum state)
        {
            var auditData = new AuditLog();

            auditData.AddressIP = ipAddress;
            auditData.ModifiedByUserId = userId;

            auditData.ModificationDate = DateTime.Now;
            auditData.TargetTableName = dbEntityEntry.GetTableName();

            auditData.AuditModificationTypeId = state;
            auditData.TargetTableId = (dbEntityEntry.Entity as IAuditable).Id.ToString();

            if (dbEntityEntry.Entity is IPetAuditable)
            {
                auditData.TargetPetId = (dbEntityEntry.Entity as IPetAuditable).PetId;
            }
            else if (dbEntityEntry.Entity is IUserAuditable)
            {
                auditData.TargetUserId = (dbEntityEntry.Entity as IUserAuditable).UserId;
            }

            auditData.XMLData = SerializationHelper.SerializeEntityToXml(dbEntityEntry.ToObject());

            return auditData;
        }
    }
}
