using System;
using System.Collections.Generic;
using System.Text;

namespace BatchSend
{
    public enum StatusCode
    {
        Default=0,
        TargetSearchFailed=12,
        TargetNotOnline=13,

        UserLoginFailed = 21,
        UserLoginSuc = 22
    }
}
