using System;
using System.Collections.Generic;
using System.Text;
using BatchSend;

namespace BatchSend
{
    [Serializable]
    class User
    {
        public User()
        {
            isUsed = false;
            sendCnt = -1;
            statusCode = BatchSend.StatusCode.UserLoginSuc;
        }
        public string ToString()
        {
            string temp = name + " " + pwd;
            if (sendCnt > 0)
            {
                temp += " " + sendCnt.ToString(); 
            }
            return temp;
        }

        public string name { get; set; }
        public string pwd { get; set; }
        public bool isUsed { get; set; }
        public StatusCode statusCode { get; set; }
        public int sendCnt { get; set; }
        public string sendStatus { get; set; }
    }

}
