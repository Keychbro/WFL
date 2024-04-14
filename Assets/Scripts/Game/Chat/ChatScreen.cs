using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using WOFL.Online;
using WOFL.Control;
using WOFL.Game;
using WOFL.Chat;

namespace WOFL.UI
{
    public class ChatScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Message _myMessagePrefab;
        [SerializeField] private OtherPlayerMessage _otherPlayerMessagePrefab;
        [Space]
        [SerializeField] private ChatView _chatView; //TODO: Change this
        [SerializeField] private ChatInputField _inputField;

        #endregion

        #region Control Methods

        private void Start()
        {
            GetAllMessages();
            _chatView.Initialize(_inputField);
            _inputField.Initialize();
        }
        private async void GetAllMessages()
        {
            List<GetMessageInfo> messages = await ServerConnectManager.Instance.GetMessages("fb988152-8f01-4f9a-b436-3691d2ffe806", Fraction.FractionName.Human);

        }
        public Fraction.FractionName GetCurrentChatType() => Fraction.FractionName.Human; //TODO: Fix this panel

        #endregion
    }
}