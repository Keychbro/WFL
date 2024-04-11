using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.UI
{
    public class PassShopPack : ShopPack
    {
        #region Variables

        [Header("Pass Variables")]
        [SerializeField] private GameObject _passHolder;

        [Header("Variables")]
        private List<GameObject> _passes = new List<GameObject>();

        #endregion

        #region Properties

        public GameObject PassHolder { get => _passHolder; }

        #endregion

        #region Control Methods

        public void AddPasses(List<GameObject> passes)
        {
            _passes = passes;
        }

        #endregion
    }
}