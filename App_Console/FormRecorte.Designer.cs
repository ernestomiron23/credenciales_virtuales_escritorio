namespace App_Console
{
    partial class FormRecorte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRecorte));
            this.btnAplicarRecorte = new System.Windows.Forms.Button();
            this.pictureBoxRecorte = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRecorte)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAplicarRecorte
            // 
            this.btnAplicarRecorte.BackColor = System.Drawing.Color.SpringGreen;
            this.btnAplicarRecorte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicarRecorte.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicarRecorte.ForeColor = System.Drawing.Color.Black;
            this.btnAplicarRecorte.Location = new System.Drawing.Point(792, 84);
            this.btnAplicarRecorte.Name = "btnAplicarRecorte";
            this.btnAplicarRecorte.Size = new System.Drawing.Size(144, 47);
            this.btnAplicarRecorte.TabIndex = 1;
            this.btnAplicarRecorte.Text = "recortar";
            this.btnAplicarRecorte.UseVisualStyleBackColor = false;
            this.btnAplicarRecorte.Click += new System.EventHandler(this.btnAplicarRecorte_Click);
            // 
            // pictureBoxRecorte
            // 
            this.pictureBoxRecorte.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.pictureBoxRecorte.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxRecorte.Name = "pictureBoxRecorte";
            this.pictureBoxRecorte.Size = new System.Drawing.Size(675, 520);
            this.pictureBoxRecorte.TabIndex = 0;
            this.pictureBoxRecorte.TabStop = false;
            this.pictureBoxRecorte.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxRecorte_Paint);
            // 
            // FormRecorte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(1054, 544);
            this.Controls.Add(this.btnAplicarRecorte);
            this.Controls.Add(this.pictureBoxRecorte);
            this.ForeColor = System.Drawing.Color.SpringGreen;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRecorte";
            this.RightToLeftLayout = true;
            this.Text = "EDITOR";
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.Load += new System.EventHandler(this.FormRecorte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRecorte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxRecorte;
        private System.Windows.Forms.Button btnAplicarRecorte;
    }
}