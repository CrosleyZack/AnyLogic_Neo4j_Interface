namespace AnyLogic_Neo4j_Interface
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.title = new System.Windows.Forms.Label();
            this.description = new System.Windows.Forms.TextBox();
            this.alpFileUploadLbl = new System.Windows.Forms.Label();
            this.excelFileUploadLbl = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.alpFileUploadBtn = new System.Windows.Forms.Button();
            this.excelFileUploadBtn = new System.Windows.Forms.Button();
            this.alpFileSelectedViewer = new System.Windows.Forms.Label();
            this.exlFileSelectedViewer = new System.Windows.Forms.Label();
            this.submitBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.alpErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.excelErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.serverErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.outputWindow = new System.Windows.Forms.TextBox();
            this.chkbxClearGraph = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.alpErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.excelErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(12, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(589, 55);
            this.title.TabIndex = 1;
            this.title.Text = "Neo4j Database Populator";
            this.title.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // description
            // 
            this.description.BackColor = System.Drawing.SystemColors.Menu;
            this.description.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.description.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.description.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.description.Location = new System.Drawing.Point(22, 76);
            this.description.Multiline = true;
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Size = new System.Drawing.Size(616, 210);
            this.description.TabIndex = 2;
            this.description.Text = resources.GetString("description.Text");
            this.description.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // alpFileUploadLbl
            // 
            this.alpFileUploadLbl.AutoSize = true;
            this.alpFileUploadLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alpFileUploadLbl.Location = new System.Drawing.Point(16, 410);
            this.alpFileUploadLbl.Name = "alpFileUploadLbl";
            this.alpFileUploadLbl.Size = new System.Drawing.Size(210, 32);
            this.alpFileUploadLbl.TabIndex = 3;
            this.alpFileUploadLbl.Text = "Select ALP File";
            // 
            // excelFileUploadLbl
            // 
            this.excelFileUploadLbl.AutoSize = true;
            this.excelFileUploadLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.excelFileUploadLbl.Location = new System.Drawing.Point(16, 520);
            this.excelFileUploadLbl.Name = "excelFileUploadLbl";
            this.excelFileUploadLbl.Size = new System.Drawing.Size(226, 32);
            this.excelFileUploadLbl.TabIndex = 4;
            this.excelFileUploadLbl.Text = "Select Excel File";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // alpFileUploadBtn
            // 
            this.alpFileUploadBtn.Location = new System.Drawing.Point(273, 409);
            this.alpFileUploadBtn.Name = "alpFileUploadBtn";
            this.alpFileUploadBtn.Size = new System.Drawing.Size(204, 42);
            this.alpFileUploadBtn.TabIndex = 5;
            this.alpFileUploadBtn.Text = "Select";
            this.alpFileUploadBtn.UseVisualStyleBackColor = true;
            this.alpFileUploadBtn.Click += new System.EventHandler(this.alpFileUploadBtn_Click);
            // 
            // excelFileUploadBtn
            // 
            this.excelFileUploadBtn.Location = new System.Drawing.Point(273, 519);
            this.excelFileUploadBtn.Name = "excelFileUploadBtn";
            this.excelFileUploadBtn.Size = new System.Drawing.Size(204, 42);
            this.excelFileUploadBtn.TabIndex = 6;
            this.excelFileUploadBtn.Text = "Select";
            this.excelFileUploadBtn.UseVisualStyleBackColor = true;
            this.excelFileUploadBtn.Click += new System.EventHandler(this.excelFileUploadBtn_Click);
            // 
            // alpFileSelectedViewer
            // 
            this.alpFileSelectedViewer.AutoSize = true;
            this.alpFileSelectedViewer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alpFileSelectedViewer.Location = new System.Drawing.Point(56, 451);
            this.alpFileSelectedViewer.Name = "alpFileSelectedViewer";
            this.alpFileSelectedViewer.Size = new System.Drawing.Size(170, 25);
            this.alpFileSelectedViewer.TabIndex = 8;
            this.alpFileSelectedViewer.Text = "No File Selected...";
            this.alpFileSelectedViewer.Click += new System.EventHandler(this.alpFileSelectedViewer_Click);
            // 
            // exlFileSelectedViewer
            // 
            this.exlFileSelectedViewer.AutoSize = true;
            this.exlFileSelectedViewer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exlFileSelectedViewer.Location = new System.Drawing.Point(57, 564);
            this.exlFileSelectedViewer.Name = "exlFileSelectedViewer";
            this.exlFileSelectedViewer.Size = new System.Drawing.Size(170, 25);
            this.exlFileSelectedViewer.TabIndex = 10;
            this.exlFileSelectedViewer.Text = "No File Selected...";
            this.exlFileSelectedViewer.Click += new System.EventHandler(this.exlFileSelectedViewer_Click);
            // 
            // submitBtn
            // 
            this.submitBtn.Location = new System.Drawing.Point(434, 822);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(204, 42);
            this.submitBtn.TabIndex = 11;
            this.submitBtn.Text = "Submit";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 299);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 32);
            this.label1.TabIndex = 12;
            this.label1.Text = "Neo4j Server Address";
            // 
            // txtServerAddress
            // 
            this.txtServerAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerAddress.Location = new System.Drawing.Point(62, 337);
            this.txtServerAddress.Name = "txtServerAddress";
            this.txtServerAddress.Size = new System.Drawing.Size(576, 30);
            this.txtServerAddress.TabIndex = 13;
            this.txtServerAddress.Leave += new System.EventHandler(this.txtServerAddress_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.Location = new System.Drawing.Point(16, 680);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 32);
            this.label2.TabIndex = 14;
            this.label2.Text = "Output";
            // 
            // alpErrorProvider
            // 
            this.alpErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.alpErrorProvider.ContainerControl = this;
            this.alpErrorProvider.RightToLeft = true;
            // 
            // excelErrorProvider
            // 
            this.excelErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.excelErrorProvider.ContainerControl = this;
            this.excelErrorProvider.RightToLeft = true;
            // 
            // serverErrorProvider
            // 
            this.serverErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.serverErrorProvider.ContainerControl = this;
            this.serverErrorProvider.RightToLeft = true;
            // 
            // outputWindow
            // 
            this.outputWindow.Location = new System.Drawing.Point(22, 732);
            this.outputWindow.Multiline = true;
            this.outputWindow.Name = "outputWindow";
            this.outputWindow.ReadOnly = true;
            this.outputWindow.Size = new System.Drawing.Size(616, 84);
            this.outputWindow.TabIndex = 15;
            // 
            // chkbxClearGraph
            // 
            this.chkbxClearGraph.AutoSize = true;
            this.chkbxClearGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbxClearGraph.Location = new System.Drawing.Point(22, 631);
            this.chkbxClearGraph.Name = "chkbxClearGraph";
            this.chkbxClearGraph.Size = new System.Drawing.Size(310, 29);
            this.chkbxClearGraph.TabIndex = 16;
            this.chkbxClearGraph.Text = "Delete Existing Graph Contents";
            this.chkbxClearGraph.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 889);
            this.Controls.Add(this.chkbxClearGraph);
            this.Controls.Add(this.outputWindow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServerAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.exlFileSelectedViewer);
            this.Controls.Add(this.alpFileSelectedViewer);
            this.Controls.Add(this.excelFileUploadBtn);
            this.Controls.Add(this.alpFileUploadBtn);
            this.Controls.Add(this.excelFileUploadLbl);
            this.Controls.Add(this.alpFileUploadLbl);
            this.Controls.Add(this.description);
            this.Controls.Add(this.title);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.alpErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.excelErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.TextBox description;
        private System.Windows.Forms.Label alpFileUploadLbl;
        private System.Windows.Forms.Label excelFileUploadLbl;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button alpFileUploadBtn;
        private System.Windows.Forms.Button excelFileUploadBtn;
        private System.Windows.Forms.Label alpFileSelectedViewer;
        private System.Windows.Forms.Label exlFileSelectedViewer;
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider alpErrorProvider;
        private System.Windows.Forms.ErrorProvider excelErrorProvider;
        private System.Windows.Forms.ErrorProvider serverErrorProvider;
        private System.Windows.Forms.TextBox outputWindow;
        private System.Windows.Forms.CheckBox chkbxClearGraph;
    }
}

