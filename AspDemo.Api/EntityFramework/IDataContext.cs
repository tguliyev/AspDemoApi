using System;
using System.Threading;
using System.Threading.Tasks;
using AspDemo.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AspDemo.Api.EntityFramework
{
    public interface IDataContext
    {
        DbSet<Item>? Items { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry Entry(object entity);
    }
}