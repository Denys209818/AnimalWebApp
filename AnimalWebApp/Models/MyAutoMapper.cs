using AnimalWebApp.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalWebApp.Models
{
    public class MyAutoMapper : Profile
    {
        public MyAutoMapper()
        {
            CreateMap<AppAnimal, AnimalViewModel>()
                .ForMember(x => x.Id, f => f.MapFrom(y => y.Id))
                .ForMember(x => x.Name, f=>f.MapFrom(y => y.Name))
                .ForMember(x => x.Image, f => f.MapFrom(y => y.Image))
                .ForMember(x => x.Price, f => f.MapFrom(y => y.Price))
                .ForMember(x => x.DateCreated, f => f.MapFrom(y => y.DateCreated))
                .ForMember(x => x.Birthday, f => f.MapFrom(y => DateTime.Parse(y.Birthday.ToString("dd.MM.yyyy"),
                new CultureInfo("uk"))))
                .ReverseMap();

            var memoryStream = new MemoryStream();
            CreateMap<AppAnimal, AnimalEditViewModel>()
                .ForMember(x => x.Id, f => f.MapFrom(y => y.Id))
                .ForMember(x => x.Name, f => f.MapFrom(y => y.Name))
                .ForMember(x => x.Image, f => f.MapFrom(y => new FormFile(memoryStream, 0,
                    memoryStream.Length, y.Image, y.Image)))
                .ForMember(x => x.Price, f => f.MapFrom(y => y.Price))
                .ForMember(x => x.Birthday, f => f.MapFrom(y => y.Birthday.ToShortDateString()))
                ;
        }
    }
}
