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

using System;
using System.Collections.Generic;

namespace CollisionDetection
{
	public class Simplex : IEquatable<Simplex>
	{
		public List<Support> Support { get; set; }
		public double W { get; set; }
		public double T { get; set; }

		#region "Constructor"

		public Simplex ()
		{
			Support = new List<Support>();
		}

		public Simplex (
			List<Support> support,
			double w,
			double t)
		{
			Support = support;
			this.W = w;
			this.T = t;
		}

		#endregion

		#region "Public Methods"

		public bool AddSupport(Support sp)
		{
			if (Support.Exists(x => x.a == sp.a && x.b == sp.b))
				return false;

			Support.Add(sp);

			return true;
		}
        		
		#endregion

		#region IEquatable implementation

		public bool Equals (Simplex other)
		{
			return (Support [0].a == other.Support [0].a &&
					Support [1].a == other.Support [1].a &&
					Support [2].a == other.Support [2].a &&
					Support [3].a == other.Support [3].a &&
					Support [0].b == other.Support [0].b &&
					Support [1].b == other.Support [1].b &&
					Support [2].b == other.Support [2].b &&
					Support [3].b == other.Support [3].b);
		}

		#endregion
	}
}

