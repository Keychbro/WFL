using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using WOFL.Control;
using Kamen.DataSave;
using WOFL.UI;
using WOFL.Game;
using System;

namespace WOFL.Chat
{
    [RequireComponent(typeof(TMP_InputField))]
    public class ChatInputField : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Button _sendMessageButton;
        private TMP_InputField _inputField;
        private ChatScreen _chatScreen;

       // [Header("Variables")]
        public event Action OnStartInput;
        public event Action OnFinishInput;
        public event Action<ChatScreen.ChatType, string> OnMessageSend;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _inputField.onValueChanged.RemoveListener(SendMessageButtonViewControl);
            _sendMessageButton.onClick.RemoveListener(CallSendMessage);
        }

        #endregion

        #region Control Methods

        public void Initialize(ChatScreen chatScreen)
        {
            _chatScreen = chatScreen;

            _inputField = GetComponent<TMP_InputField>();
            _inputField.onValueChanged.AddListener(SendMessageButtonViewControl);
            _inputField.onSelect.AddListener(StartInput);
            _inputField.onDeselect.AddListener(FinishInput);
            _sendMessageButton.onClick.AddListener(CallSendMessage);
        }

        private void SendMessageButtonViewControl(string message)
        {
            if (message.Length == 0 && _sendMessageButton.gameObject.activeSelf) _sendMessageButton.gameObject.SetActive(false);
            else if (message.Length > 0 && !_sendMessageButton.gameObject.activeSelf) _sendMessageButton.gameObject.SetActive(true);
        }
        public void StartInput(string arg0) => OnStartInput?.Invoke();
        public void FinishInput(string arg0) => OnFinishInput?.Invoke();

        private async void CallSendMessage()
        {
            ChatScreen.ChatType currentChatType = _chatScreen.GetCurrentChatType();
            switch (currentChatType)
            {
                case ChatScreen.ChatType.Global:
                case ChatScreen.ChatType.Fraction:
                    bool result = await ServerConnectManager.Instance.SendMessage(
                        DataSaveManager.Instance.MyPlayerAuthData.ServerUUID,
                        DataSaveManager.Instance.MyPlayerAuthData.PlayerUUID,
                        FractionManager.Instance.CurrentFraction.Name,
                        currentChatType == ChatScreen.ChatType.Global ? true : false,
                        _inputField.text);

                    if (result)
                    {
                        OnMessageSend?.Invoke(_chatScreen.GetCurrentChatType(), _inputField.text);
                        _inputField.text = "";
                    }
                    break;
                case ChatScreen.ChatType.Support:
                    bool supportResult = await ServerConnectManager.Instance.SendSupportMessage(
                        _inputField.text,
                        DataSaveManager.Instance.MyPlayerAuthData.PlayerUUID);

                    if (supportResult)
                    {
                        OnMessageSend?.Invoke(_chatScreen.GetCurrentChatType(), _inputField.text);
                        _inputField.text = "";
                    }
                    break;
            }
            
           // string supportResult = await ServerConnectManager.Instance.GetPlayerUUID(_email);
        }

        #endregion
    }
}