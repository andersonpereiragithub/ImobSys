using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImobSys.Presentation.Handler
{
    public class MenuStateManager
    {
        private static MenuStateManager _instance;
        public static MenuStateManager Instance => _instance ??= new MenuStateManager();

        public bool MenuAtivo { get; private set; }
        public int UltimaLinhaMenu { get; private set; }

        private MenuStateManager() { }

        public void AtivarMenu(int ultimaLinhaMenu, int linhasAdicionais)
        {
            MenuAtivo = true;
            UltimaLinhaMenu = ultimaLinhaMenu + linhasAdicionais;
        }

        public void DesativarMenu()
        {
            MenuAtivo = false;
            UltimaLinhaMenu = 0;
        }

        public int ObterProximaLinha()
        {
            return MenuAtivo ? UltimaLinhaMenu + 1 : Console.CursorTop + 1;
        }
    }

}
