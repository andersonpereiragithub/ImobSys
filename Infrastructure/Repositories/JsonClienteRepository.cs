using ImobSys.Domain.Entities;
using ImobSys.Infrastructure.Repositories;
using Newtonsoft.Json;

namespace ImobSys.Infrastructure.Repositories
{
    public class JsonClienteRepository : IClienteRepository
    {
        private readonly string _filePath;

        public JsonClienteRepository(string? filePath = null)
        {
            if (filePath == null)
            {
                string projectDirectory = Directory.GetCurrentDirectory();
                string dataDirectory = Path.Combine(projectDirectory, "Infrastructure", "Persistence", "Data");

                if (!Directory.Exists(dataDirectory))
                {
                    Directory.Exists(dataDirectory);
                }

                _filePath = Path.Combine(dataDirectory, "cliente.json");
            }
            else
            {
                _filePath = filePath;
            }
        }

        public void SalvarCliente(Cliente cliente)
        {
            var clientes = ListarTodosCliente();
            var existente = clientes.Find(i => i.Id == cliente.Id);

            if(existente != null)
            {
                clientes.Remove(existente);
            }
            
            clientes.Add(cliente);

            File.WriteAllText(_filePath, JsonConvert.SerializeObject(clientes, Formatting.Indented));
        }

        public Cliente BuscarPorIdCliente(Guid id)
        {
            var clientes = ListarTodosCliente();
            var cliente = clientes.Find(cliente => cliente.Id == id);

            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado!");
            }

            return cliente;
        }

        public List<Cliente> ListarTodosCliente()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Cliente>();
            }

            var json = File.ReadAllText(_filePath);
            var clientes = JsonConvert.DeserializeObject<List<Cliente>>(json);

            if(clientes == null)
            {
                return new List<Cliente>();
            }

            return clientes;
        }
     
        public void RemoverCliente(Guid id)
        {
            var clientes = ListarTodosCliente();
            var cliente = clientes.Find(clientes => clientes.Id == id);

            if (cliente != null) { 
                clientes.Remove(cliente);
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(clientes, Formatting.Indented));
            }
        }
    }
}
