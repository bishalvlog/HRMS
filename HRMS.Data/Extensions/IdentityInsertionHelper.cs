using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Extensions
{
    public static class IdentityInsertionHelper
    {
        private static Task EnableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, enable: true);

        private static Task DisableIdentityInsert<T> (this DbContext context) => SetIdentityInsert<T>(context, enable : false);

        private static Task SetIdentityInsert<T>(DbContext context, bool enable)
        {
            var entityType = context.Model.FindEntityTypes(typeof(T));
            var value = enable ? "ON" : "OFF";
            return context.Database.ExecuteSqlRawAsync(
                 $"SET IDENTITY_INSERT {entityType}.{entityType} {value}");
        }

        public static async Task SaveChangesWithIdentityInsert<T>(this DbContext context)
        {
            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await context.EnableIdentityInsert<T>();
                await context.SaveChangesAsync();
                await context.DisableIdentityInsert<T>();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
