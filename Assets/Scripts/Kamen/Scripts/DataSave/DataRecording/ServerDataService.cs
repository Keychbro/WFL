using UnityEngine;
using System;
using System.IO;

namespace Kamen.DataSave
{
    public class ServerDataService : IDataService, IDataException
    {
        #region Variables

        private string _key;
        private string _iv;

        #endregion

        #region Control Methods

        public void SetEncryptionData(string key, string iv)
        {
            _key = key;
            _iv = iv;
        }

        public bool SaveData<T>(string path, T data, EncryptionType encryptionType)
        {
            try
            {
                if (!File.Exists(path))
                {
                    using FileStream stream = File.Create(path);
                }

                switch (encryptionType)
                {
                    case EncryptionType.Aes:
                        using (FileStream stream = File.OpenWrite(path))
                        {
                            Encrypter.AesEncrypt(JsonUtility.ToJson(data), stream, _key, _iv);
                        }
                        break;
                    default:
                        File.WriteAllText(path, JsonUtility.ToJson(data));
                        break;
                }
                return true;
            }
            catch (Exception e)
            {
                ShowException(e);
                return false;
            }
        }
        public T LoadData<T>(string path, EncryptionType encryptionType)
        {
            try
            {
                if (File.Exists(path))
                {
                    T data = encryptionType switch
                    {
                        EncryptionType.Aes => JsonUtility.FromJson<T>(Encrypter.AesDecrypt(path, _key, _iv)),
                        _ => JsonUtility.FromJson<T>(File.ReadAllText(path)),
                    };
                    return data;
                }
                else return default;
            }
            catch (Exception e)
            {
                ShowException(e);
                return default;
            }
        }
        public bool DeleteData(string path)
        {
            try
            {
                if (File.Exists(path)) File.Delete(path);
                else Debug.LogError($"[Kamen - JsonDataService] File with path \"{path}\" does not exist");
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