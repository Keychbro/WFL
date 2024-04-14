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

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _inputField.onValueChanged.RemoveListener(SendMessageButtonViewControl);
            _sendMessageButton.onClick.RemoveListener(CallSendMessage);
        }

        #endregion

        #region Control Methods

        public void Initialize()
        {
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
            bool result = await ServerConnectManager.Instance.SendMessage(
                DataSaveManager.Instance.MyPlayerAuthData.ServerUUID, 
                DataSaveManager.Instance.MyPlayerAuthData.PlayerUUID, 
                _chatScreen.GetCurrentChatType(),
                _inputField.text);
            Debug.Log(result);
           // string result = await ServerConnectManager.Instance.GetPlayerUUID(_email);
        }

        #endregion
    }
}