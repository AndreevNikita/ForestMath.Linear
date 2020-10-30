using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ForestMath.Linear.Geometry;

namespace ForestMath.Linear {
	public struct Vector3 { 
		public double x, y, z;
		public double Yaw {
			get { return x; }
			set { x = value; }
		}
		public double Pitch {
			get { return y; }
			set { y = value; }
		}
		public double Roll {
			get { return z; }
			set { z = value; }
		}

		public double Length {
			get { return length(); }
			set { 
				double mul = value / Length;
				x *= mul;
				y *= mul;
				z *= mul;
			}
		}

		public Vector3(Vector3 src) : this(src.x, src.y, src.z) {}

		public Vector3(double x = 0.0f, double y = 0.0f, double z = 0.0f) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static Vector3 operator+(Vector3 a, Vector3 b) {
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3 operator-(Vector3 a, Vector3 b) {
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static double operator*(Vector3 a, Vector3 b) {
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		public static Vector3 operator-(Vector3 vec) {
			return new Vector3(-vec.x, -vec.y, -vec.z);
		}

		public static Vector3 operator+(Vector3 a, double b) {
			return new Vector3(a.x + b, a.y + b, a.z + b);
		}

		public static Vector3 operator-(Vector3 a, double b) {
			return new Vector3(a.x - b, a.y - b, a.z - b);
		}

		public static Vector3 operator*(Vector3 a, double b) {
			return new Vector3(a.x * b, a.y * b, a.z * b);
		}

		public static Vector3 operator*(double a, Vector3 b) {
			return b * a;
		}

		public static Vector3 operator/(Vector3 a, double b) {
			return new Vector3(a.x / b, a.y / b, a.z / b);
		}

		public static bool operator==(Vector3 a, Vector3 b) {
			return a.x == b.x && a.y == b.y && a.z == b.z;
		}

		public static bool operator!=(Vector3 a, Vector3 b) {
			return a.x != b.x || a.y != b.y || a.z != b.z;
		}

		public static double operator^(Vector3 a, Vector3 b) { 
			return Math.Acos((a * b) / (a.Length * b.Length));
		}

		public override bool Equals(object obj) {
			if(obj is Vector3) {
				Vector3 b = (Vector3)obj;
				return this.x == b.x && this.y == b.y && this.z == b.z;
			}
			return false;
		}

		public override int GetHashCode() {
			return (x, y, z).GetHashCode();
		}

		public double length() { 
			return Math.Sqrt(x * x + y * y + z * z);
		}

		public double getAngle(bool isNormalized = false, AngleMeasure measure = AngleMeasure.RADIANS) {
			Vector2 n = isNormalized ? this : this.normalized();
			double result = n.y > 0 ? Math.Acos(n.x) : -Math.Acos(n.x);
			if(measure == AngleMeasure.DEGREES)
				result = Geometry.toDegrees(result);
			return result;
		}

		public double getAngle(Vector3 b, AngleMeasure measure = AngleMeasure.RADIANS) { 
			double result = Math.Acos(this * b / (this.Length * b.Length));
			if(measure == AngleMeasure.DEGREES)
				result = Geometry.toDegrees(result);
			return result;
		}

		public double sqrLength() { 
			return x * x + y * y + z * z;
		}

		public Vector3 normalized() {
			return this / length();
		}

		public void getYawPitch(out double yaw, out double pitch, bool isNormalized = false, AngleMeasure measure = AngleMeasure.RADIANS) {
			Vector3 n = isNormalized ? this : this.normalized();
			pitch = Math.Asin(n.y);
			Vector2 xzDir = new Vector2(x, z).normalized();
			yaw = xzDir.getAngle();
			
			if(measure == AngleMeasure.DEGREES) {
				pitch = Geometry.toDegrees(pitch);
				yaw = Geometry.toDegrees(yaw);
			}

		}

		public static implicit operator Vector3(Vector4 vec4) {
			return new Vector3(vec4.x, vec4.y, vec4.z);
		}

		public static implicit operator Vector3(Vector2 vec2) {
			return new Vector3(vec2.x, vec2.y);
		}

		public static implicit operator Vector3(Quaternion q) {
			return new Vector3(q.x, q.y, q.z);
		}

		public static explicit operator Vector3((double x, double y, double z) t) {
			return new Vector3(t.x, t.y, t.z);
		}

		public static explicit operator (double x, double y, double z)(Vector3 vec3) {
			return (vec3.x, vec3.y, vec3.z);
		}

		public override string ToString() {
			return $"({x}; {y}; {z})"; 
		}
	}
}
