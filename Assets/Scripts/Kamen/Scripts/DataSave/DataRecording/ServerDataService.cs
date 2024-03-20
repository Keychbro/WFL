using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;
using WOFL.Control;

namespace Kamen.DataSave
{
    public class ServerDataService : IDataService, IDataException, IDataWebRequest
    {
        #region Variables

        private string _key;
        private string _iv;
        public string ServerUUID { get; set; }
        public string PlayerUUID { get; set; }

        #endregion

        #region Control Methods

        public void SetEncryptionData(string key, string iv)
        {
            _key = key;
            _iv = iv;
        }
        public void SetServerDataService(string serverUUID, string playerUUID)
        {
            ServerUUID = serverUUID;
            PlayerUUID = playerUUID;
        }

        public bool SaveData<T>(string path, T data, EncryptionType encryptionType)
        {
            Debug.LogError("This method is not implemented");
            return false;
        }
        public T LoadData<T>(string path, EncryptionType encryptionType)
        {
            Debug.LogError("This method is not implemented");
            return default;
        }
        public bool DeleteData(string path)
        {
            Debug.LogError("This method is not implemented");
            return false;
        }

        #endregion

        #region Async Control Methods

        public async Task<bool> SaveDataAsync<T>(string path, T data, EncryptionType encryptionType)
        {
            try
            {
                await ServerConnectManager.Instance.UpdatePlayerData(JsonUtility.ToJson(data), PlayerUUID, ServerUUID);
                return true;
            }
            catch (Exception e)
            {
                ShowException(e);
                return false;
            }
        }
        public async Task<T> LoadDataAsync<T>(string path, EncryptionType encryptionType)
        {
            try
            {
                T data = await ServerConnectManager.Instance.GetPlayerData<T>(ServerUUID, PlayerUUID);
                return data;
            }
            catch (Exception e)
            {
                ShowException(e);
                return default;
            }
        }
        public async Task<bool> DeleteDataAsync(string path)
        {
            try
            {
                await ServerConnectManager.Instance.DeletePlayerData(PlayerUUID, ServerUUID);
                return true;
            }
            catch (Exception e)
            {
                ShowException(e);
                return false;
            }
        }

        #endregion

        #region Message Methods

        public void ShowException(Exception e)
        {
            Debug.LogError($"[Kamen - JsonDataService] Unable to save data due to: {e.Message} {e.StackTrace}");
        }

        #endregion
    }
}