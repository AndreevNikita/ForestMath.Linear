using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestMath.Linear {
	//Направление OpenGL по умолчанию = (0, 0, 1)
	//Направление камеры по умолчанию = (1, 0, 0)
	public class Camera { 
		public Vector3 pos;
		public Vector3 ypr = new Vector3();
		public Vector3 Direction {
			get { return getDirection(); }
			set { setDirection(value); }
		}
		
		public Camera() : this(new Vector3(1.0, 0.0, 0.0), new Vector3(0.0, 0.0, 0.0)) {
			
		}

		public Camera(Vector3 pos, Vector3 ypr) {
			set(pos, ypr);
		}

		public void set(Vector3 pos, Vector3 ypr) {
			this.pos = pos;
			this.ypr = ypr;
		}

		//Roll - вокруг X
		//Pitch - вокруг Z
		//Yaw - вокруг Y

		public Matrix4 getMatrix() { 
			Matrix4 result = Matrix4.identity();

			//Камера OpenGL по умолчанию направлена в (0, 0, 1)
			//=> сначала поворачиваем все объекты дополнительно на 90 градусов -(ypr.Yaw + Math.PI / 2.0).
			//Просто поворот для камеры будет -Yaw. Но нам нужно развернуть всё так, чтобы камера по умолчанию смотрела на (1, 0, 0). 
			//Поэтому добавляем 90 градусов. Т.е. камера развёрнута на Yaw + 90 => Объекты развёрнуты относительно неё на -(Yaw - 90)

			//После разворота к оси z работаем уже как с ней. Т.е. pitch - поворот точки (0, 0, 1) вокруг оси x.
			//По умолчанию направление камеры вращается вниз ((0, 0, 1) вокруг oX).
			//Т.е. направление вращается на -Pitch
			//А все объекты относительно него на -(-Pitch), т.е. Pitch, поэтому
			//result *= Polygon.Matrix4.rotationX(ypr.Pitch, Geometry.AngleMeasure.RADIANS);


			result *= Matrix4.rotationZ(-ypr.Roll, Geometry.AngleMeasure.RADIANS);
			result *= Matrix4.rotationX(ypr.Pitch, Geometry.AngleMeasure.RADIANS);
			result *= Matrix4.rotationY(-(ypr.Yaw + Math.PI / 2.0), Geometry.AngleMeasure.RADIANS);
			result *= Matrix4.translation(-pos.x, -pos.y, -pos.z);
			


			return result;
		}

		//Тут ничего не трогаем, камера по умолчанию всегда смотрит в (1, 0, 0). Все повороты работают с этим вектором
		public Vector3 getDirection() {
			return Matrix4.rotationY(ypr.Yaw) * Matrix4.rotationZ(ypr.Pitch) * Matrix4.rotationZ(ypr.Roll) * new Vector4(1.0, 0.0, 0.0, 1.0);
		}

		public void setDirection(double x, double y, double z) {
			setDirection(new Vector3(x, y, z));
		}

		public void setDirection(Vector3 dir) {
			ypr.Yaw = dir.x != 0 && dir.z != 0 ? new Vector2(dir.x, -dir.z).getAngle(false) : 0;
			//Console.WriteLine("Set direction: {0}", dir);
			//Console.WriteLine(dir.y / dir.Length);
			ypr.Pitch = Math.Asin(dir.y / dir.Length);
		}

	}
}
