using System.Media;
using System.Windows.Forms;
using static JasChess.GUI;
using static JasChess.Movimentos;
using static JasChess.Pecas;
using static JasChess.Tabuleiro;
using static JasChess.Busca;
using static JasChess.Properties.Resources;

namespace JasChess {
    class Jogo {
        public const int XEQUE = -1;
        public const int XEQUE_MATE = 0;
        public const int REI_AFOGADO = 1;
        public const int EMPATE = 2;

        private static string[] sentenca = { "Xeque-mate", "Rei afogado", "Empate" };
        private static string[] resultado = { "vitória das brancas", "vitória das pretas", "não houve vencedor" };
        public static SoundPlayer[] sons = { new SoundPlayer(move),  new SoundPlayer(promocao),
                                             new SoundPlayer(xeque), new SoundPlayer(empate),
                                             new SoundPlayer(uder),  new SoundPlayer(uvi)};

        private static int situacao = 3;
        private static int vencedor = -1;
        public static int jogador = 0;

        public static bool humano = true;


        public static void NovoJogo(bool human) {
            IniciarTabuleiro();
            jogador = 0;
            humano = human;
            gui.tsTempo.Visible = !humano;
            if(temSom) sons[1].Play();
            MessageBox.Show("Tudo pronto! As brancas começam.", "JasChess", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public static void Jogar() {
            FacaMovimento(lino, colo, lind, cold, true);
            Promover(jogador);


            lino = colo = -1;
            flag = false;
            jogador = 1 - jogador;
            if(FimJogo(ref jogador, ref situacao, ref vencedor)) 
                Finaliza();
            else {
                if(situacao == XEQUE) {
                    int a = (rei[jogador] / 12 - 2) * 8;
                    int b = a + (rei[jogador] % 12) - 2;
                    casa[b].BackColor = proxO;
                    if(temSom) sons[2].Play();
                    MessageBox.Show("Xeque!", "JasChess", MessageBoxButtons.OK, MessageBoxIcon.None);
                }

                if(jogador == PRETA && !humano) {
                    ComputadorJoga();

                    lino = colo = -1;
                    flag = false;
                    jogador = 1 - jogador;
                    if(FimJogo(ref jogador, ref situacao, ref vencedor))
                        Finaliza();
                    else {
                        if(situacao == XEQUE) {
                            int a = (rei[jogador] / 12 - 2) * 8;
                            int b = a + (rei[jogador] % 12) - 2;
                            casa[b].BackColor = proxO;
                            if(temSom) sons[2].Play();
                            MessageBox.Show("Xeque!", "JasChess", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                }
            }
        }

        public static void MostrarPosicoes(int lin, int col) {
            switch(Tipo(lin, col)) {
                case PEAO:
                    for(int i = 0; i < 64; i++) {
                        int nil = i / 8 + 2;
                        int loc = i % 8 + 2;
                        if(MoverPeao(lin * 12 + col, nil * 12 + loc, jogador)) {
                            casa[i].BackColor = TemCoisa(nil, loc) ? proxO : proxV;
                        }
                    }
                    break;
                case BISPO:
                    for(int i = 0; i < 64; i++) {
                        int nil = i / 8 + 2;
                        int loc = i % 8 + 2;
                        if(MoverBispo(lin * 12 + col, nil * 12 + loc, jogador)) {
                            casa[i].BackColor = TemCoisa(nil, loc) ? proxO : proxV;
                        }
                    }
                    break;
                case CAVALO:
                    for(int i = 0; i < 64; i++) {
                        int nil = i / 8 + 2;
                        int loc = i % 8 + 2;
                        if(MoverCavalo(lin * 12 + col, nil * 12 + loc, jogador)) {
                            casa[i].BackColor = TemCoisa(nil, loc) ? proxO : proxV;
                        }
                    }
                    break;
                case TORRE:
                    for(int i = 0; i < 64; i++) {
                        int nil = i / 8 + 2;
                        int loc = i % 8 + 2;
                        if(MoverTorre(lin * 12 + col, nil * 12 + loc, jogador)) {
                            casa[i].BackColor = TemCoisa(nil, loc) ? proxO : proxV;
                        }
                    }
                    break;
                case DAMA:
                    for(int i = 0; i < 64; i++) {
                        int nil = i / 8 + 2;
                        int loc = i % 8 + 2;
                        if(MoverDama(lin * 12 + col, nil * 12 + loc, jogador)) {
                            casa[i].BackColor = TemCoisa(nil, loc) ? proxO : proxV;
                        }
                    }
                    break;
                case REI:
                    for(int i = 0; i < 64; i++) {
                        int nil = i / 8 + 2;
                        int loc = i % 8 + 2;
                        if(MoverRei(lin * 12 + col, nil * 12 + loc, jogador)) {
                            casa[i].BackColor = TemCoisa(nil, loc) ? proxO : proxV;
                        }
                    }
                    break;
            }
        }

        
        public static bool FimJogo(ref int jog, ref int sit, ref int venc) {
            if(Xeque(jog)) {
                if(!ExisteMovimentosValidos(jog)) {
                    sit = XEQUE_MATE;
                    venc = 1 - jog;
                    return true;
                } else {
                    sit = XEQUE;
                    return false;
                }
            } else if(!ExisteMovimentosValidos(jog)) {
                sit = REI_AFOGADO;
                venc = 2;
                return true;
            } else if(Quantidade() == 2) {
                sit = venc = EMPATE;
                return true;
            }

            sit = 3;
            return false;
        }

        static void Finaliza() {

            if(temSom)
                switch(situacao) {
                    case XEQUE_MATE:
                        int a = (rei[jogador] / 12 - 2) * 8;
                        int b = a + (rei[jogador] % 12) - 2;
                        casa[b].BackColor = proxO;
                        sons[5 - vencedor].Play();
                        break;
                    case REI_AFOGADO:
                    case EMPATE:
                        sons[3].Play();
                        break;
                }

            string msg = string.Format("{0}, {1}.", sentenca[situacao], resultado[vencedor]);
            MessageBox.Show(msg, "\nFim de Jogo.", MessageBoxButtons.OK, MessageBoxIcon.None);

            DialogResult d = MessageBox.Show("Iniciar nova partida?", "JasChess", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(d == DialogResult.Yes)
                NovoJogo(humano);
            else 
                for(int i = 0; i < 64; casa[i++].Enabled = false) ;
        }
    }
}