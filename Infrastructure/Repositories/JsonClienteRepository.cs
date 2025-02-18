﻿using ImobSys.Domain.Entities.Clientes;
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
            var clientes = ListarTodosClientes();
            var clienteExistente = clientes.FirstOrDefault(c => c.Id == cliente.Id);

            if (clienteExistente != null)
            {
                clientes.Remove(clienteExistente);
            }

            clientes.Add(cliente);

            File.WriteAllText(_filePath, JsonConvert.SerializeObject(clientes, Formatting.Indented));
        }

        public T BuscarClientePorId(Guid id)
        {
            var clientes = ListarTodosClientes();

            return clientes.FirstOrDefault(c => c.Id == id);
        }

        public Guid ObterClientePorNome(string nomeCliente)
        {
            var clientes = ListarTodosClientes();

            var clienteExistente = clientes.FirstOrDefault(c =>
                (c is PessoaFisica pf && pf.Nome == nomeCliente) ||
                (c is PessoaJuridica pj && pj.RazaoSocial == nomeCliente));
            
            if(clienteExistente == null)
            {
                throw new Exception($"Cliente [{nomeCliente}] não encontrado!"); 
            }
            
            return clienteExistente.Id;
        }

        public List<T> ListarTodosClientes()
        {
            var json = File.ReadAllText(_filePath);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ClienteJsonConverter());

            return JsonConvert.DeserializeObject<List<T>>(json, settings) ?? new List<T>();
        }

        bool IClienteRepository<T>.RemoverCliente(Guid id)
        {
            var clientes = ListarTodosClientes();

            var cliente = clientes.FirstOrDefault(c => c.Id == id);

            if (cliente != null)
            {
                clientes.Remove(cliente);
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(clientes, Formatting.Indented));
                return true;
            }
            return false;
        }
    }
}
