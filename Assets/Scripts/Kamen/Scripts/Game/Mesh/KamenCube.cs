using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

namespace Kamen.MyMesh
{
    public class KamenCube : KamenFigure
    {
        #region Classes

        private class Side
        {
            #region Side Properties

            public Vector3 LookDirection { get; private set; }
            public int[] VerticesIndexes { get; private set; }
            public Vector2[] UVs { get; private set; }
            public MeshTriangle[] TriangleIndexes { get; private set; }

            #endregion

            #region Side Consturtors

            public Side(Vector3 lookDirection, int[] vectorIndexes, Vector2[] uvs, MeshTriangle[] indexTriangle)
            {
                LookDirection = lookDirection;
                VerticesIndexes = vectorIndexes;
                UVs = uvs;
                TriangleIndexes = indexTriangle;
            }

            #endregion
        }

        #endregion

        #region Variables

        private Side[] _allSides;
        private Dictionary<Vector3, Side> _usedSides;

        private Vector3[] _gameInitialVertices;

        private Vector3 _offsetPosition = Vector3.zero;
        private Vector3 _scale = Vector3.one;

        #endregion

        #region Constructors

        public KamenCube()
        {
            SetVertices();
            SetCubeSides();
            InitializeArrayes();
        }
        private void SetVertices()
        {
            _initialVertices = new Vector3[8]
            {
                new Vector3(-1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, -1), new Vector3(-1, 1, -1),
                new Vector3(-1, -1, 1), new Vector3(1, -1, 1), new Vector3(1, -1, -1), new Vector3(-1, -1, -1)
            };

            //Test lines
            _gameInitialVertices = new Vector3[8];
            Array.Copy(_initialVertices, _gameInitialVertices, _initialVertices.Length);
        }
        private void SetCubeSides()
        {
            _allSides = new Side[6]
            {
                new Side
                (
                    new Vector3(0, 1, 0),
                    new int[4]
                    {
                        0, 1, 2, 3
                    },
                    new Vector2[4]
                    {
                        new Vector2(0.25f, 0.66f),
                        new Vector2(0.5f, 0.66f),
                        new Vector2(0.5f, 0.33f),
                        new Vector2(0.25f, 0.33f)
                    },
                    new MeshTriangle[2]
                    {
                        new MeshTriangle(0, 1, 2),
                        new MeshTriangle(2, 3, 0)
                    }
                ),
                new Side
                (
                    new Vector3(0, -1, 0),
                    new int[4]
                    {
                        4, 5, 6, 7
                    },
                    new Vector2[4]
                    {
                        new Vector2(1f, 0.66f),
                        new Vector2(0.75f, 0.66f),
                        new Vector2(0.75f, 0.33f),
                        new Vector2(1, 0.33f)
                    },
                    new MeshTriangle[2]
                    {
                        new MeshTriangle(0, 3, 2),
                        new MeshTriangle(2, 1, 0)
                    }
                ),
                new Side
                (
                    new Vector3(1, 0, 0),
                    new int[4]
                    {
                        1, 2, 6, 5
                    },
                    new Vector2[4]
                    {
                        new Vector2(0.5f, 0.66f),
                        new Vector2(0.5f, 0.33f),
                        new Vector2(0.75f, 0.33f),
                        new Vector2(0.75f, 0.66f)
                    },
                    new MeshTriangle[2]
                    {
                        new MeshTriangle(2, 1, 0),
                        new MeshTriangle(0, 3, 2)
                    }
                ),
                new Side
                (
                    new Vector3(-1, 0, 0),
                    new int[4]
                    {
                        0, 3, 7, 4
                    },
                    new Vector2[4]
                    {
                        new Vector2(0.25f, 0.66f),
                        new Vector2(0.25f, 0.33f),
                        new Vector2(0f, 0.33f),
                        new Vector2(0f, 0.66f)
                    },
                    new MeshTriangle[2]
                    {
                        new MeshTriangle(0, 1, 2),
                        new MeshTriangle(2, 3, 0)
                    }
                ),
                new Side
                (
                    new Vector3(0, 0, 1),
                    new int[4]
                    {
                        0, 1, 5, 4
                    },
                    new Vector2[4]
                    {
                        new Vector2(0.25f, 0.66f),
                        new Vector2(0.5f, 0.66f),
                        new Vector2(0.5f, 1f),
                        new Vector2(0.25f, 1f)
                    },
                    new MeshTriangle[2]
                    {
                        new MeshTriangle(0, 3, 2),
                        new MeshTriangle(2, 1, 0)
                    }
                ),
                new Side
                (
                    new Vector3(0, 0, -1),
                    new int[4]
                    {
                        3, 2, 6, 7
                    },
                    new Vector2[4]
                    {
                        new Vector2(0.25f, 0.33f),
                        new Vector2(0.5f, 0.33f),
                        new Vector2(0.5f, 0f),
                        new Vector2(0.25f, 0f)
                    },
                    new MeshTriangle[2]
                    {
                        new MeshTriangle(0, 1, 2),
                        new MeshTriangle(2, 3, 0)
                    }
                ),
            };
        }
        private void InitializeArrayes()
        {
            Vertices = new List<Vector3>();
            UVs = new List<Vector2>();
            TrianglesIndexes = new List<int>();
        }
        public void SetOffsetAndSizeValues(Vector3 offsetPosition, Vector3 scale)
        {
            _offsetPosition = offsetPosition;
            _scale = scale;
        }
        public void SetSideVerticesPosition(Vector3 direction, Vector3 position)
        {
            Side side = _usedSides[direction];
            for (int i = 0; i < side.VerticesIndexes.Length; i++)
            {
                int index = side.VerticesIndexes[i];
                _gameInitialVertices[index] = new Vector3 (_initialVertices[index].x * (Mathf.Abs(direction.x) == 1 ? 0 : 1) * _scale.x + position.x, 
                                                           _initialVertices[index].y * (Mathf.Abs(direction.y) == 1 ? 0 : 1) * _scale.y + position.y, 
                                                           _initialVertices[index].z * (Mathf.Abs(direction.z) == 1 ? 0 : 1) * _scale.z + position.z);
            }
        }
        public void SetCenterPoint()
        {
            foreach (Vector3 key in _usedSides.Keys)
            {
                for (int i = 0; i < _usedSides[key].VerticesIndexes.Length; i++)
                {
                    Vector3 vertex = _initialVertices[_usedSides[key].VerticesIndexes[i]];
                    _gameInitialVertices[_usedSides[key].VerticesIndexes[i]] = new Vector3(vertex.x * _scale.x, vertex.y * _scale.y, vertex.z * _scale.z) + _offsetPosition;
                }
            }
        }
        public void SetUsedSides(Vector3[] ignoreLookDirections = null)
        {
            if (ignoreLookDirections == null) ignoreLookDirections = new Vector3[0];

            //TODO: Check if dictionaries will be needed, if not change it
            _usedSides = new Dictionary<Vector3, Side>();
            for (int i = 0; i < _allSides.Length; i++)
            {
                if (CheckLookDirectionExist(ignoreLookDirections, _allSides[i].LookDirection)) continue;

                _usedSides.Add(_allSides[i].LookDirection, _allSides[i]);
            }
        }
        public void SetSideVertex()
        {

        }
        public void FillMeshInfo()
        {
            Vertices.Clear();
            UVs.Clear();
            TrianglesIndexes.Clear();

            int offset = 0;
            foreach (Vector3 key in _usedSides.Keys)
            {
                //Add vertices
                for (int i = 0; i < _usedSides[key].VerticesIndexes.Length; i++)
                {
                    Vertices.Add(_gameInitialVertices[_usedSides[key].VerticesIndexes[i]]);
                }
                //Add uvs
                UVs.AddRange(_usedSides[key].UVs);

                //Add triangles
                for (int i = 0; i < _usedSides[key].TriangleIndexes.Length; i++)
                {
                    TrianglesIndexes.AddRange(_usedSides[key].TriangleIndexes[i].GetVertexIndexesWithOffset(offset));
                }
                offset = Vertices.Count;
            }
        }

        #endregion

        #region Calculation Methods

        private bool CheckLookDirectionExist(Vector3[] list, Vector3 goal)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == goal) return true;
            }
            return false;
        }

        #endregion
    }
}