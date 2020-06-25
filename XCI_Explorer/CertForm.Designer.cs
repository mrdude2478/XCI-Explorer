using Be.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace XCI_Explorer
{
    partial class CertForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private HexBox hbxHexView;

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
            this.hbxHexView = new Be.Windows.Forms.HexBox();
            this.SuspendLayout();
            // 
            // hbxHexView
            // 
            this.hbxHexView.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.hbxHexView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hbxHexView.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hbxHexView.Location = new System.Drawing.Point(0, 0);
            this.hbxHexView.Margin = new System.Windows.Forms.Padding(4);
            this.hbxHexView.Name = "hbxHexView";
            this.hbxHexView.ReadOnly = true;
            this.hbxHexView.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hbxHexView.Size = new System.Drawing.Size(573, 256);
            this.hbxHexView.StringViewVisible = true;
            this.hbxHexView.TabIndex = 7;
            this.hbxHexView.UseFixedBytesPerLine = true;
            this.hbxHexView.VScrollBarVisible = true;
            // 
            // CertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 256);
            this.Controls.Add(this.hbxHexView);
            this.Name = "CertForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cert Data";
            this.ResumeLayout(false);

        }

        #endregion
    }
}