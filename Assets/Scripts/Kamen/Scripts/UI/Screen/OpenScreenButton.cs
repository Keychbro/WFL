using Kamen.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Kamen
{
    [RequireComponent(typeof(Button))]
    public class OpenScreenButton : MonoBehaviour
    {
        #region Variables

        [SerializeField] private string _screenID;

        #endregion

        #region Unity Methods

        private void Start()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(Click);
        }

        #endregion

        #region Click Methods

        private void Click() => ScreenManager.Instance.TransitionTo(_screenID);

        #endregion
    }
}