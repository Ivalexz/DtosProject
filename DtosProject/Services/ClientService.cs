using DtosProject.DTOs;
using DtosProject.Models;
 
namespace DtosProject.Services
{
    public class ClientService
    {
        private readonly List<Client> _clients = new List<Client>();
        public void AddClient(CreateClientDto client)
        {
            var obj = new Client
            {
                Id = _clients.Count + 1,
                FullName = client.FullName,
                Email = client.Email,
                Age = 0,
                CreatedAt = DateTime.Now
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

        public ClientDetailsDto  GetClientById(int id)
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
            searchText = searchText.ToLower();
            return _clients
                .Where(c => c.FullName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                            c.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .Select(c => new ClientListItemDto
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    Email = c.Email
                }).ToList();
        }
    }
}