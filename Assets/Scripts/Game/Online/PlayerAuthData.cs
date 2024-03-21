using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace WOFL.Online
{
    [Serializable] public class PlayerAuthData
    {
        #region Variables

        [SerializeField] private string _startUsername;
        [SerializeField] private string _email = "";
        [SerializeField] private string _serverUUID = "";
        [SerializeField] private string _playerUUID = "";

        #endregion

        #region Properties

        public string StartUsername
        {
            get => _startUsername;
            set
            {
                if (value == null || value.Length == 0)
                {
                    Debug.LogError("[PlayerAuthData] - Attempting to assign an incorrect name!");
                    return;
                }

                _startUsername = value;
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                if (value == null)
                {
                    Debug.LogError("[PlayerAuthData] - Attempt to assign the null email");
                    return;
                }

                _email = value;
            }
        }
        public string ServerUUID
        {
            get => _serverUUID;
            set
            {
                if (value == null)
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
                if (value == null)
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