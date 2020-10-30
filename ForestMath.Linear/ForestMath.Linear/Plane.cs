using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestMath.Linear {
	public class Plane {
		public Vector3 normal;
		public double d = 0.0;

		public enum ParameterType { STANDART_D, NORMAL_SHIFT };

		public Plane(Vector3 normal, double parameter = 0.0, ParameterType parameterType = ParameterType.STANDART_D) {
			this.normal = normal;
			switch (parameterType) {
			case ParameterType.STANDART_D:
				d = parameter;
				break;
			case ParameterType.NORMAL_SHIFT:
				setNormalShift(parameter);
				break;
			}
		}

		public void setNormalShift(double normalLength) {
			d = -normal.sqrLength() * normalLength;
		}
	};
}
