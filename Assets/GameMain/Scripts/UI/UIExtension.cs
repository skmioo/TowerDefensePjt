using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityGameFramework.Runtime;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using TowerDF.Data;

namespace TowerDF
{
	public static class UIExtension
	{
		public static void CloseUIForm(this UIComponent uiComponent, UGuiForm guiForm)
		{
			uiComponent.CloseUIForm(guiForm.UIForm);
		}

		public static IEnumerator FadeToAlpha(this CanvasGroup canvasGroup, float alpha, float duration)
		{
			float time = 0;
			float originalAlpha = canvasGroup.alpha;
			while (time < duration)
			{
				time += Time.deltaTime;
				canvasGroup.alpha = Mathf.Lerp(originalAlpha, alpha, time/ duration);
				yield return new WaitForEndOfFrame();
			}
			canvasGroup.alpha = alpha;
		}

		public static IEnumerator SmoothValue(this Slider slider, float value, float duration)
		{
			float time = 0;
			float originalValue = slider.value;
			while (time < duration)
			{
				time += Time.deltaTime;
				slider.value = Mathf.Lerp(originalValue, value, time / duration);
				yield return new WaitForEndOfFrame();
			}
			slider.value = value;

		}


		public static int? OpenUIForm(this UIComponent uiComponent, EnumUIForm uiFormId, object userData = null)
		{
			return uiComponent.OpenUIForm((int)uiFormId, userData);
		}

		public static int? OpenUIForm(this UIComponent uiComponent, int uiFormId, object userData = null)
		{
			UIData uiData = GameEntry.Data.GetData<DataUI>().GetUIData(uiFormId);
			if(uiData == null)
			{
				Log.Warning("Can not load UI form '{0}' from data table.", uiFormId.ToString());
				return null;
			}
			string assetName = uiData.AssetPath;
			if (!uiData.AllowMultiInstance)
			{
				if (uiComponent.IsLoadingUIForm(assetName))
					return null;
				if (uiComponent.HasUIForm(assetName))
					return null;
			}
			return uiComponent.OpenUIForm(assetName,uiData.UIGroupData.Name,Constant.AssetPriority.UIFormAsset,uiData.PauseCoveredUIForm, userData);
		}

	}
}
