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
	public struct SupportTriangle
	{
		#region Public Properties

		public Support A { get; private set; }
		public Support B { get; private set; }
		public Support C { get; private set; }
		public double S { get; private set; }
		public double T { get; private set; }
		public Vector3d Normal { get; private set; } 

		#endregion

		#region Constructor

		public SupportTriangle (
			Support a,
			Support b,
			Support c,
			double s,
			double t,
			Vector3d normal)
			:this()
		{
			this.A = a;
			this.B = b;
			this.C = c;
			this.S = s;
			this.T = t;
			this.Normal = normal;
		}

		#endregion

		#region Public Methods

		public void SetValueS (double s)
		{
			this.S = s;
		}

		public void SetValueT (double t)
		{
			this.T = t;
		}

		#endregion

	}
}

