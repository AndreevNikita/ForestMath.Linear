using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestMath.Linear {
	public struct Ray2 {
		public Vector2 Dir;
		public Vector2 StartPoint;
		public bool IsLine;

		public bool IsAxisXDirected { get { return Dir.X != 0.0; } }
		public bool IsAxisYDirected { get { return Dir.Y != 0.0; } }

		public Vector2 ShiftY { get { return IsAxisXDirected ? StartPoint - Dir * (StartPoint.X / Dir.X) : throw new Exception($"The ray {this} isn't directed by axis x"); } }
		public Vector2 ShiftX { get { return IsAxisYDirected ? StartPoint - Dir * (StartPoint.Y / Dir.Y) : throw new Exception($"The ray {this} isn't directed by axis y"); } }

		public Ray2(Vector2 point, Vector2 dir, bool isLine = false) { 
			this.Dir = dir;
			this.StartPoint = point;
			this.IsLine = isLine;
		}

		public static Ray2 FromTwoPoints(Vector2 from, Vector2 to, bool isLine = false) {
			return new Ray2(from, to - from, isLine);
		}

		
		public bool GetPointWithX(double x, out Vector3 result) {
			if(Dir.X != 0.0) {
				result = Dir * (x / Dir.X) + ShiftY;
				return true;
			} else {
				result = default;
				return false;
			}
		}
		public bool GetPointWithY(double y, out Vector3 result) { 
			if(Dir.Y != 0.0) {
				result = Dir * (y / Dir.Y) + ShiftX;
				return true;
			} else {
				result = default;
				return false;
			}
		}

		public override string ToString() {
			return $"({StartPoint}) - ({Dir})";
		}

		public bool IsXOnRay(double x) {
			return (x == StartPoint.X) || (IsAxisXDirected && (IsLine || (x > StartPoint.X ? Dir.X > 0 : Dir.X < 0)));
		}

		public bool IsYOnRay(double y) {
			return (y == StartPoint.Y) || (IsAxisYDirected && (IsLine || (y > StartPoint.Y ? Dir.Y > 0 : Dir.Y < 0)));
		}
	};
}
