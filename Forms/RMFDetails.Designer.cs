using FrameworksViewerApp.Models;
using FrameworksViewerApp.Utilities;
using MySql.Data.MySqlClient;

namespace FrameworksViewerApp
{
    partial class RMFDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public void ShowRMFDetails(int rmfId)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection newConn = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            newConn.Open();

            cmd.Connection = newConn;
            cmd.Parameters.AddWithValue("rmfid", rmfId);

            cmd.CommandText = SQLStatements.RMFSelectByRMFID;
            MySqlDataReader reader = cmd.ExecuteReader();

            if(reader != null)
            {
                reader.Read();

                this.StepName.Text = (string)reader["step"];
                this.TaskID.Text = (string)reader["taskid"];
                this.TaskName.Text = (string)reader["taskname"];
                this.TaskDescr.Text = (string)reader["taskdescr"];
            }

            reader.Close();
            newConn.Close();
        }

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
            this.StepName = new System.Windows.Forms.TextBox();
            this.TaskID = new System.Windows.Forms.TextBox();
            this.TaskName = new System.Windows.Forms.TextBox();
            this.TaskDescr = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StepName
            // 
            this.StepName.Location = new System.Drawing.Point(31, 62);
            this.StepName.Name = "StepName";
            this.StepName.Size = new System.Drawing.Size(250, 47);
            this.StepName.TabIndex = 0;
            // 
            // TaskID
            // 
            this.TaskID.Location = new System.Drawing.Point(386, 62);
            this.TaskID.Name = "TaskID";
            this.TaskID.Size = new System.Drawing.Size(250, 47);
            this.TaskID.TabIndex = 1;
            // 
            // TaskName
            // 
            this.TaskName.Location = new System.Drawing.Point(31, 175);
            this.TaskName.Name = "TaskName";
            this.TaskName.Size = new System.Drawing.Size(605, 47);
            this.TaskName.TabIndex = 2;
            // 
            // TaskDescr
            // 
            this.TaskDescr.Location = new System.Drawing.Point(31, 282);
            this.TaskDescr.Multiline = true;
            this.TaskDescr.Name = "TaskDescr";
            this.TaskDescr.Size = new System.Drawing.Size(605, 141);
            this.TaskDescr.TabIndex = 3;
            // 
            // RMFDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 450);
            this.Controls.Add(this.TaskDescr);
            this.Controls.Add(this.TaskName);
            this.Controls.Add(this.TaskID);
            this.Controls.Add(this.StepName);
            this.Name = "RMFDetails";
            this.Text = "RMFDetails";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox StepName;
        private System.Windows.Forms.TextBox TaskID;
        private System.Windows.Forms.TextBox TaskName;
        private System.Windows.Forms.TextBox TaskDescr;
    }
}