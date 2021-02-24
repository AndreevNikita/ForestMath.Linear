using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestMath.Linear {
	public struct Vector4 { 
		public double X, Y, Z, W;

		//Oh my...
		public Vector3 XYZ { get => new Vector3(X, Y, Z); set { X = value.X; Y = value.Y; Z = value.Z; } }
		public Vector3 XZY { get => new Vector3(X, Z, Y); set { X = value.X; Z = value.Y; Y = value.Z; } }
		public Vector3 XYW { get => new Vector3(X, Y, W); set { X = value.X; Y = value.Y; W = value.Z; } }
		public Vector3 XWY { get => new Vector3(X, W, Y); set { X = value.X; W = value.Y; Y = value.Z; } }
		public Vector3 XZW { get => new Vector3(X, Z, W); set { X = value.X; Z = value.Y; W = value.Z; } }
		public Vector3 XWZ { get => new Vector3(X, W, Z); set { X = value.X; W = value.Y; Z = value.Z; } }



		public Vector3 YXZ { get => new Vector3(Y, X, Z); set { Y = value.X; X = value.Y; Z = value.Z; } }
		public Vector3 YZX { get => new Vector3(Y, Z, X); set { Y = value.X; Z = value.Y; X = value.Z; } }
		public Vector3 YXW { get => new Vector3(Y, X, W); set { Y = value.X; X = value.Y; W = value.Z; } }
		public Vector3 YWX { get => new Vector3(Y, W, X); set { Y = value.X; W = value.Y; X = value.Z; } }
		public Vector3 YZW { get => new Vector3(Y, Z, W); set { Y = value.X; Z = value.Y; W = value.Z; } }
		public Vector3 YWZ { get => new Vector3(Y, W, Z); set { Y = value.X; W = value.Y; Z = value.Z; } }
		


		public Vector3 ZXY { get => new Vector3(Z, X, Y); set { Z = value.X; X = value.Y; Y = value.Z; } }
		public Vector3 ZYX { get => new Vector3(Z, Y, X); set { Z = value.X; Y = value.Y; X = value.Z; } }
		public Vector3 ZXW { get => new Vector3(Z, X, W); set { Z = value.X; X = value.Y; W = value.Z; } }
		public Vector3 ZWX { get => new Vector3(Z, W, X); set { Z = value.X; W = value.Y; X = value.Z; } }
		public Vector3 ZYW { get => new Vector3(Z, Y, W); set { Z = value.X; Y = value.Y; W = value.Z; } }
		public Vector3 ZWY { get => new Vector3(Z, W, Y); set { Z = value.X; W = value.Y; Y = value.Z; } }



		public Vector3 WXY { get => new Vector3(W, X, Y); set { W = value.X; X = value.Y; Y = value.Z; } }
		public Vector3 WYX { get => new Vector3(W, Y, X); set { W = value.X; Y = value.Y; X = value.Z; } }
		public Vector3 WXZ { get => new Vector3(W, X, Z); set { W = value.X; X = value.Y; Z = value.Z; } }
		public Vector3 WZX { get => new Vector3(W, Z, X); set { W = value.X; Z = value.Y; X = value.Z; } }
		public Vector3 WYZ { get => new Vector3(W, Y, Z); set { W = value.X; Y = value.Y; Z = value.Z; } }
		public Vector3 WZY { get => new Vector3(W, Z, Y); set { W = value.X; Z = value.Y; Y = value.Z; } }




		public Vector2 XY { get => new Vector2(X, Y); set { X = value.X; Y = value.Y; } }
		public Vector2 YZ { get => new Vector2(Y, Z); set { Y = value.X; Z = value.Y; } }
		public Vector2 ZX { get => new Vector2(Z, X); set { Z = value.X; X = value.Y; } }

		public Vector2 YX { get => new Vector2(Y, X); set { Y = value.X; X = value.Y; } }
		public Vector2 ZY { get => new Vector2(Z, Y); set { Z = value.X; Y = value.Y; } }
		public Vector2 XZ { get => new Vector2(X, Z); set { X = value.X; Z = value.Y; } }

		public double Length {
			get { return length(); }
			set { 
				double mul = value / Length;
				X *= mul;
				Y *= mul;
				Z *= mul;
				W *= mul;
			}
		}

		public Vector4(Vector4 src) : this(src.X, src.Y, src.Z, src.W) {}

		public Vector4(double x = 0.0f, double y = 0.0f, double z = 0.0f, double w = 1.0f) {
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		public static Vector4 operator+(Vector4 a, Vector4 b) {
			return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
		}

		public static Vector4 operator-(Vector4 a, Vector4 b) {
			return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
		}

		public static double operator*(Vector4 a, Vector4 b) {
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
		}

		public static Vector4 operator+(Vector4 a, double b) {
			return new Vector4(a.X + b, a.Y + b, a.Z + b, a.W + b);
		}

		public static Vector4 operator-(Vector4 a, double b) {
			return new Vector4(a.X - b, a.Y - b, a.Z - b, a.W - b);
		}

		public static Vector4 operator*(Vector4 a, double b) {
			return new Vector4(a.X * b, a.Y * b, a.Z * b, a.W * b);
		}

		public static Vector4 operator*(double a, Vector4 b) {
			return b * a;
		}

		public static Vector4 operator/(Vector4 a, double b) {
			return new Vector4(a.X / b, a.Y / b, a.Z / b, a.W / b);
		}

		public static bool operator==(Vector4 a, Vector4 b) {
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
		}

		public static bool operator!=(Vector4 a, Vector4 b) {
			return a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.W != b.W;
		}

		public static double operator^(Vector4 a, Vector4 b) { 
			return Math.Acos((a * b) / (a.Length * b.Length));
		}

		public override bool Equals(object obj) {
			if(obj is Vector4) {
				Vector4 b = (Vector4)obj;
				return this.X == b.X && this.Y == b.Y && this.Z == b.Z && this.W == b.W;
			}
			return false;
		}

		public override int GetHashCode() {
			return (X, Y, Z, W).GetHashCode();
		}

		public double length() { 
			return Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
		}

		public double sqrLength() { 
			return X * X + Y * Y + Z * Z + W * W;
		}

		public Vector4 normalized() {
			return this / length();
		}
		public Vector4 clone() {
			return new Vector4(this);
		}


		public static implicit operator Vector4(Vector3 vec3) {
			return new Vector4(vec3.X, vec3.Y, vec3.Z);
		}

		public static implicit operator Vector4(Vector2 vec2) {
			return new Vector4(vec2.X, vec2.Y);
		}

		public static explicit operator Vector4((double x, double y, double z, double w) t) {
			return new Vector4(t.x, t.y, t.z, t.w);
		}

		public static explicit operator (double x, double y, double z, double w)(Vector4 vec4) {
			return (vec4.X, vec4.Y, vec4.Z, vec4.Z);
		}

		public static Vector4 operator*(Matrix4 a, Vector4 b) {
			Vector4 result = new Vector4();
			result.X = a.MatrixData[0, 0] * b.X + a.MatrixData[0, 1] * b.Y + a.MatrixData[0, 2] * b.Z + a.MatrixData[0, 3] * b.W;
			result.Y = a.MatrixData[1, 0] * b.X + a.MatrixData[1, 1] * b.Y + a.MatrixData[1, 2] * b.Z + a.MatrixData[1, 3] * b.W;
			result.Z = a.MatrixData[2, 0] * b.X + a.MatrixData[2, 1] * b.Y + a.MatrixData[2, 2] * b.Z + a.MatrixData[2, 3] * b.W;
			result.W = a.MatrixData[3, 0] * b.X + a.MatrixData[3, 1] * b.Y + a.MatrixData[3, 2] * b.Z + a.MatrixData[3, 3] * b.W;
			return result;
		}

		public override string ToString() {
			return $"({X}; {Y}; {Z}; {W})"; 
		}
	}
}
