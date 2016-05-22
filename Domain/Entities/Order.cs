namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;

    public class Order : IEntity
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
