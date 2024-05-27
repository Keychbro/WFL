using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WOFL.Control;
using Kamen.DataSave;
using Cysharp.Threading.Tasks;
using WOFL.Game;
using Kamen.UI;

namespace WOFL.UI
{
    [RequireComponent(typeof(Button))]
    public class MiniProfileIcon : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _icon;
        [SerializeField] private Image _panelBackground;
        [SerializeField] private TextMeshProUGUI _profileText;
        private Button _button;

        [Header("Settings")]
        [SerializeField] private string _popupOpenName;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Initialize();
        }
        private void OnDestroy()
        {
            DataSaveManager.Instance.MyData.OnIconNumberChanged -= UpdateIcon;
        }

        #endregion

        #region Control Methods

        private async void Initialize()
        {
            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);
            await UniTask.WaitUntil(() => DataSaveManager.Instance.MyData.ChoosenFraction != Fraction.FractionName.None);

            UpdateIcon();
            DataSaveManager.Instance.MyData.OnIconNumberChanged += UpdateIcon;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }
        private void Click()
        {
            PopupManager.Instance.Show(_popupOpenName);
        }
        private void UpdateIcon() 
        {
            _icon.sprite = FractionManager.Instance.CurrentFraction.PlayerProfileSettings.IconsList[DataSaveManager.Instance.MyData.IconNumber];
        }

        #endregion
    }
}