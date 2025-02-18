﻿using ImobSys.Domain;
using ImobSys.Domain.Interfaces;
using Newtonsoft.Json;

namespace ImobSys.Infrastructure.Repositories
{
    public class JsonImovelRepository : IImovelRepository
    {
        private readonly string _filePath;

        public JsonImovelRepository(string? filePath = null)
        {
            if (filePath == null)
            {

                string projectDirectory = Directory.GetCurrentDirectory();

                string dataDirectory = Path.Combine(projectDirectory, "Infrastructure", "Persistence", "Data");

                if (!Directory.Exists(dataDirectory))
                {
                    Directory.CreateDirectory(dataDirectory);
                }

                _filePath = Path.Combine(dataDirectory, "imoveis.json");
            }
            else
            {
                _filePath = filePath;
            }
        }

        public void SalvarImovel(Imovel imovel)
        {
            var imoveis = ListarTodosImovel();
            var existente = imoveis.Find(i => i.Id == imovel.Id);

            if (existente != null)
            {
                imoveis.Remove(existente);
            }

            imoveis.Add(imovel);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            File.WriteAllText(_filePath, JsonConvert.SerializeObject(imoveis, Formatting.Indented));
        }

        public Imovel BuscarPorIdImovel(Guid id)
        {
            var imoveis = ListarTodosImovel();
            var imovel = imoveis.Find(imovel => imovel.Id == id);

            if (imovel == null)
            {
                throw new Exception("Imóvel não encontrado!");
            }

            return imovel;
        }
        public Imovel BuscarPorInscricaoIPTU(string inscricaoIPTU)
        {
            var imoveis = ListarTodosImovel();
            var imovel = imoveis.Find(imovel => imovel.InscricaoIPTU == inscricaoIPTU);

            if (imovel == null)
            {
                throw new Exception("Imóvel não encontrado!");
            }

            return imovel;
        }

        public List<Imovel> ListarTodosImovel()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Imovel>();
            }

            var json = File.ReadAllText(_filePath);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            var imoveis = JsonConvert.DeserializeObject<List<Imovel>>(json);

            if (imoveis == null)
            {
                return new List<Imovel>();
            }

            return imoveis;
        }

        public void RemoverImovel(Guid id)
        {
            var imoveis = ListarTodosImovel();
            var imovel = imoveis.Find(i => i.Id == id);

            if (imovel != null)
            {
                imoveis.Remove(imovel);
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(imoveis, Formatting.Indented));
            }
        }

        public bool ClientePossuiImovel(Guid clienteId)
        {
            var imoveis = ListarTodosImovel();
            return imoveis.Any(imovel => imovel.Proprietarios.Contains(clienteId));
        }

        public List<Imovel> ObterImoveisPorCliente(Guid clienteId)
        {
            var imoveis = ListarTodosImovel();

            return imoveis.Where(imovel => imovel.Proprietarios.Contains(clienteId)).ToList();
        }
    }
}
