using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestMath.Linear {
	public partial class Geometry {

		public enum AngleMeasure { RADIANS, DEGREES };

		public static double toRadians(double angle) {
			return angle / 180.0 * Math.PI;
		}

		public static double toDegrees(double rads) {
			return rads / Math.PI * 180.0;
		}

		public static bool getCrossPoint(Ray3 ray, Plane plane, out Vector3 result) { 
			return getCrossPoint(ray, plane.normal, plane.d, out result);
		}

		
		public static bool getCrossPoint(Ray3 ray, Vector3 normal, double d, out Vector3 result) {
			//Перпендикулярность вектора. Вероятность пересечения крайне мала
			if(ray.Dir.x * normal.x + ray.Dir.y * normal.y + ray.Dir.z * normal.z == 0) {
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
				double x = -(normal.y * shiftYZ.y + normal.z * shiftYZ.z + d) / (normal.x + normal.y * ray.Dir.y / ray.Dir.x + normal.z * ray.Dir.z / ray.Dir.x);
				if(ray.isXOnRay(x))
					return ray.getPointWithX(x, out result);
			} else if(ray.IsAxisYDirected) {
				Vector3 shiftXZ = ray.ShiftXZ;
				double y = -(normal.x * shiftXZ.x + normal.z * shiftXZ.z + d) / (normal.y + normal.x * ray.Dir.x / ray.Dir.y + normal.z * ray.Dir.z / ray.Dir.y);
				if(ray.isYOnRay(y))
					return ray.getPointWithY(y, out result);
			} else if(ray.IsAxisZDirected) {
				Vector3 shiftXY = ray.ShiftXY;
				double z = -(normal.y * shiftXY.y + normal.x * shiftXY.x + d) / (normal.z + normal.y * ray.Dir.y / ray.Dir.z + normal.x * ray.Dir.x / ray.Dir.z);
				if(ray.isZOnRay(z))
					return ray.getPointWithZ(z, out result);
			}

			result = default;
			return false;
		}
		public static bool getXPlaneCrossPoint(Ray3 ray, double planeX, Vector3 crossPoint, out Vector3 result) {
			return ray.getPointWithX(planeX, out result);
		}

		public static bool getYPlaneCrossPoint(Ray3 ray, double planeY, Vector3 crossPoint, out Vector3 result) {
			return ray.getPointWithY(planeY, out result);
		}

		public static bool getZPlaneCrossPoint(Ray3 ray, double planeZ, Vector3 crossPoint, out Vector3 result) {
			return ray.getPointWithZ(planeZ, out result);
		}
	}
}
