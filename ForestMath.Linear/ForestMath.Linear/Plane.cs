﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestMath.Linear {
	public class Plane {
		public Vector3 Normal;
		public double D = 0.0;

		public enum ParameterType { STANDART_D, NORMAL_SHIFT };

		public Plane(Vector3 normal, double parameter = 0.0, ParameterType parameterType = ParameterType.STANDART_D) {
			this.Normal = normal;
			switch (parameterType) {
			case ParameterType.STANDART_D:
				D = parameter;
				break;
			case ParameterType.NORMAL_SHIFT:
				SetNormalShift(parameter);
				break;
			}
		}

		public static Plane FromPoints(Vector3 point1, Vector3 point2, Vector3 point3) { 
			Vector3 vec1 = point2 - point1;
			Vector3 vec2 = point3 - point1;
			Vector3 normal = Geometry.CrossProduct(vec1, vec2);
			return new Plane(normal, -normal * point1, ParameterType.STANDART_D);
		}

		public void SetNormalShift(double normalLength) {
			D = -Normal.SqrLength() * normalLength;
		}

		public override string ToString() {
			return $"(N = {Normal}; D = {D})";
		}
	};
}
