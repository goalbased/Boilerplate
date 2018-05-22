using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boilerplate.Exceptions
{
    public enum ExceptionCode
    {        
        AccountNotExisted = 1000,
        WrongPassword,
        AccountBlocked,
        AccountNameDuplicate,
        InvalidateInput = 9000,
        SystemError = 9999
    }
}