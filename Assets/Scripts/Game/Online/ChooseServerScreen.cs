using DG.Tweening;
using Kamen;
using Kamen.DataSave;
using Kamen.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WOFL.Control;
using WOFL.Online;

namespace WOFL.UI
{
    public class ChooseServerScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private ServerPanel _serverPanelPrefab;

        [Header("Objects")]
        [SerializeField] private LoadingIcon _loadingIcon;
        [SerializeField] private Transform _serverHolder;

        [Header("Variables")]
        private List<ServerPanel> _serverPanels = new List<ServerPanel>();

        #endregion

        #region Properties



        #endregion

        #region Control Methods

        public override void Initialize()
        {
            base.Initialize();
            _ = CreateServerList();
        }
        private async Task CreateServerList()
        {
            Debug.Log(1);
            _loadingIcon.CallAppear();

            Debug.Log(2);
            List<ServerInfo> serverInfos = await ServerConnectManager.Instance.GetServersList();
            Debug.Log(3);
            _loadingIcon.CallDissapear();
            Debug.Log(serverInfos.Count);
            for (int i = 0; i < serverInfos.Count; i++)
            {
                ServerPanel newPanel = Instantiate(_serverPanelPrefab, _serverHolder);
                newPanel.Intialize(serverInfos[i]);
                _serverPanels.Add(newPanel);
            }
        }

        #endregion
    }
}