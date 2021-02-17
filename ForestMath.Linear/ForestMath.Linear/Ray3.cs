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

		public Ray2 XYProj { get => new Ray2(StartPoint.XY, Dir.XY, IsLine); }
		public Ray2 YZProj { get => new Ray2(StartPoint.YZ, Dir.YZ, IsLine); }
		public Ray2 ZXProj { get => new Ray2(StartPoint.ZX, Dir.ZX, IsLine); }


		public bool IsAxisXDirected { get { return Dir.X != 0.0; } }
		public bool IsAxisYDirected { get { return Dir.Y != 0.0; } }
		public bool IsAxisZDirected { get { return Dir.Z != 0.0; } }

		public Vector3 ShiftYZ { get { return IsAxisXDirected ? StartPoint - Dir * (StartPoint.X / Dir.X) : throw new Exception($"The ray {this} isn't directed by axis x"); } }
		public Vector3 ShiftXZ { get { return IsAxisYDirected ? StartPoint - Dir * (StartPoint.Y / Dir.Y) : throw new Exception($"The ray {this} isn't directed by axis y"); } }
		public Vector3 ShiftXY { get { return IsAxisZDirected ? StartPoint - Dir * (StartPoint.Z / Dir.Z) : throw new Exception($"The ray {this} isn't directed by axis z"); } }

		public Ray3(Vector3 point, Vector3 dir, bool isLine = false) { 
			this.Dir = dir;
			this.StartPoint = point;
			this.IsLine = isLine;
		}

		public static Ray3 FromTwoPoints(Vector3 from, Vector3 to, bool isLine = false) {
			return new Ray3(from, to - from, isLine);
		}

		

		public bool GetPointWithX(double x, out Vector3 result) {
			if(Dir.X != 0.0) {
				result = Dir * (x / Dir.X) + ShiftYZ;
				return true;
			} else {
				result = default;
				return false;
			}
		}
		public bool GetPointWithY(double y, out Vector3 result) { 
			if(Dir.Y != 0.0) {
				result = Dir * (y / Dir.Y) + ShiftXZ;
				return true;
			} else {
				result = default;
				return false;
			}
		}
		public bool GetPointWithZ(double z, out Vector3 result) { 
			if(Dir.Z != 0.0) {
				result = Dir * (z / Dir.Z) + ShiftXY;
				return true;
			} else {
				result = default;
				return false;
			}
		}

		public override string ToString() {
			return $"{StartPoint} - {Dir}";
		}

		public bool IsXOnRay(double x) {
			return (x == StartPoint.X) || (IsAxisXDirected && (IsLine || (x > StartPoint.X ? Dir.X > 0 : Dir.X < 0)));
		}

		public bool IsYOnRay(double y) {
			return (y == StartPoint.Y) || (IsAxisYDirected && (IsLine || (y > StartPoint.Y ? Dir.Y > 0 : Dir.Y < 0)));
		}

		public bool IsZOnRay(double z) {
			return (z == StartPoint.Z) || (IsAxisZDirected && (IsLine || (z > StartPoint.Z ? Dir.Z > 0 : Dir.Z < 0)));
		}
	};
}
