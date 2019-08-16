namespace JasChess {
    partial class Promocao {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.btnBispo = new System.Windows.Forms.Button();
            this.btnCavalo = new System.Windows.Forms.Button();
            this.btnTorre = new System.Windows.Forms.Button();
            this.btnDama = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Promover peão para";
            // 
            // btnBispo
            // 
            this.btnBispo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBispo.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBispo.Location = new System.Drawing.Point(14, 59);
            this.btnBispo.Name = "btnBispo";
            this.btnBispo.Size = new System.Drawing.Size(258, 45);
            this.btnBispo.TabIndex = 1;
            this.btnBispo.Text = "Bispo";
            this.btnBispo.UseVisualStyleBackColor = true;
            this.btnBispo.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnCavalo
            // 
            this.btnCavalo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCavalo.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCavalo.Location = new System.Drawing.Point(13, 109);
            this.btnCavalo.Name = "btnCavalo";
            this.btnCavalo.Size = new System.Drawing.Size(258, 45);
            this.btnCavalo.TabIndex = 2;
            this.btnCavalo.Text = "Cavalo";
            this.btnCavalo.UseVisualStyleBackColor = true;
            this.btnCavalo.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnTorre
            // 
            this.btnTorre.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnTorre.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTorre.Location = new System.Drawing.Point(14, 160);
            this.btnTorre.Name = "btnTorre";
            this.btnTorre.Size = new System.Drawing.Size(258, 45);
            this.btnTorre.TabIndex = 3;
            this.btnTorre.Text = "Torre";
            this.btnTorre.UseVisualStyleBackColor = true;
            this.btnTorre.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnDama
            // 
            this.btnDama.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDama.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDama.Location = new System.Drawing.Point(14, 211);
            this.btnDama.Name = "btnDama";
            this.btnDama.Size = new System.Drawing.Size(258, 45);
            this.btnDama.TabIndex = 4;
            this.btnDama.Text = "Rainha";
            this.btnDama.UseVisualStyleBackColor = true;
            this.btnDama.Click += new System.EventHandler(this.btn_Click);
            // 
            // Promocao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 267);
            this.ControlBox = false;
            this.Controls.Add(this.btnDama);
            this.Controls.Add(this.btnTorre);
            this.Controls.Add(this.btnCavalo);
            this.Controls.Add(this.btnBispo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Promocao";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Promover peão";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBispo;
        private System.Windows.Forms.Button btnCavalo;
        private System.Windows.Forms.Button btnTorre;
        private System.Windows.Forms.Button btnDama;
    }
}