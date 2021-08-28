using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalWebApp.Domain.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
