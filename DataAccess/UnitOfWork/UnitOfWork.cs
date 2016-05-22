namespace DataAccess.UnitOfWork
{
    using System;
    using Domain.Entities;
    using Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesReportDbContext _context = new SalesReportDbContext();

        private bool _disposed;

        private RepositoryBase<Order> _orderRepository;
        private RepositoryBase<OrderDetails> _orderDetailsRepository;

        public RepositoryBase<Order> OrderRepository => _orderRepository ?? (_orderRepository = new RepositoryBase<Order>(_context));
        public RepositoryBase<OrderDetails> OrderDetailsRepository => _orderDetailsRepository ?? (_orderDetailsRepository = new RepositoryBase<OrderDetails>(_context));

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}