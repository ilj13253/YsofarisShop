using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFARI
{
    public class WhishList
    {
        public int Id { get; set; }
        public required int IdUser { get; set; }
        public required Product Product { get; set; }
    }
}
