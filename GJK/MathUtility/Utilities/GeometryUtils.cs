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

namespace CollisionDetection.MathUtility
{
	public static class GeometryUtils
	{

        #region Public Static Methods

        public const double collinearityTolerance = 1E-6;

        /// <summary>
        /// Calculates normal.
        /// </summary>
        /// <returns>The normal.</returns>
        /// <param name="a">The alpha component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="c">C.</param>
        public static Vector3d CalculateTriangleNormal(
			Vector3d a,
			Vector3d b,
			Vector3d c)
		{
			Vector3d v1 = b - a;
			Vector3d v2 = c - a;

			double x = (v1.y * v2.z) - (v1.z * v2.y);
			double y = -((v2.z * v1.x) - (v2.x * v1.z));
			double z = (v1.x * v2.y) - (v1.y * v2.x);

			return Vector3d.Normalize(new Vector3d(x, y, z));
		}
				        					
		/// <summary>
		/// Gets the point triangle intersection, return null if it can't find correct solution.
		/// </summary>
		/// <returns>The point triangle intersection.</returns>
		/// <param name="v1">V1.</param>
		/// <param name="v2">V2.</param>
		/// <param name="v3">V3.</param>
		/// <param name="p">P.</param>
		/// <param name="t0">T0.</param>
		/// <param name="t1">T1.</param>
		public static Vector3d? GetPointTriangleIntersection(
			Vector3d v1,
			Vector3d v2,
			Vector3d v3,
			Vector3d p,
			ref double t0,
			ref double t1)
		{
			Vector3d diff = p - v1;
			Vector3d edge0 = v2 - v1;
			Vector3d edge1 = v3 - v1;
			double a00 = edge0.Dot(edge0);
			double a01 = edge0.Dot(edge1);
			double a11 = edge1.Dot(edge1);
			double b0 = -diff.Dot(edge0);
			double b1 = -diff.Dot(edge1);
			double det = a00 * a11 - a01 * a01;
			t0 = a01 * b1 - a11 * b0;
			t1 = a01 * b0 - a00 * b1;

			if (t0 + t1 <= det)
			{
				if (t0 < 0.0)
				{
					if (t1 < 0.0)  // region 4
					{
						if (b0 < 0.0)
						{
							t1 = 0.0;
							if (-b0 >= a00)  // V0
							{
								t0 = 1.0;
							}
							else  // E01
							{
								t0 = -b0 / a00;
							}
						}
						else
						{
							t0 = 0.0;
							if (b1 >= 0.0)  // V0
							{
								t1 = 0.0;
							}
							else if (-b1 >= a11)  // V2
							{
								t1 = 1.0;
							}
							else  // E20
							{
								t1 = -b1 / a11;
							}
						}
					}
					else  // region 3
					{
						t0 = 0.0;
						if (b1 >= 0.0)  // V0
						{
							t1 = 0.0;
						}
						else if (-b1 >= a11)  // V2
						{
							t1 = 1.0;
						}
						else  // E20
						{
							t1 = -b1 / a11;
						}
					}
				}
				else if (t1 < 0.0)  // region 5
				{
					t1 = 0.0;
					if (b0 >= 0.0)  // V0
					{
						t0 = 0.0;
					}
					else if (-b0 >= a00)  // V1
					{
						t0 = 1.0;
					}
					else  // E01
					{
						t0 = -b0 / a00;
					}
				}
				else  // region 0, interior
				{
					if (Math.Abs(det) > 1E-100) {
						double invDet = 1.0 / det;
						t0 *= invDet;
						t1 *= invDet;

					} else {
						return null;
					}
				}
			}
			else
			{
				double tmp0, tmp1, numer, denom;

				if (t0 < 0.0)  // region 2
				{
					tmp0 = a01 + b0;
					tmp1 = a11 + b1;
					if (tmp1 > tmp0)
					{
						numer = tmp1 - tmp0;
						denom = a00 - 2.0 * a01 + a11;
						if (numer >= denom)  // V1
						{
							t0 = 1.0;
							t1 = 0.0;
						}
						else  // E12
						{
							t0 = numer / denom;
							t1 = 1.0 - t0;
						}
					}
					else
					{
						t0 = 0.0;
						if (tmp1 <= 0.0)  // V2
						{
							t1 = 1.0;
						}
						else if (b1 >= 0.0)  // V0
						{
							t1 = 0.0;
						}
						else  // E20
						{
							t1 = -b1 / a11;
						}
					}
				}
				else if (t1 < 0.0)  // region 6
				{
					tmp0 = a01 + b1;
					tmp1 = a00 + b0;
					if (tmp1 > tmp0)
					{
						numer = tmp1 - tmp0;
						denom = a00 - 2.0 * a01 + a11;
						if (numer >= denom)  // V2
						{
							t1 = 1.0;
							t0 = 0.0;
						}
						else  // E12
						{
							t1 = numer / denom;
							t0 = 1.0 - t1;
						}
					}
					else
					{
						t1 = 0.0;
						if (tmp1 <= 0.0)  // V1
						{
							t0 = 1.0;
						}
						else if (b0 >= 0.0)  // V0
						{
							t0 = 0.0;
						}
						else  // E01
						{
							t0 = -b0 / a00;
						}
					}
				}
				else  // region 1
				{
					numer = a11 + b1 - a01 - b0;
					if (numer <= 0.0)  // V2
					{
						t0 = 0.0;
						t1 = 1.0;
					}
					else
					{
						denom = a00 - 2.0 * a01 + a11;
						if (numer >= denom)  // V1
						{
							t0 = 1.0;
							t1 = 0.0;
						}
						else  // 12
						{
							t0 = numer / denom;
							t1 = 1.0 - t0;
						}
					}
				}
			}

			return v1 + t0 * edge0 + t1 * edge1;
		}

		/// <summary>
		/// Tests collinearity.
		/// </summary>
		/// <returns><c>true</c>, if collinearity was tested, <c>false</c> otherwise.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		/// <param name="c">C.</param>
		public static bool TestCollinearity(
			Vector3d a,
			Vector3d b,
			Vector3d c)
		{
			Vector3d d = b - a;
			Vector3d e = c - a;

			Vector3d f =  d.Cross(e);
			if (Math.Abs (f.x) < collinearityTolerance &&
				Math.Abs (f.y) < collinearityTolerance &&
				Math.Abs (f.z) < collinearityTolerance)
				return true;

			return false;
		}
        		                             
        #endregion

    }
}

