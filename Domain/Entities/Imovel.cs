using ImobSys.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ImobSys.Domain
{
    public class Imovel
    {
        public Guid Id { get; private set; }

        public string InscricaoIPTU { get; set; }
        public string TipoImovel { get; set; }  // Comercial, Residencial, Misto
        public string DetalhesTipoImovel { get; set; }  // Casa, Sala, Loja, etc.
        public float AreaUtil { get; set; }

        public Endereco Endereco { get; set; }  // Classe Endereco separada

        public List<Guid> Proprietarios { get; set; }

        public bool ParaLocacao { get; set; }  // Se está disponível para locação
        public bool ParaVenda { get; set; }     // Se está disponível para venda

        public string StatusLocacao { get; set; }  // Disponível ou Alugado

        public decimal? ValorAluguel { get; set; }
        public decimal? ValorVenda { get; set; }

        public string NomeCondominio { get; set; }
        public decimal? ValorCondominio { get; set; }

        public int Quartos { get; set; }
        public int Salas { get; set; }
        public int Banheiros { get; set; }
        public int Garagens { get; set; }
        public bool Cozinha { get; set; }
        public bool Copa { get; set; }
        public bool Quintal { get; set; }

        public Imovel()
        {
            Id = Guid.NewGuid();
            Proprietarios = new List<Guid>();
        }
    }
}