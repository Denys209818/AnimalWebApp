using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalWebApp.Domain.Entities
{
    public class AppAnimal : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime Birthday { get; set; }
        public decimal Price { get; set; }
    }
}
