using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using UnityEngine.UI;
using WOFL.Control;
using Cysharp.Threading.Tasks;
using Kamen.DataSave;
using UnityEngine.SceneManagement;

namespace WOFL.UI
{
    public class ConfirmDeletaAccountPopup : Popup
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private KamenButton _noButton;
        [SerializeField] private KamenButton _yesButton;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _noButton.OnClick().RemoveListener(ClickOnNo);
            _yesButton.OnClick().RemoveListener(ClickOnYes);
        }

        #endregion

        #region Control Methods

        public override void Initialize()
        {
            base.Initialize();

            _noButton.Initialize();
            _noButton.OnClick().AddListener(ClickOnNo);

            _yesButton.Initialize();
            _yesButton.OnClick().AddListener(ClickOnYes);
        }
        private void ClickOnNo()
        {
            PopupManager.Instance.Hide("ConfirmDeletaAccountPopup");
        }
        private async void ClickOnYes()
        {
            await ServerConnectManager.Instance.DeletePlayer(DataSaveManager.Instance.MyPlayerAuthData.Email);
            SceneManager.LoadScene(0);
        }

        #endregion
    }
}