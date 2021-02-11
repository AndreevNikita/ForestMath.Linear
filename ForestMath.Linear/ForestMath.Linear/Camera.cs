using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestMath.Linear {
	//Направление OpenGL по умолчанию = (0, 0, 1)
	//Направление камеры по умолчанию = (1, 0, 0)
	public class Camera { 
		public Vector3 Pos;
		public Vector3 YPR = new Vector3();
		public Vector3 Direction {
			get { return GetDirection(); }
			set { SetDirection(value); }
		}
		
		public Camera() : this(new Vector3(1.0, 0.0, 0.0), new Vector3(0.0, 0.0, 0.0)) {
			
		}

		public Camera(Vector3 pos, Vector3 ypr) {
			set(pos, ypr);
		}

		public void set(Vector3 pos, Vector3 ypr) {
			this.Pos = pos;
			this.YPR = ypr;
		}

		//Roll - вокруг X
		//Pitch - вокруг Z
		//Yaw - вокруг Y

		public Matrix4 GetMatrix() { 
			Matrix4 result = Matrix4.Identity;

			//Камера OpenGL по умолчанию направлена в (0, 0, 1)
			//=> сначала поворачиваем все объекты дополнительно на 90 градусов -(ypr.Yaw + Math.PI / 2.0).
			//Просто поворот для камеры будет -Yaw. Но нам нужно развернуть всё так, чтобы камера по умолчанию смотрела на (1, 0, 0). 
			//Поэтому добавляем 90 градусов. Т.е. камера развёрнута на Yaw + 90 => Объекты развёрнуты относительно неё на -(Yaw - 90)

			//После разворота к оси z работаем уже как с ней. Т.е. pitch - поворот точки (0, 0, 1) вокруг оси x.
			//По умолчанию направление камеры вращается вниз ((0, 0, 1) вокруг oX).
			//Т.е. направление вращается на -Pitch
			//А все объекты относительно него на -(-Pitch), т.е. Pitch, поэтому
			//result *= Polygon.Matrix4.rotationX(ypr.Pitch, Geometry.AngleMeasure.RADIANS);


			result *= Matrix4.RotationZ(-YPR.Roll, Geometry.AngleMeasure.RADIANS);
			result *= Matrix4.RotationX(YPR.Pitch, Geometry.AngleMeasure.RADIANS);
			result *= Matrix4.RotationY(-(YPR.Yaw + Math.PI / 2.0), Geometry.AngleMeasure.RADIANS);
			result *= Matrix4.Translation(-Pos.X, -Pos.Y, -Pos.Z);
			


			return result;
		}

		//Тут ничего не трогаем, камера по умолчанию всегда смотрит в (1, 0, 0). Все повороты работают с этим вектором
		public Vector3 GetDirection() {
			return Matrix4.RotationY(YPR.Yaw) * Matrix4.RotationZ(YPR.Pitch) * Matrix4.RotationZ(YPR.Roll) * new Vector4(1.0, 0.0, 0.0, 1.0);
		}

		public void SetDirection(double x, double y, double z) {
			SetDirection(new Vector3(x, y, z));
		}

		public void SetDirection(Vector3 dir) {
			YPR.Yaw = dir.X != 0 && dir.Z != 0 ? new Vector2(dir.X, -dir.Z).GetAngle(false) : 0;
			//Console.WriteLine("Set direction: {0}", dir);
			//Console.WriteLine(dir.y / dir.Length);
			YPR.Pitch = Math.Asin(dir.Y / dir.Length);
		}

	}
}
