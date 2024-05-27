using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using Kamen;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.IO;
using System;
using WOFL.Online;
using Newtonsoft.Json;
using WOFL.Game;
using Kamen.DataSave;
using Cysharp.Threading.Tasks;

namespace WOFL.Control
{
    public class ServerConnectManager : SingletonComponent<ServerConnectManager>
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private string _hostURL;
        [Space]
        [SerializeField] private string _apiName;
        [SerializeField] private string _serverListName;
        [SerializeField] private string _getMessagesName;
        [SerializeField] private string _sendMessageName;
        [SerializeField] private string _createPlayerName;
        [SerializeField] private string _deletePlayerName;
        [SerializeField] private string _getPlayerUUIDName;
        [SerializeField] private string _getPlayerDataName;
        [SerializeField] private string _updatePlayerDataName;
        [SerializeField] private string _deletePlayerDataName;
        [SerializeField] private string _getSupportMessageName;
        [SerializeField] private string _sendSupportMessageName;

        #endregion

        #region Unity Methods

        private async void Start()
        {
            //GetServersList();
            //GetMessages("fb988152-8f01-4f9a-b436-3691d2ffe806", Fraction.FractionName.Human);
            //SendMessage("fb988152-8f01-4f9a-b436-3691d2ffe806", "8375b6a3-c965-4b61-977c-c17f17b06741", Fraction.FractionName.Angel, true, "Test message");
            //CreatePlayer("Test player9", "test8gmail.com", 0);
            //GetPlayerUUID("test5@gmail.com");
            //GetPlayerData("fb988152-8f01-4f9a-b436-3691d2ffe806", "7b867119-c1a7-40da-b51e-294d8f66b7f1");
            //UpdatePlayerData(JsonUtility.ToJson(DataSaveManager.Instance.MyData), "dc5c24c6-fd8a-404e-aee3-ff770086e201", "fb988152-8f01-4f9a-b436-3691d2ffe806");
            //DeletePlayer("test4@gmail.com");
            //DeletePlayerData("dc5c24c6-fd8a-404e-aee3-ff770086e201", "fb988152-8f01-4f9a-b436-3691d2ffe806");
            //SendSupportMessage("Hi", "d631ed78-56b7-415a-8b3d-1b52eb400b76");
            //GetSupportMessages("d631ed78-56b7-415a-8b3d-1b52eb400b76");

            //GetMessages2("23b2995f-66b9-4060-9627-900daa40961f", Fraction.FractionName.Human);
            //
            //for (int i = 0; i < 6; i++)
            //{
            //    await Task.Delay(5000);
            //    SendMessage("23b2995f-66b9-4060-9627-900daa40961f", "1187f1c5-014e-4393-b0f0-3b6c7b346498", Fraction.FractionName.Human, "hi" + i);
            //}

            //SendSupportMessage("i have a problem", "d631ed78-56b7-415a-8b3d-1b52eb400b76");
            //GetSupportMessages("d631ed78-56b7-415a-8b3d-1b52eb400b76");
        }
        private async void OnApplicationQuit()
        {
            //await websocket.Close();
        }

        #endregion

        #region Servers Methods

        public async Task<List<ServerInfo>> GetServersList()
        {
            using UnityWebRequest www = UnityWebRequest.Get($"{_hostURL}/{_apiName}/{_serverListName}/");
            var operation = www.SendWebRequest();
            Debug.Log(4);
            while (!operation.isDone) await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                return null;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                return JsonConvert.DeserializeObject<List<ServerInfo>>(www.downloadHandler.text);
            }
        }

        #endregion

        #region Messages Methods

        public async Task<List<GetMessageInfo>> GetMessages(string server_uuid, Fraction.FractionName fraction, bool isGlobalChat)
        {
            string realFraction = fraction.ToString();
            string globalChatValue = isGlobalChat ? "true" : "false";
            using UnityWebRequest www = UnityWebRequest.Get($"{_hostURL}/{_apiName}/{_getMessagesName}/?server_uuid={server_uuid}&fraction={realFraction}&global_chat={globalChatValue}");
            var operation = www.SendWebRequest();

            while (!operation.isDone) await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                return null;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                return JsonConvert.DeserializeObject<List<GetMessageInfo>>(www.downloadHandler.text); 
            }
        }
        //public async Task GetMessagesWithSocket(string server_uuid, Fraction.FractionName fraction)
        //{
        //    string realFraction = fraction == Fraction.FractionName.None ? "global" : fraction.ToString();
        //    string url = $"{_hostURL}/{_apiName}/{_getMessagesName}/?server_uuid={server_uuid}&fraction={realFraction}";
        //
        //    websocket = new WebSocket(url);
        //
        //    websocket.OnOpen += () =>
        //    {
        //        Debug.Log("Connection open!");
        //    };
        //
        //    websocket.OnMessage += (bytes) =>
        //    {
        //        Debug.Log("OnMessage!");
        //        Debug.Log(bytes);
        //        var message = System.Text.Encoding.UTF8.GetString(bytes);
        //        Debug.Log("OnMessage! " + message);
        //    };
        //
        //    await websocket.Connect();
        //}
        public async Task<bool> SendMessage(string server_uuid, string player_uuid, Fraction.FractionName fraction, bool isGlobalChat, string message)
        {
            WWWForm form = new WWWForm();

            form.AddField("server_uuid", server_uuid);
            form.AddField("player_uuid", player_uuid);
            form.AddField("fraction", fraction.ToString());
            form.AddField("global_chat", isGlobalChat ? "true" : "false");
            form.AddField("text", message);

            using UnityWebRequest www = UnityWebRequest.Post($"{_hostURL}/{_apiName}/{_sendMessageName}/", form);
            var operation = www.SendWebRequest();

            while (!operation.isDone) await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                return false;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                return true;
            }
        }

        #endregion

        #region Player Methods

        public async Task<string> CreatePlayer(string name, string email)
        {
            WWWForm form = new WWWForm();

            form.AddField("name", name);
            form.AddField("email", email);

            using UnityWebRequest www = UnityWebRequest.Post($"{_hostURL}/{_apiName}/{_createPlayerName}/", form);
            var operation = www.SendWebRequest();

            while (!operation.isDone) await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                return null;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text.Contains("email busy"))
                {
                    return JsonConvert.DeserializeObject<CreatePlayerFailureInfo>(www.downloadHandler.text).info;
                }
                else
                {
                    return JsonConvert.DeserializeObject<CreatePlayerSuccessInfo>(www.downloadHandler.text).player_uuid;
                }
            }
        }
        public async UniTask DeletePlayer(string email)
        {
            WWWForm form = new WWWForm();

            form.AddField("email", email);

            using UnityWebRequest www = UnityWebRequest.Post($"{_hostURL}/{_apiName}/{_deletePlayerName}/", form);
            var operation = www.SendWebRequest();

            while (!operation.isDone) await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                //return null;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
        public async Task<string> GetPlayerUUID(string email)
        {
            using UnityWebRequest www = UnityWebRequest.Get($"{_hostURL}/{_apiName}/{_getPlayerUUIDName}/?email={email}");
            var operation = www.SendWebRequest();

            while (!operation.isDone) await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                return null;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                return JsonConvert.DeserializeObject<GetPlayerUUIDInfo>(www.downloadHandler.text).player_uuid;
            }
        }
        public async Task<T> GetPlayerData<T>(string server_uuid, string player_uuid)
        {
            using UnityWebRequest www = UnityWebRequest.Get($"{_hostURL}/{_apiName}/{_getPlayerDataName}/?server_uuid={server_uuid}&player_uuid={player_uuid}");
            var operation = www.SendWebRequest();

            while (!operation.isDone) await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                return default;
            }
            else
            {
                string finish = www.downloadHandler.text.Trim('"').Replace("\\", "");
                Debug.Log(finish);

                T loadedData;
                if (finish == "" || finish == null)
                {
                    loadedData = default;
                }
                else
                {
                    loadedData = JsonUtility.FromJson<T>(www.downloadHandler.text.Trim('"').Replace("\\", ""));
                }
                return loadedData;
            }
        }
        public async Task UpdatePlayerData(string new_data, string player_uuid, string server_uuid)
        {
            WWWForm form = new WWWForm();

            form.AddField("new_data", new_data);
            form.AddField("player_uuid", player_uuid);
            form.AddField("server_uuid", server_uuid);

            using UnityWebRequest www = UnityWebRequest.Post($"{_hostURL}/{_apiName}/{_updatePlayerDataName}/", form);
            var operation = www.SendWebRequest();

            while (!operation.isDone) await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                //return null;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
        public async Task DeletePlayerData(string player_uuid, string server_uuid)
        {
            WWWForm form = new WWWForm();

            form.AddField("player_uuid", player_uuid);
            form.AddField("server_uuid", server_uuid);

            using UnityWebRequest www = UnityWebRequest.Post($"{_hostURL}/{_apiName}/{_deletePlayerDataName}/", form);
            var operation = www.SendWebRequest();

            while (!operation.isDone) await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                //return null;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }

        #endregion

        #region Support Methods

        public async Task<List<GetSupportMessageInfo>> GetSupportMessages(string player_uuid)
        {
            using UnityWebRequest www = UnityWebRequest.Get($"{_hostURL}/{_apiName}/{_getSupportMessageName}/?player_uuid={player_uuid}");
            var operation = www.SendWebRequest();
       
            while (!operation.isDone) await Task.Yield();
       
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                return null;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                return JsonConvert.DeserializeObject<List<GetSupportMessageInfo>>(www.downloadHandler.text);
            }
        }
        public async Task<bool> SendSupportMessage(string message, string player_uuid)
        {
            WWWForm form = new WWWForm();

            form.AddField("text", message);
            form.AddField("answer", 0);
            form.AddField("player_uuid", player_uuid);

            using UnityWebRequest www = UnityWebRequest.Post($"{_hostURL}/{_apiName}/{_sendSupportMessageName}/", form);
            var operation = www.SendWebRequest();

            while (!operation.isDone) await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                return false;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                return true;
            }
        }

        #endregion
    }
}