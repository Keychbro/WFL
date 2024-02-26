using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    public class Castle : MonoBehaviour, IDamagable
    {
        #region Variables

       //[Header("Variables")
       //[]

        #endregion

        #region Properties

        public int CurrentGold { get; private set; }

        public int MaxHealth { get; }

        public event Action<int> OnTakedDamage;

        #endregion

        #region IDamagable Methods

        public void TakeDamage(int value)
        {

        }

        #endregion

    }
}

