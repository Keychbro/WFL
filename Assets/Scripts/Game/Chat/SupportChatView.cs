using Kamen.DataSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Control;
using WOFL.Game;
using WOFL.Online;
using DG.Tweening;
using System;
using System.Threading.Tasks;
using WOFL.UI;
using System.Threading;


namespace WOFL.Chat
{
    public class SupportChatView : ChatView
    {
        #region Variables

        [Header("Support Chat Settings")]
        [SerializeField] private Sprite _adminIcon;

        [Header("Support Chat Varaibles")]
        private List<GetSupportMessageInfo> _spawnedSupportMessageInfo = new List<GetSupportMessageInfo>();

        #endregion

        #region Control Methods

        protected override async Task GetAllMessages(CancellationToken cancellationToken)
        {
            _isAppWorking = true;
            await Task.Delay(1000, cancellationToken);

            while (_isAppWorking)
            {
                List<GetSupportMessageInfo> messages = await ServerConnectManager.Instance.GetSupportMessages(DataSaveManager.Instance.MyPlayerAuthData.PlayerUUID);
                DistributeSupportMessages(messages);

                await Task.Delay(Mathf.RoundToInt(_delayBetweenRequest * 1000), cancellationToken);
            }
        }
        private void DistributeSupportMessages(List<GetSupportMessageInfo> messagesInfo)
        {
            for (int i = 0; i < messagesInfo.Count; i++)
            {
                if (_spawnedSupportMessageInfo.Contains(messagesInfo[i])) continue;

                if (!messagesInfo[i].answer) CreatePlayerSupportMessage(messagesInfo[i], false);
                else CreateAdminMessage(messagesInfo[i]);
            }

            while (_localMessages.Count > 0)
            {
                Destroy(_localMessages[0].gameObject);
                _localMessages.RemoveAt(0);
            }
        }
        private void CreatePlayerSupportMessage(GetSupportMessageInfo messageInfo, bool isLocal)
        {
            Message newMessage = Instantiate(_playerMessagePrefab, _messagesHolder.transform);
            newMessage.AdjustSupportMessage(messageInfo);

            if (isLocal) _localMessages.Add(newMessage);
            else AddSupportMessageInLists(newMessage, messageInfo);
        }
        private void CreateAdminMessage(GetSupportMessageInfo messageInfo)
        {
            OtherPlayerMessage newMessage = Instantiate(_otherPlayerMessagePrefab, _messagesHolder.transform);
            newMessage.AdjustSupportMessage(messageInfo);
            newMessage.SetSupportMessageIcon(_adminIcon);

            AddSupportMessageInLists(newMessage, messageInfo);
        }
        private void CallShowPlayerSupportMessage(ChatScreen.ChatType chatType, string messageText)
        {
            if (chatType != _chatType) return;
            GetSupportMessageInfo messageInfo = new GetSupportMessageInfo(
                DataSaveManager.Instance.MyPlayerAuthData.PlayerUUID,
                DataSaveManager.Instance.MyData.Username,
                messageText,
                false);

            CreatePlayerSupportMessage(messageInfo, true);
        }
        private void AddSupportMessageInLists(Message message, GetSupportMessageInfo supportMessageInfo)
        {
            _spawnedMessage.Add(message);
            _spawnedSupportMessageInfo.Add(supportMessageInfo);

            if (_spawnedMessage.Count > _maxMessagesInChat)
            {
                Message deleteMessage = _spawnedMessage[0];
                _spawnedMessageInfo.RemoveAt(0);
                _spawnedMessage.RemoveAt(0);
                Destroy(deleteMessage.gameObject);
            }
        }

        #endregion
        
    }
}