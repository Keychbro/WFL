using UnityEngine;
using Kamen.Theme;

namespace Kamen
{
    public class ThemeManager : SingletonComponent<ThemeManager>
    {
        #region Variables

        [SerializeField] private ThemeObjectsInfo[] _themeObjectInfos;
        [SerializeField] private string[] _themesID;
        [Space]
        [SerializeField] private float _changeDuration;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < _themeObjectInfos.Length; i++)
            {
                _themeObjectInfos[i].Initialize();
            }
        }

        #endregion

        #region Control Methods

        public void ChangeTheme(string id, bool isFast = false)
        {
            for (int i = 0; i < _themeObjectInfos.Length; i++) 
            {
                _themeObjectInfos[i].ChangeTheme(id, isFast ? 0 : _changeDuration);
            }
        }

        #endregion
    }
}