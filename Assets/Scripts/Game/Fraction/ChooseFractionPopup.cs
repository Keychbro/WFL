using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using Cysharp.Threading.Tasks;
using Kamen.DataSave;
using WOFL.Control;

namespace WOFL.UI
{
    public class ChooseFractionPopup : Popup
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private FractionPanel _fractionPanelPrefab;

        [Header("Objects")]
        [SerializeField] private Transform _panelsHolder;
        [SerializeField] private KamenButton _startGameButton;

        [Header("Variables")]
        private List<FractionPanel> _fractionPanels = new List<FractionPanel>();
        private FractionPanel _currentFractionPanel;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _startGameButton.OnClick().RemoveListener(StartGame);
            for (int i = 0; i < _fractionPanels.Count; i++)
            {
                _fractionPanels[i].OnFractionPanelClicked -= ChangeCurrentPanel;
            }
        }

        #endregion

        #region Control Methods

        public async override void Initialize()
        {
            base.Initialize();
            
            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);
            CreateFractionPanels();

            _startGameButton.Initialize();
            _startGameButton.OnClick().AddListener(StartGame);
            _startGameButton.ChangeInteractable(false);
        }
        private void CreateFractionPanels()
        {
            for (int i = 0; i < FractionManager.Instance.Fractions.Length; i++)
            {
                FractionPanel newPanel = Instantiate(_fractionPanelPrefab, _panelsHolder);
                newPanel.Initialize(FractionManager.Instance.Fractions[i]);
                newPanel.OnFractionPanelClicked += ChangeCurrentPanel;

                _fractionPanels.Add(newPanel);
            }
        }
        private void ChangeCurrentPanel(FractionPanel fractionPanel)
        {
            if (fractionPanel == _currentFractionPanel) return;

            if (_currentFractionPanel == null) _startGameButton.ChangeInteractable(true);
            else _currentFractionPanel.SwitchActive(false);

            fractionPanel.SwitchActive(true);

            _currentFractionPanel = fractionPanel;
        }
        private void StartGame()
        {
            DataSaveManager.Instance.MyData.ChoosenFraction = _currentFractionPanel.GetFractionName();
            DataSaveManager.Instance.MyData.IconNumber = Random.Range(0, FractionManager.Instance.CurrentFraction.PlayerProfileSettings.IconsList.Length);
            DataSaveManager.Instance.SaveData();

            PopupManager.Instance.Hide("ChooseFractionPopup");
        }

        #endregion
    }
}