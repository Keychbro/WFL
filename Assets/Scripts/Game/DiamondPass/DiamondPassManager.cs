using Cysharp.Threading.Tasks;
using Kamen;
using Kamen.DataSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.DiamondPass
{
    public class DiamondPassManager : SingletonComponent<DiamondPassManager>
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private DiamondPassStageInfo[] _stageInfos;

        #endregion

        #region Control Methods

        protected async override void Awake()
        {
            base.Awake();
            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);
            
            DataSaveManager.Instance.MyData.DiamondPassDataSave.AdjustDiamondPassStages(_stageInfos);
            DataSaveManager.Instance.SaveData();
        }

        #endregion
    }
}