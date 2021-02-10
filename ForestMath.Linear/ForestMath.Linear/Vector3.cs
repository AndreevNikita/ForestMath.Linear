using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ForestMath.Linear.Geometry;

namespace ForestMath.Linear {
	public struct Vector3 { 
		public double X, Y, Z;
		public double Yaw {
			get { return X; }
			set { X = value; }
		}
		public double Pitch {
			get { return Y; }
			set { Y = value; }
		}
		public double Roll {
			get { return Z; }
			set { Z = value; }
		}

		public double Length {
			get { return GetLength(); }
			set { 
				double mul = value / Length;
				X *= mul;
				Y *= mul;
				Z *= mul;
			}
		}

		public Vector3(Vector3 src) : this(src.X, src.Y, src.Z) {}

		public Vector3(double x = 0.0f, double y = 0.0f, double z = 0.0f) {
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public static Vector3 operator+(Vector3 a, Vector3 b) {
			return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public static Vector3 operator-(Vector3 a, Vector3 b) {
			return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		public static double operator*(Vector3 a, Vector3 b) {
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
		}

		public static Vector3 operator-(Vector3 vec) {
			return new Vector3(-vec.X, -vec.Y, -vec.Z);
		}

		public static Vector3 operator+(Vector3 a, double b) {
			return new Vector3(a.X + b, a.Y + b, a.Z + b);
		}

		public static Vector3 operator-(Vector3 a, double b) {
			return new Vector3(a.X - b, a.Y - b, a.Z - b);
		}

		public static Vector3 operator*(Vector3 a, double b) {
			return new Vector3(a.X * b, a.Y * b, a.Z * b);
		}

		public static Vector3 operator*(double a, Vector3 b) {
			return b * a;
		}

		public static Vector3 operator/(Vector3 a, double b) {
			return new Vector3(a.X / b, a.Y / b, a.Z / b);
		}

		public static bool operator==(Vector3 a, Vector3 b) {
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
		}

		public static bool operator!=(Vector3 a, Vector3 b) {
			return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
		}

		public static double operator^(Vector3 a, Vector3 b) { 
			return Math.Acos((a * b) / (a.Length * b.Length));
		}

		public override bool Equals(object obj) {
			if(obj is Vector3) {
				Vector3 b = (Vector3)obj;
				return this.X == b.X && this.Y == b.Y && this.Z == b.Z;
			}
			return false;
		}

		public override int GetHashCode() {
			return (X, Y, Z).GetHashCode();
		}

		public double GetLength() { 
			return Math.Sqrt(X * X + Y * Y + Z * Z);
		}

		public double GetAngle(bool isNormalized = false, AngleMeasure measure = AngleMeasure.RADIANS) {
			Vector2 n = isNormalized ? this : this.Normalized();
			double result = n.Y > 0 ? Math.Acos(n.X) : -Math.Acos(n.X);
			if(measure == AngleMeasure.DEGREES)
				result = Geometry.ToDegrees(result);
			return result;
		}

		public double GetAngle(Vector3 b, AngleMeasure measure = AngleMeasure.RADIANS) { 
			double result = Math.Acos(this * b / (this.Length * b.Length));
			if(measure == AngleMeasure.DEGREES)
				result = Geometry.ToDegrees(result);
			return result;
		}

		public double SqrLength() { 
			return X * X + Y * Y + Z * Z;
		}

		public Vector3 Normalized() {
			return this / GetLength();
		}

		public void GetYawPitch(out double yaw, out double pitch, bool isNormalized = false, AngleMeasure measure = AngleMeasure.RADIANS) {
			Vector3 n = isNormalized ? this : this.Normalized();
			pitch = Math.Asin(n.Y);
			Vector2 xzDir = new Vector2(X, Z).Normalized();
			yaw = xzDir.GetAngle();
			
			if(measure == AngleMeasure.DEGREES) {
				pitch = Geometry.ToDegrees(pitch);
				yaw = Geometry.ToDegrees(yaw);
			}

		}

		public static implicit operator Vector3(Vector4 vec4) {
			return new Vector3(vec4.X, vec4.Y, vec4.Z);
		}

		public static implicit operator Vector3(Vector2 vec2) {
			return new Vector3(vec2.X, vec2.Y);
		}

		public static implicit operator Vector3(Quaternion q) {
			return new Vector3(q.X, q.Y, q.Z);
		}

		public static explicit operator Vector3((double x, double y, double z) t) {
			return new Vector3(t.x, t.y, t.z);
		}

		public static explicit operator (double x, double y, double z)(Vector3 vec3) {
			return (vec3.X, vec3.Y, vec3.Z);
		}

		public override string ToString() {
			return $"({X}; {Y}; {Z})"; 
		}
	}
}
