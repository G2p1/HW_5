using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW_5
{
    public class Order
    {
        [Key]   
        public int ord_id { set; get; }
        public DateTime ord_datetime { set; get; }
        public int ord_an { set; get; }

    }
}
