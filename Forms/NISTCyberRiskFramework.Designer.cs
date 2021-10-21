
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using FrameworksViewerApp.Models;
using FrameworksViewerApp.Utilities;

namespace FrameworksViewerApp
{
    partial class NISTCyberRiskFramework
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private MySqlConnection conn = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            this.conn.Close();
            base.Dispose(disposing);
            
        }

        protected override void OnLoad(EventArgs e)
        {
            this.conn = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            this.conn.Open();

            this.populateGridView();
            base.OnLoad(e);

            this.RMFTree.DoubleClick += RMFTree_DoubleClick;
            this.STIGTree.DoubleClick += STIGTree_DoubleClick;
            
        }

        private void STIGTree_DoubleClick(object sender, EventArgs e)
        {
            STIGDetails viewDetails = new STIGDetails();
            STIGModel selectedModel = null;


            if (this.STIGTree.SelectedNode.Tag != null)
            {
                selectedModel = (STIGModel)this.STIGTree.SelectedNode.Tag;


                viewDetails.ShowSTIGDetails(selectedModel);
                viewDetails.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                viewDetails.Show();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please click on a STIG item in the list.", "You clicked a Parent item.", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        private void RMFTree_DoubleClick(object sender, EventArgs e)
        {
            RMFModel model = (RMFModel)this.RMFTree.SelectedNode.Tag;

            RMFDetails viewDetails = new RMFDetails();
            viewDetails.ShowRMFDetails(model.ID);
            viewDetails.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            viewDetails.Show();
        }

        private void populateGridView()
        {
            List<NISTCyberSecurityFrameworkModel> cyberItems = new List<NISTCyberSecurityFrameworkModel>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = this.conn;
            cmd.CommandText = SQLStatements.NISTCyberSelectAll;
            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                //id, functionname, categoryname, categorynameid, categorynamedescr, subcategoryid, subcategorydescr
                NISTCyberSecurityFrameworkModel item = new NISTCyberSecurityFrameworkModel();
                item.id = (int)reader["id"];
                item.subcategoryid = (string)reader["subcategoryid"];
                item.subcategorydescr = (string)reader["subcategorydescr"];
                item.functionname = (string)reader["functionname"];
                item.categoryname = (string)reader["categoryname"];
                item.categorynameid = (string)reader["categorynameid"];
                item.categorynamedescr = (string)reader["categorynamedescr"];
                cyberItems.Add(item);

                System.Windows.Forms.TreeNode parentNode = NISTCyberTree.Nodes.Add(item.functionname + " - " + item.categoryname + " - " + item.subcategoryid);
                parentNode.Tag = item;
                parentNode.Nodes.Add(item.subcategorydescr).Tag = item;
            }

            NISTCyberTree.DoubleClick += NISTCyberTree_DoubleClick;


        }

        private void NISTCyberTree_DoubleClick(object sender, EventArgs e)
        {
            PleaseWaitForm loadingMsg = new PleaseWaitForm();

            loadingMsg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            loadingMsg.Show();
            loadingMsg.Refresh();

            System.Windows.Forms.TreeNode selectedNode;


            selectedNode = this.NISTCyberTree.SelectedNode;
            NISTCyberSecurityFrameworkModel selectedItem = (NISTCyberSecurityFrameworkModel)selectedNode.Tag;

            this.FunctionName.Text = selectedItem.functionname; 
            this.CategoryID.Text = selectedItem.categorynameid;
            this.CategoryName.Text = selectedItem.categoryname;
            this.SubcategoryID.Text = selectedItem.subcategoryid;
            this.SubcategoryDescription.Text = selectedItem.subcategorydescr;

            this.clearTreeNodes();

            this.populateNISTControlList(selectedItem.id,5,this.NIST800Rev5Tree);
            this.populateNISTControlList(selectedItem.id, 4, this.NIST800Rev4Tree);
            this.populateRMFList(selectedItem.id);
            
            loadingMsg.Close();
        }

        

        private void clearTreeNodes()
        {
            
            this.NIST800Rev4Tree.Nodes.Clear();
            this.NIST800Rev5Tree.Nodes.Clear();
            this.FedRAMPTree.Nodes.Clear();
            this.RMFTree.Nodes.Clear();
            this.STIGTree.Nodes.Clear();
            this.CCITree.Nodes.Clear();
        }

        private void populateNISTControlList(int cyberId, int revisionNumber, System.Windows.Forms.TreeView targetTree)
        {
            CCITree.Nodes.Clear();
            
            if(this.conn.State == System.Data.ConnectionState.Open)
            {
                this.conn.Close();
                this.conn.Open();
            }

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = this.conn;
            cmd.CommandText = "nistcyberrisk_nistcontrol_details";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlParameter cyberIdParam = HelperFunctions.getSqlParameter(System.Data.DbType.Int32, System.Data.ParameterDirection.Input, "nistcyberriskid", cyberId);
            MySqlParameter revNoParam = HelperFunctions.getSqlParameter(System.Data.DbType.Int32, System.Data.ParameterDirection.Input, "revisionno", revisionNumber);
            cmd.Parameters.Add(cyberIdParam);
            cmd.Parameters.Add(revNoParam);

            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                NIST80053Model item = new NIST80053Model();

                item.id = (int)reader["id"];
                item.number = (string)reader["number"];

                if ((string)reader["title"] == String.Empty)
                {
                    item.title = (string)reader["nistfamilyname"];
                }
                else
                {
                    item.title = (string)reader["title"];
                }

                System.Windows.Forms.TreeNode parentNode = targetTree.Nodes.Add(item.number + " - " + item.title);
                parentNode.Tag = item;

                this.populateCCIList(item.id);
                this.populateFedRAMPList(item.id);
            }

           
        }

        private void populateRMFList(int cyberId)
        {
            

            if (this.conn.State == System.Data.ConnectionState.Open)
            {
                this.conn.Close();
                this.conn.Open();
            }

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = this.conn;
            cmd.CommandText = "nistcyberrisk_rmf_details";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlParameter cyberIdParam = HelperFunctions.getSqlParameter(System.Data.DbType.Int32, System.Data.ParameterDirection.Input, "cyberid", cyberId);
            cmd.Parameters.Add(cyberIdParam);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                RMFModel item = new RMFModel();

                item.ID = (int)reader["id"];
                item.TaskID = (string)reader["taskid"];
                item.TaskDescription = (string)reader["taskdescr"];

                this.RMFTree.Nodes.Add(item.TaskID).Tag = item;

               
            }

            

        }

        private void populateFedRAMPList(int nistId)
        {
           
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection newConn = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            newConn.Open();

            cmd.Connection = newConn;

            cmd.CommandText = "fedramp_nist_details";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlParameter nistIdParam = HelperFunctions.getSqlParameter(System.Data.DbType.Int32, System.Data.ParameterDirection.Input, "nistid", nistId);
            cmd.Parameters.Add(nistIdParam);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                FedRAMPModel item = new FedRAMPModel();

                item.id = (int)reader["id"];
                item.controlname = (string)reader["controlname"];
                item.controldescr = (string)reader["controldescr"];
                item.supplementalguidance = (string)reader["supplementguidance"];

                System.Windows.Forms.TreeNode parentNode = FedRAMPTree.Nodes.Add(System.Convert.ToString(item.id), item.controlname);
                parentNode.Tag = item;
                parentNode.Nodes.Add(item.controldescr).Tag = item;
                parentNode.Nodes.Add(item.supplementalguidance).Tag = item;

                this.populateSTIGList(item.id);
            }

            reader.Close();
            newConn.Close();


        }


        private void populateCCIList(int nistId)
        {
           
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection newConn = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            newConn.Open();

            cmd.Connection = newConn;

            cmd.CommandText = "cci_nist_details";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlParameter nistIdParam = HelperFunctions.getSqlParameter(System.Data.DbType.Int32, System.Data.ParameterDirection.Input, "nistid", nistId);
            cmd.Parameters.Add(nistIdParam);

            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                CCIModel item = new CCIModel();

                item.id = (int)reader["id"];
                item.cciidno = (string)reader["cciidno"];
                item.definition = (string)reader["definition"];

                System.Windows.Forms.TreeNode parentNode = this.CCITree.Nodes.Add(item.cciidno);
                parentNode.Tag = item;

                parentNode.Nodes.Add(item.definition).Tag = item;

                this.populateSTIGList(item.id);
            }

            reader.Close();

            
            newConn.Close();
        }

        private void populateSTIGList(int cciId)
        {

            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection newConn = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            newConn.Open();

            cmd.Connection = newConn;

            cmd.CommandText = "stig_cci_details";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlParameter cciIdParam = HelperFunctions.getSqlParameter(System.Data.DbType.Int32, System.Data.ParameterDirection.Input, "cciid", cciId);
            cmd.Parameters.Add(cciIdParam);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                STIGModel item = new STIGModel();

                item.id = (int)reader["id"];
                item.groupidno = (string)reader["groupidno"];
                item.descr = (string)reader["descr"];
                item.title = (string)reader["title"];
                item.cciid = (int)reader["cciid"];
                item.target = (string)reader["target"];

                int count = this.STIGTree.Nodes.Count;

                System.Windows.Forms.TreeNode parentNode = null;

                for (int i =0; i < count; i++)
                {
                    if(this.STIGTree.Nodes[i].Text == item.target)
                    {
                        parentNode = this.STIGTree.Nodes[i];
                        i = count;
                    }
                }

                if (parentNode == null)
                {
                    parentNode = this.STIGTree.Nodes.Add(item.target);
                }

                parentNode.Nodes.Add(item.groupidno).Tag = item;
                
            }

            reader.Close();

            newConn.Close();


        }

       

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.NISTRev5Tab = new System.Windows.Forms.TabPage();
            this.NIST800Rev5Tree = new System.Windows.Forms.TreeView();
            this.NISTRev4Tab = new System.Windows.Forms.TabPage();
            this.NIST800Rev4Tree = new System.Windows.Forms.TreeView();
            this.RMFTab = new System.Windows.Forms.TabPage();
            this.RMFTree = new System.Windows.Forms.TreeView();
            this.FedRAMPTab = new System.Windows.Forms.TabPage();
            this.FedRAMPTree = new System.Windows.Forms.TreeView();
            this.CCITab = new System.Windows.Forms.TabPage();
            this.CCITree = new System.Windows.Forms.TreeView();
            this.STIGTab = new System.Windows.Forms.TabPage();
            this.STIGTree = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SubcategoryDescription = new System.Windows.Forms.TextBox();
            this.SubcategoryID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CategoryName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CategoryID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FunctionName = new System.Windows.Forms.TextBox();
            this.NISTCyberTree = new System.Windows.Forms.TreeView();
            this.tabControl1.SuspendLayout();
            this.NISTRev5Tab.SuspendLayout();
            this.NISTRev4Tab.SuspendLayout();
            this.RMFTab.SuspendLayout();
            this.FedRAMPTab.SuspendLayout();
            this.CCITab.SuspendLayout();
            this.STIGTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.NISTRev5Tab);
            this.tabControl1.Controls.Add(this.NISTRev4Tab);
            this.tabControl1.Controls.Add(this.RMFTab);
            this.tabControl1.Controls.Add(this.FedRAMPTab);
            this.tabControl1.Controls.Add(this.CCITab);
            this.tabControl1.Controls.Add(this.STIGTab);
            this.tabControl1.Location = new System.Drawing.Point(364, 192);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(639, 395);
            this.tabControl1.TabIndex = 1;
            // 
            // NISTRev5Tab
            // 
            this.NISTRev5Tab.Controls.Add(this.NIST800Rev5Tree);
            this.NISTRev5Tab.Location = new System.Drawing.Point(4, 24);
            this.NISTRev5Tab.Name = "NISTRev5Tab";
            this.NISTRev5Tab.Size = new System.Drawing.Size(631, 367);
            this.NISTRev5Tab.TabIndex = 6;
            this.NISTRev5Tab.Text = "NIST 800-53 Rev 5";
            this.NISTRev5Tab.UseVisualStyleBackColor = true;
            // 
            // NIST800Rev5Tree
            // 
            this.NIST800Rev5Tree.Location = new System.Drawing.Point(20, 16);
            this.NIST800Rev5Tree.Name = "NIST800Rev5Tree";
            this.NIST800Rev5Tree.Size = new System.Drawing.Size(590, 331);
            this.NIST800Rev5Tree.TabIndex = 0;
            // 
            // NISTRev4Tab
            // 
            this.NISTRev4Tab.Controls.Add(this.NIST800Rev4Tree);
            this.NISTRev4Tab.Location = new System.Drawing.Point(4, 24);
            this.NISTRev4Tab.Name = "NISTRev4Tab";
            this.NISTRev4Tab.Padding = new System.Windows.Forms.Padding(3);
            this.NISTRev4Tab.Size = new System.Drawing.Size(631, 367);
            this.NISTRev4Tab.TabIndex = 0;
            this.NISTRev4Tab.Text = "NIST SP 800-53 Rev 4";
            this.NISTRev4Tab.UseVisualStyleBackColor = true;
            // 
            // NIST800Rev4Tree
            // 
            this.NIST800Rev4Tree.Location = new System.Drawing.Point(15, 14);
            this.NIST800Rev4Tree.Name = "NIST800Rev4Tree";
            this.NIST800Rev4Tree.Size = new System.Drawing.Size(593, 337);
            this.NIST800Rev4Tree.TabIndex = 1;
            // 
            // RMFTab
            // 
            this.RMFTab.Controls.Add(this.RMFTree);
            this.RMFTab.Location = new System.Drawing.Point(4, 24);
            this.RMFTab.Name = "RMFTab";
            this.RMFTab.Padding = new System.Windows.Forms.Padding(3);
            this.RMFTab.Size = new System.Drawing.Size(631, 367);
            this.RMFTab.TabIndex = 1;
            this.RMFTab.Text = "RMF";
            this.RMFTab.UseVisualStyleBackColor = true;
            // 
            // RMFTree
            // 
            this.RMFTree.Location = new System.Drawing.Point(22, 13);
            this.RMFTree.Name = "RMFTree";
            this.RMFTree.Size = new System.Drawing.Size(586, 341);
            this.RMFTree.TabIndex = 1;
            // 
            // FedRAMPTab
            // 
            this.FedRAMPTab.Controls.Add(this.FedRAMPTree);
            this.FedRAMPTab.Location = new System.Drawing.Point(4, 24);
            this.FedRAMPTab.Name = "FedRAMPTab";
            this.FedRAMPTab.Padding = new System.Windows.Forms.Padding(3);
            this.FedRAMPTab.Size = new System.Drawing.Size(631, 367);
            this.FedRAMPTab.TabIndex = 2;
            this.FedRAMPTab.Text = "FedRAMP";
            this.FedRAMPTab.UseVisualStyleBackColor = true;
            // 
            // FedRAMPTree
            // 
            this.FedRAMPTree.Location = new System.Drawing.Point(21, 15);
            this.FedRAMPTree.Margin = new System.Windows.Forms.Padding(1);
            this.FedRAMPTree.Name = "FedRAMPTree";
            this.FedRAMPTree.Size = new System.Drawing.Size(584, 279);
            this.FedRAMPTree.TabIndex = 1;
            // 
            // CCITab
            // 
            this.CCITab.Controls.Add(this.CCITree);
            this.CCITab.Location = new System.Drawing.Point(4, 24);
            this.CCITab.Name = "CCITab";
            this.CCITab.Padding = new System.Windows.Forms.Padding(3);
            this.CCITab.Size = new System.Drawing.Size(631, 367);
            this.CCITab.TabIndex = 4;
            this.CCITab.Text = "CCI";
            this.CCITab.UseVisualStyleBackColor = true;
            // 
            // CCITree
            // 
            this.CCITree.Location = new System.Drawing.Point(16, 16);
            this.CCITree.Name = "CCITree";
            this.CCITree.Size = new System.Drawing.Size(590, 336);
            this.CCITree.TabIndex = 1;
            // 
            // STIGTab
            // 
            this.STIGTab.Controls.Add(this.STIGTree);
            this.STIGTab.Location = new System.Drawing.Point(4, 24);
            this.STIGTab.Name = "STIGTab";
            this.STIGTab.Padding = new System.Windows.Forms.Padding(3);
            this.STIGTab.Size = new System.Drawing.Size(631, 367);
            this.STIGTab.TabIndex = 5;
            this.STIGTab.Text = "STIGs";
            this.STIGTab.UseVisualStyleBackColor = true;
            // 
            // STIGTree
            // 
            this.STIGTree.Location = new System.Drawing.Point(14, 14);
            this.STIGTree.Name = "STIGTree";
            this.STIGTree.Size = new System.Drawing.Size(585, 336);
            this.STIGTree.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.SubcategoryDescription);
            this.groupBox1.Controls.Add(this.SubcategoryID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.CategoryName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.CategoryID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.FunctionName);
            this.groupBox1.Location = new System.Drawing.Point(364, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(635, 173);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(230, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Subcategory Description";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(63, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Subcategory ID";
            // 
            // SubcategoryDescription
            // 
            this.SubcategoryDescription.Location = new System.Drawing.Point(231, 107);
            this.SubcategoryDescription.Multiline = true;
            this.SubcategoryDescription.Name = "SubcategoryDescription";
            this.SubcategoryDescription.Size = new System.Drawing.Size(340, 56);
            this.SubcategoryDescription.TabIndex = 7;
            // 
            // SubcategoryID
            // 
            this.SubcategoryID.Location = new System.Drawing.Point(64, 107);
            this.SubcategoryID.Name = "SubcategoryID";
            this.SubcategoryID.Size = new System.Drawing.Size(148, 23);
            this.SubcategoryID.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(390, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Category Name";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // CategoryName
            // 
            this.CategoryName.Location = new System.Drawing.Point(390, 37);
            this.CategoryName.Name = "CategoryName";
            this.CategoryName.Size = new System.Drawing.Size(181, 23);
            this.CategoryName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Category ID";
            // 
            // CategoryID
            // 
            this.CategoryID.Location = new System.Drawing.Point(231, 37);
            this.CategoryID.Name = "CategoryID";
            this.CategoryID.Size = new System.Drawing.Size(133, 23);
            this.CategoryID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Function";
            // 
            // FunctionName
            // 
            this.FunctionName.Location = new System.Drawing.Point(64, 37);
            this.FunctionName.Name = "FunctionName";
            this.FunctionName.Size = new System.Drawing.Size(148, 23);
            this.FunctionName.TabIndex = 0;
            // 
            // NISTCyberTree
            // 
            this.NISTCyberTree.Location = new System.Drawing.Point(12, 13);
            this.NISTCyberTree.Name = "NISTCyberTree";
            this.NISTCyberTree.Size = new System.Drawing.Size(346, 574);
            this.NISTCyberTree.TabIndex = 3;
            // 
            // NISTCyberRiskFramework
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 606);
            this.Controls.Add(this.NISTCyberTree);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Name = "NISTCyberRiskFramework";
            this.Text = "NIST Cyber Security Framework Details";
            this.tabControl1.ResumeLayout(false);
            this.NISTRev5Tab.ResumeLayout(false);
            this.NISTRev4Tab.ResumeLayout(false);
            this.RMFTab.ResumeLayout(false);
            this.FedRAMPTab.ResumeLayout(false);
            this.CCITab.ResumeLayout(false);
            this.STIGTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage NISTRev4Tab;
        private System.Windows.Forms.TabPage RMFTab;
        private System.Windows.Forms.TabPage FedRAMPTab;
        private System.Windows.Forms.TabPage CCITab;
        private System.Windows.Forms.TabPage STIGTab;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox CategoryID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FunctionName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox CategoryName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SubcategoryID;
        private System.Windows.Forms.TextBox SubcategoryDescription;
        private System.Windows.Forms.TreeView FedRAMPTree;
        private System.Windows.Forms.TreeView NISTCyberTree;
        private System.Windows.Forms.TreeView NIST800Rev4Tree;
        private System.Windows.Forms.TreeView RMFTree;
        private System.Windows.Forms.TreeView CCITree;
        private System.Windows.Forms.TreeView STIGTree;
        private System.Windows.Forms.TabPage NISTRev5Tab;
        private System.Windows.Forms.TreeView NIST800Rev5Tree;
    }
}

