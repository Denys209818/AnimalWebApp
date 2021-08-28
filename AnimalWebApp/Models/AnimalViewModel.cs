using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalWebApp.Models
{
    public class AnimalViewModel
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Birthday { get; set; }
        public string Image { get; set; }
    }

    public class AnimalSearchViewModel
    {
        public string Name { get; set; }
    }

    public class AnimalPageData
    {
        public int Page { get; set; }
        public int PageCount { get; set; }
        public List<AnimalViewModel> animals { get; set; }
        public AnimalSearchViewModel search { get;set;}
    }

    public class AnimalEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Назва")]
        public string Name { get; set; }
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }
        [Display(Name = "Дата народження")]
        public string Birthday { get; set; }
        [Display(Name = "Фотографія")]
        public IFormFile Image { get; set; }
    }
}
