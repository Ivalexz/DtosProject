using System.Diagnostics;
using DtosProject.Models;
using DtosProject.DTOs;
using DtosProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace DtosProject.Controllers
{
    public class HomeController : Controller
    {
        ClientService _clientService = new ClientService();
        
        public HomeController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult Index(string? search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Clients = _clientService.SearchClients(search);
                ViewBag.Search = search;
            }
            else
            {
                ViewBag.Clients = _clientService.GetAllClients();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClientFormModel obj)
        {
            var dto = new CreateClientDto
            {
                FullName = obj.FullName,
                Email = obj.Email
            };
            _clientService.AddClient(dto);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var dto = _clientService.GetClientById(id);
            if (dto == null) return NotFound();
            return View(dto);
        }
        
        [HttpGet]
        public IActionResult Update(int id)
        {
            var client = _clientService.GetClientById(id);
            if (client == null) return NotFound();

            var dto = new UpdateClientDto
            {
                Id = client.Id,
                FullName = client.FullName,
                Email = client.Email,
                Age = client.Age
            };
            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit(UpdateClientDto dto)
        {
            _clientService.UpdateClient(dto);
            return RedirectToAction("Index");
        }
    }
}