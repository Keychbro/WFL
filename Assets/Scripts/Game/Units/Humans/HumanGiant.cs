using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Control;
using WOFL.Save;
using WOFL.Settings;
using Random = UnityEngine.Random;

namespace WOFL.Game
{
    public class HumanGiant : AttackingUnit
    {
        #region IAttacking Methods

        public override void FindClosestTarget(List<IDamageable> allTargets)
        {
            IDamageable closestTarget = null;
            float minDistance = float.MaxValue;

            for (int i = 0; i < allTargets.Count; i++)
            {
                MonoBehaviour monoBehaviour = GetTargetMonoBehaviour(allTargets[i]);
                if (monoBehaviour == null) continue;
                
                if (!monoBehaviour.TryGetComponent<IBuilding>(out IBuilding building)) continue;

                float distance = Vector2.Distance(transform.position, monoBehaviour.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = allTargets[i];
                }
            }

            _currentTargetDamageable = closestTarget;

            if (_currentTargetDamageable != null)
            {
                Type damageableType = _currentTargetDamageable.GetType();
                if (typeof(MonoBehaviour).IsAssignableFrom(damageableType))
                {
                    _currentTargetObject = (MonoBehaviour)_currentTargetDamageable;
                }
            }
            else _currentTargetObject = null;
        }

        #endregion
    }
}