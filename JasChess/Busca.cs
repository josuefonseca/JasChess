using System;
using static JasChess.Jogo;
using static JasChess.Movimentos;
using static JasChess.Pecas;
using static JasChess.Tabuleiro;
using static JasChess.Heuristica;

namespace JasChess {
    class Busca {
        static int topo = 5;

        static int maxlino, maxcolo, maxlind, maxcold;
        public static double x = 0;

        private static void Pensar() {
            Pensando p = new Pensando();
            p.ShowDialog();
        }

        public static void ComputadorJoga() {
            x = 0;
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(Pensar));
            t.Start();

            int[,] tabMem = new int[12, 12];
            for(int i = 0; i < 12; i++)
                for(int j = 0; j < 12; j++)
                    tabMem[i, j] = tabuleiro[i, j];


            int[] memRei = new int[2];
            memRei[0] = rei[0];
            memRei[1] = rei[1];

            Buscar(PRETA, topo, int.MinValue, int.MaxValue);
            jogador = PRETA;

            for(int i = 0; i < 12; i++)
                for(int j = 0; j < 12; j++)
                    tabuleiro[i, j] = tabMem[i, j];

            rei[0] = memRei[0];
            rei[1] = memRei[1];

            FacaMovimento(maxlino, maxcolo, maxlind, maxcold, true);
            Promover(jogador);
            t.Abort();
            gui.tsTempo.Text = "Tempo de resposta: " + x + " s";

        }

        static int Buscar(int jogador, int nivel, int alpha, int beta) {
            int mv;
            int mmlo, mmco, mmld, mmcd, vencAux = 9, sitAux = 3;

            int[,] tabSim = new int[12, 12];
            for(int i = 0; i < 12; i++)
                for(int j = 0; j < 12; j++)
                    tabSim[i, j] = tabuleiro[i, j];

            int[] reiSim = new int[2];
            reiSim[0] = rei[0];
            reiSim[1] = rei[1];

            if(nivel == 0 || FimJogo(ref jogador, ref sitAux, ref vencAux)) {
                for(int i = 0; i < 12; i++)
                    for(int j = 0; j < 12; j++)
                        tabuleiro[i, j] = tabSim[i, j];

                int AvRel = 0;

                switch(sitAux) {
                    case XEQUE:
                        AvRel = -2000;
                        break;
                    case XEQUE_MATE:
                        if(vencAux == jogador)
                            AvRel = 9999999;
                        else
                            AvRel = -9999999;
                        break;
                    case EMPATE:
                        break;
                    default:
                        if(Quantidade(jogador) > Quantidade(1 - jogador))
                            AvRel = 200;
                        break;
                }

                return Avaliacao() + AvRel;

            }

            mmlo = mmco = mmld = mmcd = 0;

            mv = (jogador == BRANCA) ? int.MaxValue : int.MinValue;

            for(int lin = 2; lin < 10; lin++)
                for(int col = 2; col < 10; col++)
                    if(TemCoisa(lin, col) && Cor(lin, col) == jogador)
                        switch(Tipo(lin, col)) {

                            case PEAO:
                                for(int i = 0; i < 3; i++) {
                                    int destino = ATAQUES_PEAO[lin * 12 + col - 38, 3 * jogador + i];
                                    int nil = destino / 12;
                                    int loc = destino % 12;

                                    verif:
                                    if(MoverPeao(lin * 12 + col, nil * 12 + loc, jogador)) {
                                        int mem = tabuleiro[nil, loc];
                                        FacaMovimento(lin, col, nil, loc, false);
                                        if(nil == 2 || nil == 9) tabuleiro[nil, loc] = 50 + jogador;
                                        mv = (jogador == BRANCA) ?
                                            Math.Min(mv, Buscar(1 - jogador, nivel - 1, alpha, beta)) :
                                            Math.Max(mv, Buscar(1 - jogador, nivel - 1, alpha, beta));

                                        if(jogador == PRETA) {
                                            if(mv > alpha) {
                                                alpha = mv;
                                                mmlo = lin;
                                                mmco = col;
                                                mmld = nil;
                                                mmcd = loc;
                                            }
                                        }
                                        else {
                                            if(mv < beta) {
                                                beta = mv;
                                                mmlo = lin;
                                                mmco = col;
                                                mmld = nil;
                                                mmcd = loc;
                                            }
                                        }

                                        if(beta <= alpha) goto saida;
                                        tabuleiro[nil, loc] = 10 + jogador;
                                        tabuleiro[lin, col] = tabuleiro[nil, loc];
                                        tabuleiro[nil, loc] = mem;

                                        if((lin == 2 && Cor(lin, col) == PRETA) || (lin == 9 && Cor(lin, col) == BRANCA)) {
                                            destino = lin * 12 + col + (24 * (2*jogador-1));
                                            goto verif;
                                        }
                                    }
                                }

                                break;

                            case BISPO:
                                for(int i = 0; i < 28; i++) {
                                    int destino = ATAQUES_BISPO[lin * 12 + col - 26, i];
                                    int nil = destino / 12;
                                    int loc = destino % 12;

                                    if(MoverBispo(lin * 12 + col, destino, jogador)) {
                                        int mem = tabuleiro[nil, loc];
                                        FacaMovimento(lin, col, nil, loc, false);
                                        mv = (jogador == BRANCA) ?
                                            Math.Min(mv, Buscar(1 - jogador, nivel - 1, alpha, beta)) :
                                            Math.Max(mv, Buscar(1 - jogador, nivel - 1, alpha, beta));

                                        if(jogador == PRETA) {
                                            if(mv > alpha) {
                                                alpha = mv;
                                                mmlo = lin;
                                                mmco = col;
                                                mmld = nil;
                                                mmcd = loc;
                                            }
                                        }
                                        else {
                                            if(mv < beta) {
                                                beta = mv;
                                                mmlo = lin;
                                                mmco = col;
                                                mmld = nil;
                                                mmcd = loc;
                                            }
                                        }

                                        if(beta <= alpha) goto saida;
                                        tabuleiro[lin, col] = tabuleiro[nil, loc];
                                        tabuleiro[nil, loc] = mem;
                                    }
                                }
                                break;

                            case CAVALO:
                                for(int i = 0; i < 8; i++) {
                                    int destino = ATAQUES_CAVALO[lin * 12 + col - 26, i];
                                    int nil = destino / 12;
                                    int loc = destino % 12;

                                    if(MoverCavalo(lin * 12 + col, destino, jogador)) {
                                        int mem = tabuleiro[nil, loc];
                                        FacaMovimento(lin, col, nil, loc, false);
                                        mv = (jogador == BRANCA) ?
                                            Math.Min(mv, Buscar(1 - jogador, nivel - 1, alpha, beta)) :
                                            Math.Max(mv, Buscar(1 - jogador, nivel - 1, alpha, beta));

                                        if(jogador == PRETA) {
                                            if(mv > alpha) {
                                                alpha = mv;
                                                mmlo = lin;
                                                mmco = col;
                                                mmld = nil;
                                                mmcd = loc;
                                            }
                                        }
                                        else {
                                            if(mv < beta) {
                                                beta = mv;
                                                mmlo = lin;
                                                mmco = col;
                                                mmld = nil;
                                                mmcd = loc;
                                            }
                                        }

                                        if(beta <= alpha) goto saida;
                                        tabuleiro[lin, col] = tabuleiro[nil, loc];
                                        tabuleiro[nil, loc] = mem;
                                    }
                                }
                                break;

                            case TORRE:
                                for(int i = 0; i < 28; i++) {
                                    int destino = ATAQUES_TORRE[lin * 12 + col - 26, i];
                                    int nil = destino / 12;
                                    int loc = destino % 12;

                                    if(MoverTorre(lin * 12 + col, destino, jogador)) {
                                        int mem = tabuleiro[nil, loc];
                                        FacaMovimento(lin, col, nil, loc, false);
                                        mv = (jogador == BRANCA) ?
                                            Math.Min(mv, Buscar(1 - jogador, nivel - 1, alpha, beta)) :
                                            Math.Max(mv, Buscar(1 - jogador, nivel - 1, alpha, beta));

                                        if(jogador == PRETA) {
                                            if(mv > alpha) {
                                                alpha = mv;
                                                mmlo = lin;
                                                mmco = col;
                                                mmld = nil;
                                                mmcd = loc;
                                            }
                                        }
                                        else {
                                            if(mv < beta) {
                                                beta = mv;
                                                mmlo = lin;
                                                mmco = col;
                                                mmld = nil;
                                                mmcd = loc;
                                            }
                                        }

                                        if(beta <= alpha) goto saida;
                                        tabuleiro[lin, col] = tabuleiro[nil, loc];
                                        tabuleiro[nil, loc] = mem;
                                    }
                                }
                                break;

                            case DAMA:
                                for(int i = 0; i < 64; i++) {
                                    int nil = i / 8 + 2;
                                    int loc = i % 8 + 2;
                                    if(MoverDama(lin * 12 + col, nil * 12 + loc, jogador)) {
                                        int mem = tabuleiro[nil, loc];
                                        FacaMovimento(lin, col, nil, loc, false);
                                        mv = (jogador == BRANCA) ?
                                            Math.Min(mv, Buscar(1 - jogador, nivel - 1, alpha, beta)) :
                                            Math.Max(mv, Buscar(1 - jogador, nivel - 1, alpha, beta));

                                        if(jogador == PRETA) {
                                            if(mv > alpha) {
                                                alpha = mv;
                                                mmlo = lin;
                                                mmco = col;
                                                mmld = nil;
                                                mmcd = loc;
                                            }
                                        }
                                        else {
                                            if(mv < beta) {
                                                beta = mv;
                                                mmlo = lin;
                                                mmco = col;
                                                mmld = nil;
                                                mmcd = loc;
                                            }
                                        }

                                        if(beta <= alpha) goto saida;
                                        tabuleiro[lin, col] = tabuleiro[nil, loc];
                                        tabuleiro[nil, loc] = mem;
                                    }
                                }
                                break;

                            case REI:
                                for(int i = -1; i < 2; i++)
                                    for(int j = -1; j < 2; j++) {
                                        int destino = (lin + i) * 12 + col + j;
                                        int nil = destino / 12;
                                        int loc = destino % 12;
                                        if(MoverRei(lin * 12 + col, destino, jogador)) {
                                            int mem = tabuleiro[nil, loc];
                                            FacaMovimento(lin, col, nil, loc, false);
                                            mv = (jogador == BRANCA) ?
                                                Math.Min(mv, Buscar(1 - jogador, nivel - 1, alpha, beta)) :
                                                Math.Max(mv, Buscar(1 - jogador, nivel - 1, alpha, beta));

                                            if(jogador == PRETA) {
                                                if(mv > alpha) {
                                                    alpha = mv;
                                                    mmlo = lin;
                                                    mmco = col;
                                                    mmld = nil;
                                                    mmcd = loc;
                                                }
                                            }
                                            else {
                                                if(mv < beta) {
                                                    beta = mv;
                                                    mmlo = lin;
                                                    mmco = col;
                                                    mmld = nil;
                                                    mmcd = loc;
                                                }
                                            }

                                            if(beta <= alpha) goto saida;
                                            rei[jogador] = lin * 12 + col;
                                            tabuleiro[lin, col] = tabuleiro[nil, loc];
                                            tabuleiro[nil, loc] = mem;
                                        }

                                    }
                                break;
                        }

            saida:
            if(nivel == topo) {
                maxlino = mmlo;
                maxcolo = mmco;
                maxlind = mmld;
                maxcold = mmcd;
            }

            for(int i = 0; i < 12; i++)
                for(int j = 0; j < 12; j++)
                    tabuleiro[i, j] = tabSim[i, j];

            rei[0] = reiSim[0];
            rei[1] = reiSim[1];
            return mv;


        }
    }
}