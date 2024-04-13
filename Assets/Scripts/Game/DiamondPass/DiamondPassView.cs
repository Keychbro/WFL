using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WOFL.Payment;
using WOFL.Settings;

namespace WOFL.DiamondPass
{
    public class DiamondPassView : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _background;
        [SerializeField] private Image _ticket;
        [SerializeField] private Image _icon;
        [Space]
        [SerializeField] private PaymentView _paymentView;
        [Space]
        [SerializeField] private Slider _levelsBar;
        [SerializeField] private TextMeshProUGUI _stageText;
        [SerializeField] private TextMeshProUGUI _progressText;

        [Header("Settings")]
        [SerializeField] private DiamondPassTicketSettings _diamondPassTicketSettings;

        [Header("Variables")]
        private DiamondPassStageInfo[] _stageInfos;
        private DiamondPassDataSave _passDataSave;
        private DiamondPassStageInfo _currentLevelInfo;
        
        #endregion

        #region Control Methods

        public void Initialize(DiamondPassStageInfo[] stageInfos, DiamondPassDataSave passDataSave)
        {
            _stageInfos = stageInfos;
            _passDataSave = passDataSave;

            _passDataSave.OnPurchased += UpdatePassMode;
            UpdatePassMode();

        }
        private void UpdatePassMode()
        {
            if (_passDataSave.IsPassPurchased)
            {
                _ticket.sprite = _diamondPassTicketSettings.OnTicket;
                _paymentView.gameObject.SetActive(false);
                _levelsBar.gameObject.SetActive(true);
                _icon.gameObject.SetActive(true);
            }
            else
            {
                _ticket.sprite = _diamondPassTicketSettings.OffTicket;
                _paymentView.gameObject.SetActive(true);
                _levelsBar.gameObject.SetActive(false);
                _icon.gameObject.SetActive(false);
            }
        }

        #endregion
    }
}