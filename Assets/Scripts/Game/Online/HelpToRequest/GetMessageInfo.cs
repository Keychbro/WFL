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

        #region Constructors

        public GetMessageInfo(string new_player_uuid, string new_player_name, string new_player_icon, string new_text)
        {
            player_uuid = new_player_uuid;
            player_name = new_player_name;
            player_icon = new_player_icon;
            text = new_text;
            created_at = DateTime.Now;
        }
        
        #endregion

        #region Control Methods

        public override bool Equals(object obj)
        {
            if (obj is GetMessageInfo other)
            {
                return player_uuid == other.player_uuid &&
                       player_name == other.player_name &&
                       player_icon == other.player_icon &&
                       text == other.text &&
                       created_at == other.created_at;
            }
            return false;
        }

        #endregion
    }
}