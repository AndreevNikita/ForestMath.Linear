using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestMath.Linear {
	public struct Vector4 { 
		public double x, y, z, w;

		public double Length {
			get { return length(); }
			set { 
				double mul = value / Length;
				x *= mul;
				y *= mul;
				z *= mul;
				w *= mul;
			}
		}

		public Vector4(Vector4 src) : this(src.x, src.y, src.z, src.w) {}

		public Vector4(double x = 0.0f, double y = 0.0f, double z = 0.0f, double w = 1.0f) {
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public static Vector4 operator+(Vector4 a, Vector4 b) {
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}

		public static Vector4 operator-(Vector4 a, Vector4 b) {
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}

		public static double operator*(Vector4 a, Vector4 b) {
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		public static Vector4 operator+(Vector4 a, double b) {
			return new Vector4(a.x + b, a.y + b, a.z + b, a.w + b);
		}

		public static Vector4 operator-(Vector4 a, double b) {
			return new Vector4(a.x - b, a.y - b, a.z - b, a.w - b);
		}

		public static Vector4 operator*(Vector4 a, double b) {
			return new Vector4(a.x * b, a.y * b, a.z * b, a.w * b);
		}

		public static Vector4 operator*(double a, Vector4 b) {
			return b * a;
		}

		public static Vector4 operator/(Vector4 a, double b) {
			return new Vector4(a.x / b, a.y / b, a.z / b, a.w / b);
		}

		public static bool operator==(Vector4 a, Vector4 b) {
			return a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
		}

		public static bool operator!=(Vector4 a, Vector4 b) {
			return a.x != b.x || a.y != b.y || a.z != b.z || a.w != b.w;
		}

		public static double operator^(Vector4 a, Vector4 b) { 
			return Math.Acos((a * b) / (a.Length * b.Length));
		}

		public override bool Equals(object obj) {
			if(obj is Vector4) {
				Vector4 b = (Vector4)obj;
				return this.x == b.x && this.y == b.y && this.z == b.z && this.w == b.w;
			}
			return false;
		}

		public override int GetHashCode() {
			return (x, y, z, w).GetHashCode();
		}

		public double length() { 
			return Math.Sqrt(x * x + y * y + z * z + w * w);
		}

		public double sqrLength() { 
			return x * x + y * y + z * z + w * w;
		}

		public Vector4 normalized() {
			return this / length();
		}
		public Vector4 clone() {
			return new Vector4(this);
		}


		public static implicit operator Vector4(Vector3 vec3) {
			return new Vector4(vec3.x, vec3.y, vec3.z);
		}

		public static implicit operator Vector4(Vector2 vec2) {
			return new Vector4(vec2.x, vec2.y);
		}

		public static explicit operator Vector4((double x, double y, double z, double w) t) {
			return new Vector4(t.x, t.y, t.z, t.w);
		}

		public static explicit operator (double x, double y, double z, double w)(Vector4 vec4) {
			return (vec4.x, vec4.y, vec4.z, vec4.z);
		}

		public static Vector4 operator*(Matrix4 a, Vector4 b) {
			Vector4 result = new Vector4();
			result.x = a.matrix[0, 0] * b.x + a.matrix[0, 1] * b.y + a.matrix[0, 2] * b.z + a.matrix[0, 3] * b.w;
			result.y = a.matrix[1, 0] * b.x + a.matrix[1, 1] * b.y + a.matrix[1, 2] * b.z + a.matrix[1, 3] * b.w;
			result.z = a.matrix[2, 0] * b.x + a.matrix[2, 1] * b.y + a.matrix[2, 2] * b.z + a.matrix[2, 3] * b.w;
			result.w = a.matrix[3, 0] * b.x + a.matrix[3, 1] * b.y + a.matrix[3, 2] * b.z + a.matrix[3, 3] * b.w;
			return result;
		}

		public override string ToString() {
			return $"({x}; {y}; {z}; {w})"; 
		}
	}
}
