using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kamen.UI;


namespace WOFL.UI
{
    public class UserProfilePopup : Popup
    {
        #region Variables

        [Header("User Datas Objects")]
        [SerializeField] private Image _userIcon;
        [SerializeField] private TextMeshProUGUI _userlevel;
        [SerializeField] private TMP_InputField _userNameField;
        [SerializeField] private Button _updateNameButton;

        [Header("Languages Objects")]
        [SerializeField] private Image _choosenLanguageIcon;
        [SerializeField] private TMP_Dropdown _languageChooser;

        #endregion
    }
}
