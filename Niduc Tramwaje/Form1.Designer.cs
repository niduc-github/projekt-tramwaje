namespace Niduc_Tramwaje
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            this.components = new System.ComponentModel.Container();
            this.picMap = new System.Windows.Forms.PictureBox();
            this.btnTest_RuszTramwajem = new System.Windows.Forms.Button();
            this.tmrGraphics = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.SuspendLayout();
            // 
            // picMap
            // 
            this.picMap.Location = new System.Drawing.Point(12, 12);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(640, 480);
            this.picMap.TabIndex = 0;
            this.picMap.TabStop = false;
            // 
            // btnTest_RuszTramwajem
            // 
            this.btnTest_RuszTramwajem.Location = new System.Drawing.Point(682, 35);
            this.btnTest_RuszTramwajem.Name = "btnTest_RuszTramwajem";
            this.btnTest_RuszTramwajem.Size = new System.Drawing.Size(134, 23);
            this.btnTest_RuszTramwajem.TabIndex = 1;
            this.btnTest_RuszTramwajem.Text = "Test_RuszTramwajem";
            this.btnTest_RuszTramwajem.UseVisualStyleBackColor = true;
            this.btnTest_RuszTramwajem.Click += new System.EventHandler(this.btnTest_RuszTramwajem_Click);
            // 
            // tmrGraphics
            // 
            this.tmrGraphics.Tick += new System.EventHandler(this.tmrGraphics_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 537);
            this.Controls.Add(this.btnTest_RuszTramwajem);
            this.Controls.Add(this.picMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picMap;
        private System.Windows.Forms.Button btnTest_RuszTramwajem;
        private System.Windows.Forms.Timer tmrGraphics;
    }
}

