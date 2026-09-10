using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Stolovayaa.DTOs
{
    public class OrderItemDto
    {
        public int DishId { get; set; }       
        public int Count { get; set; }        
        public decimal PriceAtOrder { get; set; } 
    }
}