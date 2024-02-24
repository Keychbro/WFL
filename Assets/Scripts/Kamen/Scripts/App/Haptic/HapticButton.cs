using UnityEngine;
using UnityEngine.UI;

namespace Kamen
{
    [RequireComponent(typeof(Button))]
    public class HapticButton : MonoBehaviour
    {
        #region Variables

        [SerializeField] private string _hapticID;

        #endregion

        #region Unity Methods

        private void Start()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(Click);
        }

        #endregion

        #region Click Methods

        public void Click() => HapticManager.Instance.Play(_hapticID);

        #endregion
    }
}