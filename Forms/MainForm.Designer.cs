
using System;

namespace FrameworksViewerApp
{
    partial class MainForm
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

        protected override void OnLoad(EventArgs e)
        {
            NISTCyberRiskFramework ncrf = new NISTCyberRiskFramework();
            ncrf.MdiParent = this;
            ncrf.Show();
            base.OnLoad(e);
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStripView = new System.Windows.Forms.MenuStrip();
            this.contextNISTCRF = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 366);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(737, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // menuStripView
            // 
            this.menuStripView.ContextMenuStrip = this.contextNISTCRF;
            this.menuStripView.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStripView.Location = new System.Drawing.Point(0, 0);
            this.menuStripView.Name = "menuStripView";
            this.menuStripView.Padding = new System.Windows.Forms.Padding(2, 1, 0, 1);
            this.menuStripView.Size = new System.Drawing.Size(737, 24);
            this.menuStripView.TabIndex = 4;
            this.menuStripView.Text = "View NIST CRF";
            // 
            // contextNISTCRF
            // 
            this.contextNISTCRF.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.contextNISTCRF.Name = "contextNISTCRF";
            this.contextNISTCRF.Size = new System.Drawing.Size(61, 4);
            this.contextNISTCRF.Text = "NIST Cyber Risk Framework";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 388);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStripView);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "Security Frameworks Mapper and Viewer";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.MenuStrip menuStripView;
        private System.Windows.Forms.ContextMenuStrip contextNISTCRF;
    }
}



