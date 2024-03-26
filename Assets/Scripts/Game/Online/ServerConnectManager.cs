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

        [Header("Variables")]
        [SerializeField] private List<ServerInfo> _serverInfos;
        [SerializeField] private List<GetMessageInfo> _getMessageInfos;
        [SerializeField] private CreatePlayerSuccessInfo _createPlayerAnswerInfo;
        [SerializeField] private CreatePlayerFailureInfo _createPlayerFailureInfo;
        [SerializeField] private GetPlayerUUIDInfo _getPlayerUUIDInfo;
        [SerializeField] private Data _data;

        #endregion

        #region Control Methods

        private void Start()
        {
            //GetServersList();
            //GetMessages("fb988152-8f01-4f9a-b436-3691d2ffe806", Fraction.FractionName.Human);
            //SendMessage("fb988152-8f01-4f9a-b436-3691d2ffe806", "7b867119-c1a7-40da-b51e-294d8f66b7f1", Fraction.FractionName.Human, "Test message");
            //CreatePlayer("Test player9", "test8gmail.com", 0);
            //GetPlayerUUID("test5@gmail.com");
            //GetPlayerData("fb988152-8f01-4f9a-b436-3691d2ffe806", "7b867119-c1a7-40da-b51e-294d8f66b7f1");
            //UpdatePlayerData(JsonUtility.ToJson(DataSaveManager.Instance.MyData), "dc5c24c6-fd8a-404e-aee3-ff770086e201", "fb988152-8f01-4f9a-b436-3691d2ffe806");
            //DeletePlayer("test4@gmail.com");
            //DeletePlayerData("dc5c24c6-fd8a-404e-aee3-ff770086e201", "fb988152-8f01-4f9a-b436-3691d2ffe806");
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
                _serverInfos = JsonConvert.DeserializeObject<List<ServerInfo>>(www.downloadHandler.text);
                return _serverInfos;
            }
        }

        #endregion

        #region Messages Methods

        public async Task<List<GetMessageInfo>> GetMessages(string server_uuid, Fraction.FractionName fraction)
        {
            string realFraction = fraction == Fraction.FractionName.None ? "global" : fraction.ToString();
            using UnityWebRequest www = UnityWebRequest.Get($"{_hostURL}/{_apiName}/{_getMessagesName}/?server_uuid={server_uuid}&fraction={realFraction}");
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
                _getMessageInfos = JsonConvert.DeserializeObject<List<GetMessageInfo>>(www.downloadHandler.text);
                return _getMessageInfos;

                //_serverInfos = JsonConvert.DeserializeObject<List<ServerInfo>>(www.downloadHandler.text);
            }
        }
        public async void SendMessage(string server_uuid, string player_uuid, Fraction.FractionName fraction, string message)
        {
            WWWForm form = new WWWForm();

            form.AddField("server_uuid", server_uuid);
            form.AddField("player_uuid", player_uuid);
            form.AddField("fraction", fraction == Fraction.FractionName.None ? "global" : fraction.ToString());
            form.AddField("text", message);

            using UnityWebRequest www = UnityWebRequest.Post($"{_hostURL}/{_apiName}/{_sendMessageName}/", form);
            var operation = www.SendWebRequest();

            while (!operation.isDone) await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
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
                    _createPlayerFailureInfo = JsonConvert.DeserializeObject<CreatePlayerFailureInfo>(www.downloadHandler.text);
                    return _createPlayerFailureInfo.info;
                }
                else
                {
                    _createPlayerAnswerInfo = JsonConvert.DeserializeObject<CreatePlayerSuccessInfo>(www.downloadHandler.text);
                    return _createPlayerAnswerInfo.player_uuid;
                }
            }
        }
        public async void DeletePlayer(string email)
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
                if (www.downloadHandler.text.Contains("email busy"))
                {
                    _createPlayerFailureInfo = JsonConvert.DeserializeObject<CreatePlayerFailureInfo>(www.downloadHandler.text);
                   // return _createPlayerFailureInfo.info;
                }
                else
                {
                    _createPlayerAnswerInfo = JsonConvert.DeserializeObject<CreatePlayerSuccessInfo>(www.downloadHandler.text);
                   // return _createPlayerAnswerInfo.player_uuid;
                }
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
                _getPlayerUUIDInfo = JsonConvert.DeserializeObject<GetPlayerUUIDInfo>(www.downloadHandler.text);
                return _getPlayerUUIDInfo.player_uuid;
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
    }
}