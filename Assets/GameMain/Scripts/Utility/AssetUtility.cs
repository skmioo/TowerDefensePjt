﻿using GameFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDF
{
	public static class AssetUtility
	{
		public static string GetConfigAsset(string assetName, bool fromBytes = false)
		{
			return Utility.Text.Format("Assets/GameMain/Configs/{0}.{1}", assetName, fromBytes ? "bytes" : "txt");
		}

		public static string GetDataTableAsset(string assetName, bool fromBytes = true)
		{
			return Utility.Text.Format("Assets/GameMain/DataTables/{0}.{1}", assetName, fromBytes ? "bytes" : "txt");
		}

		public static string GetDictionaryAsset(string assetName, bool fromBytes = false)
		{
			return Utility.Text.Format("Assets/GameMain/Localization/{0}/Dictionaries/{1}.{2}", GameEntry.Localization.Language.ToString(), assetName, fromBytes ? "bytes" : "json");
		}

		public static string GetFontAsset(string assetName)
		{
			return Utility.Text.Format("Assets/GameMain/Fonts/{0}.ttf", assetName);
		}

		public static string GetSceneAsset(string assetName)
		{
			return Utility.Text.Format("Assets/GameMain/Scenes/{0}/{1}.unity", assetName, assetName);
		}

		public static string GetMusicAsset(string assetName)
		{
			return Utility.Text.Format("Assets/GameMain/Music/{0}.mp3", assetName);
		}

		public static string GetSoundAsset(string assetName)
		{
			return Utility.Text.Format("Assets/GameMain/Sounds/{0}.wav", assetName);
		}

		public static string GetEntityAsset(string assetName)
		{
			return Utility.Text.Format("Assets/GameMain/Entities/{0}.prefab", assetName);
		}

		public static string GetUIFormAsset(string assetName)
		{
			return Utility.Text.Format("Assets/GameMain/UI/UIForms/{0}.prefab", assetName);
		}

		public static string GetUISoundAsset(string assetName)
		{
			return Utility.Text.Format("Assets/GameMain/UI/UISounds/{0}.wav", assetName);
		}
	}
}
