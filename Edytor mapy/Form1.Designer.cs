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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.SuspendLayout();
            // 
            // picMap
            // 
            this.picMap.Location = new System.Drawing.Point(16, 69);
            this.picMap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(853, 591);
            this.picMap.TabIndex = 1;
            this.picMap.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Test";
            // 
            // btnDodajPrzystanek
            // 
            this.btnDodajPrzystanek.Location = new System.Drawing.Point(949, 84);
            this.btnDodajPrzystanek.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDodajPrzystanek.Name = "btnDodajPrzystanek";
            this.btnDodajPrzystanek.Size = new System.Drawing.Size(167, 28);
            this.btnDodajPrzystanek.TabIndex = 3;
            this.btnDodajPrzystanek.Text = "Dodaj przystanek";
            this.btnDodajPrzystanek.UseVisualStyleBackColor = true;
            // 
            // btnEdytujPrzystanek
            // 
            this.btnEdytujPrzystanek.Location = new System.Drawing.Point(949, 119);
            this.btnEdytujPrzystanek.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEdytujPrzystanek.Name = "btnEdytujPrzystanek";
            this.btnEdytujPrzystanek.Size = new System.Drawing.Size(167, 28);
            this.btnEdytujPrzystanek.TabIndex = 3;
            this.btnEdytujPrzystanek.Text = "Edytuj przystanek";
            this.btnEdytujPrzystanek.UseVisualStyleBackColor = true;
            // 
            // btnUsuńPrzystanek
            // 
            this.btnUsuńPrzystanek.Location = new System.Drawing.Point(949, 155);
            this.btnUsuńPrzystanek.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUsuńPrzystanek.Name = "btnUsuńPrzystanek";
            this.btnUsuńPrzystanek.Size = new System.Drawing.Size(167, 28);
            this.btnUsuńPrzystanek.TabIndex = 4;
            this.btnUsuńPrzystanek.Text = "Usuń przystanek";
            this.btnUsuńPrzystanek.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(949, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 27);
            this.button1.TabIndex = 5;
            this.button1.Text = "Utwórz linie";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(949, 256);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(167, 28);
            this.button2.TabIndex = 6;
            this.button2.Text = "Usuń linie";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(949, 223);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(167, 27);
            this.button3.TabIndex = 7;
            this.button3.Text = "Edytuj linie";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(949, 290);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(167, 30);
            this.button4.TabIndex = 8;
            this.button4.Text = "Zapisz do pliku";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(949, 326);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(167, 31);
            this.button5.TabIndex = 9;
            this.button5.Text = "Wczytaj z pliku";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 704);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnUsuńPrzystanek);
            this.Controls.Add(this.btnEdytujPrzystanek);
            this.Controls.Add(this.btnDodajPrzystanek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picMap);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

