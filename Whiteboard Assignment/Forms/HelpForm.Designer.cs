namespace Whiteboard_Assignment
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.lblPenThicknessHelp = new System.Windows.Forms.Label();
            this.lblTriangleHelp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPenThicknessHelp
            // 
            this.lblPenThicknessHelp.Location = new System.Drawing.Point(12, 155);
            this.lblPenThicknessHelp.Name = "lblPenThicknessHelp";
            this.lblPenThicknessHelp.Size = new System.Drawing.Size(299, 120);
            this.lblPenThicknessHelp.TabIndex = 0;
            this.lblPenThicknessHelp.Text = resources.GetString("lblPenThicknessHelp.Text");
            this.lblPenThicknessHelp.Visible = false;
            // 
            // lblTriangleHelp
            // 
            this.lblTriangleHelp.Location = new System.Drawing.Point(12, 9);
            this.lblTriangleHelp.Name = "lblTriangleHelp";
            this.lblTriangleHelp.Size = new System.Drawing.Size(299, 120);
            this.lblTriangleHelp.TabIndex = 1;
            this.lblTriangleHelp.Text = resources.GetString("lblTriangleHelp.Text");
            this.lblTriangleHelp.Visible = false;
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 284);
            this.Controls.Add(this.lblTriangleHelp);
            this.Controls.Add(this.lblPenThicknessHelp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "HelpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HelpForm_FormClosing);
            this.Load += new System.EventHandler(this.HelpForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPenThicknessHelp;
        private System.Windows.Forms.Label lblTriangleHelp;
    }
}