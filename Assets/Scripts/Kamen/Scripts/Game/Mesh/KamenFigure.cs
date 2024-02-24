using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen.MyMesh
{
    public abstract class KamenFigure
    {
        #region Variables

        protected Vector3[] _initialVertices;

        #endregion

        #region Properties

        public List<Vector3> Vertices { get; protected set; }
        public List<Vector2> UVs { get; protected set; }
        public List<int> TrianglesIndexes { get; protected set; }

        #endregion
    }
}

