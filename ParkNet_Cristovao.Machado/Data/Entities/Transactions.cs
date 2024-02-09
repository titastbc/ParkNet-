using System;
using System.ComponentModel.DataAnnotations;

namespace ParkNet_Cristovao.Machado.Data.Entities
{
    public class Transactions
    {
        public string Id { get; set; }
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
        public string Description { get; set; }
        [StringLength(100)]
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }
}
