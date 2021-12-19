using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TANGOCCONG.ANUIShop.API.Interfaces
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogErr(string message);
    }
}
