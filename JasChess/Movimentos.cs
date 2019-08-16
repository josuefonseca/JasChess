using static JasChess.Pecas;
using static JasChess.Tabuleiro;

namespace JasChess {
    class Movimentos {

        public static void FacaMovimento(int lin_o, int col_o, int lin_d, int col_d, bool real) {

            if(Tipo(lin_o, col_o) == REI)
                rei[Jogo.jogador] = lin_d * 12 + col_d;

            tabuleiro[lin_d, col_d] = tabuleiro[lin_o, col_o];
            tabuleiro[lin_o, col_o] = CASA_VAZIA;
            
            if(real) {
                ResetCor();
                casa[(lin_o - 2) * 8 + col_o - 2].BackColor = GUI.antO;
                casa[(lin_d - 2) * 8 + col_d - 2].BackColor = GUI.antD;

                casa[(lin_d - 2) * 8 + col_d - 2].BackgroundImage = casa[(lin_o - 2) * 8 + col_o - 2].BackgroundImage;
                casa[(lin_o - 2) * 8 + col_o - 2].BackgroundImage = null;

                if(GUI.temSom) Jogo.sons[0].Play();

            }
        }


        public static bool PeaoAtaca(int pos_o, int pos_d, int jogador) {
            // VERIFICA SE O PEÃO DE jogador SE MOVE DE pos_o PARA pos_d

            // *j <- -1, PARA O JOGADOR 0}
            // *j <-  1, PARA O JOGADOR 1}

            for(int i = 0, j = (2 * jogador - 1); i < 6; i++)
                if(pos_d == ATAQUES_PEAO[pos_o - 38, i])
                    return (pos_o / 12 + j == pos_d / 12);

            return false;
        }
        public static bool BispoAtaca(int pos_o, int pos_d, int max) {
            int casa;
            max = 4 * max - 1;

            // VERIFICA SE O "BISPO" SE MOVE DE pos_o PARA pos_d
            for(int i = 0; i <= max; i++) {
                if(pos_d == ATAQUES_BISPO[pos_o - 26, i]) {
                    // VERIFICA SE HÁ ALGUMA PEÇA NO CAMINHO OU SE CHEGOU NO FIM DO TABULEIRO
                    for(int j = i % 4; j < i; j += 4) {
                        casa = ATAQUES_BISPO[pos_o - 26, j];
                        if(TemCoisa(casa / 12, casa % 12))
                            return false;
                    }
                    return true;
                }
            }
            return false;
        }
        public static bool CavaloAtaca(int pos_o, int pos_d) {
            for(int i = 0; i < 8; i++)
                if(pos_d == ATAQUES_CAVALO[pos_o - 26, i])
                    return true;

            return false;
        }
        public static bool TorreAtaca(int pos_o, int pos_d, int max) {
            int casa;
            max = 4 * max;

            // VERIFICA SE A "TORRE" SE MOVE DE pos_o PARA pos_d
            for(int i = 0; i < max; i++) {
                if(pos_d == ATAQUES_TORRE[pos_o - 26, i]) {
                    // VERIFICA SE HÁ ALGUMA PEÇA NO CAMINHO OU SE CHEGOU NO FIM DO TABULEIRO
                    for(int j = i % 4; j < i; j += 4) {
                        casa = ATAQUES_TORRE[pos_o - 26, j];
                        if(TemCoisa(casa / 12, casa % 12))
                            return false;
                    }
                    return true;
                }
            }

            return false;
        }


        public static bool Xeque(int jogador) {
            // VERIFICA SE O REI DE jogador ESTÁ EM XEQUE

            int x, y, oponente = 1 - jogador;

            // ATAQUE POR CAVALO
            for(int i = 0; i < 8; i++) {
                x = ATAQUES_CAVALO[rei[jogador] - 26, i] / 12;
                y = ATAQUES_CAVALO[rei[jogador] - 26, i] % 12;

                if(Tipo(x, y) == CAVALO && Cor(x, y) == oponente)
                        return true;
            }

            // ATAQUE POR TORRE, REI OU RAINHA (EM LINHA RETA)
            for(int i = 0; i < 4; i++) {
                for(int j = i; j < 28; j += 4) {
                    x = (ATAQUES_TORRE[rei[jogador] - 26, j] / 12);
                    y = (ATAQUES_TORRE[rei[jogador] - 26, j] % 12);
                    if(i == j && tabuleiro[x, y] == 60 + oponente)
                        return true;

                    switch(Tipo(x, y)) {
                        case TORRE:
                        case DAMA:
                            if(TorreAtaca((x * 12 + y), rei[jogador], 7) && Cor(x, y) == oponente)
                                    return true;
                            break;
                    }
                }
            }

            // ATAQUE POR BISPO, REI OU RAINHA (EM DIAGONAL)
            for(int i = 0; i < 4; i++)
                for(int j = i; j < 28; j += 4) {
                    x = (ATAQUES_BISPO[rei[jogador] - 26, j] / 12);
                    y = (ATAQUES_BISPO[rei[jogador] - 26, j] % 12);
                    if(i == j && tabuleiro[x, y] == 60 + oponente)
                        return true;

                    switch(Tipo(x, y)) {
                        case BISPO:
                        case DAMA:
                            if(BispoAtaca((x * 12 + y), rei[jogador], 7) && Cor(x, y) == oponente)
                                    return true;
                            break;
                    }
                }

            // ATAQUE POR PEÃO
            for(int i = -1; i < 2; i += 2) {
                x = (rei[jogador] / 12) + (2 * jogador - 1);
                y = (rei[jogador] % 12) + i;
                if(tabuleiro[x, y] == 10 + oponente)
                    return true;

            }

            return false;
        }
        public static bool ExisteMovimentosValidos(int jogador) {
            // VERIFICA SE O jogador PODE FAZER ALGUM LANCE VÁLIDO

            int origem, destino;

            for(int lin = 2; lin < 10; lin++)
                for(int col = 2; col < 10; col++)
                    if(TemCoisa(lin, col) && (Cor(lin, col) == jogador)) {
                        origem = lin * 12 + col;

                        switch(Tipo(lin, col)) { 
                            case PEAO:
                                for(int i = 0; i < 3; i++) {
                                    destino = ATAQUES_PEAO[origem - 38, 3 * jogador + i];
                                    if(MoverPeao(origem, destino, jogador)) 
                                        return true;
                                }
                                if((lin == 2 && Cor(lin, col) == PRETA) || (lin == 9 && Cor(lin, col) == BRANCA)) {
                                    destino = lin * 12 + col + (24 * (2 * jogador - 1));
                                    if(MoverPeao(origem, destino, jogador))
                                        return true;
                                }
                                break;

                            case CAVALO:
                                for(int i = 0; i < 8; i++) {
                                    destino = ATAQUES_CAVALO[origem - 26, i];
                                    if(MoverCavalo(origem, destino, jogador))
                                        return true;
                                }
                                break;

                            case BISPO:
                            case DAMA:
                                for(int i = 0; i < 28; i++) {
                                    destino = ATAQUES_BISPO[origem - 26, i];
                                    if(MoverBispo(origem, destino, jogador))
                                        return true;
                                }
                                if(Tipo(lin, col) == DAMA) goto case TORRE;
                                break;

                            case TORRE:
                                for(int i = 0; i < 28; i++) {
                                    destino = ATAQUES_TORRE[origem - 26, i];
                                    if(MoverTorre(origem, destino, jogador))
                                        return true;
                                }
                                break;

                            case REI:
                                for(int i = -1; i < 2; i++) {
                                    for(int j = -1; j < 2; j++) {
                                        destino = (lin + i) * 12 + col + j;
                                        if(ÉValida(lin + i, col + j))
                                            if(MoverRei(origem, destino, jogador))
                                                return true;
                                    }
                                }
                                break;
                        }
                    }

            return false;
        }
        public static bool Ilegal(int pos_o, int pos_d, int jogador) {
            // EXECUTA O MOVIMENTO, VERIFICA SE O REI ESTARÁ EM XEQUE, DESFAZ A JOGADA
            // RETORNA VERDADEIRO SE O REI FICARÁ EM XEQUE
            int lin_o = pos_o / 12;
            int col_o = pos_o % 12;

            int lin_d = pos_d / 12;
            int col_d = pos_d % 12;

            int aux = tabuleiro[lin_d, col_d];
            FacaMovimento(lin_o, col_o, lin_d, col_d, false);
            bool ret = Xeque(jogador);
            tabuleiro[lin_o, col_o] = tabuleiro[lin_d, col_d];
            tabuleiro[lin_d, col_d] = aux;

            return ret;
        }


        public static bool MoverPeao(int pos_o, int pos_d, int jogador) {

            int lin_o = pos_o / 12;
            int col_o = pos_o % 12;

            int lin_d = pos_d / 12;
            int col_d = pos_d % 12;
            bool valido = !Ilegal(pos_o, pos_d, jogador) && (ÉValida(lin_d, col_d));


            if(Tipo(lin_o, col_o) == PEAO)
                // VERIFICA SE ESTÁ NA PRIMEIRA FILA
                if((lin_o == (5 * (1 - jogador) + 3)) && (col_o == col_d)) {
                    int i = (2 * jogador - 1);
                    // UMA OU DUAS CASAS À FRENTE
                    for(int j = 1; j < 3; j++)
                        if((lin_o + i * j) == lin_d && j == 1)
                            return !TemCoisa(lin_d, col_d) && valido;
                        else if((lin_o + i * j) == lin_d && j == 2)
                            return !TemCoisa(lin_d, col_d) && valido && !TemCoisa(lin_d - i, col_d);
                } else {
                    if(PeaoAtaca(pos_o, pos_d, jogador))
                        if(col_o == col_d)
                            // MOVIMENTO SIMPLES(PARA FRENTE)
                            return (!TemCoisa(lin_d, col_d)) && valido;
                        else
                            // MOVIMENTO DE CAPTURA (NA DIAGONAL)
                            return ((Cor(lin_d, col_d) != jogador) && TemCoisa(lin_d, col_d)) && valido;
                }

            return false;
        }
        public static bool MoverBispo(int pos_o, int pos_d, int jogador) {
            int lin_o = pos_o / 12;
            int col_o = pos_o % 12;

            int lin_d = pos_d / 12;
            int col_d = pos_d % 12;

            // VERIFICA SE É BISPO OU RAINHA
            if((Tipo(lin_o, col_o) == BISPO) || (Tipo(lin_o, col_o) == DAMA)) {
                // VALIDAÇÃO DO MOVIMENTO
                if(BispoAtaca(pos_o, pos_d, 7))
                    if((tabuleiro[lin_d, col_d] == CASA_VAZIA) || ((Cor(lin_d, col_d) != jogador) && (tabuleiro[lin_d, col_d] != CASA_NULA)))
                        return !Ilegal(pos_o, pos_d, jogador);
            }

            return false;
        }
        public static bool MoverCavalo(int pos_o, int pos_d, int jogador) {
            int lin_o = pos_o / 12;
            int col_o = pos_o % 12;

            int lin_d = pos_d / 12;
            int col_d = pos_d % 12;

            // VERIFICA SE É CAVALO
            if(Tipo(lin_o, col_o) == CAVALO) {
                // VALIDAÇÃO DO MOVIMENTO
                if(CavaloAtaca(pos_o, pos_d))
                    if((tabuleiro[lin_d, col_d] == CASA_VAZIA) || ((Cor(lin_d, col_d) != jogador) && (tabuleiro[lin_d, col_d] != CASA_NULA)))
                        return !Ilegal(pos_o, pos_d, jogador);
            }

            return false;
        }
        public static bool MoverTorre(int pos_o, int pos_d, int jogador) {
            int lin_o = pos_o / 12;
            int col_o = pos_o % 12;

            int lin_d = pos_d / 12;
            int col_d = pos_d % 12;

            // VERIFICA SE É TORRE OU RAINHA
            if((Tipo(lin_o, col_o) == TORRE) || (Tipo(lin_o, col_o) == DAMA)) {
                // VALIDAÇÃO DO MOVIMENTO
                if(TorreAtaca(pos_o, pos_d, 7))
                    if((tabuleiro[lin_d, col_d] == CASA_VAZIA) || ((Cor(lin_d, col_d) != jogador) && (tabuleiro[lin_d, col_d] != CASA_NULA)))
                        return !Ilegal(pos_o, pos_d, jogador);
            }

            return false;
        }
        public static bool MoverDama(int pos_o, int pos_d, int jogador) {
            int lin_o = pos_o / 12;
            int col_o = pos_o % 12;

            int lin_d = pos_d / 12;
            int col_d = pos_d % 12;

            // VERIFICA SE É RAINHA
            if(Tipo(lin_o, col_o) == DAMA)
                if(((lin_o == lin_d) && (col_o != col_d)) || ((lin_o != lin_d) && (col_o == col_d)))
                    // MOVIMENTO EM LINHA RETA
                    return MoverTorre(pos_o, pos_d, jogador);
                else
                    //MOVIMENTO EM DIAGONAL
                    return MoverBispo(pos_o, pos_d, jogador);

            return false;
        }
        public static bool MoverRei(int pos_o, int pos_d, int jogador) {
            int lin_o = pos_o / 12;
            int col_o = pos_o % 12;

            int lin_d = pos_d / 12;
            int col_d = pos_d % 12;

            if(!ÉValida(lin_d, col_d)) return false;
            bool valido = false;

            // VERIFICA SE É REI
            if(Tipo(lin_o, col_o) == REI)
                // PODE SE MOVIMENTAR
                if(((lin_o == lin_d) && (col_o != col_d)) || ((lin_o != lin_d) && (col_o == col_d)))
                    // - EM LINHA RETA?
                    valido = TorreAtaca(pos_o, pos_d, 1);
                else
                    // - EM DIAGONAL?
                    valido = BispoAtaca(pos_o, pos_d, 1);


            if(valido) {
                // VERIFICA SE O REI FICARÁ EM XEQUE
                rei[jogador] = pos_d;
                valido = !Ilegal(pos_o, pos_d, jogador);
                valido &= !TemCoisa(lin_d, col_d) || ((Cor(lin_d, col_d) != jogador) && (ÉValida(lin_d, col_d)));
                rei[jogador] = pos_o;
                return valido;
            }

            return false;
        }
    }
}