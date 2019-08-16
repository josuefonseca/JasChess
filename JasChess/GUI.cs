using System;
using System.Drawing;
using System.Windows.Forms;
using static JasChess.Jogo;
using static JasChess.Pecas;
using static JasChess.Tabuleiro;

namespace JasChess {
    public partial class GUI : Form {

        public GUI() {
            InitializeComponent();

        }

        private void pictureBox_MouseEnter(object sender, EventArgs e) {
            PictureBox c = sender as PictureBox;
            c.Image = Properties.Resources.selecao;
            c.Cursor = c.BackgroundImage != null || c.BackColor == proxV ? Cursors.Hand : Cursors.Arrow;
            int ind;
            for(ind = 0; ind < 64; ind++)
                if(casa[ind] == (PictureBox)sender) {
                    if(casa[ind].BackgroundImage != null)
                        tsFoco.Text = Nome(ind);
                    break;
                }

        }

        private void pictureBox_MouseLeave(object sender, EventArgs e) {
            PictureBox c = sender as PictureBox;
            c.Image = null;
            tsFoco.Text = null;
        }

        private void tsJNH_Click(object sender, EventArgs e) {
            NovoJogo(false);
            tsVez.Text = "Vez das: Brancas";
        }
        private void tsNJH_Click(object sender, EventArgs e) {
            NovoJogo(true);
            tsVez.Text = "Vez das: Brancas";
        }

        private void tsSobre_Click(object sender, EventArgs e) {
            new Sobre().ShowDialog();
        }

        public static Color atual = Color.Gold;
        public static Color proxV = Color.PaleGreen;
        public static Color proxO = Color.IndianRed;
        public static Color antO = Color.LightSkyBlue;
        public static Color antD = Color.PowderBlue;

        public static int lino;
        public static int colo;
        public static int lind;
        public static int cold;
        public static bool flag = false, temSom = true;

        private void tocarSonsToolStripMenuItem_Click(object sender, EventArgs e) {
            tocarSonsToolStripMenuItem.Checked = !tocarSonsToolStripMenuItem.Checked;
            temSom = tocarSonsToolStripMenuItem.Checked;
        }

        private void GUI_Load(object sender, EventArgs e) {

        }

        private void pictureBox_Click(object sender, EventArgs e) {

            int ind;
            int lin = -1;
            int col = -1;

            for(ind = 0; ind < 64; ind++) {
                if(casa[ind] == (PictureBox)sender) {
                    lin = lind = ind / 8 + 2;
                    col = cold = ind % 8 + 2;
                    break;
                }
            }

            if(!TemCoisa(lin, col) && !flag)
                ResetCor();
            else if(flag && (casa[ind].BackColor == proxO || casa[ind].BackColor == proxV)) {
                Jogar();
                tsVez.Text = "Vez das: " + new string[] { "Brancas", "Pretas" }[jogador];
            }
            else if(TemCoisa(lin, col) && Cor(lin, col) == jogador) {
                ResetCor();
                casa[ind].BackColor = atual;
                lino = ind / 8 + 2;
                colo = ind % 8 + 2;
                flag = true;

                MostrarPosicoes(lin, col);
               
            }
            else ResetCor();
        }

     
    }
    }
