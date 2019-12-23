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

namespace CollisionDetection.MathUtility
{
	public struct Vector3d: IComparable, IEquatable<Vector3d>
	{
		#region Public Properties

		public readonly double x;
		public readonly double y;
		public readonly double z;

        public const double precision = 0.0000001;

        #endregion

        #region Constructors

        public Vector3d (
			double x, 
			double y, 
			double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
        }

		public Vector3d(double[] vec)
		{
			if (vec.Length == 3) 
			{
				x = vec [0];
				y = vec [1];
				z = vec [2];
            } 
			else 
			{
				throw new ArgumentException(COMPONENT_EXCEPTION);
			}
		}

		public Vector3d(Vector3d v)
		{
			x = v.x;
			y = v.y;
			z = v.z;
        }


		#endregion

		#region Public Methods

		public static Vector3d operator+(Vector3d a, Vector3d b)
		{
			return new Vector3d (a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3d operator+(Vector3d a, double b)
		{
			return new Vector3d (a.x + b, a.y + b, a.z + b);
		}

		public static Vector3d operator+(double b, Vector3d a)
		{
			return new Vector3d (a.x + b, a.y + b, a.z + b);
		}

		public static Vector3d operator-(Vector3d a, Vector3d b)
		{
			return new Vector3d (a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector3d operator*(Vector3d a, Vector3d b)
		{
			return new Vector3d (a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public static Vector3d operator*(Vector3d a, double b)
		{
			return new Vector3d (a.x * b, a.y * b, a.z * b);
		}

		public static Vector3d operator*(double b, Vector3d a)
		{
			return new Vector3d (a.x * b, a.y * b, a.z * b);
		}

		public static Vector3d operator/(Vector3d a, Vector3d b)
		{
			return new Vector3d (a.x / b.x, a.y / b.y, a.z / b.z);
		}

		public static Vector3d operator/(Vector3d a, double d)
		{
            var dp = 1.0 / d;
			return new Vector3d (a.x * dp, a.y * dp, a.z * dp);
		}
        		
		public double[] Array
		{
			get
			{
				return new[] { x, y, z };
			}
		}

		public double this[ int index ]
		{
			get	
			{
				switch (index)
				{
				case 0:
					return x;
				case 1:
					return y;
				case 2:
					return z;
				default:
					throw new ArgumentException(COMPONENT_EXCEPTION, nameof(index));
				}
			}
		}

		public List<double> ToList
		{
			get
			{
				return new List<double>() { x, y, z };
			}
		}

		public static bool operator>(Vector3d a, Vector3d b)
		{
			if (Length (a) + precision > Length (b) + precision)
				return true;
			
			return false;
		}

        public static bool operator <(Vector3d a, Vector3d b)
        {
            if (Length(a) + precision < Length(b) + precision)
                return true;

            return false;
        }

        public static bool operator==(Vector3d a, Vector3d b)
		{
			if (Math.Abs (a.x - b.x) < precision &&
				Math.Abs (a.y - b.y) < precision &&
				Math.Abs (a.z - b.z) < precision)
				return true;
			
			return false;
		}

		public static bool operator!=(Vector3d a, Vector3d b)
		{
			if (!(a == b))
				return true;
			
			return false;
		}

				
		public static double Dot(Vector3d a, Vector3d b)
		{
            return Dot(ref a, ref b);
		}

        public static double Dot(ref Vector3d a, ref Vector3d b)
        {
            return a.x * b.x +
                   a.y * b.y +
                   a.z * b.z;
        }

        public double Dot(ref Vector3d a)
        {
            return x * a.x +
                   y * a.y +
                   z * a.z;
        }

        public double Dot(Vector3d a)
		{
			return Dot(ref a);
		}

		public static double Length(Vector3d a)
		{
			return Math.Sqrt (Dot (a, a));
		}

		public double Length()
		{
			return Math.Sqrt (Dot (this));
		}

		public static Vector3d ToZero()
		{
			return new Vector3d (0.0, 0.0, 0.0);
		}
                

		public static Vector3d Normalize(Vector3d a)
		{
			double l = Length (a);
			if (l > 0.0) {
				
				l = 1.0 / l;

				return new Vector3d (
					a.x * l, 
					a.y * l, 
					a.z * l);
			}

			return a;
		}

		public Vector3d Normalize()
		{
			return Normalize (this);
		}

		public static Vector3d Zero()
		{
			return new Vector3d(0.0, 0.0, 0.0);
		}

		public static Vector3d RotatePoint(Vector3d a, Vector3d versor, double angle)
		{
			var p= versor * versor;
            double c = Math.Cos (angle);
			double s = Math.Sin (angle);
			double t = 1.0 - angle;

            double X = (a.x * (p.x + (1.0 - p.x) * c)) + (a.y * ((t * versor.x * versor.y) - (s * versor.z))) + (a.z * ((t * versor.x * versor.z) + (s * versor.y)));
            double Y = (a.x * ((t * versor.x * versor.y) + (s * versor.z))) + (a.y * (p.y + (1.0 - p.y) * c)) + (a.z * ((t * versor.y * versor.z) - (s * versor.x)));
            double Z = (a.x * ((t * versor.z * versor.x) - (s * versor.y))) + (a.y * ((t * versor.z * versor.y) + (s * versor.x))) + (a.z * (p.z + (1.0 - p.z) * c));

			return new Vector3d (X, Y, Z);
		}

		public static Vector3d Cross(Vector3d a, Vector3d b) {
			return new Vector3d(
				(a.y * b.z)-(a.z * b.y),
				(a.z * b.x)-(a.x * b.z),
				(a.x * b.y)-(a.y * b.x));
		}

		public Vector3d Cross(Vector3d a)
		{
			return Cross (this, a);
		}
        		        		
		public static Vector3d UniformSign(Vector3d input, Vector3d sign)
		{
			if (input.Dot(sign) >= 0.0)
				return input;

			double a = input.x;
			double b = input.y;
			double c = input.z;

			if (Math.Sign(input.x) != Math.Sign(sign.x) && 
			    Math.Sign(sign.x) != 0)
				a = -input.x;
			
			if (Math.Sign(input.y) != Math.Sign(sign.y) &&
			    Math.Sign(sign.y) != 0)
				b = -input.y;

			if (Math.Sign(input.z) != Math.Sign(sign.z) &&
			    Math.Sign(sign.z) != 0)
				c = -input.z;

			return new Vector3d(a, b, c);
		}

        public static bool CheckZeroWithTolerance(Vector3d input, double tolerance)
        {
            if (Math.Abs(input.x) < tolerance &&
                Math.Abs(input.y) < tolerance &&
                Math.Abs(input.z) < tolerance)
                return true;

            return false;
        }

        #region Round

        public static Vector3d Round(Vector3d v1)
        {
            return new Vector3d(Math.Round(v1.x), Math.Round(v1.y), Math.Round(v1.z));
        }

        public static Vector3d Round(Vector3d v1, MidpointRounding mode)
        {
            return new Vector3d(
                Math.Round(v1.x, mode),
                Math.Round(v1.y, mode),
                Math.Round(v1.z, mode));
        }

        public Vector3d Round()
        {
            return new Vector3d(Math.Round(this.x), Math.Round(this.y), Math.Round(this.z));
        }

        public Vector3d Round(MidpointRounding mode)
        {
            return new Vector3d(
                Math.Round(this.x, mode),
                Math.Round(this.y, mode),
                Math.Round(this.z, mode));
        }

        public static Vector3d Round(Vector3d v1, int digits)
        {
            return new Vector3d(
                Math.Round(v1.x, digits),
                Math.Round(v1.y, digits),
                Math.Round(v1.z, digits));
        }

        public static Vector3d Round(Vector3d v1, int digits, MidpointRounding mode)
        {
            return new Vector3d(
                Math.Round(v1.x, digits, mode),
                Math.Round(v1.y, digits, mode),
                Math.Round(v1.z, digits, mode));
        }

        public Vector3d Round(int digits)
        {
            return new Vector3d(
                Math.Round(this.x, digits),
                Math.Round(this.y, digits),
                Math.Round(this.z, digits));
        }

        public Vector3d Round(int digits, MidpointRounding mode)
        {
            return new Vector3d(
                Math.Round(this.x, digits, mode),
                Math.Round(this.y, digits, mode),
                Math.Round(this.z, digits, mode));
        }

        public bool IsNaN()
        {
            if (double.IsNaN(this.x) &&
                double.IsNaN(this.y) &&
                double.IsNaN(this.z))
                return true;

            return false;
        }

        #endregion

        #region IComparable implementation

        public int CompareTo (object obj)
		{
			if (obj is Vector3d)
			{
				return CompareTo((Vector3d)obj);
			}

			throw new ArgumentException ();
		}

		public int CompareTo(Vector3d other)
		{
			if (this < other)
			{
				return -1;
			}

			if (this > other)
			{
				return 1;
			}

			return 0;
		}

		#endregion
        
		#region IEquatable implementation

		public bool Equals (Vector3d other)
		{
			return this == other;
		}

		public override bool Equals(object obj)
		{
			if (obj is Vector3d otherVector)
			{
				return otherVector.Equals(this);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = x.GetHashCode();
				hashCode = (hashCode * 397) ^ y.GetHashCode();
				hashCode = (hashCode * 397) ^ z.GetHashCode();
				return hashCode;
			}
		}

		#endregion

		#region Const

		private const string COMPONENT_EXCEPTION = "Vector must contain three components (x,y,z)";

		//TODO aggiungere eventuali altre eccezioni

		#endregion

		#endregion
	}
}

