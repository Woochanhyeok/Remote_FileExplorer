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
using System.Threading;
using System.IO;
using ClassLibrary3;

namespace HW2
{
    public partial class ServerForm : Form
    {
        private NetworkStream m_networkstream;
        private TcpListener m_listener;

        private byte[] sendBuffer = new byte[1024 * 4];
        private byte[] readBuffer = new byte[1024 * 4];

        private bool m_bClientOn = false;

        private Thread m_thread;
        
        public Dir m_dirClass;
        public BeforeSelect m_beforeselectClass;
        public BeforeExpand m_beforeexpandClass;
        public Download m_downloadClass;
        public string PATH;

        public int PORT;

        private byte[] StringToByte(string str)
        {
            byte[] StrByte = Encoding.UTF8.GetBytes(str); return StrByte;
        }
        
        public void Send()
        {
            this.m_networkstream.Write(this.sendBuffer, 0, this.sendBuffer.Length);
            this.m_networkstream.Flush();

            for (int i = 0; i < 1024 * 4; i++)
            {
                this.sendBuffer[i] = 0;
            }
        }


        public void RUN()
        {
            PORT = Convert.ToInt32(this.textBox2.Text);
            this.m_listener = new TcpListener(PORT);
            this.m_listener.Start();

            if (!this.m_bClientOn)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    this.textBox4.AppendText("클라이언트 접속 대기중\r\n");
                }));
            }

            TcpClient client = this.m_listener.AcceptTcpClient();

            if (client.Connected)
            {
                this.m_bClientOn = true;
                this.Invoke(new MethodInvoker(delegate ()
                {
                    this.textBox4.AppendText("클라이언트 접속\r\n");
                }));
                m_networkstream = client.GetStream();

                Dir_send();                                                                       //send
            }
            int nRead = 0;

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
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                this.textBox4.AppendText("패킷 전송 성공.\r\n");
                                //Browser_Load();
                                Dir_send();

                            }));
                            break;
                        }
                    case (int)PacketType.BeforeSelect:
                        {
                            this.m_beforeselectClass=
                                (BeforeSelect)Packet.Desserialize(this.readBuffer);
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                this.textBox4.AppendText("BeforeSelect. 데이터 요청..\r\n");
                                BeforeSelect();

                            }));
                            break;
                        }
                    case (int)PacketType.BeforeExpand:
                        {
                            this.m_beforeexpandClass =
                                (BeforeExpand)Packet.Desserialize(this.readBuffer);
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                this.textBox4.AppendText("BeforeExpand. 데이터 요청..\r\n");
                                BeforeExpand();

                            }));
                            break;
                        }
                    case (int)PacketType.다운로드:
                        {
                            this.m_downloadClass =
                                (Download)Packet.Desserialize(this.readBuffer);
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                //this.textBox4.AppendText("BeforeExpand. 데이터 요청..\r\n");
                                FileStream fs = new FileStream(m_downloadClass.FilePath, FileMode.Open, FileAccess.Read);
                                byte[] buf = new byte[fs.Length];
                                
                                fs.Close();
                                BinaryWriter writer = new BinaryWriter(m_networkstream);
                                byte[] data = Packet.Serialize(m_downloadClass);
                                writer.Write(data.Length);
                                writer.Write(data);
                                client.Close();
                            }));
                            break;
                        }
                }

            }
        }
        //---------------탐색기 관련 메소드--------------------------
     
        private void Dir_send()
        {
            if (!this.m_bClientOn)
            {
                MessageBox.Show("전송 오류");
                return;
            }
            Dir di = new Dir();
            di.Type = (int)PacketType.디렉토리;
            di.dir = new DirectoryInfo(PATH); 
            Packet.Serialize(di).CopyTo(this.sendBuffer, 0);
            this.Send();
        }

        private void BeforeSelect()
        {
            try
            {
                m_beforeselectClass.diarray = m_beforeselectClass.dir.GetDirectories();
                m_beforeselectClass.fiarray = m_beforeselectClass.dir.GetFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Packet.Serialize(m_beforeselectClass).CopyTo(this.sendBuffer, 0);
            this.Send();
        }

        private void BeforeExpand()
        {
            m_beforeexpandClass.dir = new DirectoryInfo(m_beforeexpandClass.path);
            m_beforeexpandClass.diarray = m_beforeexpandClass.dir.GetDirectories();
            Packet.Serialize(m_beforeexpandClass).CopyTo(this.sendBuffer, 0);
            this.Send();
        }


        //---------------------------------------------------

        public ServerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox3.Text)) {          //경로 선택을 안 하면 에러 출력
                MessageBox.Show("경로를 선택해주세요",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            else
            {
               if (button1.Text == "서버켜기")
                {
                    PATH = textBox3.Text;
                    m_thread = new Thread(new ThreadStart(RUN));
                    m_thread.Start();

                    button1.Text = "서버끊기";
                    button1.ForeColor = Color.Red;
                }
                else
                {
                    m_listener.Stop();
                    m_networkstream.Close();
                    m_thread.Abort();
                    button1.Text = "서버켜기";
                    button1.ForeColor = Color.Black;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)      //경로 선택 버튼 눌렀을 때 이벤트 메소드
        {
            FolderBrowserDialog fdb;
            fdb = new System.Windows.Forms.FolderBrowserDialog();
            DialogResult result = fdb.ShowDialog();
            textBox3.Text = fdb.SelectedPath;           //경로를 textbox에 출력
            this.textBox4.AppendText(fdb.SelectedPath + "로 경로가 수정되었습니다.\r\n");
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            
        }

        
    }
}
