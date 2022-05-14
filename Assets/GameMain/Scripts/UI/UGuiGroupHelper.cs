using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace TowerDF
{
	public class UGuiGroupHelper : UIGroupHelperBase
	{
		public const int DepthFactor = 100;

		private int m_Depth = 0;
		private Canvas m_CachedCanvas = null;

		public override void SetDepth(int depth)
		{
			m_Depth = depth;
			m_CachedCanvas.overrideSorting = true;
			m_CachedCanvas.sortingOrder = DepthFactor * depth;
		}

		private void Awake()
		{
			m_CachedCanvas = gameObject.GetOrAddComponent<Canvas>();
			gameObject.GetOrAddComponent<GraphicRaycaster>();
		}

		private void Start()
		{
			m_CachedCanvas.overrideSorting = true;
			m_CachedCanvas.sortingOrder = DepthFactor * m_Depth;

			RectTransform transform = GetComponent<RectTransform>();
			transform.anchorMin = Vector2.zero;
			transform.anchorMax = Vector2.one;
			transform.anchoredPosition = Vector2.zero;
			transform.sizeDelta = Vector2.zero;
		}

	}
}
