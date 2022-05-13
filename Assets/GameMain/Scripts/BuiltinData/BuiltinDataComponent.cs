﻿using GameFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace TowerDF
{
	public class BuiltinDataComponent : GameFrameworkComponent
	{
		[SerializeField]
		private TextAsset m_BuildInfoTextAsset = null;

		[SerializeField]
		private TextAsset m_DefaultDictionaryTextAsset = null;

		[SerializeField]
		private UIUpdateResourceForm m_UIUpdateResourceForm = null;

		private BuildInfo m_BuildInfo = null;

		public BuildInfo BuildInfo
		{
			get { return m_BuildInfo; }
		}


		public UIUpdateResourceForm UpdateResourceFormTemplate
		{
			get
			{
				return m_UIUpdateResourceForm;
			}
		}


		public void InitBuildInfo()
		{
			if (m_BuildInfoTextAsset == null || string.IsNullOrEmpty(m_BuildInfoTextAsset.text))
			{
				Log.Info("Build info can not be found or empty.");
				return;
			}

			m_BuildInfo = Utility.Json.ToObject<BuildInfo>(m_BuildInfoTextAsset.text);
			if (m_BuildInfo == null)
			{
				Log.Warning("Parse build info failure.");
				return;
			}
		}

		public void InitDefaultDictionary()
		{
			if (m_DefaultDictionaryTextAsset == null || string.IsNullOrEmpty(m_DefaultDictionaryTextAsset.text))
			{
				Log.Info("Default dictionary can not be found or empty.");
				return;
			}

			if (!GameEntry.Localization.ParseData(m_DefaultDictionaryTextAsset.text))
			{
				Log.Warning("Parse default dictionary failure.");
				return;
			}
		}
	}
}

