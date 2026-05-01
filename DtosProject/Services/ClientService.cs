using DtosProject.DTOs;
using DtosProject.Models;

namespace DtosProject.Services
{
    public class ClientService
    {
        private static List<Client> _clients = new List<Client>();
        private static int _nextId = 1;

        public void AddClient(CreateClientDto dto)
        {
            var obj = new Client
            {
                Id = _nextId++,
                FullName = dto.FullName,
                Email = dto.Email,
                Age = dto.Age,
                CreatedAt = DateTime.UtcNow
            };
            _clients.Add(obj);
        }

        public List<ClientListItemDto> GetAllClients()
        {
            return _clients.Select(c => new ClientListItemDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email
            }).ToList();
        }

        public ClientDetailsDto GetClientById(int id)
        {
            var client = _clients.FirstOrDefault(c => c.Id == id);
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
            var client = _clients.FirstOrDefault(c => c.Id == dto.Id);
            if (client != null)
            {
                client.FullName = dto.FullName;
                client.Email = dto.Email;
                client.Age = dto.Age;
            }
        }

        public List<ClientListItemDto> SearchClients(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return GetAllClients();

            return _clients
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