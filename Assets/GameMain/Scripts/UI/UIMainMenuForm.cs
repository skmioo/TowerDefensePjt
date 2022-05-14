using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace TowerDF
{
	public class UIMainMenuForm : UGuiForm
	{

		public Button levelSelectButton;
		public Button optionButton;
		public Button quitButton;

		protected override void OnInit(object userData)
		{
			base.OnInit(userData);

			levelSelectButton.onClick.AddListener(OnLevelSelectButtonClick);
			optionButton.onClick.AddListener(OnOptionButtonClick);
			quitButton.onClick.AddListener(OnQuitButtonClick);
		}


		private void OnLevelSelectButtonClick()
		{
			GameEntry.Sound.PlaySound(EnumSound.ui_sound_forward);
			GameEntry.UI.OpenUIForm(EnumUIForm.UILevelSelectForm);
		}

		private void OnOptionButtonClick()
		{
			GameEntry.Sound.PlaySound(EnumSound.ui_sound_forward);
			GameEntry.UI.OpenUIForm(EnumUIForm.UIOptionsForm);
		}

		private void OnQuitButtonClick()
		{
			UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit);
		}

	}
}
