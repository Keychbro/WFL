using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using System.Threading.Tasks;
using WOFL.Control;

namespace WOFL.UI
{
    public class SignInPopup : Popup
    {
        #region Variables

        [Header("Objects")]

        [SerializeField] private RegistrationInputFieldInfo _emailField;
        [Space]
        [SerializeField] private Button _signInButton;
        [SerializeField] private Button _openSignInPopupButton;

        [Header("Settings")]
        [SerializeField] private float _finishSignUpWithSuccessDelay;

        [Header("Variables")]
        private RegistrationStates _state = RegistrationStates.FillingField;
        private string _email;

        #endregion

        #region Control Methods

        public override void Initialize()
        {
            base.Initialize();

            _signInButton.onClick.AddListener(() => _ = FinishSignIn());
            _openSignInPopupButton.onClick.AddListener(OpenSignUpPopup);
        }
        private async Task FinishSignIn()
        {
            if (_state != RegistrationStates.FillingField) return;
            _state = RegistrationStates.CheckingData;

            _email = _emailField.InputField.text;


            if (!_email.Contains("@") || !_email.Contains(".") || _email == "")
            {
                _emailField.RequesResultView.CallResult(RequestResultView.ResultType.Failure);
                _state = RegistrationStates.FillingField;
                return;
            }
            else _emailField.RequesResultView.CallResult(RequestResultView.ResultType.None);

            _emailField.RequesResultView.CallResult(RequestResultView.ResultType.Loading);
            string result = await ServerConnectManager.Instance.GetPlayerUUID(_email);
            Debug.Log(result);

            if (result == null)
            {
                _emailField.RequesResultView.CallResult(RequestResultView.ResultType.Failure);
                _state = RegistrationStates.FillingField;
                return;
            }
            else
            {
                //Save uuid in data
                _state = RegistrationStates.SuccesResult;
                _emailField.RequesResultView.CallResult(RequestResultView.ResultType.Success);
                await Task.Delay(Mathf.RoundToInt(_finishSignUpWithSuccessDelay * 1000));
                _state = RegistrationStates.FinishSignUp;
            }

            //TODO: Hide Registration Screen
        }

        #endregion

        #region Help Methods

        private void OpenSignUpPopup()
        {
            PopupManager.Instance.Hide("SignInPopup");
            PopupManager.Instance.Show("SignUpPopup");
        }

        #endregion
    }
}