using UnityEngine;
using UnityEngine.UI;

namespace Kamen
{
    [RequireComponent(typeof(Button))]
    public class SoundButton : MonoBehaviour
    {
        #region Variables

        [SerializeField] private string _soundID;

        #endregion

        #region Unity Methods

        private void Start()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(Click);
        }

        #endregion

        #region Click Methods

        public void Click() => SoundManager.Instance.Play(_soundID);

        #endregion
    }
}