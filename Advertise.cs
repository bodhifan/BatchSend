using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BatchSend
{
    [Serializable]
    public class Advertise
    {

//        public int usedNum = 0;
//        public string checkMsg = "";
//        public string msgContent = "";
       public Advertise()
       {
           TotalNum = 0;
           UsedNum = 0;
           CheckMsg = "";
           MsgContent = "";
       }
       public int TotalNum { get; set; }
       public int UsedNum { get; set; }
       public string CheckMsg { get; set; }
       public string MsgContent { get; set; }
       
    }

    public class AdvertiseList
    {
        private static AdvertiseList mInstance = null;
        public static AdvertiseList getInstance()
        {
            if (mInstance == null)
            {
                mInstance = new AdvertiseList();
                mInstance.parse();
            }
            return mInstance;
        }
        public List<Advertise> myAdvs = new List<Advertise>();
        int usedIdx = 0;

        string filePath = @"adverties.bat";

        public AdvertiseList()
        {

        }
        public bool Reset()
        {
            usedIdx = 0;
            return true;
        }
        public bool hasNext()
        {
            if (usedIdx < myAdvs.Count)
            {
                return true;
            }
            return false;
        }
        public Advertise next()
        {
            return myAdvs[usedIdx++];
        }

        public void dump()
        {
            FileStream fs = new FileStream(filePath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, myAdvs);
            fs.Close();
        }
        public void parse()
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            FileStream fs = new FileStream(filePath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            myAdvs = bf.Deserialize(fs) as List<Advertise>;
        }
    }
}
