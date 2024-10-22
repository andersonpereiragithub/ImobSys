using System;
using ImobSys.Domain.Entities.Enums;

namespace ImobSys.Domain.Entities
{
    public class Imovel
    {
        public string InscricaoIPTU { get; set; }
        public Endereco Endereco { get; set; }
        public float AreaUtil { get; set; }

        public TipoImovel TipoImovel { get; set; }  // Comercial, Residencial, Misto
        public SubtipoImovel SubtipoImovel { get; set; }  // Ex.: Casa, Sala, etc.

        public FinalidadeImovel Finalidade { get; set; }
        public decimal? ValorLocacao { get; set; }
        public decimal? ValorVenda { get; set; }

        public bool EstaAlugado { get; set; }  // Indica se está ou não alugado

        public Condominio? Condominio { get; set; }
        public string? MatriculaCESAMA { get; set; }  // Para imóveis sem condomínio

        public List<int> ProprietariosIds { get; set; }  // Relacionamento com proprietários (IDs)

        public int Quartos { get; set; }
        public int Salas { get; set; }
        public int Banheiros { get; set; }
        public int Garagens { get; set; }
        public bool TemCozinha { get; set; }
        public bool TemCopa { get; set; }
        public bool TemQuintal { get; set; }

        public Imovel()
        {
            ProprietariosIds = new List<int>();
        }
    }
}
