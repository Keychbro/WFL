using System.Threading.Tasks;

namespace Kamen.DataSave
{
    public interface IDataService
    {
        #region Enums

        public enum DataHandleType
        {
            MainThread,
            Async
        }

        #endregion

        #region Methods

        public void SetEncryptionData(string key, string iv);

        public bool SaveData<T>(string path, T data, EncryptionType encryptionType);
        public T LoadData<T>(string path, EncryptionType encryptionType);
        public bool DeleteData(string path);

        public Task<bool> SaveDataAsync<T>(string path, T data, EncryptionType encryptiontype);
        public Task<T> LoadDataAsync<T>(string path, EncryptionType encryptionType);
        public Task<bool> DeleteDataAsync(string path);

        #endregion
    }
}
