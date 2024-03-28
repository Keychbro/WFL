using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WOFL.Online;
using Kamen.UI;
using Kamen.DataSave;

namespace WOFL.UI
{
    public class ServerPanel : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private TextMeshProUGUI _serverNameText;
        [SerializeField] private Button _playButton;

        [Header("Settings")]
        [SerializeField] private float _starPlayDuration;

        [Header("Variables")]
        private ServerInfo _currentServerInfo;

        #endregion

        #region Control Methods

        public void Intialize(ServerInfo serverInfo)
        {
            _currentServerInfo = serverInfo;

            _serverNameText.text = _currentServerInfo.name;
            _playButton.onClick.AddListener(ClickOnPlayButton);
        }
        private void ClickOnPlayButton()
        {
            DataSaveManager.Instance.MyPlayerAuthData.ServerUUID = _currentServerInfo.uuid;
            DataSaveManager.Instance.AdjustPlayerDataOnServer();

            ScreenManager.Instance.TransitionWithOwnDuration("LoadingScreen", _starPlayDuration);
        }

        #endregion
    }
}