using System;
using System.Collections.Generic;
using System.Text;

namespace OneSignalAPI.Interface
{
    public interface IOneSignalNotification
    {
        Dictionary<string, object> getData();
    }
}
