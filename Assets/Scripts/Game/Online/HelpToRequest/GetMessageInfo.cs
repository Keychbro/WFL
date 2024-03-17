using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Online
{
    [Serializable] public class GetMessageInfo
    {
        #region Variables

        public string player_uuid;
        public string player_name;
        public string player_icon;
        public string text;
        public DateTime created_at;

        #endregion
    }
}