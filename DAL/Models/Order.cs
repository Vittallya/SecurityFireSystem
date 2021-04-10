using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum OrderStatus
    {
        Active, Completed, Canceled
    }

    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ServiceId { get; set; }

        public double FullCost { get; set; }

        public DateTimeOffset CreationDate { get; set; }
        public double PersonalSale { get; set; }
        public string Address { get; set; }
        public DateTimeOffset OrderDate { get; set; }

        public virtual Client Client { get; set; }
        public virtual Service Service { get; set; }
        public OrderStatus OrderStatus { get; set; }

    }
}
