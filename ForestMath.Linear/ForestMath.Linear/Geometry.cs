using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestMath.Linear {
	public static partial class Geometry {

		public enum AngleMeasure { RADIANS, DEGREES };

		public static double ToRadians(double angle) {
			return angle / 180.0 * Math.PI;
		}

		public static double ToDegrees(double rads) {
			return rads / Math.PI * 180.0;
		}

		//Cross points

		public static bool GetCrossPoint(Ray3 ray, Plane plane, out Vector3 result) { 
			return GetCrossPoint(ray, plane.Normal, plane.D, out result);
		}

		
		public static bool GetCrossPoint(Ray3 ray, Vector3 normal, double d, out Vector3 result) {
			//Перпендикулярность вектора. Вероятность пересечения крайне мала
			if(ray.Dir.X * normal.X + ray.Dir.Y * normal.Y + ray.Dir.Z * normal.Z == 0) {
				result = default;
				return false;
			}

			//normal.x * x + normal.y * (line.dir.y * (x / line.dir.x) + line.shiftYZ.y) + normal.z * (line.dir.z * (x / line.dir.x) + line.shiftYZ.z) + d = 0
			//normal.x * x + normal.y * line.dir.y * x / line.dir.x + normal.dir.y * line.shiftYZ.y + normal.z * line.dir.z * x / line.dir.x + normal.z * line.shiftYZ.z + d = 0
			//normal.x * x + normal.y * line.dir.y * x / line.dir.x + normal.z * line.dir.z * x / line.dir.x = -(normal.y * line.shiftYZ.y + normal.z * line.shiftYZ.z + d)
			//x * (normal.x + normal.y * line.dir.y / line.dir.x + normal.z * line.dir.z / line.dir.x) = -(normal.y * line.shiftYZ.y + normal.z * line.shiftYZ.z + d)
			//x = -(normal.y * line.shiftYZ.y + normal.z * line.shiftYZ.z + d) / (normal.x + normal.y * line.dir.y / line.dir.x + normal.z * line.dir.z / line.dir.x)

			if(ray.IsAxisXDirected) {
				Vector3 shiftYZ = ray.ShiftYZ;
				double x = -(normal.Y * shiftYZ.Y + normal.Z * shiftYZ.Z + d) / (normal.X + normal.Y * ray.Dir.Y / ray.Dir.X + normal.Z * ray.Dir.Z / ray.Dir.X);
				if(ray.IsXOnRay(x))
					return ray.GetPointWithX(x, out result);
			} else if(ray.IsAxisYDirected) {
				Vector3 shiftXZ = ray.ShiftXZ;
				double y = -(normal.X * shiftXZ.X + normal.Z * shiftXZ.Z + d) / (normal.Y + normal.X * ray.Dir.X / ray.Dir.Y + normal.Z * ray.Dir.Z / ray.Dir.Y);
				if(ray.IsYOnRay(y))
					return ray.GetPointWithY(y, out result);
			} else if(ray.IsAxisZDirected) {
				Vector3 shiftXY = ray.ShiftXY;
				double z = -(normal.Y * shiftXY.Y + normal.X * shiftXY.X + d) / (normal.Z + normal.Y * ray.Dir.Y / ray.Dir.Z + normal.X * ray.Dir.X / ray.Dir.Z);
				if(ray.IsZOnRay(z))
					return ray.GetPointWithZ(z, out result);
			}

			result = default;
			return false;
		}
		public static bool GetXPlaneCrossPoint(Ray3 ray, double planeX, Vector3 crossPoint, out Vector3 result) {
			return ray.GetPointWithX(planeX, out result);
		}

		public static bool GetYPlaneCrossPoint(Ray3 ray, double planeY, Vector3 crossPoint, out Vector3 result) {
			return ray.GetPointWithY(planeY, out result);
		}

		public static bool GetZPlaneCrossPoint(Ray3 ray, double planeZ, Vector3 crossPoint, out Vector3 result) {
			return ray.GetPointWithZ(planeZ, out result);
		}

		//In checkers
		public static bool CheckIn(double a, double value, double b) { 
			return (a <= value) && (value <= b);
		}

		public static bool CheckIn(Vector2 a, Vector2 value, Vector2 b) { 
			//Optimized without top level CheckIn call
			return (a.X <= value.X) && (value.X <= b.X) && (a.Y <= value.Y) && (value.Y <= b.Y);
		}

		public static bool CheckIn(Vector3 a, Vector3 value, Vector3 b) { 
			//Optimized without top level CheckIn call
			return (a.X <= value.X) && (value.X <= b.X) && (a.Y <= value.Y) && (value.Y <= b.Y) && (a.Z <= value.Z) && (value.Z <= b.Z);
		}


		//Interpolation

		public static Vector2 Interpolate(Vector2 a, Vector2 b, double percent) {
			return a * (1.0 - percent) + b * percent;
		}

		public static Vector3 Interpolate(Vector3 a, Vector3 b, double percent) {
			return a * (1.0 - percent) + b * percent;
		}

		public static Vector4 Interpolate(Vector4 a, Vector4 b, double percent) {
			return a * (1.0 - percent) + b * percent;
		}

		public static Vector3 Interpolate(Quaternion a, Quaternion b, double percent) {
			return a * (1.0 - percent) + b * percent;
		}

		//Quaternions

		public static double ScalarMultiply(Quaternion a, Quaternion b) {
			return a.W * b.W + a.X * b.X + a.Y * b.Y * a.Z * b.Z;
		}

		public static Vector3 Rotate(this Vector3 vec, Quaternion quaternion) {
			return  quaternion * new Quaternion(vec) * (-quaternion);
		}

		public static void Rotate(ref Vector3 vec, Quaternion quaternion) {
			vec = quaternion * new Quaternion(vec) * (-quaternion);
		}

		public static Ray3 RotateFull(this Ray3 ray, Quaternion quaternion) {
			return Ray3.FromTwoPoints(Rotate(ray.StartPoint, quaternion), Rotate(ray.StartPoint + ray.Dir, quaternion), ray.IsLine);
		}

		public static Ray3 RotateDir(this Ray3 ray, Quaternion quaternion) {
			return new Ray3(ray.StartPoint, Rotate(ray.StartPoint + ray.Dir, quaternion), ray.IsLine);
		}

		public static Ray3 Shift(this Ray3 ray, Vector3 shiftVec) { 
			return new Ray3(ray.StartPoint + shiftVec, ray.Dir, ray.IsLine);
		}

	}
}
