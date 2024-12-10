using ImobSys.Application.Services.Interfaces;
using ImobSys.Domain;
using ImobSys.Domain.Entities;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Enums;
using ImobSys.Domain.Interfaces;
using ImobSys.Presentation.Handler;

namespace ImobSys.Application.Services
{
    public class ImovelService : IImovelService
    {
        private readonly UserInteractionHandler _userInteractionHandler;
        private readonly IImovelRepository _imovelRepository;
        private readonly IClienteService _clienteService;

        public ImovelService(UserInteractionHandler interactionHandler, IImovelRepository imovelRepository, IClienteService clienteService)
        {
            _userInteractionHandler = interactionHandler;
            _imovelRepository = imovelRepository;
            _clienteService = clienteService;
        }

        public void CadastrarNovoImovel()
        {
            _userInteractionHandler.ExibirMensagem("==== Cadastro de Novo Imóvel ====", ConsoleColor.Cyan);

            var novoImovel = new Imovel { Id = Guid.NewGuid() };

            novoImovel.TipoImovel = _userInteractionHandler.ConfigurarTipoImovel();
            novoImovel.AreaUtil = _userInteractionHandler.ObterAreaUtil();

            novoImovel.InscricaoIPTU = _userInteractionHandler.ObterInscricaoIPTU();
            novoImovel.DetalhesTipoImovel = _userInteractionHandler.ObterTipoImovel();

            _userInteractionHandler.ConfigurarLocacaoEVenda(novoImovel);

            novoImovel.Endereco = _userInteractionHandler.ObterEndereco();

            _userInteractionHandler.ConfigurarCaracteristicasInternas(novoImovel);

            _clienteService.AtribuirProprietarios(novoImovel);

            _imovelRepository.SalvarImovel(novoImovel);
            Console.Clear();
            _userInteractionHandler.ExibirSucesso("Imóvel cadastrado com sucesso!");

            _userInteractionHandler.ExibirMensagem("Pressione qualquer tecla para retornar ao Menu.");
            Console.ReadKey();
        }

        public List<Imovel> ListarTodosImoveis()
        {
            return _imovelRepository.ListarTodosImovel();
        }
    }
}
