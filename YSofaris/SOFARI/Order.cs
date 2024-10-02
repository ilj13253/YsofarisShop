using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFARI
{
   public class Order
    {
        public int Id { get; set; }
        public required decimal Price { get; set; }
        public required DateTime PurchaseDate { get; set; }
        public required int IdUser { get; set; }
        public required List<Product> Products { get; set; }
    }
}
