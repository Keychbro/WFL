using System;
using UnityEngine;
using WOFL.Online;

namespace Kamen.DataSave
{
    public class DataSaveManager : SingletonComponent<DataSaveManager>
    {
        #region Enums

        private enum SaveType
        {
            Json,
            Server
        }

        #endregion

        #region Classes

        [Serializable] private class DataBaseInfo
        {
            #region DataBaseInfo Variables

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

            #region DataBaseInfo Properties

            public SaveType SaveType { get => _saveType; }
            public string FileName { get => _fileName; }

            public EncryptionType EncryptionType { get => _encryptionType; }
            public string Key { get => _key; }  
            public string IV { get => _iv; }

            public IDataService DataService { get => _dataService; }
            public string Path { get => _path; }
            public string Extension { get => _extension; }

            #endregion

            #region DataBaseInfo Methods

            public void UpdateSaveSettings(IDataService dataService, string path, string extension)
            {
                _dataService = dataService;
                _path = path;
                _extension = extension;
            }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private DataBaseInfo _myDataInfo;
        [SerializeField] private DataBaseInfo _myPlayerAuthDataInfo;

        #endregion

        #region Properties

        public Data MyData { get; private set; }
        public PlayerAuthData MyPlayerAuthData { get; private set; }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            SetUpManager(_myDataInfo);
            SetUpManager(_myPlayerAuthDataInfo);
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
            MyData = _myDataInfo.DataService.LoadData<Data>(_myDataInfo.Path, _myDataInfo.EncryptionType);
            MyData ??= new Data();

            MyPlayerAuthData = _myPlayerAuthDataInfo.DataService.LoadData<PlayerAuthData>(_myPlayerAuthDataInfo.Path, _myPlayerAuthDataInfo.EncryptionType);
            MyPlayerAuthData ??= new PlayerAuthData();
        }
        public void SaveData() => _myDataInfo.DataService.SaveData(_myDataInfo.Path, MyData, _myDataInfo.EncryptionType);
        public void SavePlayerAuthData() => _myPlayerAuthDataInfo.DataService.SaveData(_myPlayerAuthDataInfo.Path, MyData, _myPlayerAuthDataInfo.EncryptionType);
        public void DeleteData()
        {
            _myDataInfo.DataService.DeleteData(_myDataInfo.Path);
        }
        public void DeletePlayerAuthData()
        {
            _myPlayerAuthDataInfo.DataService.DeleteData(_myPlayerAuthDataInfo.Path);
        }

        #endregion

        #region Set Up Methods

        private void OnValidate()
        {
            SetUpManager(_myDataInfo);
            SetUpManager(_myPlayerAuthDataInfo);
        }
        private void SetUpManager(DataBaseInfo dataBaseInfo)
        {
            IDataService dataService;
            string path;
            string extension;


            switch (dataBaseInfo.SaveType)
            {
                case SaveType.Json:
                    dataService = new JsonDataService();
                    extension = ".json";
                    break;
                case SaveType.Server:
                    dataService = new ServerDataService();
                    extension = ".json";
                    break;
                default:
                    dataService = new JsonDataService();
                    extension = ".json";
                    break;
            }
            dataService.SetEncryptionData(dataBaseInfo.Key, dataBaseInfo.IV);
            path = Application.persistentDataPath + "/" + dataBaseInfo.FileName + extension;
        }
        //public void GenerateNewKeyAndIV() => Encrypter.GenerateAesKeyAndIV(ref _key, ref _iv);

        #endregion
    }
}