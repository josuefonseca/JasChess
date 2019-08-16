using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static JasChess.Tabuleiro;

namespace JasChess {
    public static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        

        [STAThread]
        public static void Main() {
            Application.EnableVisualStyles();
            Pecas.AtribuirValores();
            IniciarTabuleiro();
            Application.Run(gui);
        }
    }
}