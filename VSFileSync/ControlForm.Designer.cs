namespace Raydude.VSFileSync
{
    partial class ControlForm
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
            this.fastObjectListView1 = new BrightIdeasSoftware.FastObjectListView();
            this.lblProjectList = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnDeleteTracker = new System.Windows.Forms.Button();
            this.btnAddSolution = new System.Windows.Forms.Button();
            this.btnAddRemoteLocation = new System.Windows.Forms.Button();
            this.lblLocalPath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddProject = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkDoNotTrack = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // fastObjectListView1
            // 
            this.fastObjectListView1.Location = new System.Drawing.Point(27, 31);
            this.fastObjectListView1.Name = "fastObjectListView1";
            this.fastObjectListView1.ShowGroups = false;
            this.fastObjectListView1.Size = new System.Drawing.Size(552, 135);
            this.fastObjectListView1.TabIndex = 0;
            this.fastObjectListView1.UseCompatibleStateImageBehavior = false;
            this.fastObjectListView1.View = System.Windows.Forms.View.Details;
            this.fastObjectListView1.VirtualMode = true;
            // 
            // lblProjectList
            // 
            this.lblProjectList.AutoSize = true;
            this.lblProjectList.Location = new System.Drawing.Point(36, 12);
            this.lblProjectList.Name = "lblProjectList";
            this.lblProjectList.Size = new System.Drawing.Size(445, 13);
            this.lblProjectList.TabIndex = 1;
            this.lblProjectList.Text = "List of Solutions and Projects stored in the database (red indicates current Solu" +
    "tion not found)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(79, 193);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(500, 20);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(79, 220);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(500, 20);
            this.textBox2.TabIndex = 3;
            // 
            // btnDeleteTracker
            // 
            this.btnDeleteTracker.Location = new System.Drawing.Point(459, 246);
            this.btnDeleteTracker.Name = "btnDeleteTracker";
            this.btnDeleteTracker.Size = new System.Drawing.Size(120, 23);
            this.btnDeleteTracker.TabIndex = 4;
            this.btnDeleteTracker.Text = "Delete Entry";
            this.btnDeleteTracker.UseVisualStyleBackColor = true;
            // 
            // btnAddSolution
            // 
            this.btnAddSolution.Location = new System.Drawing.Point(76, 246);
            this.btnAddSolution.Name = "btnAddSolution";
            this.btnAddSolution.Size = new System.Drawing.Size(172, 23);
            this.btnAddSolution.TabIndex = 5;
            this.btnAddSolution.Text = "Add Local Solution";
            this.btnAddSolution.UseVisualStyleBackColor = true;
            // 
            // btnAddRemoteLocation
            // 
            this.btnAddRemoteLocation.Location = new System.Drawing.Point(76, 276);
            this.btnAddRemoteLocation.Name = "btnAddRemoteLocation";
            this.btnAddRemoteLocation.Size = new System.Drawing.Size(172, 23);
            this.btnAddRemoteLocation.TabIndex = 6;
            this.btnAddRemoteLocation.Text = "Add Remote Location";
            this.btnAddRemoteLocation.UseVisualStyleBackColor = true;
            // 
            // lblLocalPath
            // 
            this.lblLocalPath.AutoSize = true;
            this.lblLocalPath.Location = new System.Drawing.Point(19, 196);
            this.lblLocalPath.Name = "lblLocalPath";
            this.lblLocalPath.Size = new System.Drawing.Size(61, 13);
            this.lblLocalPath.TabIndex = 7;
            this.lblLocalPath.Text = "Local Path:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Remote Path:";
            // 
            // btnAddProject
            // 
            this.btnAddProject.Location = new System.Drawing.Point(254, 246);
            this.btnAddProject.Name = "btnAddProject";
            this.btnAddProject.Size = new System.Drawing.Size(137, 23);
            this.btnAddProject.TabIndex = 9;
            this.btnAddProject.Text = "Add Project Dir";
            this.btnAddProject.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(322, 319);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(126, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(474, 319);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkDoNotTrack
            // 
            this.chkDoNotTrack.AutoSize = true;
            this.chkDoNotTrack.Location = new System.Drawing.Point(257, 281);
            this.chkDoNotTrack.Name = "chkDoNotTrack";
            this.chkDoNotTrack.Size = new System.Drawing.Size(170, 17);
            this.chkDoNotTrack.TabIndex = 12;
            this.chkDoNotTrack.Text = "Do Not Track Solution/Project";
            this.chkDoNotTrack.UseVisualStyleBackColor = true;
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 355);
            this.Controls.Add(this.chkDoNotTrack);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAddProject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLocalPath);
            this.Controls.Add(this.btnAddRemoteLocation);
            this.Controls.Add(this.btnAddSolution);
            this.Controls.Add(this.btnDeleteTracker);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblProjectList);
            this.Controls.Add(this.fastObjectListView1);
            this.MinimumSize = new System.Drawing.Size(623, 393);
            this.Name = "ControlForm";
            this.Text = "VS File Sync Config";
            this.Load += new System.EventHandler(this.ControlForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView fastObjectListView1;
        private System.Windows.Forms.Label lblProjectList;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnDeleteTracker;
        private System.Windows.Forms.Button btnAddSolution;
        private System.Windows.Forms.Button btnAddRemoteLocation;
        private System.Windows.Forms.Label lblLocalPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddProject;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkDoNotTrack;
    }
}