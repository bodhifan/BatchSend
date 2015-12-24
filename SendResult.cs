using System;
using System.Collections.Generic;
using System.Text;

namespace BatchSend
{
    class SendResult
    {
        public SendResult()
        {
            date = DateTime.Now;
        }
        static public string STATUS_SUCC = "发送成功";
        static public string STATUS_LOGIN_FAIL = "登录失败";
        static public string STATUS_LOGIN_SUC = "登录成功";
        static public string STATUS_SEARCH_FAIL = "搜索用户失败";
        static public string STATUS_NOT_ONLINE = "不在线";

        public DateTime date
        { get; set; }
        public string userName
        {get;set;}
        public string password
        { get; set; }
        public string target
        { get; set; }
        public string status
        {get;set;}
        public bool isSucc
        {get;set;}
        public int order
        { get; set; }
    }
}
