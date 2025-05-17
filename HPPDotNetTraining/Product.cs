using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPPDotNetTraining
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price {  get; set; }
        
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }  

    }
}
