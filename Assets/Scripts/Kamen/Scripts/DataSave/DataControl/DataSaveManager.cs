using System;
using System.Threading.Tasks;
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
            [SerializeField] private IDataService.DataHandleType _dataHandleType;
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

            public IDataService.DataHandleType DataHandleType { get => _dataHandleType; }
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
        public bool IsDataLoaded { get; private set; }

        public PlayerAuthData MyPlayerAuthData { get; private set; }
        public bool IsPlayerAuthDataLoaded { get; private set; }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            SetUpManager(_myPlayerAuthDataInfo);        
            GetMyPlayerAuthData();
            Debug.Log(123);
        }
        public void AdjustPlayerDataOnServer()
        {
            SetUpManager(_myDataInfo);
            (_myDataInfo.DataService as IDataWebRequest).ServerUUID = MyPlayerAuthData.ServerUUID;
            (_myDataInfo.DataService as IDataWebRequest).PlayerUUID = MyPlayerAuthData.PlayerUUID;

            GetData();
        }

        #endregion

        #region Control Methods

        public async Task GetData()
        {
            switch (_myDataInfo.DataHandleType)
            {
                case IDataService.DataHandleType.MainThread:
                    MyData = _myDataInfo.DataService.LoadData<Data>(_myDataInfo.Path, _myDataInfo.EncryptionType);
                    MyData ??= new Data();
                    IsDataLoaded = true;
                    break;
                case IDataService.DataHandleType.Async:
                    MyData = await _myDataInfo.DataService.LoadDataAsync<Data>(_myDataInfo.Path, _myDataInfo.EncryptionType);
                    MyData ??= new Data();
                    IsDataLoaded = true;
                    break;
            }            
        }
        public async Task GetMyPlayerAuthData()
        {
            Debug.Log(12323);
            switch (_myPlayerAuthDataInfo.DataHandleType)
            {
                case IDataService.DataHandleType.MainThread:
                    Debug.Log(1);
                    MyPlayerAuthData = _myPlayerAuthDataInfo.DataService.LoadData<PlayerAuthData>(_myPlayerAuthDataInfo.Path, _myPlayerAuthDataInfo.EncryptionType);
                    Debug.Log(2);
                    MyPlayerAuthData ??= new PlayerAuthData();
                    Debug.Log(3);
                    IsPlayerAuthDataLoaded = true;
                    Debug.Log(123123);
                    break;
                case IDataService.DataHandleType.Async:
                    Debug.Log(1223);
                    MyPlayerAuthData = await _myPlayerAuthDataInfo.DataService.LoadDataAsync<PlayerAuthData>(_myPlayerAuthDataInfo.Path, _myPlayerAuthDataInfo.EncryptionType);
                    MyPlayerAuthData ??= new PlayerAuthData();
                    IsPlayerAuthDataLoaded = true;
                    break;
            }
        }
        public void SaveData()
        {
            switch (_myDataInfo.DataHandleType)
            {
                case IDataService.DataHandleType.MainThread:
                    _myDataInfo.DataService.SaveData(_myDataInfo.Path, MyData, _myDataInfo.EncryptionType);
                    break;
                case IDataService.DataHandleType.Async:
                    _myDataInfo.DataService.SaveDataAsync(_myDataInfo.Path, MyData, _myDataInfo.EncryptionType);
                    break;
            }
        }
        public void SavePlayerAuthData()
        {
            switch (_myPlayerAuthDataInfo.DataHandleType)
            {
                case IDataService.DataHandleType.MainThread:
                    _myPlayerAuthDataInfo.DataService.SaveData(_myPlayerAuthDataInfo.Path, MyData, _myPlayerAuthDataInfo.EncryptionType);
                    break;
                case IDataService.DataHandleType.Async:
                    _myPlayerAuthDataInfo.DataService.SaveDataAsync(_myPlayerAuthDataInfo.Path, MyData, _myPlayerAuthDataInfo.EncryptionType);
                    break;
            }
        }
        public void DeleteData()
        {
            switch (_myDataInfo.DataHandleType)
            {
                case IDataService.DataHandleType.MainThread:
                    _myDataInfo.DataService.DeleteData(_myDataInfo.Path);
                    break;
                case IDataService.DataHandleType.Async:
                    _myDataInfo.DataService.DeleteDataAsync(_myDataInfo.Path);
                    break;
            }
        }
        public void DeletePlayerAuthData()
        {
            switch (_myPlayerAuthDataInfo.DataHandleType)
            {
                case IDataService.DataHandleType.MainThread:
                    _myPlayerAuthDataInfo.DataService.DeleteData(_myPlayerAuthDataInfo.Path);
                    break;
                case IDataService.DataHandleType.Async:
                    _myPlayerAuthDataInfo.DataService.DeleteDataAsync(_myPlayerAuthDataInfo.Path);
                    break;
            }
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