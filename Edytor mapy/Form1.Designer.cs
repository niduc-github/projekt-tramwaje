namespace Edytor_mapy
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.picMap = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDodajPrzystanek = new System.Windows.Forms.Button();
            this.btnEdytujPrzystanek = new System.Windows.Forms.Button();
            this.btnUsuńPrzystanek = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.SuspendLayout();
            // 
            // picMap
            // 
            this.picMap.Location = new System.Drawing.Point(12, 56);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(640, 480);
            this.picMap.TabIndex = 1;
            this.picMap.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Test";
            // 
            // btnDodajPrzystanek
            // 
            this.btnDodajPrzystanek.Location = new System.Drawing.Point(712, 68);
            this.btnDodajPrzystanek.Name = "btnDodajPrzystanek";
            this.btnDodajPrzystanek.Size = new System.Drawing.Size(125, 23);
            this.btnDodajPrzystanek.TabIndex = 3;
            this.btnDodajPrzystanek.Text = "Dodaj przystanek";
            this.btnDodajPrzystanek.UseVisualStyleBackColor = true;
            // 
            // btnEdytujPrzystanek
            // 
            this.btnEdytujPrzystanek.Location = new System.Drawing.Point(712, 97);
            this.btnEdytujPrzystanek.Name = "btnEdytujPrzystanek";
            this.btnEdytujPrzystanek.Size = new System.Drawing.Size(125, 23);
            this.btnEdytujPrzystanek.TabIndex = 3;
            this.btnEdytujPrzystanek.Text = "Edytuj przystanek";
            this.btnEdytujPrzystanek.UseVisualStyleBackColor = true;
            // 
            // btnUsuńPrzystanek
            // 
            this.btnUsuńPrzystanek.Location = new System.Drawing.Point(712, 126);
            this.btnUsuńPrzystanek.Name = "btnUsuńPrzystanek";
            this.btnUsuńPrzystanek.Size = new System.Drawing.Size(125, 23);
            this.btnUsuńPrzystanek.TabIndex = 4;
            this.btnUsuńPrzystanek.Text = "Usuń przystanek";
            this.btnUsuńPrzystanek.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 572);
            this.Controls.Add(this.btnUsuńPrzystanek);
            this.Controls.Add(this.btnEdytujPrzystanek);
            this.Controls.Add(this.btnDodajPrzystanek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picMap);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picMap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDodajPrzystanek;
        private System.Windows.Forms.Button btnEdytujPrzystanek;
        private System.Windows.Forms.Button btnUsuńPrzystanek;
    }
}

