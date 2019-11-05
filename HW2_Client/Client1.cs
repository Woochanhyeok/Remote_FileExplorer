using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Diagnostics;
using ClassLibrary3;
using System.Runtime.Serialization.Formatters.Binary;

namespace HW2_Client
{
    public partial class ClientForm1 : Form
    {
        private NetworkStream m_networkstream;
        private TcpListener m_listener;
        private TcpClient m_client;

        private byte[] sendBuffer = new byte[1024 * 4];
        private byte[] readBuffer = new byte[1024 * 4];

        private bool m_bConnect = false;
        private bool m_bClientOn = false;

        private Thread m_thread;

        public int PORT;
        public string path;
        
        public Dir m_dirClass;
        public BeforeSelect m_beforeselectClass;
        public BeforeExpand m_beforeexpandClass;
        //-------------------------------------------

        public void Send()
        {
            this.m_networkstream.Write(this.sendBuffer, 0, this.sendBuffer.Length);
            this.m_networkstream.Flush();

            for (int i = 0; i < 1024 * 4; i++)
            {
                this.sendBuffer[i] = 0;
            }
        }

        public void Connect()
        {
            this.m_client = new TcpClient();
            PORT = Convert.ToInt32(this.textBox2.Text);
            try
            {
                this.m_client.Connect(this.textBox1.Text, PORT);
            }
            catch
            {
                MessageBox.Show("접속 에러");
                return;
            }
            this.m_bConnect = true;
            this.m_networkstream = this.m_client.GetStream();

        }



        public void RUN()                       //데이터 수신 메소드
        {
            int nRead = 0;
            m_bClientOn = true;

            while (this.m_bClientOn)
            {
                try
                {
                    nRead = 0;
                    nRead = this.m_networkstream.Read(readBuffer, 0, 1024 * 4);
                }
                catch
                {
                    this.m_bClientOn = false;
                    this.m_networkstream = null;
                }

                Packet packet = (Packet)Packet.Desserialize(this.readBuffer);

                switch ((int)packet.Type)
                {
                    case (int)PacketType.디렉토리:
                        {
                            this.m_dirClass =
                                (Dir)Packet.Desserialize(this.readBuffer);
                            treeView1.BeginInvoke((MethodInvoker)delegate
                            {
                                Browser_Load();                            //server에서 보내준 첫 디렉토리정보로 탐색기 실행
                            });
                            break;
                        }
                    case (int)PacketType.BeforeSelect:
                        {
                            this.m_beforeselectClass =
                                (BeforeSelect)Packet.Desserialize(this.readBuffer);
                            break;
                        }
                    case (int)PacketType.BeforeExpand:
                        {
                            this.m_beforeexpandClass =
                                (BeforeExpand)Packet.Desserialize(this.readBuffer);
                            break;
                        }
                }
            }

        }
        //---------------탐색기 관련 메소드----------------
        
        private void Browser_Load()
        {
            string[] Drv_list;
            TreeNode root;
            root = treeView1.Nodes.Add(m_dirClass.dir.FullName);         
            root.ImageIndex = 0;
            treeView1.SelectedNode = root;
            root.SelectedImageIndex = root.ImageIndex;
            root.Nodes.Add("");
        }


        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            BeforeSelect bs = new BeforeSelect();
            DirectoryInfo[] diarray;
            ListViewItem item;
            FileInfo[] fiarray;

            try
            {

                bs.Type = (int)PacketType.BeforeSelect;
                bs.dir = new DirectoryInfo(e.Node.FullPath);
                Packet.Serialize(bs).CopyTo(this.sendBuffer, 0);
                this.Send();

                Thread.Sleep(100);

                listView1.Items.Clear();

                diarray = m_beforeselectClass.diarray;
                foreach (DirectoryInfo tdis in diarray)
                {
                    item = listView1.Items.Add(tdis.Name);
                    item.SubItems.Add("");
                    item.SubItems.Add(tdis.LastWriteTime.ToString());
                    item.ImageIndex = 0;
                    item.Tag = "D";
                }

                fiarray = m_beforeselectClass.fiarray;
                foreach (FileInfo fis in fiarray)
                {
                    item = listView1.Items.Add(fis.Name);
                    item.SubItems.Add(fis.Length.ToString());
                    item.SubItems.Add(fis.LastWriteTime.ToString());
                    item.SubItems.Add(fis.LastAccessTime.ToString());
                    item.SubItems.Add(fis.CreationTime.ToString());
                    item.SubItems.Add(fis.FullName);
                    if (fis.Name.Contains(".avi"))
                        item.ImageIndex = 1;
                    else if (fis.Name.Contains(".png"))
                        item.ImageIndex = 2;
                    else if (fis.Name.Contains(".mp3"))
                        item.ImageIndex = 3;
                    else if (fis.Name.Contains(".txt"))
                        item.ImageIndex = 4;
                    else
                        item.ImageIndex = 5;
                    item.Tag = "F";
                }

                listView1.Columns.Add("파일명", 200, HorizontalAlignment.Left);
                listView1.Columns.Add("사이즈", 70, HorizontalAlignment.Left);
                listView1.Columns.Add("수정한날짜", 150, HorizontalAlignment.Left);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            BeforeExpand be = new BeforeExpand();
            DirectoryInfo[] di;
            TreeNode node;
            try
            {
                e.Node.Nodes.Clear();
                be.Type = (int)PacketType.BeforeExpand;
                be.path = e.Node.FullPath;
                Packet.Serialize(be).CopyTo(this.sendBuffer, 0);
                this.Send();

                Thread.Sleep(100);

                foreach (DirectoryInfo dirs in m_beforeexpandClass.diarray)
                {
                    node = e.Node.Nodes.Add(dirs.Name);
                    setPlus(node);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void setPlus(TreeNode node)
        {
            string path;
            DirectoryInfo dir;
            DirectoryInfo[] di;

            try
            {
                path = node.FullPath;
                dir = new DirectoryInfo(path);
                di = dir.GetDirectories();
                if (di.Length > 0)
                    node.Nodes.Add("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            OpenFiles();
        }

        private void mnuView_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            mnuDetail.Checked = false;
            mnuList.Checked = false;
            mnuSmall.Checked = false;
            mnuLarge.Checked = false;

            switch (item.Text)
            {
                case "자세히":
                    mnuDetail.Checked = true;
                    listView1.View = View.Details;
                    break;
                case "간단히":
                    mnuList.Checked = true;
                    listView1.View = View.List;
                    break;
                case "작은아이콘":
                    mnuSmall.Checked = true;
                    listView1.View = View.SmallIcon;
                    break;
                case "큰아이콘":
                    mnuLarge.Checked = true;
                    listView1.View = View.LargeIcon;
                    break;
            }
        }

        public void OpenFiles()
        {
            ListView.SelectedListViewItemCollection siList;
            siList = listView1.SelectedItems;

            foreach (ListViewItem item in siList)
            {
                OpenItem(item);
            }
        }
        public void OpenItem(ListViewItem item)
        {
            TreeNode node;
            TreeNode child;

            if (item.Tag.ToString() == "D")
            {
                node = treeView1.SelectedNode;
                node.Expand();

                child = node.FirstNode;

                while (child != null)
                {
                    if (child.Text == item.Text)
                    {
                        treeView1.SelectedNode = child;
                        treeView1.Focus();
                        break;
                    }
                    child = child.NextNode;
                }
            }
            else
            {
                ClientForm2 form2 = new ClientForm2(listView1.FocusedItem);
                form2.Show();
            }
        }



        //-------------------------------------------
        public ClientForm1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {          //경로 선택을 안 하면 에러 출력
                MessageBox.Show("경로를 선택해주세요",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            else
            {
                if (button1.Text == "서버연결")                         //서버연결 버튼 누르면
                {
                    Connect();
                    if (m_bConnect)
                    {
                        m_thread = new Thread(new ThreadStart(RUN));               //데이터 전송
                        m_thread.Start();

                        //treeView1.BeginInvoke((MethodInvoker)delegate
                        //{
                            //Browser_Load();
                            //MessageBox.Show(m_dirClass.dir.FullName);
                        //});

                        button1.Text = "서버끊기";
                        button1.ForeColor = Color.Red;

                    }
                }
                else
                {
                    if (m_client != null && m_networkstream != null)
                    {
                        this.m_client.Close();
                        this.m_networkstream.Close();
                    }
                    button1.Text = "서버연결";
                    button1.ForeColor = Color.Black;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb;
            fdb = new System.Windows.Forms.FolderBrowserDialog();
            DialogResult result = fdb.ShowDialog();
            textBox3.Text = fdb.SelectedPath;           //경로를 textbox에 출력
        }

        private void ClientForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Disconnect();
        }

        private void 상세정보ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(listView1.FocusedItem.SubItems[3].Text);                    //0: 이름, 1: 사이즈, 2: 수정, 3: 엑세스한 날짜, 4: 만든날짜, 5: 파일 경로
            if (listView1.FocusedItem.Tag.ToString() == "D")
            {
                MessageBox.Show("폴더는 상세보기를 지원하지 않습니다.");
            }
            else
            {
                ClientForm2 form2 = new ClientForm2(listView1.FocusedItem);
                form2.Show();
            }
        }

        private void 다운로드ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread m_thread2;
            Download dl = new Download();
            dl.Type = (int)PacketType.다운로드;
            dl.FileName = listView1.FocusedItem.SubItems[0].Text;
            dl.FilePath = listView1.FocusedItem.SubItems[5].Text;
            Packet.Serialize(dl).CopyTo(this.sendBuffer, 0);
            this.Send();
            Thread.Sleep(200);
            m_thread2 = new Thread(new ThreadStart(AcceptFile));               
            m_thread2.Start();
        }

        private void AcceptFile()
        {
            while (true)
            {
                MessageBox.Show("hi");
                ListViewItem item;
                BinaryReader reader = new BinaryReader(m_networkstream);
                int length = reader.ReadInt32();                                //'스트림의 끝을 넘어 읽을 수 없습니다.'
                byte[] packet = reader.ReadBytes(length);
                //Download download = BytesToObject(packet);
                Download download = Packet.BytesToObject(packet);
                FileStream fs = new FileStream(textBox3.Text, FileMode.Create);
                fs.Write(download.Data, 0, download.Size);
                fs.Close();
                /*
                item = listView1.Items.Add(fis.Name);
                item.SubItems.Add(fis.Length.ToString());
                item.SubItems.Add(fis.LastWriteTime.ToString());
                item.SubItems.Add(fis.LastAccessTime.ToString());
                item.SubItems.Add(fis.CreationTime.ToString());
                item.SubItems.Add(fis.FullName);
                if (fis.Name.Contains(".avi"))
                    item.ImageIndex = 1;
                else if (fis.Name.Contains(".png"))
                    item.ImageIndex = 2;
                else if (fis.Name.Contains(".mp3"))
                    item.ImageIndex = 3;
                else if (fis.Name.Contains(".txt"))
                    item.ImageIndex = 4;
                else
                    item.ImageIndex = 5;
                item.Tag = "F";
                */
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection siList;
            siList = listView1.SelectedItems;

            foreach (ListViewItem item in siList)
            {
                TreeNode node;
                TreeNode child;

                if (item.Tag.ToString() == "D")
                {
                    node = treeView1.SelectedNode;
                    node.Expand();

                    child = node.FirstNode;

                    while (child != null)
                    {
                        if (child.Text == item.Text)
                        {
                            treeView1.SelectedNode = child;
                            treeView1.Focus();
                            break;
                        }
                        child = child.NextNode;
                    }
                }
            }
        }
    }
}