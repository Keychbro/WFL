using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen.MyMesh
{
    [Serializable] public struct MeshTriangle
    {
        #region Variables

        [SerializeField] private int[] _vertexIndexes;

        #endregion

        #region Properties

        public int[] VertexIndexes { get => _vertexIndexes; }

        #endregion

        #region Consturctors

        public MeshTriangle(int index1, int index2, int index3)
        {
            _vertexIndexes = new int[] { index1, index2, index3 };
        }

        #endregion

        #region Methods

        //TODO Redo this methods
        public int[] GetVertexIndexesWithOffset(int offset)
        {
            int[] indexesWithOffset = new int[VertexIndexes.Length];
            for (int i = 0; i < VertexIndexes.Length; i++)
            {
                indexesWithOffset[i] = VertexIndexes[i] + offset;
            }
            return indexesWithOffset;
        }

        #endregion
    }
}