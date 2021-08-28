using AnimalWebApp.Domain;
using AnimalWebApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalWebApp.Controllers
{
    public class AnimalController : Controller
    {
        public EFDataContext _context { get; set; }
        public IMapper _mapper { get; set; }
        public AnimalController(EFDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index(AnimalSearchViewModel search, int page = 1)
        {
            int itemsCount = 2;
             
            var query = _context.animals.AsQueryable();
            if (!string.IsNullOrEmpty(search.Name)) 
            {
                query = query.Where(x => x.Name.ToUpper().Contains(search.Name.ToUpper()));
            }
            var models = query.Select(x => 
            _mapper.Map<AnimalViewModel>(x))
                .Skip((page-1)* itemsCount).Take(itemsCount).ToList();
            AnimalPageData data = new AnimalPageData();
            data.animals = models;
            data.search = search;
            data.Page = page;
            data.PageCount = (int)Math.Ceiling(query.Count()/(double)itemsCount);
            if (data.Page > data.PageCount && data.PageCount > 0)
                return RedirectToAction("Index", new { search.Name, page = data.PageCount });
            return View(data);
        }
        [HttpGet]
        public IActionResult Get(int id, AnimalSearchViewModel search) 
        {
            search.Name = search.Name == null ? "" : search.Name;
            var _animals = _context.animals
                .Where(x => x.Name.ToUpper().Contains(search.Name.ToUpper()));
            if (_animals == null)
                return Ok(JsonConvert.SerializeObject(null));
            string json = JsonConvert.SerializeObject(_animals.Skip(id).Take(2).Select(x => new { 
                    Name = x.Name,
                    Image = x.Image,
                    Id = x.Id,
                    Birthday = x.Birthday.ToLongDateString(),
                    Price = x.Price
                }));
            return Ok(json);
        }
        [HttpGet]
        public IActionResult GetElement(int count, AnimalSearchViewModel search, int page = 1) 
        {
            search.Name = search.Name == null ? "" : search.Name;
            int itemsCount = 2;
            var animal = _context.animals.Where(x => x.Name.Contains(search.Name))
                .Skip(((page-1)* itemsCount)+count).Take(1).FirstOrDefault();
            if (animal == null)
                return Ok(JsonConvert.SerializeObject(null));
            return Ok(JsonConvert.SerializeObject(new { 
                Name= animal.Name,
                Price = animal.Price,
                Birthday = animal.Birthday.ToLongDateString(),
                Image = animal.Image,
                Id= animal.Id
            }));
        }

        [HttpPost]
        public IActionResult Delete(int id) 
        {
            var animal = _context.animals.FirstOrDefault(x => x.Id == id);
            if (!animal.Image.Contains("http"))
            {
                string currDir = Directory.GetCurrentDirectory();
                string[] files = animal.Image.Split('/').Select(x => x).ToArray();
                string filePath = Directory.GetCurrentDirectory();
                foreach (var file in files.Skip(1)) 
                {
                     filePath = System.IO.Path.Combine(filePath, file);
                }
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            if (animal != null)
            {
                _context.animals.Remove(animal);
                _context.SaveChanges();
            }
            return Ok();
        }

        
        public IActionResult Edit(int id) 
        {
            var _contextEl = _context.animals.FirstOrDefault(x => x.Id == id);
            if (_contextEl == null) return RedirectToAction("Index");
            var animal = _mapper.Map< AnimalEditViewModel>(
                _contextEl);
            
            return View(animal);
        }
        [HttpPost]
        public IActionResult Edit(AnimalEditViewModel edit)
        {
            var animal = _context.animals.FirstOrDefault(x => x.Id == edit.Id);
            var dt = DateTime.Parse(edit.Birthday);
            animal.Name = edit.Name;
            animal.Price = edit.Price;
            animal.Birthday = dt;
            string file = Directory.GetCurrentDirectory();
            if (edit.Image != null)
            {
                if (!animal.Image.Contains("http"))
                {
                    foreach (var pathEl in animal.Image.Split('\\').Skip(1))
                    {
                        file = Path.Combine(file, pathEl);
                    }

                    if (System.IO.File.Exists(file))
                    {
                        System.IO.File.Delete(file);
                    }
                }

                string newFileName = "\\Images\\" + Path.GetRandomFileName() + 
                    Path.GetExtension(edit.Image.FileName);

                string createdFilePath = Directory.GetCurrentDirectory() + newFileName;
                using (var stream = System.IO.File.Create(createdFilePath)) 
                {
                    edit.Image.CopyToAsync(stream).Wait();
                }

                animal.Image = newFileName;
            }
            
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
