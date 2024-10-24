using Microsoft.Extensions.DependencyInjection;
using ImobSys.Infrastructure.Repositories;
using ImobSys.Presentation.ConsoleApp;
using ImobSys.Domain;
using System;

namespace ImobSys
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configura os serviços de DI
            var startup = new Startup();
            var serviceProvider = startup.ConfigureServices();

            var imovelRepository = serviceProvider.GetService<IImovelRepository>();

            var novoImovel = new Imovel
            {
                InscricaoIPTU = "123456",
                TipoImovel = "Residencial",
                DetalhesTipoImovel = "Casa",
                AreaUtil = 120.5f
            };

            if (imovelRepository == null)
            {
                Console.WriteLine("Erro: ImovelRepository não foi resolvido.");
                return;
            }

            imovelRepository.Salvar(novoImovel);

            Console.WriteLine("Imóvel salvo com sucesso!");
        }
    }
}

