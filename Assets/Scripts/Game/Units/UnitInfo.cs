using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Game;
using WOFL.Game.Components;

namespace WOFL.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class UnitInfo : MonoBehaviour, IDamagable, IDeathable
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private HealthBar _healtBar;

        [Header("Settings")]
        [SerializeField] private string _name;
        [SerializeField] private int _startHealthValue;
        [SerializeField] private Sprite _appearance;


        #endregion

        #region Properties

        public string Name { get => _name; }
        public int StartHealthValue { get => _startHealthValue; }
        public Sprite Appearance { get => _appearance; }

        #endregion

        #region Control Methods


        
        #endregion

        #region IDamagable Methods

        public void TakeDamage(int value)
        {
            
        }

        #endregion

        #region IDeathable Methods

        public void Death()
        {

        }

        #endregion
    }
}
