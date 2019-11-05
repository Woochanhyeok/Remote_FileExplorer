using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.ComponentModel;

namespace ClassLibrary3
{
    public enum PacketType
    {
        초기화 = 0,
        디렉토리,
        BeforeSelect,
        BeforeExpand,
        다운로드
    }
    public enum PacketSendERROR
    {
        정상 = 0,
        에러
    }
    [Serializable]
    public class Packet
    {
        public int Length;
        public int Type;

        public Packet()
        {
            this.Length = 0;
            this.Type = 0;
        }
        public static byte[] Serialize(Object o)
        {
            MemoryStream ms = new MemoryStream(1024 * 4);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, o);                        //System.Runtime.Serialization.SerializationException: ''ClassLibrary3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' 어셈블리의 'ClassLibrary3.Packet' 형식이 serializable로 표시되어 있지 않습니다.'

            return ms.ToArray();
        }
        public static Object Desserialize(byte[] bt)
        {
            MemoryStream ms = new MemoryStream(1024 * 4);
            foreach (byte b in bt)
            {
                ms.WriteByte(b);
            }

            ms.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            Object obj = bf.Deserialize(ms);                    //개체 참조가 개체의 인스턴스로 설정되지 않았습니다. ms가 null이라는 뜻
            ms.Close();
            return obj;

        }


        public static Download BytesToObject(byte[] data)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            ms.Write(data, 0, data.Length);
            ms.Seek(0, SeekOrigin.Begin);
            object obj = bf.Deserialize(ms) as object;
            return obj as Download;
        }
    

    }
    [Serializable]
    public class Dir : Packet
    {
        public DirectoryInfo dir;
    }
    [Serializable]
    public class BeforeSelect : Packet
    {
        public DirectoryInfo dir;
        public DirectoryInfo[] diarray;
        public FileInfo[] fiarray;
    }
    [Serializable]
    public class BeforeExpand : Packet
    {
        public string path;
        public DirectoryInfo dir;
        public DirectoryInfo[] diarray;
    }
    [Serializable]
    public class Download : Packet
    {
        public byte[] Data;
        public string FileName;
        public string FilePath;
        public int Size;
        /*
        public Download(string filename, int filelength, byte[] buf)
        {
            FileName = filename;
            Data = buf;
            Size = filelength;

        }*/
    }
}