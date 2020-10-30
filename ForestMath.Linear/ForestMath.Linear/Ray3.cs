using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestMath.Linear {
	public struct Ray3 {
		public Vector3 Dir;
		public Vector3 StartPoint;
		public bool IsLine;

		public bool IsAxisXDirected { get { return Dir.x != 0.0; } }
		public bool IsAxisYDirected { get { return Dir.y != 0.0; } }
		public bool IsAxisZDirected { get { return Dir.z != 0.0; } }

		public Vector3 ShiftYZ { get { return IsAxisXDirected ? StartPoint - Dir * (StartPoint.x / Dir.x) : throw new Exception($"The ray {this} isn't directed by axis x"); } }
		public Vector3 ShiftXZ { get { return IsAxisYDirected ? StartPoint - Dir * (StartPoint.y / Dir.y) : throw new Exception($"The ray {this} isn't directed by axis y"); } }
		public Vector3 ShiftXY { get { return IsAxisZDirected ? StartPoint - Dir * (StartPoint.z / Dir.z) : throw new Exception($"The ray {this} isn't directed by axis z"); } }

		public Ray3(Vector3 point, Vector3 dir, bool isLine = false) { 
			this.Dir = dir;
			this.StartPoint = point;
			this.IsLine = isLine;
		}

		public static Ray3 fromTwoPoints(Vector3 from, Vector3 to, bool isLine = false) {
			return new Ray3(from, to - from, isLine);
		}

		

		public bool getPointWithX(double x, out Vector3 result) {
			if(Dir.x != 0.0) {
				result = (x / Dir.x) * Dir + ShiftYZ;
				return true;
			} else {
				result = default;
				return false;
			}
		}
		public bool getPointWithY(double y, out Vector3 result) { 
			if(Dir.y != 0.0) {
				result = Dir * (y / Dir.y) + ShiftXZ;
				return true;
			} else {
				result = default;
				return false;
			}
		}
		public bool getPointWithZ(double z, out Vector3 result) { 
			if(Dir.z != 0.0) {
				result = Dir * (z / Dir.z) + ShiftXY;
				return true;
			} else {
				result = default;
				return false;
			}
		}

		public override string ToString() {
			return $"({StartPoint}) - ({Dir})";
		}

		public bool isXOnRay(double x) {
			return (x == StartPoint.x) || (IsAxisXDirected && (IsLine || (x > StartPoint.x ? Dir.x > 0 : Dir.x < 0)));
		}

		public bool isYOnRay(double y) {
			return (y == StartPoint.y) || (IsAxisYDirected && (IsLine || (y > StartPoint.y ? Dir.y > 0 : Dir.y < 0)));
		}

		public bool isZOnRay(double z) {
			return (z == StartPoint.z) || (IsAxisZDirected && (IsLine || (z > StartPoint.z ? Dir.z > 0 : Dir.z < 0)));
		}
	};
}
