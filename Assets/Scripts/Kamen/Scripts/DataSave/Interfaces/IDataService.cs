namespace Kamen.DataSave
{
    public interface IDataService
    {
        #region Methods

        public void SetEncryptionData(string key, string iv);
        public bool SaveData<T>(string path, T data, EncryptionType encryptionType);
        public T LoadData<T>(string path, EncryptionType encryptionType);
        public bool DeleteData(string path);

        #endregion
    }
}
