using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDF
{
	public class IntervalParticleSystemPlayer : MonoBehaviour
	{
		public ParticleSystem ps;
		public float interval;
		public DateTime playTime;

		private void Start()
		{
			playTime = DateTime.Now.AddSeconds(interval);
		}

		private void Update()
		{
			if (ps != null && playTime <= DateTime.Now)
			{
				ps.Play();
				playTime = DateTime.Now.AddSeconds(interval);
			}
		}


	}
}
