using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JasChess {
    public partial class Pensando : Form {
        DateTime inicio;

        public Pensando() {
            InitializeComponent();
            this.inicio = System.DateTime.Now;
        }
        int estado = 0;
        private void timer1_Tick(object sender, EventArgs e) {

            if(estado == 0) {
                Opacity -= 0.07;
            }
            else {
                Opacity += 0.07;
            }
            estado = Opacity == 0 || Opacity == 1 ? 1 - estado : estado;
        }

        //public int tempo = 0;
        private void timer2_Tick(object sender, EventArgs e) {
            Busca.x = DateTime.Now.Subtract(inicio).TotalSeconds;
        }
    }
}
