using FrameworksViewerApp.Models;
using FrameworksViewerApp.Utilities;
using MySql.Data.MySqlClient;

namespace FrameworksViewerApp
{
    partial class STIGDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public void ShowSTIGDetails(STIGModel stig)
        {

            GroupIDNo.Text = stig.groupidno;
            Target.Text = stig.target;
            Description.Text = stig.descr;
            Title.Text = stig.title;

            this.Text = "Details for " + stig.groupidno;

            this.populateRuleDetails(stig.id);
        }

        private void populateRuleDetails(int stigId)
        {
            int ruleId = 0;
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection newConn = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            newConn.Open();

            cmd.Connection = newConn;
            cmd.Parameters.AddWithValue("stigid", stigId);

            cmd.CommandText = "stig_rules_by_stigid";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader != null)
            {
                reader.Read();

                ruleId = (int)reader["id"];
                this.RuleIDNo.Text = (string)reader["ruleidno"];
                this.Weight.Text = System.Convert.ToString((int)reader["weight"]);
                this.Version.Text = (string)reader["version"];
                this.RuleDescr.Text = (string)reader["descr"];
            }

            reader.Close();
            newConn.Close();

            this.populateCheckDetails(ruleId);
            this.populateFixDetails(ruleId);
        }

        private void populateCheckDetails(int ruleId)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection newConn = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            newConn.Open();

            cmd.Connection = newConn;
            cmd.Parameters.AddWithValue("ruleid", ruleId);

            cmd.CommandText = "stig_check_rules_by_ruleid";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            MySqlDataReader reader = cmd.ExecuteReader();

            if(reader != null)
            {
                reader.Read();
                CheckIDNo.Text = (string)reader["checksystemno"];
                CheckDescr.Text = (string)reader["descr"];
                UICheck.Text = (string)reader["uichecktext"];
                CLICheck.Text = (string)reader["clichecktext"];
            }

            reader.Close();
            newConn.Close();
            
        }

        private void populateFixDetails(int ruleId)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection newConn = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            newConn.Open();

            cmd.Connection = newConn;
            cmd.Parameters.AddWithValue("ruleid", ruleId);

            cmd.CommandText = "stig_fix_rules_by_ruleid";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader != null)
            {
                reader.Read();
                UIFix.Text = (string)reader["uifixtext"];
                CLIFix.Text = (string)reader["clifixtext"];
                FixIDNo.Text = (string)reader["fixidno"];
                FixDescr.Text = (string)reader["descr"];

                UIFix.Text.Trim();
                CLICheck.Text.Trim();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.TextBox();
            this.Target = new System.Windows.Forms.TextBox();
            this.Description = new System.Windows.Forms.TextBox();
            this.GroupIDNo = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CheckIDNo = new System.Windows.Forms.TextBox();
            this.CheckDescr = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.UICheck = new System.Windows.Forms.TextBox();
            this.CLICheck = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.FixDescr = new System.Windows.Forms.TextBox();
            this.FixIDNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.UIFix = new System.Windows.Forms.TextBox();
            this.CLIFix = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.RuleDescr = new System.Windows.Forms.TextBox();
            this.Version = new System.Windows.Forms.TextBox();
            this.Weight = new System.Windows.Forms.TextBox();
            this.Severity = new System.Windows.Forms.TextBox();
            this.RuleIDNo = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Title);
            this.groupBox1.Controls.Add(this.Target);
            this.groupBox1.Controls.Add(this.Description);
            this.groupBox1.Controls.Add(this.GroupIDNo);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox1.Size = new System.Drawing.Size(748, 106);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "Title";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(234, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Group ID Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(346, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(502, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Target";
            // 
            // Title
            // 
            this.Title.Location = new System.Drawing.Point(8, 38);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(220, 23);
            this.Title.TabIndex = 3;
            // 
            // Target
            // 
            this.Target.Location = new System.Drawing.Point(502, 34);
            this.Target.Multiline = true;
            this.Target.Name = "Target";
            this.Target.Size = new System.Drawing.Size(241, 54);
            this.Target.TabIndex = 2;
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(346, 38);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(150, 23);
            this.Description.TabIndex = 1;
            // 
            // GroupIDNo
            // 
            this.GroupIDNo.Location = new System.Drawing.Point(234, 38);
            this.GroupIDNo.Name = "GroupIDNo";
            this.GroupIDNo.Size = new System.Drawing.Size(106, 23);
            this.GroupIDNo.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.CheckIDNo);
            this.groupBox2.Controls.Add(this.CheckDescr);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.UICheck);
            this.groupBox2.Controls.Add(this.CLICheck);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(10, 243);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox2.Size = new System.Drawing.Size(374, 278);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Check details";
            // 
            // CheckIDNo
            // 
            this.CheckIDNo.Location = new System.Drawing.Point(10, 35);
            this.CheckIDNo.Name = "CheckIDNo";
            this.CheckIDNo.Size = new System.Drawing.Size(111, 23);
            this.CheckIDNo.TabIndex = 11;
            // 
            // CheckDescr
            // 
            this.CheckDescr.Location = new System.Drawing.Point(8, 77);
            this.CheckDescr.Multiline = true;
            this.CheckDescr.Name = "CheckDescr";
            this.CheckDescr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CheckDescr.Size = new System.Drawing.Size(113, 149);
            this.CheckDescr.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(125, 19);
            this.label8.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 15);
            this.label8.TabIndex = 6;
            this.label8.Text = "CLI";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(125, 124);
            this.label7.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "UI";
            // 
            // UICheck
            // 
            this.UICheck.Location = new System.Drawing.Point(125, 140);
            this.UICheck.Margin = new System.Windows.Forms.Padding(1);
            this.UICheck.Multiline = true;
            this.UICheck.Name = "UICheck";
            this.UICheck.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UICheck.Size = new System.Drawing.Size(238, 86);
            this.UICheck.TabIndex = 3;
            // 
            // CLICheck
            // 
            this.CLICheck.Location = new System.Drawing.Point(125, 35);
            this.CLICheck.Margin = new System.Windows.Forms.Padding(1);
            this.CLICheck.Multiline = true;
            this.CLICheck.Name = "CLICheck";
            this.CLICheck.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CLICheck.Size = new System.Drawing.Size(239, 86);
            this.CLICheck.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 231);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source Code Mgmt Location";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 247);
            this.textBox1.Margin = new System.Windows.Forms.Padding(1);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(350, 23);
            this.textBox1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.FixDescr);
            this.groupBox3.Controls.Add(this.FixIDNo);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.UIFix);
            this.groupBox3.Controls.Add(this.CLIFix);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Location = new System.Drawing.Point(386, 243);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox3.Size = new System.Drawing.Size(372, 278);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fix details";
            // 
            // FixDescr
            // 
            this.FixDescr.Location = new System.Drawing.Point(4, 77);
            this.FixDescr.Multiline = true;
            this.FixDescr.Name = "FixDescr";
            this.FixDescr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FixDescr.Size = new System.Drawing.Size(108, 149);
            this.FixDescr.TabIndex = 9;
            // 
            // FixIDNo
            // 
            this.FixIDNo.Location = new System.Drawing.Point(4, 35);
            this.FixIDNo.Name = "FixIDNo";
            this.FixIDNo.Size = new System.Drawing.Size(108, 23);
            this.FixIDNo.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(116, 19);
            this.label10.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 15);
            this.label10.TabIndex = 7;
            this.label10.Text = "CLI";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(116, 124);
            this.label9.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 15);
            this.label9.TabIndex = 6;
            this.label9.Text = "UI";
            // 
            // UIFix
            // 
            this.UIFix.Location = new System.Drawing.Point(116, 140);
            this.UIFix.Margin = new System.Windows.Forms.Padding(1);
            this.UIFix.Multiline = true;
            this.UIFix.Name = "UIFix";
            this.UIFix.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UIFix.Size = new System.Drawing.Size(251, 86);
            this.UIFix.TabIndex = 5;
            // 
            // CLIFix
            // 
            this.CLIFix.Location = new System.Drawing.Point(116, 35);
            this.CLIFix.Margin = new System.Windows.Forms.Padding(1);
            this.CLIFix.Multiline = true;
            this.CLIFix.Name = "CLIFix";
            this.CLIFix.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CLIFix.Size = new System.Drawing.Size(251, 86);
            this.CLIFix.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 231);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Source Code Mgmt Location";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 247);
            this.textBox2.Margin = new System.Windows.Forms.Padding(1);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(339, 23);
            this.textBox2.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.RuleDescr);
            this.groupBox4.Controls.Add(this.Version);
            this.groupBox4.Controls.Add(this.Weight);
            this.groupBox4.Controls.Add(this.Severity);
            this.groupBox4.Controls.Add(this.RuleIDNo);
            this.groupBox4.Location = new System.Drawing.Point(10, 118);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox4.Size = new System.Drawing.Size(748, 123);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Rule details";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(130, 63);
            this.label15.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 15);
            this.label15.TabIndex = 11;
            this.label15.Text = "Version";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 63);
            this.label14.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 15);
            this.label14.TabIndex = 10;
            this.label14.Text = "Weight";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(260, 13);
            this.label13.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 15);
            this.label13.TabIndex = 9;
            this.label13.Text = "Description";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(130, 22);
            this.label12.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 15);
            this.label12.TabIndex = 8;
            this.label12.Text = "Severity";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 22);
            this.label11.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 15);
            this.label11.TabIndex = 7;
            this.label11.Text = "ID Number";
            // 
            // RuleDescr
            // 
            this.RuleDescr.Location = new System.Drawing.Point(260, 29);
            this.RuleDescr.Multiline = true;
            this.RuleDescr.Name = "RuleDescr";
            this.RuleDescr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RuleDescr.Size = new System.Drawing.Size(477, 72);
            this.RuleDescr.TabIndex = 4;
            // 
            // Version
            // 
            this.Version.Location = new System.Drawing.Point(130, 78);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(110, 23);
            this.Version.TabIndex = 3;
            // 
            // Weight
            // 
            this.Weight.Location = new System.Drawing.Point(4, 78);
            this.Weight.Name = "Weight";
            this.Weight.Size = new System.Drawing.Size(110, 23);
            this.Weight.TabIndex = 2;
            // 
            // Severity
            // 
            this.Severity.Location = new System.Drawing.Point(130, 38);
            this.Severity.Name = "Severity";
            this.Severity.Size = new System.Drawing.Size(110, 23);
            this.Severity.TabIndex = 1;
            // 
            // RuleIDNo
            // 
            this.RuleIDNo.Location = new System.Drawing.Point(4, 38);
            this.RuleIDNo.Name = "RuleIDNo";
            this.RuleIDNo.Size = new System.Drawing.Size(110, 23);
            this.RuleIDNo.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(4, 19);
            this.label16.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 15);
            this.label16.TabIndex = 10;
            this.label16.Text = "ID Number";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(4, 61);
            this.label17.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 15);
            this.label17.TabIndex = 11;
            this.label17.Text = "Description";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 19);
            this.label18.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(63, 15);
            this.label18.TabIndex = 12;
            this.label18.Text = "ID number";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(10, 61);
            this.label19.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 15);
            this.label19.TabIndex = 13;
            this.label19.Text = "Description";
            // 
            // STIGDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 531);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "STIGDetails";
            this.Text = "Technical Implementation Guide";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox UICheck;
        private System.Windows.Forms.TextBox CLICheck;
        private System.Windows.Forms.TextBox UIFix;
        private System.Windows.Forms.TextBox CLIFix;
        private System.Windows.Forms.TextBox Title;
        private System.Windows.Forms.TextBox Target;
        private System.Windows.Forms.TextBox Description;
        private System.Windows.Forms.TextBox GroupIDNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox RuleDescr;
        private System.Windows.Forms.TextBox Version;
        private System.Windows.Forms.TextBox Weight;
        private System.Windows.Forms.TextBox Severity;
        private System.Windows.Forms.TextBox RuleIDNo;
        private System.Windows.Forms.TextBox CheckIDNo;
        private System.Windows.Forms.TextBox CheckDescr;
        private System.Windows.Forms.TextBox FixDescr;
        private System.Windows.Forms.TextBox FixIDNo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
    }
}