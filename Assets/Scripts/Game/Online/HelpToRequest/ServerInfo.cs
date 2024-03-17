using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Online
{
    [Serializable] public class ServerInfo
    {
        #region Variables

        public string uuid;
        public string name;
        public DateTime created_at;

        #endregion
    }
}