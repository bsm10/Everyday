namespace Everyday
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
            this.components = new System.ComponentModel.Container();
            this.pbxKlient = new System.Windows.Forms.PictureBox();
            this.txtUserInfo = new System.Windows.Forms.TextBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.listView1 = new System.Windows.Forms.ListView();
            this.getEventsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eventBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbxKlient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getEventsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventBindingSource)).BeginInit();
            this.SuspendLayout();
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
            this.txtUserInfo.Location = new System.Drawing.Point(77, 1);
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
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.CheckBoxes = true;
            this.listView1.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.eventBindingSource, "expert_name", true));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(176, 74);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(376, 218);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // eventBindingSource
            // 
            this.eventBindingSource.DataSource = typeof(Everyday.Event);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 295);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.txtUserInfo);
            this.Controls.Add(this.pbxKlient);
            this.Name = "frmMain";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbxKlient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getEventsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxKlient;
        private System.Windows.Forms.TextBox txtUserInfo;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.BindingSource getEventsBindingSource;
        private System.Windows.Forms.BindingSource eventBindingSource;
    }
}

