using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Settings;

namespace WOFL.Game
{
    public interface IAttacking
    {
        #region Properties

        public bool IsAttacking { get; }

        #endregion

        #region Control Methods

        public void Attack();
        public void FindClosestTarget(List<IDamageable> allTargets);
        public Vector3 GetTargetPosition(IDamageable iDamageable);

        #endregion
    }
}

