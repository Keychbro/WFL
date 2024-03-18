using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace WOFL.Online
{
    [Serializable] public class PlayerAuthData
    {
        #region Variables

        [SerializeField] private string _serverUUID;
        [SerializeField] private string _playerUUID;

        #endregion

        #region Properties

        public string ServerUUID
        {
            get => _serverUUID;
            set
            {
                if (_serverUUID != null)
                {
                    Debug.LogError("[PlayerAuthData] - Attempt to assign the wrong uuid to the server");
                    return;
                }

                _serverUUID = value;
            }
        }
        public string PlayerUUID
        {
            get => _playerUUID;
            set
            {
                if (_playerUUID != null)
                {
                    Debug.LogError("[PlayerAuthData] - Attempt to assign the wrong uuid to the player");
                    return;
                }

                _playerUUID = value;
            }
        }

        #endregion
    }
}