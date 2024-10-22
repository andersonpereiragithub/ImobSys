using System;

namespace ImobSys.Domain.Entities
{
    public class Endereco
    {
        // Atributos principais do endereço
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string Pais { get; set; }

        public string Referencia { get; set; }
        public Coordenadas CoordenadasGeograficas { get; set; }


        public Endereco(string logradouro, string numero, string bairro, string cidade, string uf, string cep, string pais = "Brasil")
        {
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
            CEP = cep;
            Pais = pais;
            Complemento = string.Empty;
            Referencia = string.Empty;
            CoordenadasGeograficas = new Coordenadas();
        }
    }

    public class Coordenadas
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Coordenadas(double latitude = 0.0, double longitude = 0.0)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
