namespace HW2_Client
{
    partial class ClientForm1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.상세정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.다운로드ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSmall = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLarge = new System.Windows.Forms.ToolStripMenuItem();
            this.iml = new System.Windows.Forms.ImageList(this.components);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1.SuspendLayout();
            this.cms.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "다운로드 경로:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(575, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "PORT:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(77, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(492, 25);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(633, 24);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(116, 25);
            this.textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(159, 66);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(590, 25);
            this.textBox3.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(104, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 41);
            this.button1.TabIndex = 6;
            this.button1.Text = "서버연결";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(327, 119);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 41);
            this.button2.TabIndex = 6;
            this.button2.Text = "경로설정";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(548, 119);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(132, 41);
            this.button3.TabIndex = 6;
            this.button3.Text = "폴더열기";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Location = new System.Drawing.Point(0, 166);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(781, 413);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.ContextMenuStrip = this.cms;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.LargeImageList = this.iml;
            this.listView1.Location = new System.Drawing.Point(170, 21);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(608, 389);
            this.listView1.SmallImageList = this.iml;
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // cms
            // 
            this.cms.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.상세정보ToolStripMenuItem,
            this.다운로드ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.mnuDetail,
            this.mnuList,
            this.mnuSmall,
            this.mnuLarge});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(154, 154);
            // 
            // 상세정보ToolStripMenuItem
            // 
            this.상세정보ToolStripMenuItem.Name = "상세정보ToolStripMenuItem";
            this.상세정보ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.상세정보ToolStripMenuItem.Text = "상세정보";
            this.상세정보ToolStripMenuItem.Click += new System.EventHandler(this.상세정보ToolStripMenuItem_Click);
            // 
            // 다운로드ToolStripMenuItem
            // 
            this.다운로드ToolStripMenuItem.Name = "다운로드ToolStripMenuItem";
            this.다운로드ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.다운로드ToolStripMenuItem.Text = "다운로드";
            this.다운로드ToolStripMenuItem.Click += new System.EventHandler(this.다운로드ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(150, 6);
            // 
            // mnuDetail
            // 
            this.mnuDetail.Name = "mnuDetail";
            this.mnuDetail.Size = new System.Drawing.Size(153, 24);
            this.mnuDetail.Text = "자세히";
            this.mnuDetail.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // mnuList
            // 
            this.mnuList.Name = "mnuList";
            this.mnuList.Size = new System.Drawing.Size(153, 24);
            this.mnuList.Text = "간단히";
            this.mnuList.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // mnuSmall
            // 
            this.mnuSmall.Name = "mnuSmall";
            this.mnuSmall.Size = new System.Drawing.Size(153, 24);
            this.mnuSmall.Text = "작은아이콘";
            this.mnuSmall.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // mnuLarge
            // 
            this.mnuLarge.Name = "mnuLarge";
            this.mnuLarge.Size = new System.Drawing.Size(153, 24);
            this.mnuLarge.Text = "큰아이콘";
            this.mnuLarge.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // iml
            // 
            this.iml.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml.ImageStream")));
            this.iml.TransparentColor = System.Drawing.Color.Transparent;
            this.iml.Images.SetKeyName(0, "folder.png");
            this.iml.Images.SetKeyName(1, "avi.png");
            this.iml.Images.SetKeyName(2, "image.png");
            this.iml.Images.SetKeyName(3, "music.png");
            this.iml.Images.SetKeyName(4, "text.png");
            this.iml.Images.SetKeyName(5, "temp.png");
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.iml;
            this.treeView1.Location = new System.Drawing.Point(3, 21);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(167, 389);
            this.treeView1.TabIndex = 8;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
            // 
            // ClientForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 577);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ClientForm1";
            this.Text = "File Manager - Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientForm1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.cms.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem 상세정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 다운로드ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuDetail;
        private System.Windows.Forms.ToolStripMenuItem mnuList;
        private System.Windows.Forms.ToolStripMenuItem mnuSmall;
        private System.Windows.Forms.ToolStripMenuItem mnuLarge;
        private System.Windows.Forms.ImageList iml;
    }
}

