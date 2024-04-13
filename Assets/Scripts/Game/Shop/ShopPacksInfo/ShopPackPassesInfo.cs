using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Shop Pack Passes Info", menuName = "WOFL/Shop/Shop Pack Passes Info", order = 1)]
    public class ShopPackPassesInfo : ShopPackInfo
    {
        #region Variables

        [SerializeField] private bool _isCreateDiamondPass;
        [SerializeField] private bool _isCreateBattlePass;

        #endregion

        #region Properties

        public bool IsCreateDiamondPass { get => _isCreateDiamondPass; }
        public bool IsCreateBattlePass { get => _isCreateBattlePass; }

        #endregion
    }
}