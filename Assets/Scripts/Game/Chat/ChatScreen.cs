using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using WOFL.Online;
using WOFL.Control;
using WOFL.Game;
using WOFL.Chat;
using System;
using System.Linq;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;
using Kamen.DataSave;
using System.Threading.Tasks;

namespace WOFL.UI
{
    public class ChatScreen : Kamen.UI.Screen
    {
        #region Enums

        public enum ChatType
        {
            Global,
            Fraction,
            Support
        }

        #endregion

        #region Classes

        [Serializable] private struct ChatViewInfo
        {
            #region ChatViewInfo Variables

            [SerializeField] private ChatViewButton _button;
            [SerializeField] private ChatView _view;

            #endregion

            #region ChatViewInfo Properties

            public ChatViewButton Button { get => _button; }
            public ChatView View { get => _view; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] private Message _myMessagePrefab;
        [SerializeField] private OtherPlayerMessage _otherPlayerMessagePrefab;
        [Space]
        [SerializeField] private ChatInputField _inputField;

        [Header("Settings")]
        [SerializeField] private ChatViewInfo[] _chatViewsInfo;
        [SerializeField] private ChatType _startChat;

        [Header("Variables")]
        private ChatViewButton _activeButton;
        private ChatViewInfo _currentChatViewInfo;

        #endregion

        #region Control Methods

        public async override void Initialize()
        {
            base.Initialize();

            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);
            await UniTask.WaitUntil(() => DataSaveManager.Instance.MyData.ChoosenFraction != Fraction.FractionName.None);
            await Task.Delay(100);

            _inputField.Initialize(this);
            AdjustShopView();
        }
        private void AdjustShopView()
        {
            for (int i = 0; i < _chatViewsInfo.Length; i++)
            {
                _chatViewsInfo[i].Button.Initialize();
                _chatViewsInfo[i].Button.OnButtonClicked += ChangeChatView;

                _chatViewsInfo[i].View.Initialize(_inputField);
                if (_chatViewsInfo[i].View.ChatType == _startChat) ChangeChatView(_chatViewsInfo[i].Button);
            }
        }
        private void ChangeChatView(ChatViewButton button)
        {
            if (button == _activeButton) return;

            if (_activeButton != null)
            {
                _activeButton.SwitchActive(false);
                _chatViewsInfo.First(chatViewInfo => chatViewInfo.Button == _activeButton).View.gameObject.SetActive(false);
            }
            button.SwitchActive(true);
            _currentChatViewInfo =  _chatViewsInfo.First(chatViewInfo => chatViewInfo.Button == button);
            _currentChatViewInfo.View.gameObject.SetActive(true);
            _activeButton = button;
        }
        public ChatType GetCurrentChatType() => _currentChatViewInfo.View.ChatType;

        #endregion
    }
}