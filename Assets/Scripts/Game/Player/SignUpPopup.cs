using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using TMPro;
using WOFL.Control;
using System.Threading.Tasks;
using Kamen.DataSave;

namespace WOFL.UI
{
    public class SignUpPopup : Popup
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private RegistrationInputFieldInfo _emailField;
        [SerializeField] private RegistrationInputFieldInfo _nameField;
        [Space]
        [SerializeField] private Button _signUpButton;
        [SerializeField] private Button _openSignInPopupButton;
        [Space]
        [SerializeField] private RegistrationScreen _registrationScreen;

        [Header("Settings")]
        [SerializeField] private float _finishSignUpWithSuccessDelay;

        [Header("Variables")]
        private RegistrationStates _registrationState = RegistrationStates.FillingField;
        private string _email;
        private string _name;

        #endregion

        #region Control Methods

        public override void Initialize()
        {
            base.Initialize();

            _signUpButton.onClick.AddListener(() => _ = FinishSignUp());
            _openSignInPopupButton.onClick.AddListener(OpenSignInPopup);
        }
        private async Task FinishSignUp()
        {
            if (_registrationState != RegistrationStates.FillingField) return;
            _registrationState = RegistrationStates.CheckingData;

            _email = _emailField.InputField.text;
            _name = _nameField.InputField.text;

            if (!CheckFieldCorrect())
            {
                _registrationState = RegistrationStates.FillingField;
                return;

            }

            UpdateFieldResult(RequestResultView.ResultType.Loading, RequestResultView.ResultType.Loading);
            string result = await ServerConnectManager.Instance.CreatePlayer(_name, _email);

            if (result.Contains("email busy"))
            {
                UpdateFieldResult(RequestResultView.ResultType.Failure, RequestResultView.ResultType.Success);
                _registrationState = RegistrationStates.FillingField;
                return;
            }
            else
            {
                DataSaveManager.Instance.MyPlayerAuthData.StartUsername = _name;
                DataSaveManager.Instance.MyPlayerAuthData.Email = _email;
                DataSaveManager.Instance.MyPlayerAuthData.PlayerUUID = result;
                DataSaveManager.Instance.SavePlayerAuthData();

                _registrationState = RegistrationStates.SuccesResult;
                UpdateFieldResult(RequestResultView.ResultType.Success, RequestResultView.ResultType.Success);
                await Task.Delay(Mathf.RoundToInt(_finishSignUpWithSuccessDelay * 1000));
                _registrationState = RegistrationStates.FinishSignUp;
            }

            _registrationScreen.FinishRegistration();
        }
        private void OpenSignInPopup()
        {
            PopupManager.Instance.Hide("SignUpPopup");
            PopupManager.Instance.Show("SignInPopup");
        }

        #endregion

        #region Help Methods

        private bool CheckFieldCorrect()
        {
            bool isCorrect = true;
            if (!_email.Contains("@") || !_email.Contains(".") || _email == "")
            {
                _emailField.RequesResultView.CallResult(RequestResultView.ResultType.Failure);
                isCorrect = false;
            }
            else _emailField.RequesResultView.CallResult(RequestResultView.ResultType.None);
            if (_name == "")
            {
                _nameField.RequesResultView.CallResult(RequestResultView.ResultType.Failure);
                isCorrect = false;
            }
            else _nameField.RequesResultView.CallResult(RequestResultView.ResultType.None);

            return isCorrect;
        }
        private void UpdateFieldResult(RequestResultView.ResultType emailResult, RequestResultView.ResultType nameResult)
        {
            _emailField.RequesResultView.CallResult(emailResult);
            _nameField.RequesResultView.CallResult(nameResult);
        }

        #endregion
    }
}

