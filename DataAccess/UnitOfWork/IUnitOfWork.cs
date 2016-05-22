namespace DataAccess.UnitOfWork
{
    using System;
    using Domain.Entities;
    using Repositories;

    public interface IUnitOfWork : IDisposable
    {
        RepositoryBase<Order> OrderRepository { get; }
        RepositoryBase<OrderDetails> OrderDetailsRepository { get; }

        void Commit();
    }
}