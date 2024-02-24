using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Setings
{
    [CreateAssetMenu(fileName = "Skin", menuName = "WOFL/Settings/Skin", order = 1)]
    public class Skin : ScriptableObject
    {
        #region Variables

        [SerializeField] private Sprite _skinSprite;
        //[SerializeField] private 

        #endregion
    }
}