namespace ImobSys.Presentation.Handler
{
    public class MenuStateManager
    {
        private static MenuStateManager _instance;
        public static MenuStateManager Instance => _instance ??= new MenuStateManager();

        private const int LinhasMenuPadrao = 6;

        public bool MenuAtivo { get; private set; }
        public int UltimaLinhaMenu { get; private set; }
        public int LinhasMenu { get; private set; }

        private MenuStateManager()
        {
            LinhasMenu = LinhasMenuPadrao;
        }

        public void AtivarMenu(int ultimaLinhaMenu)
        {
            MenuAtivo = true;

            UltimaLinhaMenu = ultimaLinhaMenu + LinhasMenu;
        }

        public void EscreverAbaixoDoMenu(string mensagem)
        {
            Console.SetCursorPosition(0, ObterProximaLinha());
            Console.WriteLine(mensagem);
        }

        public void DesativarMenu()
        {
            MenuAtivo = false;

            UltimaLinhaMenu = 0;
        }

        public int ObterProximaLinha()
        {
            return MenuAtivo ? UltimaLinhaMenu : Console.CursorTop++;
        }

        public void DefinirLinhasMenu(int linhas)
        {
            LinhasMenu = linhas > 0 ? linhas : LinhasMenuPadrao;
        }
    }
}
