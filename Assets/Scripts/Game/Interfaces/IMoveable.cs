using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    public interface IMoveable
    {
        #region Enums

        public enum MoveType
        {
            Standing,
            Going
        }

        #endregion

        #region Properties

        public Vector3 MoveDirection { get; }
        public MoveType MovingType { get; }

        #endregion

        #region Control Methods

        public void Move();
        public void Stand();

        #endregion
    }
}