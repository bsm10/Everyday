﻿namespace Everyday
{
    partial class frmMain
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.pbxKlient = new System.Windows.Forms.PictureBox();
            this.txtUserInfo = new System.Windows.Forms.TextBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            ((System.ComponentModel.ISupportInitialize)(this.pbxKlient)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(172, 74);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(147, 155);
            this.treeView1.TabIndex = 0;
            // 
            // pbxKlient
            // 
            this.pbxKlient.Location = new System.Drawing.Point(3, 3);
            this.pbxKlient.Name = "pbxKlient";
            this.pbxKlient.Size = new System.Drawing.Size(68, 67);
            this.pbxKlient.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxKlient.TabIndex = 1;
            this.pbxKlient.TabStop = false;
            // 
            // txtUserInfo
            // 
            this.txtUserInfo.BackColor = System.Drawing.SystemColors.Control;
            this.txtUserInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserInfo.Location = new System.Drawing.Point(86, 3);
            this.txtUserInfo.Multiline = true;
            this.txtUserInfo.Name = "txtUserInfo";
            this.txtUserInfo.ReadOnly = true;
            this.txtUserInfo.Size = new System.Drawing.Size(233, 67);
            this.txtUserInfo.TabIndex = 2;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(3, 74);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 235);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.txtUserInfo);
            this.Controls.Add(this.pbxKlient);
            this.Controls.Add(this.treeView1);
            this.Name = "frmMain";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbxKlient)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PictureBox pbxKlient;
        private System.Windows.Forms.TextBox txtUserInfo;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
    }
}

