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
    public partial class Promocao : Form {
        public Promocao() {
            InitializeComponent();
        }

        private int _nova;

        public int nova {
            get { return _nova; }
            set { _nova = value; }
        }

        private void btn_Click(object sender, EventArgs e) {
            Button b = sender as Button;
            nova = b.TabIndex;
            this.Close();
        }
    }
}