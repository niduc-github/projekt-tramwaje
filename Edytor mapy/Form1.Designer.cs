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
            this.btnUtwórzLinię = new System.Windows.Forms.Button();
            this.btnUsuńLinię = new System.Windows.Forms.Button();
            this.btnEdytujLinię = new System.Windows.Forms.Button();
            this.btnZapiszDoPliku = new System.Windows.Forms.Button();
            this.btnWczytajZPliku = new System.Windows.Forms.Button();
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
            this.picMap.Click += new System.EventHandler(this.picMap_Click);
            this.picMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseClick);
            this.picMap.MouseHover += new System.EventHandler(this.picMap_MouseHover);
            this.picMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseMove);
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
            this.btnDodajPrzystanek.Click += new System.EventHandler(this.btnDodajPrzystanek_Click);
            // 
            // btnEdytujPrzystanek
            // 
            this.btnEdytujPrzystanek.Location = new System.Drawing.Point(712, 97);
            this.btnEdytujPrzystanek.Name = "btnEdytujPrzystanek";
            this.btnEdytujPrzystanek.Size = new System.Drawing.Size(125, 23);
            this.btnEdytujPrzystanek.TabIndex = 3;
            this.btnEdytujPrzystanek.Text = "Edytuj przystanek";
            this.btnEdytujPrzystanek.UseVisualStyleBackColor = true;
            this.btnEdytujPrzystanek.Click += new System.EventHandler(this.btnEdytujPrzystanek_Click);
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
            // btnUtwórzLinię
            // 
            this.btnUtwórzLinię.Location = new System.Drawing.Point(712, 154);
            this.btnUtwórzLinię.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUtwórzLinię.Name = "btnUtwórzLinię";
            this.btnUtwórzLinię.Size = new System.Drawing.Size(125, 23);
            this.btnUtwórzLinię.TabIndex = 5;
            this.btnUtwórzLinię.Text = "Utwórz linię";
            this.btnUtwórzLinię.UseVisualStyleBackColor = true;
            // 
            // btnUsuńLinię
            // 
            this.btnUsuńLinię.Location = new System.Drawing.Point(712, 208);
            this.btnUsuńLinię.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUsuńLinię.Name = "btnUsuńLinię";
            this.btnUsuńLinię.Size = new System.Drawing.Size(125, 23);
            this.btnUsuńLinię.TabIndex = 6;
            this.btnUsuńLinię.Text = "Usuń linię";
            this.btnUsuńLinię.UseVisualStyleBackColor = true;
            // 
            // btnEdytujLinię
            // 
            this.btnEdytujLinię.Location = new System.Drawing.Point(712, 181);
            this.btnEdytujLinię.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEdytujLinię.Name = "btnEdytujLinię";
            this.btnEdytujLinię.Size = new System.Drawing.Size(125, 23);
            this.btnEdytujLinię.TabIndex = 7;
            this.btnEdytujLinię.Text = "Edytuj linię";
            this.btnEdytujLinię.UseVisualStyleBackColor = true;
            // 
            // btnZapiszDoPliku
            // 
            this.btnZapiszDoPliku.Location = new System.Drawing.Point(712, 235);
            this.btnZapiszDoPliku.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnZapiszDoPliku.Name = "btnZapiszDoPliku";
            this.btnZapiszDoPliku.Size = new System.Drawing.Size(125, 23);
            this.btnZapiszDoPliku.TabIndex = 8;
            this.btnZapiszDoPliku.Text = "Zapisz do pliku";
            this.btnZapiszDoPliku.UseVisualStyleBackColor = true;
            // 
            // btnWczytajZPliku
            // 
            this.btnWczytajZPliku.Location = new System.Drawing.Point(712, 262);
            this.btnWczytajZPliku.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnWczytajZPliku.Name = "btnWczytajZPliku";
            this.btnWczytajZPliku.Size = new System.Drawing.Size(125, 23);
            this.btnWczytajZPliku.TabIndex = 9;
            this.btnWczytajZPliku.Text = "Wczytaj z pliku";
            this.btnWczytajZPliku.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 572);
            this.Controls.Add(this.btnWczytajZPliku);
            this.Controls.Add(this.btnZapiszDoPliku);
            this.Controls.Add(this.btnEdytujLinię);
            this.Controls.Add(this.btnUsuńLinię);
            this.Controls.Add(this.btnUtwórzLinię);
            this.Controls.Add(this.btnUsuńPrzystanek);
            this.Controls.Add(this.btnEdytujPrzystanek);
            this.Controls.Add(this.btnDodajPrzystanek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picMap);
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
        private System.Windows.Forms.Button btnUtwórzLinię;
        private System.Windows.Forms.Button btnUsuńLinię;
        private System.Windows.Forms.Button btnEdytujLinię;
        private System.Windows.Forms.Button btnZapiszDoPliku;
        private System.Windows.Forms.Button btnWczytajZPliku;
    }
}

