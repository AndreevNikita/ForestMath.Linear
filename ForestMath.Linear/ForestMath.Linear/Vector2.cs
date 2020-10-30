using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ForestMath.Linear.Geometry;

namespace ForestMath.Linear {
	public struct Vector2 { 
		public double x, y;

		public double Length {
			get { return length(); }
			set { 
				double mul = value / Length;
				x *= mul;
				y *= mul;
			}
		}

		public Vector2(Vector2 src) : this(src.x, src.y) {}

		public Vector2(double x = 0.0f, double y = 0.0f) {
			this.x = x;
			this.y = y;
		}

		public static Vector2 operator+(Vector2 a, Vector2 b) {
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		public static Vector2 operator-(Vector2 a, Vector2 b) {
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		public static double operator*(Vector2 a, Vector2 b) {
			return a.x * b.x + a.y * b.y;
		}

		public static Vector2 operator-(Vector2 vec) {
			return new Vector2(-vec.x, -vec.y);
		}

		public static Vector2 operator+(Vector2 a, double b) {
			return new Vector2(a.x + b, a.y + b);
		}

		public static Vector2 operator-(Vector2 a, double b) {
			return new Vector2(a.x - b, a.y - b);
		}

		public static Vector2 operator*(Vector2 a, double b) {
			return new Vector2(a.x * b, a.y * b);
		}

		public static Vector2 operator*(double a, Vector2 b) {
			return b * a;
		}

		public static Vector2 operator/(Vector2 a, double b) {
			return new Vector2(a.x / b, a.y);
		}

		public static bool operator==(Vector2 a, Vector2 b) {
			return a.x == b.x && a.y == b.y;
		}

		public static bool operator!=(Vector2 a, Vector2 b) {
			return a.x != b.x || a.y != b.y;
		}

		public static double operator^(Vector2 a, Vector2 b) { 
			return a.getAngle(b);
		}

		public override bool Equals(object obj) {
			if(obj is Vector2) {
				Vector2 b = (Vector2)obj;
				return this.x == b.x && this.y == b.y;
			}
			return false;
		}

		public override int GetHashCode() {
			return (x, y).GetHashCode();
		}

		public double length() { 
			return Math.Sqrt(x * x + y * y);
		}

		public double sqrLength() { 
			return x * x + y * y;
		}

		public Vector2 normalized() {
			return this / length();
		}

		public double getAngle(bool isNormalized = false, AngleMeasure measure = AngleMeasure.RADIANS) {
			Vector2 n = isNormalized ? this : this.normalized();
			double result = n.y > 0 ? Math.Acos(n.x) : -Math.Acos(n.x);
			if(measure == AngleMeasure.DEGREES)
				result = Geometry.toDegrees(result);
			return result;
		}

		public double getAngle(Vector2 b, AngleMeasure measure = AngleMeasure.RADIANS) {
			double result = Math.Acos(this * b / (this.length() * b.length()));
			if(new Vector2(-y, x) * b < 0)
				result = -result;
			if(measure == AngleMeasure.DEGREES)
				result = Geometry.toDegrees(result);
			return result;
		}

		public Vector2 clone() {
			return new Vector2(this);
		}

		public static implicit operator Vector2(Vector4 vec4) {
			return new Vector2(vec4.x, vec4.y);
		}

		public static implicit operator Vector2(Vector3 vec3) {
			return new Vector2(vec3.x, vec3.y);
		}

		public static explicit operator Vector2((double x, double y) t) {
			return new Vector2(t.x, t.y);
		}

		public static explicit operator (double x, double y)(Vector2 vec2) {
			return (vec2.x, vec2.y);
		}

		public override string ToString() {
			return $"({x}; {y})"; 
		}
	}
}
