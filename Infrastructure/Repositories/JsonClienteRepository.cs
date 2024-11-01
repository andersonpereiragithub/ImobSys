using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;
using Newtonsoft.Json;

namespace ImobSys.Infrastructure.Repositories
{
    public class JsonClienteRepository<T> : IClienteRepository<T> where T : Cliente
    {
        private readonly string _filePath;

        public JsonClienteRepository(string filePath)
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public void SalvarCliente(T cliente)
        {
            var clientes = ListarTodosCliente();
            var clienteExistente = clientes.FirstOrDefault(c => c.Id == cliente.Id);

            if (clienteExistente != null)
            {
                clientes.Remove(clienteExistente);
            }

            clientes.Add(cliente);

            File.WriteAllText(_filePath, JsonConvert.SerializeObject(clientes, Formatting.Indented));
        }

        public T BuscarPorIdCliente(Guid id)
        {
            var clientes = ListarTodosCliente();

            return clientes.FirstOrDefault(c => c.Id == id);
        }

        public T BuscarPorNomeCliente(string nomeCliente)
        {
            var cliente = ListarTodosCliente();
            
            if (cliente == null)
            {
                return null;
            }

            return ListarTodosCliente().FirstOrDefault(cliente => cliente.Nome == nomeCliente); ;
        }

        public List<T> ListarTodosCliente()
        {
            var json = File.ReadAllText(_filePath);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ClienteJsonConverter());

            return JsonConvert.DeserializeObject<List<T>>(json, settings) ?? new List<T>();
        }

        bool IClienteRepository<T>.RemoverCliente(Guid id)
        {
            var clientes = ListarTodosCliente();

            var cliente = clientes.FirstOrDefault(c => c.Id == id);

            if (cliente != null)
            {
                clientes.Remove(cliente);
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(clientes, Formatting.Indented));
                return true;//VERIFICAR
            }
            return false;
        }
    }
}
