using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Shop Pack Passes Info", menuName = "WOFL/Shop/Shop Pack Passes Info", order = 1)]
    public class ShopPackPassesInfo : ShopPackInfo
    {
        #region Variables

        [SerializeField] private bool _isCreateBattlePass;

        #endregion

        #region Properties

        public bool IsCreateBattlePass { get => _isCreateBattlePass; }

        #endregion
    }
}