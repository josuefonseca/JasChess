using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JasChess.Pecas;
using static JasChess.Movimentos;
using static JasChess.Tabuleiro;

namespace JasChess {
    class Heuristica {
        public static int Avaliacao() {
            int pontuacao = 0;

            for(int lin = 2; lin < 10; lin++)
                for(int col = 2; col < 10; col++)
                    if(TemCoisa(lin, col))
                        pontuacao += ValorDe(tabuleiro[lin, col], lin);


            return pontuacao;
        }

        private static int ValorDe(int peca, int lin) {
            int valor = 0;
            switch(Tipo(peca)) {
                case PEAO:
                    valor = ((Cor(peca) == PRETA && lin >= 5) || (Cor(peca) == BRANCA && lin <= 6)) ? 125 : 100;
                    valor = ((Cor(peca) == PRETA && lin == 9) || (Cor(peca) == BRANCA && lin == 2)) ? 500 : valor;
                    valor += int.Parse(Math.Pow(2, (Cor(peca) == PRETA ? lin - 2 : 9 - lin)).ToString());
                    break;
                case BISPO:
                case CAVALO:
                    valor = TemPar(peca) ? 325 : 300;
                    break;
                case TORRE:
                    valor = TemPar(peca) ? 425 : 400;
                    break;
                case DAMA:
                    valor = TemPar(peca) ? 900 : 500;
                    break;
                case REI:
                    valor = Xeque(Cor(peca)) ? -2000 : 0;
                    break;
            }

            return Cor(peca) == BRANCA ? -valor : valor;
        }
    }
}