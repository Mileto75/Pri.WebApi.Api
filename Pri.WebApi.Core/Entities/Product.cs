using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public int? CategoryId { get; set; }
        public string Description { get; set; }
        public ICollection<Property> Properties { get; set; }
        public decimal Price { get; set; }
    }
}
