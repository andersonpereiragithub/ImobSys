using ImobSys.Domain.Entities;
using ImobSys.Domain.Entities.ImobSys.Domain;
using System;

namespace ImobSys.Domain.Entities
{
    public class Endereco
    {
        public string TipoLogradouro {  get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string Pais { get; set; }

        public string Referencia { get; set; }
        public CoordenadasGeograficas? Coordenadas { get; set; }


        public Endereco(string tipoLogradouro, string logradouro, string numero, string bairro, string cidade, string uf, string cep, string pais = "Brasil")
        {
            TipoLogradouro = tipoLogradouro;
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
            CEP = cep;
            Pais = pais;
            Complemento = string.Empty;
            Referencia = string.Empty;
        }

        public Endereco() {
            Coordenadas = new CoordenadasGeograficas();
        }
    }
}
