using DtosProject.DTOs;
using DtosProject.Models;
using DtosProject.Data;
 
namespace DtosProject.Services
{
    public class ClientService
    {
        private readonly AppDbContext _db;
        public ClientService(AppDbContext db)
        {
            _db = db;
        }
    
        public void AddClient(CreateClientDto dto)
        {
            var obj = new Client
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Age = dto.Age,
                CreatedAt = DateTime.UtcNow
            };
            _db.Clients.Add(obj);
            _db.SaveChanges();
        }
        public List<ClientListItemDto> GetAllClients()
        {
            return _db.Clients.Select(c => new ClientListItemDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email
            }).ToList();
        }

        public ClientDetailsDto  GetClientById(int id)
        {
            var client = _db.Clients.FirstOrDefault(c => c.Id == id);
            if (client == null) return null;

            return new ClientDetailsDto 
            {
                Id = client.Id,
                FullName = client.FullName,
                Email = client.Email,
                Age = client.Age,
                CreatedAt = client.CreatedAt
            };
        }

        public void UpdateClient(UpdateClientDto dto)
        {
            var client = _db.Clients.FirstOrDefault(c => c.Id == dto.Id);
            if (client != null)
            {
                client.FullName = dto.FullName;
                client.Email = dto.Email;
                client.Age = dto.Age;
            }
            _db.SaveChanges();
        }
        
        public List<ClientListItemDto> SearchClients(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return GetAllClients();

            return _db.Clients
                .Where(c => c.FullName.ToLower().Contains(searchText.ToLower()) ||
                            c.Email.ToLower().Contains(searchText.ToLower()))
                .Select(c => new ClientListItemDto
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    Email = c.Email
                }).ToList();
        }
    }
}