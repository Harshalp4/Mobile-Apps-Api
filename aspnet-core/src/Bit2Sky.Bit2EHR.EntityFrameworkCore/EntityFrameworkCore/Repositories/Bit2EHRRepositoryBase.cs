using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace Bit2Sky.Bit2EHR.EntityFrameworkCore.Repositories;

/// <summary>
/// Base class for custom repositories of the application.
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
/// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
public abstract class Bit2EHRRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<Bit2EHRDbContext, TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    protected Bit2EHRRepositoryBase(IDbContextProvider<Bit2EHRDbContext> dbContextProvider)
        : base(dbContextProvider)
    {

    }

    //add your common methods for all repositories
}

/// <summary>
/// Base class for custom repositories of the application.
/// This is a shortcut of <see cref="Bit2EHRRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
public abstract class Bit2EHRRepositoryBase<TEntity> : Bit2EHRRepositoryBase<TEntity, int>
    where TEntity : class, IEntity<int>
{
    protected Bit2EHRRepositoryBase(IDbContextProvider<Bit2EHRDbContext> dbContextProvider)
        : base(dbContextProvider)
    {

    }

    //do not add any method here, add to the class above (since this inherits it)!!!
}

