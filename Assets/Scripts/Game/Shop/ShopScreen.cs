using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using System;

namespace WOFL.UI
{
    public class ShopScreen : Kamen.UI.Screen
    {
        #region Classes

        [Serializable] private struct ShopViewControl
        {
            #region ShopViewControl Variables

            [Header("Objects")]
            [SerializeField] private ShopViewButton _shopViewButton;
            [SerializeField] private GameObject _shopView;

            #endregion

            #region ShopViewControl Methods



            #endregion
        }

        #endregion

        #region Variables



        #endregion
    }
}