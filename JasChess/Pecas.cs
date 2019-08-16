using System.Drawing;
using static JasChess.Properties.Resources;
using static JasChess.Tabuleiro;

namespace JasChess {

    public class Pecas {

        public const int PEAO_BRANCO = 10;
        public const int BISPO_BRANCO = 20;
        public const int CAVALO_BRANCO = 30;
        public const int TORRE_BRANCA = 40;
        public const int DAMA_BRANCA = 50;
        public const int REI_BRANCO = 60;

        public const int PEAO_PRETO = 11;
        public const int BISPO_PRETO = 21;
        public const int CAVALO_PRETO = 31;
        public const int TORRE_PRETA = 41;
        public const int DAMA_PRETA = 51;
        public const int REI_PRETO = 61;

        public const int PEAO = 1;
        public const int BISPO = 2;
        public const int CAVALO = 3;
        public const int TORRE = 4;
        public const int DAMA = 5;
        public const int REI = 6;

        public const int BRANCA = 0;
        public const int PRETA = 1;

        public static int[] rei = new int[2];

        public static Image[,] peca = { { _10, _20, _30, _40, _50, _60 }, { _11, _21, _31, _41, _51, _61 } };

        public static int[,] ATAQUES_PEAO = new int[68, 6];
        public static int[,] ATAQUES_BISPO = new int[92, 28];
        public static int[,] ATAQUES_CAVALO = new int[92, 8];
        public static int[,] ATAQUES_TORRE = new int[92, 28];


        public static int Cor(int peca) {
            return peca % 10;
        }
        public static int Cor(int lin, int col) {
            return tabuleiro[lin, col] % 10;
        }

        public static int Tipo(int lin, int col) {
            return tabuleiro[lin, col] / 10;
        }
        public static int Tipo(int peca) {
            return peca / 10;
        }

        public static int Quantidade() {
            int q = 0;
            for(int i = 2; i < 9; i++) 
                for(int j = 2; j < 9; j++) 
                    if(TemCoisa(i, j)) q++;

            return q;
        }
        public static int Quantidade(int cor) {
            int q = 0;
            for(int i = 2; i < 9; i++)
                for(int j = 2; j < 9; j++)
                    if(TemCoisa(i, j) && Cor(i,j) == cor) q++;

            return q;
        }

        public static bool TemPar(int peca) {
            bool achou = false;
            for(int i = 2; i < 10; i++) 
                for(int j = 2; j < 10; j++) 
                    if(tabuleiro[i,j] == peca) 
                        if(achou) return true; 
                        else achou = true;

            return false;
        }

        public static string Nome(int pos) {
            pos = (pos / 8+2) * 12 + (pos % 8+2);
            string[] tipo = { "Peão", "Bispo", "Cavalo", "Torre", "Rainha", "Rei" };
            string[] cor = { "branco", "preto", "branca", "preta"};

            if(TemCoisa(pos / 12, pos % 12) && ÉValida(pos / 12, pos % 12)) {
                int a = Tipo(pos / 12, pos % 12) - 1;
                int b = a == TORRE-1 || a == DAMA -1? Cor(pos / 12, pos % 12) + 2 : Cor(pos / 12, pos % 12);
                return tipo[a] + " " + cor[b];
            } else
                return null;
        }

        public static void Promover(int jog) {
            int lin = 7 * jog + 2;
            for(int col = 2; col < 10; col++) {
                if(Tipo(lin, col) == PEAO) {
                    if(!Jogo.humano && Cor(lin,col) == PRETA) {
                        tabuleiro[lin, col] = DAMA_PRETA;
                        casa[(lin - 2) * 8 + col - 2].BackgroundImage = peca[PRETA, DAMA - 1];
                    } else {
                        Promocao p = new Promocao();
                        p.ShowDialog();
                        tabuleiro[lin, col] = (p.nova + 1) * 10 + jog;
                        casa[(lin - 2) * 8 + col - 2].BackgroundImage = peca[jog, p.nova];
                        
                    }
                    if(GUI.temSom) Jogo.sons[1].Play();
                }
            }
        }

        public static void AtribuirValores() {
            int lin, col;
            int[] bispo = { -1, 1 };
            int[,] cavalo = { { -1, 1 }, { 2, -2 } };
            int[] torreA = { -1, 0, 1, 0 };
            int[] torreB = { 0, 1, 0, -1 };

            for(int cont = 0, x = 26, y = 0; cont < 92; cont++, x++, y = 0) {
                // CAVALO
                for(int i = 0; i <= 1; i++)
                    for(int j = 0; j <= 1; j++)
                        for(int k = 0; k <= 1; k++) {
                            lin = cavalo[i, j] + x / 12;
                            col = cavalo[1 - i, k] + x % 12;
                            ATAQUES_CAVALO[cont, y++] = lin * 12 + col;
                        }

                y = 0;
                // TORRE
                for(int i = 1; i < 8; i++)
                    for(int j = 0; j < 4; j++, y++) {
                        lin = (i * torreA[j]) + x / 12;
                        col = (i * torreB[j]) + x % 12;
                        if((lin * 12 + col > -1) && (lin * 12 + col < 144))
                            ATAQUES_TORRE[cont, y] = lin * 12 + col;
                    }

                y = 0;
                // BISPO
                for(int k = 1; k < 8; k++)
                    for(int i = 0; i <= 1; i++)
                        for(int j = 0; j <= 1; j++, y++) {
                            lin = (k * bispo[i]) + x / 12;
                            col = (k * bispo[j]) + x % 12;
                            if((lin * 12 + col > -1) && (lin * 12 + col < 144))
                                ATAQUES_BISPO[cont, y] = lin * 12 + col;
                        }
            }

            // PEÃO
            for(int cont = 0, x = 38, y = 0; cont < 68; cont++, x++, y = 0)
                for(int i = -1; i < 2; i += 2)
                    for(int j = -1; j < 2; j++, y++) {
                        lin = i + x / 12;
                        col = j + x % 12;
                        ATAQUES_PEAO[cont, y] = lin * 12 + col;
                    }

        }

    }
}