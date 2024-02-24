using System;
using UnityEngine;

namespace Kamen.DataSave
{
    public class DataSaveManager : SingletonComponent<DataSaveManager>
    {
        #region Enums

        private enum SaveType
        {
            Json
        }

        #endregion

        #region Variables

        [Header("Save settings")]
        [SerializeField] private SaveType _saveType;
        [SerializeField] private string _fileName;
        private IDataService _dataService;
        private string _path;
        private string _extension;

        [Header("Encryption settings")]
        [SerializeField] private EncryptionType _encryptionType;
        [SerializeField] private string _key;
        [SerializeField] private string _iv;

        #endregion

        #region Properties

        public Data MyData { get; private set; }
        public EncryptionType MyEncryptionType { get => _encryptionType; } 

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            SetUpManager();
            GetData();
        }
        private void OnEnable()
        {
            MyData.OnDataChanged += SaveData;
        }
        private void OnDisable()
        {
            MyData.OnDataChanged -= SaveData;
        }

        #endregion

        #region Control Methods

        public void GetData()
        {
            MyData = _dataService.LoadData<Data>(_path, _encryptionType);
            MyData ??= new Data();
        }
        public void SaveData() => _dataService.SaveData(_path, MyData, _encryptionType);
        public void DeleteData()
        {
            _dataService.DeleteData(_path);
            PlayerPrefs.DeleteKey("LeaderboardPlayerRank");
        }

        #endregion

        #region Set Up Methods

        private void OnValidate()
        {
            SetUpManager();
        }
        private void SetUpManager()
        {
            switch (_saveType)
            {
                case SaveType.Json:
                    _dataService = new JsonDataService();
                    _extension = ".json";
                    break;
            }
            _dataService.SetEncryptionData(_key, _iv);
            _path = Application.persistentDataPath + "/" + _fileName + _extension;
        }
        public void GenerateNewKeyAndIV() => Encrypter.GenerateAesKeyAndIV(ref _key, ref _iv);

        #endregion
    }
}