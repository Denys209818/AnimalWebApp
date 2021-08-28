using AnimalWebApp.Domain.Entities;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalWebApp.Services
{
    public static class BogusService
    {
        private static Faker<AppAnimal> _animalFaker { get; set; }

        public static AppAnimal Generate() 
        {
            if (_animalFaker == null)
                Initialize();

            return _animalFaker.Generate();
        }
        private static void Initialize() 
        {
            _animalFaker = new Faker<AppAnimal>("uk")
                .RuleFor(x => x.Name, f => f.Name.FullName())
                .RuleFor(x => x.Image, f => f.Image.LoremPixelUrl())
                .RuleFor(x => x.Price, f => (decimal)Math.Ceiling(f.Random.Decimal(0, 1000)))
                .RuleFor(x => x.DateCreated, DateTime.Now)
                .RuleFor(x => x.Birthday, f => f.Date.Between(
                    new DateTime(DateTime.Now.Year - 15, DateTime.Now.Month, DateTime.Now.Day), DateTime.Now));
        }
    }
}
