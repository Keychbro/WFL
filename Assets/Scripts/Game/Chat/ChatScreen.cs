using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using WOFL.Online;
using WOFL.Control;
using WOFL.Game;

namespace WOFL.UI
{
    public class ChatScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Message _myMessagePrefab;
        [SerializeField] private OtherPlayerMessage _otherPlayerMessagePrefab;

        #endregion

        #region Control Methods

        private void Start()
        {
            GetAllMessages();
        }
        private async void GetAllMessages()
        {
            List<GetMessageInfo> messages = await ServerConnectManager.Instance.GetMessages("fb988152-8f01-4f9a-b436-3691d2ffe806", Fraction.FractionName.Human);

        }

        #endregion
    }
}