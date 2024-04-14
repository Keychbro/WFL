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

namespace WOFL.Chat
{
    public class ChatView : MonoBehaviour
    {
        #region Enums

        public enum ChatType
        {
            Global,
            Fraction
        }

        #endregion

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

        [Header("Objects")]
        private RectTransform _chatViewRect;
        private ChatInputField _inputField;

        [Header("Settings")]
        [SerializeField] private ChatType _chatType;
        [SerializeField] private ChatSizeInfo _fullSizeInfo;
        [SerializeField] private ChatSizeInfo _miniSizeInfo;

        [Header("Variables")]
        private Fraction.FractionName _chatFractionName;
        private float _keyboardSize;

        #endregion

        #region Control Methods
        
        public void Initialize(ChatInputField inputField)
        {
            _inputField = inputField;
            _inputField.OnStartInput += MoveUp;
            _inputField.OnFinishInput += MoveDown;

            _chatFractionName = AdjustChatFractionName();
            _chatViewRect = GetComponent<RectTransform>();

            CalculateResizeChat();
        }
        public Fraction.FractionName AdjustChatFractionName()
        {
            return _chatType switch
            {
                ChatType.Global => Fraction.FractionName.None,
                ChatType.Fraction => DataSaveManager.Instance.MyData.ChoosenFraction,
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
            //_keyboardSize = MobileUtilities.GetKeyboardHeight(true);
            _keyboardSize = 400;

            _fullSizeInfo.ChatViewSize = _chatViewRect.sizeDelta;
            _fullSizeInfo.InputFieldPosition = _inputField.transform.localPosition;

            _miniSizeInfo.ChatViewSize = _chatViewRect.sizeDelta + new Vector2(0, -_keyboardSize);
            _miniSizeInfo.InputFieldPosition = _inputField.transform.localPosition + new Vector3(0, _keyboardSize, 0);
        }
        private  void ChangeSize()
        {


        }
        private async void GetAllMessages()
        {
            List<GetMessageInfo> messages = await ServerConnectManager.Instance.GetMessages(DataSaveManager.Instance.MyPlayerAuthData.ServerUUID, _chatFractionName);

        }

       //WebSocket websocket;
       //async void Start()
       //{
       //    websocket = new WebSocket("ws://localhost:2567");
       //
       //    websocket.OnOpen += () =>
       //    {
       //        Debug.Log("Connection open!");
       //    };
       //
       //    websocket.OnError += (e) =>
       //    {
       //        Debug.Log("Error! " + e);
       //    };
       //
       //    websocket.OnClose += (e) =>
       //    {
       //        Debug.Log("Connection closed!");
       //    };
       //
       //    websocket.OnMessage += (bytes) =>
       //    {
       //        Debug.Log("OnMessage!");
       //        Debug.Log(bytes);
       //
       //        // getting the message as a string
       //        // var message = System.Text.Encoding.UTF8.GetString(bytes);
       //        // Debug.Log("OnMessage! " + message);
       //    };
       //
       //    // Keep sending messages at every 0.3s
       //    InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);
       //
       //    // waiting for messages
       //    await websocket.Connect();
       //}

        #endregion
    }
}