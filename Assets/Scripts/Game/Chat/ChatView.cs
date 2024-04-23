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
using System.Threading;

namespace WOFL.Chat
{
    public class ChatView : MonoBehaviour
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] protected Message _playerMessagePrefab;
        [SerializeField] protected OtherPlayerMessage _otherPlayerMessagePrefab;

        [Header("Objects")]
        [SerializeField] protected GameObject _messagesHolder;
        protected RectTransform _chatViewRect;
        protected ChatInputField _inputField;

        [Header("Settings")]
        [SerializeField] protected ChatScreen.ChatType _chatType;
        [SerializeField] protected ChatSizeInfo _fullSizeInfo;
        [SerializeField] protected ChatSizeInfo _miniSizeInfo;
        [Space]
        [SerializeField] protected int _maxMessagesInChat;
        [SerializeField] protected float _delayBetweenRequest;

        [Header("Variables")]
        protected Fraction.FractionName _chatFractionName;
        protected float _keyboardSize;
        protected List<GetMessageInfo> _spawnedMessageInfo = new List<GetMessageInfo>();
        protected List<Message> _spawnedMessage = new List<Message>();
        protected List<Message> _localMessages = new List<Message>();
        protected bool _isAppWorking = false;
        protected CancellationTokenSource _cancellationTokenSource;

        #endregion

        #region Properties

        public ChatScreen.ChatType ChatType { get => _chatType; }

        #endregion

        #region Unity Methods

        public void OnApplicationQuit()
        {
            StopGettingMessages();
        }
        public void OnApplicationPause(bool pause)
        {
            if (pause) StopGettingMessages();
            else StartGettingMessages();
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
            StartGettingMessages();
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
        protected void MoveUp()
        {
            _inputField.transform.DOLocalMove(_miniSizeInfo.InputFieldPosition, _miniSizeInfo.ChangeSizeDuration).SetEase(_miniSizeInfo.ChangeSizeEase);
            _chatViewRect.DOSizeDelta(_miniSizeInfo.ChatViewSize, _miniSizeInfo.ChangeSizeDuration).SetEase(_miniSizeInfo.ChangeSizeEase);
        }
        protected void MoveDown()
        {
            _inputField.transform.DOLocalMove(_fullSizeInfo.InputFieldPosition, _fullSizeInfo.ChangeSizeDuration).SetEase(_fullSizeInfo.ChangeSizeEase);
            _chatViewRect.DOSizeDelta(_fullSizeInfo.ChatViewSize, _fullSizeInfo.ChangeSizeDuration).SetEase(_fullSizeInfo.ChangeSizeEase);
        }
        protected async void CalculateResizeChat()
        {
            await Task.Delay(100);
            _keyboardSize = MobileUtilities.GetKeyboardHeight(true);

            _fullSizeInfo.ChatViewSize = _chatViewRect.sizeDelta;
            _fullSizeInfo.InputFieldPosition = _inputField.transform.localPosition;

            _miniSizeInfo.ChatViewSize = _chatViewRect.sizeDelta + new Vector2(0, -_keyboardSize);
            _miniSizeInfo.InputFieldPosition = _inputField.transform.localPosition + new Vector3(0, _keyboardSize, 0);
        }
        protected virtual async Task GetAllMessages(CancellationToken cancellationToken)
        {
            _isAppWorking = true;
            await Task.Delay(1000, cancellationToken);

            while (_isAppWorking)
            {
                cancellationToken.ThrowIfCancellationRequested();

                List<GetMessageInfo> messages = await ServerConnectManager.Instance.GetMessages(DataSaveManager.Instance.MyPlayerAuthData.ServerUUID, _chatFractionName);
                DistributeMessages(messages);

                await Task.Delay(Mathf.RoundToInt(_delayBetweenRequest * 1000), cancellationToken);
            }
        }
        protected virtual void StartGettingMessages()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;
            GetAllMessages(cancellationToken);
        }

        protected virtual void StopGettingMessages()
        {
            _isAppWorking = false;
            _cancellationTokenSource?.Cancel();
        }
        protected void DistributeMessages(List<GetMessageInfo> messagesInfo)
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
        protected void CreatePlayerMessage(GetMessageInfo messageInfo, bool isLocal)
        {
            Message newMessage = Instantiate(_playerMessagePrefab, _messagesHolder.transform);
            newMessage.AdjustMessage(messageInfo);

            if (isLocal) _localMessages.Add(newMessage);
            else AddMessageInLists(newMessage, messageInfo);
        }
        protected void CreateOtherPlayerMessage(GetMessageInfo messageInfo)
        {
            OtherPlayerMessage newMassage = Instantiate(_otherPlayerMessagePrefab, _messagesHolder.transform);
            newMassage.AdjustMessage(messageInfo);

            AddMessageInLists(newMassage, messageInfo);
        }
        protected virtual void CallShowPlayerMessage(ChatScreen.ChatType chatType, string messageText)
        {
            if (chatType != _chatType) return;
            GetMessageInfo messageInfo = new GetMessageInfo(
                DataSaveManager.Instance.MyPlayerAuthData.PlayerUUID,
                DataSaveManager.Instance.MyData.Username,
                DataSaveManager.Instance.MyData.IconNumber.ToString(),
                messageText);

            CreatePlayerMessage(messageInfo, true);
        }
        protected void AddMessageInLists(Message message, GetMessageInfo messageInfo)
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