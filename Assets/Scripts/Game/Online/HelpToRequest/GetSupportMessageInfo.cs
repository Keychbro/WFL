using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Online
{
    [Serializable] public class GetSupportMessageInfo
    {
        #region Variables

        public string player_uuid;
        public string player_name;
        public string text;
        public bool answer;
        public DateTime created_at;

        #endregion
    }
}

