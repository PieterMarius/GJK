/******************************************************************************
 *
 * The MIT License (MIT)
 *
 * PhysicsEngine, Copyright (c) 2018 Pieter Marius van Duin
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 *  
 *****************************************************************************/

using CollisionDetection.MathUtility;
using System.Collections.Generic;
using System.Linq;

namespace CollisionDetection
{
    public sealed class VertexProperties
    {
        #region Fields

        public Vector3d Vertex { get; private set; }
        public List<int> Adjacency {
            get
            {
                SetAdjacencyList();
                return _adjacency;
            }
            private set { }
        }
        public int? ID { get; private set; }
        public int?[] LinkedID { get; private set; }

        #endregion

        #region Private Fields

        private List<int> _adjacency;
        private HashSet<int> AdjacencyHash;

        #endregion

        #region Constructor

        public VertexProperties(
            Vector3d vertex,
            HashSet<int> adjacency,
            int? id,
            int?[] linkedID)
        {
            Vertex = vertex;
            AdjacencyHash = adjacency;

            if (AdjacencyHash != null)
                _adjacency = AdjacencyHash.ToList();
            else
            {
                AdjacencyHash = new HashSet<int>();
                _adjacency = new List<int>();
            }

            ID = id;
            LinkedID = linkedID;
        }

        public VertexProperties(
            Vector3d vertex,
            HashSet<int> adjacency)
            : this(vertex, adjacency, null, null)
        { }

        public VertexProperties(
            Vector3d vertex)
            : this(vertex, null, null, null)
        { }

        public VertexProperties(
            Vector3d vertex,
            int? id)
            : this(vertex, null, id, null)
        { }
                
        public VertexProperties(
            Vector3d vertex,
            int?[] linkedID)
            : this(vertex, null, null, linkedID)
        { }

        #endregion

        #region Public Methods

        public void SetVertexPosition(Vector3d position)
        {
            Vertex = position;
        }
        
        public void AddVertexToAdjList(int vertex)
        {
            AdjacencyHash.Add(vertex);
        }

        public HashSet<int> GetAdjacencyList()
        {
            return AdjacencyHash;
        }

        #endregion

        #region Private Methods

        private void SetAdjacencyList()
        {
            if (AdjacencyHash.Count() != _adjacency.Count)
                _adjacency = AdjacencyHash.ToList();
        }

        #endregion
    }
}
