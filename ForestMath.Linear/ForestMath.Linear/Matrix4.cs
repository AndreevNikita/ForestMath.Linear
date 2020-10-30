using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ForestMath.Linear.Geometry;

namespace ForestMath.Linear {
	public class Matrix4 { 
		public double[,] matrix = new double[4, 4];

		public Matrix4(bool identity = true) {
			for(int i = 0; i < 4; i++)
				for(int j = 0; j < 4; j++)
					if(identity)
						matrix[i, j] = i != j ? 0.0 : 1.0;
					else
						matrix[i, j] = 0.0;
		}

		public static Matrix4 operator+(Matrix4 a, Matrix4 b) {
			Matrix4 result = new Matrix4();
			result.matrix[0, 0] = a.matrix[0, 0] + b.matrix[0, 0];
			result.matrix[0, 1] = a.matrix[0, 1] + b.matrix[0, 1];
			result.matrix[0, 2] = a.matrix[0, 2] + b.matrix[0, 2];
			result.matrix[0, 3] = a.matrix[0, 3] + b.matrix[0, 3];
			result.matrix[1, 0] = a.matrix[1, 0] + b.matrix[1, 0];
			result.matrix[1, 1] = a.matrix[1, 1] + b.matrix[1, 1];
			result.matrix[1, 2] = a.matrix[1, 2] + b.matrix[1, 2];
			result.matrix[1, 3] = a.matrix[1, 3] + b.matrix[1, 3];
			result.matrix[2, 0] = a.matrix[2, 0] + b.matrix[2, 0];
			result.matrix[2, 1] = a.matrix[2, 1] + b.matrix[2, 1];
			result.matrix[2, 2] = a.matrix[2, 2] + b.matrix[2, 2];
			result.matrix[2, 3] = a.matrix[2, 3] + b.matrix[2, 3];
			result.matrix[3, 0] = a.matrix[3, 0] + b.matrix[3, 0];
			result.matrix[3, 1] = a.matrix[3, 1] + b.matrix[3, 1];
			result.matrix[3, 2] = a.matrix[3, 2] + b.matrix[3, 2];
			result.matrix[3, 3] = a.matrix[3, 3] + b.matrix[3, 3];
			return result;
		}

		public static Matrix4 operator-(Matrix4 a, Matrix4 b) {
			Matrix4 result = new Matrix4();
			result.matrix[0, 0] = a.matrix[0, 0] - b.matrix[0, 0];
			result.matrix[0, 1] = a.matrix[0, 1] - b.matrix[0, 1];
			result.matrix[0, 2] = a.matrix[0, 2] - b.matrix[0, 2];
			result.matrix[0, 3] = a.matrix[0, 3] - b.matrix[0, 3];
			result.matrix[1, 0] = a.matrix[1, 0] - b.matrix[1, 0];
			result.matrix[1, 1] = a.matrix[1, 1] - b.matrix[1, 1];
			result.matrix[1, 2] = a.matrix[1, 2] - b.matrix[1, 2];
			result.matrix[1, 3] = a.matrix[1, 3] - b.matrix[1, 3];
			result.matrix[2, 0] = a.matrix[2, 0] - b.matrix[2, 0];
			result.matrix[2, 1] = a.matrix[2, 1] - b.matrix[2, 1];
			result.matrix[2, 2] = a.matrix[2, 2] - b.matrix[2, 2];
			result.matrix[2, 3] = a.matrix[2, 3] - b.matrix[2, 3];
			result.matrix[3, 0] = a.matrix[3, 0] - b.matrix[3, 0];
			result.matrix[3, 1] = a.matrix[3, 1] - b.matrix[3, 1];
			result.matrix[3, 2] = a.matrix[3, 2] - b.matrix[3, 2];
			result.matrix[3, 3] = a.matrix[3, 3] - b.matrix[3, 3];
			return result;
		}

		public static Matrix4 operator*(Matrix4 a, Matrix4 b) {
			Matrix4 result = new Matrix4();
			result.matrix[0, 0] = a.matrix[0, 0] * b.matrix[0, 0] + a.matrix[0, 1] * b.matrix[1, 0] + a.matrix[0, 2] * b.matrix[2, 0] + a.matrix[0, 3] * b.matrix[3, 0];
			result.matrix[0, 1] = a.matrix[0, 0] * b.matrix[0, 1] + a.matrix[0, 1] * b.matrix[1, 1] + a.matrix[0, 2] * b.matrix[2, 1] + a.matrix[0, 3] * b.matrix[3, 1];
			result.matrix[0, 2] = a.matrix[0, 0] * b.matrix[0, 2] + a.matrix[0, 1] * b.matrix[1, 2] + a.matrix[0, 2] * b.matrix[2, 2] + a.matrix[0, 3] * b.matrix[3, 2];
			result.matrix[0, 3] = a.matrix[0, 0] * b.matrix[0, 3] + a.matrix[0, 1] * b.matrix[1, 3] + a.matrix[0, 2] * b.matrix[2, 3] + a.matrix[0, 3] * b.matrix[3, 3];
				
			result.matrix[1, 0] = a.matrix[1, 0] * b.matrix[0, 0] + a.matrix[1, 1] * b.matrix[1, 0] + a.matrix[1, 2] * b.matrix[2, 0] + a.matrix[1, 3] * b.matrix[3, 0];
			result.matrix[1, 1] = a.matrix[1, 0] * b.matrix[0, 1] + a.matrix[1, 1] * b.matrix[1, 1] + a.matrix[1, 2] * b.matrix[2, 1] + a.matrix[1, 3] * b.matrix[3, 1];
			result.matrix[1, 2] = a.matrix[1, 0] * b.matrix[0, 2] + a.matrix[1, 1] * b.matrix[1, 2] + a.matrix[1, 2] * b.matrix[2, 2] + a.matrix[1, 3] * b.matrix[3, 2];
			result.matrix[1, 3] = a.matrix[1, 0] * b.matrix[0, 3] + a.matrix[1, 1] * b.matrix[1, 3] + a.matrix[1, 2] * b.matrix[2, 3] + a.matrix[1, 3] * b.matrix[3, 3];

			result.matrix[2, 0] = a.matrix[2, 0] * b.matrix[0, 0] + a.matrix[2, 1] * b.matrix[1, 0] + a.matrix[2, 2] * b.matrix[2, 0] + a.matrix[2, 3] * b.matrix[3, 0];
			result.matrix[2, 1] = a.matrix[2, 0] * b.matrix[0, 1] + a.matrix[2, 1] * b.matrix[1, 1] + a.matrix[2, 2] * b.matrix[2, 1] + a.matrix[2, 3] * b.matrix[3, 1];
			result.matrix[2, 2] = a.matrix[2, 0] * b.matrix[0, 2] + a.matrix[2, 1] * b.matrix[1, 2] + a.matrix[2, 2] * b.matrix[2, 2] + a.matrix[2, 3] * b.matrix[3, 2];
			result.matrix[2, 3] = a.matrix[2, 0] * b.matrix[0, 3] + a.matrix[2, 1] * b.matrix[1, 3] + a.matrix[2, 2] * b.matrix[2, 3] + a.matrix[2, 3] * b.matrix[3, 3];

			result.matrix[3, 0] = a.matrix[3, 0] * b.matrix[0, 0] + a.matrix[3, 1] * b.matrix[1, 0] + a.matrix[3, 2] * b.matrix[2, 0] + a.matrix[3, 3] * b.matrix[3, 0];
			result.matrix[3, 1] = a.matrix[3, 0] * b.matrix[0, 1] + a.matrix[3, 1] * b.matrix[1, 1] + a.matrix[3, 2] * b.matrix[2, 1] + a.matrix[3, 3] * b.matrix[3, 1];
			result.matrix[3, 2] = a.matrix[3, 0] * b.matrix[0, 2] + a.matrix[3, 1] * b.matrix[1, 2] + a.matrix[3, 2] * b.matrix[2, 2] + a.matrix[3, 3] * b.matrix[3, 2];
			result.matrix[3, 3] = a.matrix[3, 0] * b.matrix[0, 3] + a.matrix[3, 1] * b.matrix[1, 3] + a.matrix[3, 2] * b.matrix[2, 3] + a.matrix[3, 3] * b.matrix[3, 3];
			return result;
		}

		public static Matrix4 operator*(Matrix4 a, double b) {
			Matrix4 result = new Matrix4();
			result.matrix[0, 0] = a.matrix[0, 0] * b;
			result.matrix[0, 1] = a.matrix[0, 1] * b;
			result.matrix[0, 2] = a.matrix[0, 2] * b;
			result.matrix[0, 3] = a.matrix[0, 3] * b;
			result.matrix[1, 0] = a.matrix[1, 0] * b;
			result.matrix[1, 1] = a.matrix[1, 1] * b;
			result.matrix[1, 2] = a.matrix[1, 2] * b;
			result.matrix[1, 3] = a.matrix[1, 3] * b;
			result.matrix[2, 0] = a.matrix[2, 0] * b;
			result.matrix[2, 1] = a.matrix[2, 1] * b;
			result.matrix[2, 2] = a.matrix[2, 2] * b;
			result.matrix[2, 3] = a.matrix[2, 3] * b;
			result.matrix[3, 0] = a.matrix[3, 0] * b;
			result.matrix[3, 1] = a.matrix[3, 1] * b;
			result.matrix[3, 2] = a.matrix[3, 2] * b;
			result.matrix[3, 3] = a.matrix[3, 3] * b;
			return result;
		}

		public Matrix4 transposed() { 
			Matrix4 result = new Matrix4(false);
			result.matrix[0, 0] = matrix[0, 0];
			result.matrix[0, 1] = matrix[1, 0];
			result.matrix[0, 2] = matrix[2, 0];
			result.matrix[0, 3] = matrix[3, 0];

			result.matrix[1, 0] = matrix[0, 1];
			result.matrix[1, 1] = matrix[1, 1];
			result.matrix[1, 2] = matrix[2, 1];
			result.matrix[1, 3] = matrix[3, 1];

			result.matrix[2, 0] = matrix[0, 2];
			result.matrix[2, 1] = matrix[1, 2];
			result.matrix[2, 2] = matrix[2, 2];
			result.matrix[2, 3] = matrix[3, 2];

			result.matrix[3, 0] = matrix[0, 3];
			result.matrix[3, 1] = matrix[1, 3];
			result.matrix[3, 2] = matrix[2, 3];
			result.matrix[3, 3] = matrix[3, 3];
			return result;
		}

		public static Matrix4 identity() { 
			return new Matrix4();
		}

		public float[] toFloatArray(float[] arr = null, bool transposed = false) {
			if(arr == null)
				arr = new float[16];
			if(transposed) { 
				arr[00] = (float)matrix[0, 0];
				arr[01] = (float)matrix[1, 0];
				arr[02] = (float)matrix[2, 0];
				arr[03] = (float)matrix[3, 0];
				arr[04] = (float)matrix[0, 1];
				arr[05] = (float)matrix[1, 1];
				arr[06] = (float)matrix[2, 1];
				arr[07] = (float)matrix[3, 1];
				arr[08] = (float)matrix[0, 2];
				arr[09] = (float)matrix[1, 2];
				arr[10] = (float)matrix[2, 2];
				arr[11] = (float)matrix[3, 2];
				arr[12] = (float)matrix[0, 3];
				arr[13] = (float)matrix[1, 3];
				arr[14] = (float)matrix[2, 3];
				arr[15] = (float)matrix[3, 3];
			} else {
				arr[00] = (float)matrix[0, 0];
				arr[01] = (float)matrix[0, 1];
				arr[02] = (float)matrix[0, 2];
				arr[03] = (float)matrix[0, 3];
				arr[04] = (float)matrix[1, 0];
				arr[05] = (float)matrix[1, 1];
				arr[06] = (float)matrix[1, 2];
				arr[07] = (float)matrix[1, 3];
				arr[08] = (float)matrix[2, 0];
				arr[09] = (float)matrix[2, 1];
				arr[10] = (float)matrix[2, 2];
				arr[11] = (float)matrix[2, 3];
				arr[12] = (float)matrix[3, 0];
				arr[13] = (float)matrix[3, 1];
				arr[14] = (float)matrix[3, 2];
				arr[15] = (float)matrix[3, 3];
			}
			return arr;
		}

		public static Matrix4 rotationX(double angle, AngleMeasure measure = AngleMeasure.RADIANS) { 
			if(measure == AngleMeasure.DEGREES)
				angle = Geometry.toRadians(angle);
			Matrix4 result = new Matrix4(false);
			result.matrix[0, 0] = 1.0;
			double cosA = Math.Cos(angle);
			double sinA = Math.Sin(angle);
			result.matrix[0, 0] = 1.0;
			result.matrix[1, 1] =  cosA;
			result.matrix[1, 2] = -sinA;
			result.matrix[2, 1] =  sinA;
			result.matrix[2, 2] =  cosA;
			result.matrix[3, 3] = 1.0;
			return result;
		}

		public static Matrix4 rotationY(double angle, AngleMeasure measure = AngleMeasure.RADIANS) {
			if(measure == AngleMeasure.DEGREES)
				angle = Geometry.toRadians(angle);
			Matrix4 result = new Matrix4(false);
			result.matrix[0, 0] = 1.0;
			double cosA = Math.Cos(angle);
			double sinA = Math.Sin(angle);
			result.matrix[1, 1] = 1.0;
			result.matrix[0, 0] =  cosA;
			result.matrix[0, 2] =  sinA;
			result.matrix[2, 0] =  -sinA;
			result.matrix[2, 2] =  cosA;
			result.matrix[3, 3] = 1.0;
			return result;
		}

		public static Matrix4 rotationZ(double angle, AngleMeasure measure = AngleMeasure.RADIANS) {
			if(measure == AngleMeasure.DEGREES)
				angle = Geometry.toRadians(angle);
			Matrix4 result = new Matrix4(false);
			double cosA = Math.Cos(angle);
			double sinA = Math.Sin(angle);
			
			result.matrix[0, 0] =  cosA;
			result.matrix[0, 1] = -sinA;
			result.matrix[1, 0] =  sinA;
			result.matrix[1, 1] =  cosA;
			result.matrix[2, 2] = 1.0;
			result.matrix[3, 3] = 1.0;
			return result;
		}

		public static Matrix4 scale(double scaleX, double scaleY, double scaleZ) {
			Matrix4 result = new Matrix4(true);
			result.matrix[0, 0] = scaleX;
			result.matrix[1, 1] = scaleY;
			result.matrix[2, 2] = scaleZ;
			return result;
		}

		public static Matrix4 translation(double x, double y, double z) { 
			Matrix4 result = Matrix4.identity();
			result.matrix[0, 3] = x;
			result.matrix[1, 3] = y;
			result.matrix[2, 3] = z;
			return result;
		} 

		public static Matrix4 projection(double fov, double aspect, double nearDist, double farDist) {
			//
			// General form of the Projection Matrix
			//
			// uh = Cot( fov/2 ) == 1/Tan(fov/2)
			// uw / uh = 1/aspect
			// 
			//   uw         0       0       0
			//    0        uh       0       0
			//    0         0      f/(f-n)  1
			//    0         0    -fn/(f-n)  0
			//
			// Make result to be identity first
			Matrix4 result = new Matrix4();
			double frustumDepth = farDist - nearDist;
			double oneOverDepth = 1 / frustumDepth;

			result.matrix[1, 1] = 1 / Math.Tan(0.5f * fov);
			result.matrix[0, 0] = -result.matrix[1, 1] / aspect;
			result.matrix[2, 2] = farDist * oneOverDepth;
			result.matrix[2, 3] = (-farDist * nearDist) * oneOverDepth;
			result.matrix[3, 2] = 1.0;
			result.matrix[3, 3] = 0;
			return result;
		}

		public double this[int i, int j] {
			get { return matrix[i, j]; }
			set { matrix[i, j] = value; }
		}

		public override string ToString() {
			string result = "";
			for(int i = 0; i < 4; i++) {
				for(int j = 0; j < 4; j++)
					result += matrix[i, j] + " ";
				result += "\n";
			}
			return result;
		}

	}
}
