using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ForestMath.Linear.Geometry;

namespace ForestMath.Linear {
	public struct Vector2 { 
		public double X, Y;

		public double Tan { get => Y / X; }

		public double Length {
			get { return GetLength(); }
			set { 
				double mul = value / Length;
				X *= mul;
				Y *= mul;
			}
		}

		public Vector2(Vector2 src) : this(src.X, src.Y) {}

		public Vector2(double x = 0.0f, double y = 0.0f) {
			this.X = x;
			this.Y = y;
		}

		public static Vector2 operator+(Vector2 a, Vector2 b) {
			return new Vector2(a.X + b.X, a.Y + b.Y);
		}

		public static Vector2 operator-(Vector2 a, Vector2 b) {
			return new Vector2(a.X - b.X, a.Y - b.Y);
		}

		public static double operator*(Vector2 a, Vector2 b) {
			return a.X * b.X + a.Y * b.Y;
		}

		public static Vector2 operator-(Vector2 vec) {
			return new Vector2(-vec.X, -vec.Y);
		}

		public static Vector2 operator+(Vector2 a, double b) {
			return new Vector2(a.X + b, a.Y + b);
		}

		public static Vector2 operator-(Vector2 a, double b) {
			return new Vector2(a.X - b, a.Y - b);
		}

		public static Vector2 operator*(Vector2 a, double b) {
			return new Vector2(a.X * b, a.Y * b);
		}

		public static Vector2 operator*(double a, Vector2 b) {
			return b * a;
		}

		public static Vector2 operator/(Vector2 a, double b) {
			return new Vector2(a.X / b, a.Y);
		}

		public static bool operator==(Vector2 a, Vector2 b) {
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator!=(Vector2 a, Vector2 b) {
			return a.X != b.X || a.Y != b.Y;
		}

		public static double operator^(Vector2 a, Vector2 b) { 
			return a.GetAngle(b);
		}

		public override bool Equals(object obj) {
			if(obj is Vector2) {
				Vector2 b = (Vector2)obj;
				return this.X == b.X && this.Y == b.Y;
			}
			return false;
		}

		public override int GetHashCode() {
			return (X, Y).GetHashCode();
		}

		public double GetLength() { 
			return Math.Sqrt(X * X + Y * Y);
		}

		public double SqrLength() { 
			return X * X + Y * Y;
		}

		public Vector2 Normalized() {
			return this / GetLength();
		}

		public double GetAngle(bool isNormalized = false, AngleMeasure measure = AngleMeasure.RADIANS) {
			Vector2 n = isNormalized ? this : this.Normalized();
			double result = n.Y > 0 ? Math.Acos(n.X) : -Math.Acos(n.X);
			if(measure == AngleMeasure.DEGREES)
				result = Geometry.ToDegrees(result);
			return result;
		}

		public double GetAngle(Vector2 b, AngleMeasure measure = AngleMeasure.RADIANS) {
			double result = Math.Acos(this * b / (this.GetLength() * b.GetLength()));
			if(new Vector2(-Y, X) * b < 0)
				result = -result;
			if(measure == AngleMeasure.DEGREES)
				result = Geometry.ToDegrees(result);
			return result;
		}

		public Vector2 Clone() {
			return new Vector2(this);
		}

		public static implicit operator Vector2(Vector4 vec4) {
			return new Vector2(vec4.X, vec4.Y);
		}

		public static implicit operator Vector2(Vector3 vec3) {
			return new Vector2(vec3.X, vec3.Y);
		}

		public static explicit operator Vector2((double x, double y) t) {
			return new Vector2(t.x, t.y);
		}

		public static explicit operator (double x, double y)(Vector2 vec2) {
			return (vec2.X, vec2.Y);
		}

		public override string ToString() {
			return $"({X}; {Y})"; 
		}
	}
}
