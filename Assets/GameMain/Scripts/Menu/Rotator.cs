using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDF
{
	public class Rotator : MonoBehaviour
	{
		public Vector3 rotateSpeed;
		private void Update()
		{
			transform.localEulerAngles += rotateSpeed;
		}
	}
}
