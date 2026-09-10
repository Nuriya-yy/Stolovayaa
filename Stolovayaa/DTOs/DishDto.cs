using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stolovayaa.DTOs
{
    public class DishDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public decimal Price {  get; set; }
    }
}
