using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDF
{
	public class UIUpdateResourceForm : MonoBehaviour
	{
		[SerializeField]
		private Text m_DescriptionText = null;
		[SerializeField]
		private Slider m_ProgressSlider = null;

		public void SetProgress(float progress, string description)
		{
			m_ProgressSlider.value = progress;
			m_DescriptionText.text = description;
		}
	}
}
