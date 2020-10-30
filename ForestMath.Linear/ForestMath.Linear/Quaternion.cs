using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ForestMath.Linear.Geometry;

namespace ForestMath.Linear {
	public struct Quaternion {
		public double w, x, y, z;

		public Quaternion(double w, double x, double y, double z, bool fromEulers = true, bool normalize = true, AngleMeasure measure = AngleMeasure.RADIANS) {
			if(fromEulers) {
				if(measure == AngleMeasure.DEGREES)
					w = Geometry.toRadians(w);
				this.w = Math.Cos(w / 2.0);
				double sinwd2 = Math.Sin(w / 2.0);
				this.x = x * sinwd2;
				this.y = y * sinwd2;
				this.z = z * sinwd2;
			} else {
				this.w = w;
				this.x = x;
				this.y = y;
				this.z = z;
			}

			
			if(normalize)
				this.normalize();
		}

		public Quaternion(Vector3 vec) {
			w = 0;
			x = vec.x;
			y = vec.y;
			z = vec.z;
		}

		public static Quaternion operator+(Quaternion a, Quaternion b) {
			return new Quaternion(a.w + b.w, a.x + b.x, a.y + b.y, a.z + b.z, false, false);
		}

		public static Quaternion operator-(Quaternion a, Quaternion b) {
			return new Quaternion(a.w - b.w, a.x - b.x, a.y - b.y, a.z - b.z, false, false);
		}

		public static Quaternion operator-(Quaternion a) {
			return new Quaternion(a.w, -a.x, -a.y, -a.z, false, false);
		}

		
		public static Quaternion operator*(Quaternion a, double mul) {
			return new Quaternion(a.w * mul, a.x * mul , a.y * mul, a.z * mul, false, false);
		}

		public static Quaternion operator/(Quaternion a, double div) {
			return new Quaternion(a.w / div, a.x / div , a.y / div, a.z / div, false, false);
		}

		//a * b дают кватернион, равносильный повороту сначала b потом a
		public static Quaternion operator*(Quaternion a, Quaternion b) {
			return new Quaternion(
				a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z, 
				a.x * b.w + a.w * b.x + a.y * b.z - a.z * b.y,
				a.y * b.w + a.w * b.y + a.z * b.x - a.x * b.z, 
				a.z * b.w + a.w * b.z + a.x * b.y - a.y * b.x, false, true);
		}

		public double length() {
			return Math.Sqrt(w * w + x * x + y * y + z * z);
		}

		public void normalize() {
			double l = length();
			w /= l;
			x /= l;
			y /= l;
			z /= l;
		}

		public Quaternion normalized() {
			return new Quaternion(w, x, y, z, false, true);
		}

		public Vector3 getYawPitchRoll(AngleMeasure measure = AngleMeasure.RADIANS) {
			return measure == AngleMeasure.RADIANS ? new Vector3(
				Math.Atan2(2*y*w - 2*x*z, 1 - 2*y*y - 2*z*z),
				Math.Asin(Math.Max(Math.Min(2*x*y + 2*z*w, 1.0), -1.0)),
				Math.Atan2(2*x*w - 2*y*z, 1 - 2*x*x - 2*z*z)
			) : new Vector3(
				Geometry.toDegrees(Math.Atan2(2*y*w - 2*x*z, 1 - 2*y*y - 2*z*z)),
				Geometry.toDegrees(Math.Asin(Math.Max(Math.Min(2*x*y + 2*z*w, 1.0), -1.0))),
				Geometry.toDegrees(Math.Atan2(2*x*w - 2*y*z, 1 - 2*x*x - 2*z*z))
			);
		}

		public Vector3 rotate(Vector3 vec) {
			return this * new Quaternion(vec) * (-this);
		}

		public static Quaternion fromYawPitch(double yaw, double pitch, double roll) {
			return new Quaternion(yaw, 0.0, 1.0, 0.0) * new Quaternion(pitch, 0.0, 0.0, 1.0) * new Quaternion(roll, 1.0, 0.0, 0.0);
		}

		public static double scalarMultiply(Quaternion a, Quaternion b) {
			return a.w * b.w + a.x * b.x + a.y * b.y * a.z * b.z;
		}

		public static Vector3 transform(Vector3 vec, Quaternion quaternion) {
			return  quaternion * new Quaternion(vec) * (-quaternion);
		}

		public static implicit operator Quaternion(Vector3 vec) {
			return new Quaternion(0.0, vec.x, vec.y, vec.z, false);
		}

		public override string ToString() {
			 return getYawPitchRoll(AngleMeasure.DEGREES).ToString();
		}
	};
}
