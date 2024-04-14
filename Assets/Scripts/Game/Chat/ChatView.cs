using Kamen.DataSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Control;
using WOFL.Game;
using WOFL.Online;
using NativeWebSocket;
using DG.Tweening;
using System;
using System.Threading.Tasks;
using WOFL.UI;
using UnityEngine.SocialPlatforms;

namespace WOFL.Chat
{
    public class ChatView : MonoBehaviour
    {
        #region Classes

        [Serializable] private struct ChatSizeInfo
        {
            #region Variables

            [Header("Settings")]
            [SerializeField] private float _changeSizeDuration;
            [SerializeField] private Ease _changeSizeEase;

            private Vector2 _chatViewSize;
            private Vector3 _inputFieldPosition;


            #endregion

            #region Properties

            public float ChangeSizeDuration { get => _changeSizeDuration; }
            public Ease ChangeSizeEase { get => _changeSizeEase; }

            public Vector2 ChatViewSize 
            {
                get => _chatViewSize; 
                set
                {
                    if (_chatViewSize != Vector2.zero) return;
                    _chatViewSize = value;
                }
            }

            public Vector3 InputFieldPosition 
            { 
                get => _inputFieldPosition; 
                set
                {
                    if (_inputFieldPosition != Vector3.zero) return;
                    _inputFieldPosition = value;
                }
            }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Prefabs")]
        [SerializeField] private Message _playerMessagePrefab;
        [SerializeField] private OtherPlayerMessage _otherPlayerMessagePrefab;

        [Header("Objects")]
        [SerializeField] private GameObject _messagesHolder;
        private RectTransform _chatViewRect;
        private ChatInputField _inputField;

        [Header("Settings")]
        [SerializeField] private ChatScreen.ChatType _chatType;
        [SerializeField] private ChatSizeInfo _fullSizeInfo;
        [SerializeField] private ChatSizeInfo _miniSizeInfo;
        [Space]
        [SerializeField] private int _maxMessagesInChat;
        [SerializeField] private float _delayBetweenRequest;

        [Header("Variables")]
        private Fraction.FractionName _chatFractionName;
        private float _keyboardSize;
        private List<GetMessageInfo> _spawnedMessageInfo = new List<GetMessageInfo>();
        private List<Message> _spawnedMessage = new List<Message>();
        private List<Message> _localMessages = new List<Message>();
        private bool _isAppWorking = false;

        #endregion

        #region Properties

        public ChatScreen.ChatType ChatType { get => _chatType; }

        #endregion

        #region Unity Methods

        public void OnApplicationQuit()
        {
            _isAppWorking = false;
        }

        #endregion

        #region Control Methods

        public void Initialize(ChatInputField inputField)
        {
            _inputField = inputField;
            _inputField.OnStartInput += MoveUp;
            _inputField.OnFinishInput += MoveDown;
            _inputField.OnMessageSend += CallShowPlayerMessage;

            _chatFractionName = AdjustChatFractionName();
            _chatViewRect = GetComponent<RectTransform>();

            CalculateResizeChat();
            GetAllMessages();
        }
        public Fraction.FractionName AdjustChatFractionName()
        {
            return _chatType switch
            {
                ChatScreen.ChatType.Global => Fraction.FractionName.None,
                ChatScreen.ChatType.Fraction => DataSaveManager.Instance.MyData.ChoosenFraction,
                _ => Fraction.FractionName.None,
            };
        }
        private void MoveUp()
        {
            _inputField.transform.DOLocalMove(_miniSizeInfo.InputFieldPosition, _miniSizeInfo.ChangeSizeDuration).SetEase(_miniSizeInfo.ChangeSizeEase);
            _chatViewRect.DOSizeDelta(_miniSizeInfo.ChatViewSize, _miniSizeInfo.ChangeSizeDuration).SetEase(_miniSizeInfo.ChangeSizeEase);
        }
        private void MoveDown()
        {
            _inputField.transform.DOLocalMove(_fullSizeInfo.InputFieldPosition, _fullSizeInfo.ChangeSizeDuration).SetEase(_fullSizeInfo.ChangeSizeEase);
            _chatViewRect.DOSizeDelta(_fullSizeInfo.ChatViewSize, _fullSizeInfo.ChangeSizeDuration).SetEase(_fullSizeInfo.ChangeSizeEase);
        }
        private async void CalculateResizeChat()
        {
            await Task.Delay(100);
            _keyboardSize = MobileUtilities.GetKeyboardHeight(true);

            _fullSizeInfo.ChatViewSize = _chatViewRect.sizeDelta;
            _fullSizeInfo.InputFieldPosition = _inputField.transform.localPosition;

            _miniSizeInfo.ChatViewSize = _chatViewRect.sizeDelta + new Vector2(0, -_keyboardSize);
            _miniSizeInfo.InputFieldPosition = _inputField.transform.localPosition + new Vector3(0, _keyboardSize, 0);
        }
        private async void GetAllMessages()
        {
            _isAppWorking = true;
            await Task.Delay(1000);

            while (_isAppWorking)
            {
                List<GetMessageInfo> messages = await ServerConnectManager.Instance.GetMessages(DataSaveManager.Instance.MyPlayerAuthData.ServerUUID, _chatFractionName);
                DistributeMessages(messages);

                await Task.Delay(Mathf.RoundToInt(_delayBetweenRequest * 1000));
            }
        }
        private void DistributeMessages(List<GetMessageInfo> messagesInfo)
        {
            for (int i = 0; i <  messagesInfo.Count; i++)
            {
                if (_spawnedMessageInfo.Contains(messagesInfo[i])) continue;

                if (messagesInfo[i].player_uuid == DataSaveManager.Instance.MyPlayerAuthData.PlayerUUID) CreatePlayerMessage(messagesInfo[i], false);
                else CreateOtherPlayerMessage(messagesInfo[i]);
            }

            while (_localMessages.Count > 0)
            {
                Destroy(_localMessages[0].gameObject);
                _localMessages.RemoveAt(0);
            }
        }
        private void CreatePlayerMessage(GetMessageInfo messageInfo, bool isLocal)
        {
            Message newMessage = Instantiate(_playerMessagePrefab, _messagesHolder.transform);
            newMessage.AdjustMessage(messageInfo);

            if (isLocal) _localMessages.Add(newMessage);
            else AddMessageInLists(newMessage, messageInfo);
        }
        private void CreateOtherPlayerMessage(GetMessageInfo messageInfo)
        {
            OtherPlayerMessage newMassage = Instantiate(_otherPlayerMessagePrefab, _messagesHolder.transform);
            newMassage.AdjustMessage(messageInfo);

            AddMessageInLists(newMassage, messageInfo);
        }
        private void CallShowPlayerMessage(ChatScreen.ChatType chatType, string messageText)
        {
            if (chatType != _chatType) return;
            GetMessageInfo messageInfo = new GetMessageInfo(
                DataSaveManager.Instance.MyPlayerAuthData.PlayerUUID,
                DataSaveManager.Instance.MyData.Username,
                DataSaveManager.Instance.MyData.IconNumber.ToString(),
                messageText);

            CreatePlayerMessage(messageInfo, true);
        }
        private void AddMessageInLists(Message message, GetMessageInfo messageInfo)
        {
            _spawnedMessage.Add(message);
            _spawnedMessageInfo.Add(messageInfo);

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