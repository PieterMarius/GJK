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

namespace CollisionDetection
{
	public class CollisionPointOutput
	{
		#region Public Properties

		public Vector3d Dist { get; private set; }
		public VertexProperties A { get; private set; }
		public VertexProperties B { get; private set; }
		public Vector3d Normal { get; private set; }
		
		#endregion

		#region Constructor

		public CollisionPointOutput (
			Vector3d dist,
			VertexProperties a,
			VertexProperties b,
			Vector3d normal)
		{
			this.Dist = dist;
			this.A = a;
			this.B = b;
			this.Normal = normal;
		}

		public CollisionPointOutput()
		{}

		#endregion

		#region Public Methods

		public void SetA(VertexProperties a)
		{
			this.A = a;
		}

		public void SetB(VertexProperties b)
		{
			this.B = b;
		}

		public void SetDist(Vector3d dist)
		{
			this.Dist = dist;
		}
			
		public void SetNormal(Vector3d normal)
		{
			this.Normal = normal;
		}

		#endregion
	}
}

