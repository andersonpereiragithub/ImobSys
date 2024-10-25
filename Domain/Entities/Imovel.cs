using ImobSys.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ImobSys.Domain
{
    public class Imovel
    {
        public Guid Id { get; set; }

        public string InscricaoIPTU { get; set; }
        public string TipoImovel { get; set; }
        public string DetalhesTipoImovel { get; set; }
        public float AreaUtil { get; set; }

        public Endereco Endereco { get; set; }

        public List<Guid> Proprietarios { get; set; }

        public bool ParaLocacao { get; set; }
        public bool ParaVenda { get; set; }

        public string StatusLocacao { get; set; }

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