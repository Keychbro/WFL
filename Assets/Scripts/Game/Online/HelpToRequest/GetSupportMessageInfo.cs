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

        #region Constructors

        public GetSupportMessageInfo(string new_player_uuid, string new_player_name, string new_text, bool new_answer)
        {
            player_uuid = new_player_uuid;
            player_name = new_player_name;
            text = new_text;
            answer = new_answer;
            created_at = DateTime.Now;
        }

        #endregion

        #region Control Methods

        public override bool Equals(object obj)
        {
            if (obj is GetSupportMessageInfo other)
            {
                return player_uuid == other.player_uuid &&
                       player_name == other.player_name &&
                       text == other.text &&
                       answer == other.answer &&
                       created_at == other.created_at;
            }
            return false;
        }

        #endregion
    }
}

