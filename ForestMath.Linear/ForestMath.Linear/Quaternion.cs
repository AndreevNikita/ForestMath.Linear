using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ForestMath.Linear.Geometry;

namespace ForestMath.Linear {
	public struct Quaternion {
		public double W, X, Y, Z;

		public Quaternion(double w, double x, double y, double z, bool fromEulers = true, bool normalize = true, AngleMeasure measure = AngleMeasure.RADIANS) {
			if(fromEulers) {
				if(measure == AngleMeasure.DEGREES)
					w = Geometry.ToRadians(w);
				this.W = Math.Cos(w / 2.0);
				double sinwd2 = Math.Sin(w / 2.0);
				this.X = x * sinwd2;
				this.Y = y * sinwd2;
				this.Z = z * sinwd2;
			} else {
				this.W = w;
				this.X = x;
				this.Y = y;
				this.Z = z;
			}

			
			if(normalize)
				this.Normalize();
		}

		public Quaternion(Vector3 vec) {
			W = 0;
			X = vec.X;
			Y = vec.Y;
			Z = vec.Z;
		}

		public static Quaternion operator+(Quaternion a, Quaternion b) {
			return new Quaternion(a.W + b.W, a.X + b.X, a.Y + b.Y, a.Z + b.Z, false, false);
		}

		public static Quaternion operator-(Quaternion a, Quaternion b) {
			return new Quaternion(a.W - b.W, a.X - b.X, a.Y - b.Y, a.Z - b.Z, false, false);
		}

		public static Quaternion operator-(Quaternion a) {
			return new Quaternion(a.W, -a.X, -a.Y, -a.Z, false, false);
		}

		
		public static Quaternion operator*(Quaternion a, double mul) {
			return new Quaternion(a.W * mul, a.X * mul , a.Y * mul, a.Z * mul, false, false);
		}

		public static Quaternion operator/(Quaternion a, double div) {
			return new Quaternion(a.W / div, a.X / div , a.Y / div, a.Z / div, false, false);
		}

		//a * b дают кватернион, равносильный повороту сначала b потом a
		public static Quaternion operator*(Quaternion a, Quaternion b) {
			return new Quaternion(
				a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z, 
				a.X * b.W + a.W * b.X + a.Y * b.Z - a.Z * b.Y,
				a.Y * b.W + a.W * b.Y + a.Z * b.X - a.X * b.Z, 
				a.Z * b.W + a.W * b.Z + a.X * b.Y - a.Y * b.X, false, true);
		}

		public double GetLength() {
			return Math.Sqrt(W * W + X * X + Y * Y + Z * Z);
		}

		public void Normalize() {
			double l = GetLength();
			W /= l;
			X /= l;
			Y /= l;
			Z /= l;
		}

		public Quaternion Normalized() {
			return new Quaternion(W, X, Y, Z, false, true);
		}

		public Vector3 GetYawPitchRoll(AngleMeasure measure = AngleMeasure.RADIANS) {
			return measure == AngleMeasure.RADIANS ? new Vector3(
				Math.Atan2(2*Y*W - 2*X*Z, 1 - 2*Y*Y - 2*Z*Z),
				Math.Asin(Math.Max(Math.Min(2*X*Y + 2*Z*W, 1.0), -1.0)),
				Math.Atan2(2*X*W - 2*Y*Z, 1 - 2*X*X - 2*Z*Z)
			) : new Vector3(
				Geometry.ToDegrees(Math.Atan2(2*Y*W - 2*X*Z, 1 - 2*Y*Y - 2*Z*Z)),
				Geometry.ToDegrees(Math.Asin(Math.Max(Math.Min(2*X*Y + 2*Z*W, 1.0), -1.0))),
				Geometry.ToDegrees(Math.Atan2(2*X*W - 2*Y*Z, 1 - 2*X*X - 2*Z*Z))
			);
		}

		public Vector3 Rotate(Vector3 vec) {
			return this * new Quaternion(vec) * (-this);
		}

		public static Quaternion FromYawPitch(double yaw, double pitch, double roll) {
			return new Quaternion(yaw, 0.0, 1.0, 0.0) * new Quaternion(pitch, 0.0, 0.0, 1.0) * new Quaternion(roll, 1.0, 0.0, 0.0);
		}

		public static double ScalarMultiply(Quaternion a, Quaternion b) {
			return a.W * b.W + a.X * b.X + a.Y * b.Y * a.Z * b.Z;
		}

		public static Vector3 Transform(Vector3 vec, Quaternion quaternion) {
			return  quaternion * new Quaternion(vec) * (-quaternion);
		}

		public static implicit operator Quaternion(Vector3 vec) {
			return new Quaternion(0.0, vec.X, vec.Y, vec.Z, false);
		}

		public override string ToString() {
			 return GetYawPitchRoll(AngleMeasure.DEGREES).ToString();
		}
	};
}
