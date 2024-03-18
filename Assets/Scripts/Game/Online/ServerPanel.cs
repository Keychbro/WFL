using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WOFL.Online;
using Kamen.UI;

namespace WOFL.UI
{
    public class ServerPanel : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private TextMeshProUGUI _serverNameText;
        [SerializeField] private Button _playButton;

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
            //TODO: Save server_uuid
            ScreenManager.Instance.TransitionTo("Fight");
        }

        #endregion
    }
}