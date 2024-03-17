using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using TMPro;
using WOFL.Control;
using System.Threading.Tasks;

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

        [Header("Settings")]
        [SerializeField] private float _finishSignUpWithSuccessDelay;

        [Header("Variables")]
        private RegistrationStates _state = RegistrationStates.FillingField;
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
            if (_state != RegistrationStates.FillingField) return;
            _state = RegistrationStates.CheckingData;

            _email = _emailField.InputField.text;
            _name = _nameField.InputField.text;

            if (!CheckFieldCorrect())
            {
                _state = RegistrationStates.FillingField;
                return;

            }

            UpdateFieldResult(RequestResultView.ResultType.Loading, RequestResultView.ResultType.Loading);
            string result = await ServerConnectManager.Instance.CreatePlayer(_name, _email, 0);

            if (result.Contains("email busy"))
            {
                UpdateFieldResult(RequestResultView.ResultType.Failure, RequestResultView.ResultType.Success);
                _state = RegistrationStates.FillingField;
                return;
            }
            else
            {
                //Save uuid in data
                _state = RegistrationStates.SuccesResult;
                UpdateFieldResult(RequestResultView.ResultType.Success, RequestResultView.ResultType.Success);
                await Task.Delay(Mathf.RoundToInt(_finishSignUpWithSuccessDelay * 1000));
                _state = RegistrationStates.FinishSignUp;
            }

            //TODO: Hide Registration Screen
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

