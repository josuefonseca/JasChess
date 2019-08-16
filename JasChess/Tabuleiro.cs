using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static JasChess.Program;
using static JasChess.Pecas;

namespace JasChess {
    class Tabuleiro {
        public const int CASA_VAZIA = 0;
        public const int CASA_NULA = -1;

        public static GUI gui = new GUI();

        public static int[,] tabuleiro = new int[12, 12];
        public static PictureBox[] casa = new PictureBox[64];

        public static bool TemCoisa(int lin, int col) {
            return tabuleiro[lin, col] != CASA_VAZIA;
        }
        public static bool ÉValida(int lin, int col) {
            return tabuleiro[lin, col] != CASA_NULA;
        }


        public static void IniciarTabuleiro() {
           
            int i = 0;

            foreach(Control c in gui.Controls) {
                if(c is PictureBox) {
                   casa[i] = c as PictureBox;
                   casa[i].Enabled = true;
                   casa[i++].BackgroundImage = null;
                }
            }
            ResetCor();

            // MOLDURA
            for(int lin = 0; lin < 2; lin++)
                for(int col = 0; col < 12; col++)
                    tabuleiro[lin, col] = CASA_NULA;
            //--------

            // PEÇAS PRETAS (COMPUTADOR)
            {
                tabuleiro[2, 0] = CASA_NULA;
                tabuleiro[2, 1] = CASA_NULA;
                tabuleiro[2, 2] = TORRE_PRETA;      casa[0].BackgroundImage = peca[PRETA, TORRE - 1];
                tabuleiro[2, 3] = CAVALO_PRETO;     casa[1].BackgroundImage = peca[PRETA, CAVALO - 1];
                tabuleiro[2, 4] = BISPO_PRETO;      casa[2].BackgroundImage = peca[PRETA, BISPO - 1];
                tabuleiro[2, 5] = DAMA_PRETA;       casa[3].BackgroundImage = peca[PRETA, DAMA - 1];
                tabuleiro[2, 6] = REI_PRETO;        casa[4].BackgroundImage = peca[PRETA, REI - 1];
                tabuleiro[2, 7] = BISPO_PRETO;      casa[5].BackgroundImage = peca[PRETA, BISPO - 1];
                tabuleiro[2, 8] = CAVALO_PRETO;     casa[6].BackgroundImage = peca[PRETA, CAVALO - 1];
                tabuleiro[2, 9] = TORRE_PRETA;      casa[7].BackgroundImage = peca[PRETA, TORRE - 1];
                tabuleiro[2, 10] = CASA_NULA;
                tabuleiro[2, 11] = CASA_NULA;

                tabuleiro[3, 0] = CASA_NULA;
                tabuleiro[3, 1] = CASA_NULA;
                for(int col = 2; col < 10; tabuleiro[3, col] = PEAO_PRETO, casa[col + 6].BackgroundImage = peca[PRETA, PEAO - 1], col++) ;
                tabuleiro[3, 10] = CASA_NULA;
                tabuleiro[3, 11] = CASA_NULA;

                rei[1] = 30;
            }
            //--------

            // MEIO DO TABULEIRO
            for(int lin = 4; lin < 8; lin++) {
                for(int col = 0; col < 2; tabuleiro[lin, col] = CASA_NULA, col++) ;
                for(int col = 2; col < 10; tabuleiro[lin, col] = CASA_VAZIA, col++) ;
                for(int col = 10; col < 12; tabuleiro[lin, col] = CASA_NULA, col++) ; 
            }
            //--------

            // PEÇAS BRANCAS (JOGADOR HUMANO)
            {
                tabuleiro[8, 0] = CASA_NULA;
                tabuleiro[8, 1] = CASA_NULA;
                for(int col = 2; col < 10; tabuleiro[8, col] = PEAO_BRANCO, casa[col + 46].BackgroundImage = peca[BRANCA, PEAO - 1], col++) ;
                tabuleiro[8, 10] = CASA_NULA;
                tabuleiro[8, 11] = CASA_NULA;

                tabuleiro[9, 0] = CASA_NULA;
                tabuleiro[9, 1] = CASA_NULA;
                tabuleiro[9, 2] = TORRE_BRANCA;     casa[56].BackgroundImage = peca[BRANCA, TORRE - 1];
                tabuleiro[9, 3] = CAVALO_BRANCO;    casa[57].BackgroundImage = peca[BRANCA, CAVALO - 1];
                tabuleiro[9, 4] = BISPO_BRANCO;     casa[58].BackgroundImage = peca[BRANCA, BISPO - 1];
                tabuleiro[9, 5] = DAMA_BRANCA;      casa[59].BackgroundImage = peca[BRANCA, DAMA - 1];
                tabuleiro[9, 6] = REI_BRANCO;       casa[60].BackgroundImage = peca[BRANCA, REI - 1];
                tabuleiro[9, 7] = BISPO_BRANCO;     casa[61].BackgroundImage = peca[BRANCA, BISPO - 1];
                tabuleiro[9, 8] = CAVALO_BRANCO;    casa[62].BackgroundImage = peca[BRANCA, CAVALO - 1];
                tabuleiro[9, 9] = TORRE_BRANCA;     casa[63].BackgroundImage = peca[BRANCA, TORRE - 1];
                tabuleiro[9, 10] = CASA_NULA; 
                tabuleiro[9, 11] = CASA_NULA; 
                rei[0] = 114;
            }
            
            // MOLDURA
            for(int lin = 10; lin < 12; lin++)
                for(int col = 0; col < 12; col++)
                    tabuleiro[lin, col] = CASA_NULA;
            //--------
        }

        public static void ResetCor() {
            for(int i = 0; i < 64; i++) 
                casa[i].BackColor = (i % 8 % 2 == 1) && (i / 8 % 2 == 0) ||
                                    (i % 8 % 2 == 0) && (i / 8 % 2 == 1)  ?
                                    System.Drawing.Color.SaddleBrown : System.Drawing.Color.SandyBrown;
            
        }
    }
}