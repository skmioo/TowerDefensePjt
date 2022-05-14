using GameFramework;
using GameFramework.Event;
using GameFramework.Procedure;
using System.Collections.Generic;
using TowerDF.Data;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace TowerDF
{
	/// <summary>
	/// 
	/// </summary>
	public class ProcedurePreload : ProcedureBase
	{
		private DataBase[] datas;

		private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();
		protected override void OnInit(ProcedureOwner procedureOwner)
		{
			base.OnInit(procedureOwner);
		}

		protected override void OnEnter(ProcedureOwner procedureOwner)
		{
			base.OnEnter(procedureOwner);
			GameEntry.Event.Subscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
			GameEntry.Event.Subscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
			GameEntry.Event.Subscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
			GameEntry.Event.Subscribe(LoadDictionaryFailureEventArgs.EventId, OnLoadDictionaryFailure);

			GameFramework.Data.Data[] _datas = GameEntry.Data.GetAllData();
			datas = new DataBase[_datas.Length];
			for (int i = 0; i < _datas.Length; i++)
			{
				if (_datas[i] is DataBase)
				{
					datas[i] = _datas[i] as DataBase;
				}
				else
				{
					throw new System.Exception(string.Format("Data {0} is not derive form DataBase", _datas[i].GetType()));
				}
			}
			PreloadResources();
		}


		private void PreloadResources()
		{
			// Preload configs
			//ConfigComponent 通过 DefaultConfigHelper进行数据解析，最终数据解析到了ConfigManager中
			LoadConfig("DefaultConfig");

			// Preload dictionaries
			//LocalizationComponent 通过 JsonLocallizationHelper进行数据解析，最终数据解析到了LocalizationManager中
			LoadDictionary("Default");
			//所有继承Data的，此处为DataBase : GameFramework.Data.Data 执行它们的Preload函数
			GameEntry.Data.PreLoadAllData();
		}

		
		private void LoadConfig(string name)
		{
			string configAssetName = AssetUtility.GetConfigAsset(name, false);
			m_LoadedFlag.Add(configAssetName, false);
			//ConfigComponent 通过 DefaultConfigHelper进行数据解析，最终数据解析到了ConfigManager中
			GameEntry.Config.ReadData(configAssetName,this);
		}

		private void LoadDictionary(string name)
		{
			string dicName = AssetUtility.GetDictionaryAsset(name, false);
			m_LoadedFlag.Add(dicName, false);
			//LocalizationComponent 通过 JsonLocallizationHelper进行数据解析，最终数据解析到了LocalizationManager中
			GameEntry.Localization.ReadData(dicName, this);
		}


		protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
		{
			base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
			foreach (bool v in m_LoadedFlag.Values)
			{
				if (!v)
					return;
			}
			if (datas == null)
				return;
			foreach (DataBase data in datas)
			{
				if (!data.isPreloadLoadOk)
					return;
			}
			SetComponents();
			procedureOwner.SetData<VarInt32>(Constant.ProcedureData.NextSceneId, GameEntry.Config.GetInt("Scene.Menu"));
			ChangeState<ProcedureLoadingScene>(procedureOwner);
		}

		private void SetComponents()
		{
			SetDataComponent();
			SetUIComponent();
			//SetEntityComponent();
			SetItemComponent();
			SetSoundComponent();
		}


		private void SetDataComponent()
		{
			GameEntry.Data.LoadAllData();
		}

		private void SetUIComponent()
		{
			UIGroupData[] uiGroupDatas = GameEntry.Data.GetData<DataUI>().GetAllUIGroupData();
			foreach (var item in uiGroupDatas)
			{
				GameEntry.UI.AddUIGroup(item.Name, item.Depth);
				GameEntry.Sound.SetVolume(item.Name, GameEntry.Setting.GetFloat(Utility.Text.Format(Constant.Setting.SoundGroupVolume, item.Name), 1));
			}
		}

		private void SetItemComponent()
		{
		
		}

		private void SetSoundComponent()
		{
			SoundGroupData[]  soundGroupDatas = GameEntry.Data.GetData<DataSound>().GetAllSoundGroupData();
			foreach (var data in soundGroupDatas)
			{
				GameEntry.Sound.AddSoundGroup(data.Name, data.AvoidBeingReplacedBySamePriority, data.Mute,data.Volume, data.SoundAgentCount);
				GameEntry.Sound.SetVolume(data.Name, GameEntry.Setting.GetFloat(Utility.Text.Format(Constant.Setting.SoundGroupVolume, data.Name), 1));
			}
		}

		protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
		{
			base.OnLeave(procedureOwner, isShutdown);

			GameEntry.Event.Unsubscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
			GameEntry.Event.Unsubscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
			GameEntry.Event.Unsubscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
			GameEntry.Event.Unsubscribe(LoadDictionaryFailureEventArgs.EventId, OnLoadDictionaryFailure);
		}

		protected override void OnDestroy(ProcedureOwner procedureOwner)
		{
			base.OnDestroy(procedureOwner);
		}

		private void OnLoadConfigSuccess(object sender, GameEventArgs e)
		{
			LoadConfigSuccessEventArgs ne = (LoadConfigSuccessEventArgs)e;
			if (ne.UserData != this)
			{
				return;
			}

			m_LoadedFlag[ne.ConfigAssetName] = true;
			Log.Info("Load config '{0}' OK.", ne.ConfigAssetName);
		}

		private void OnLoadConfigFailure(object sender, GameEventArgs e)
		{
			LoadConfigFailureEventArgs ne = (LoadConfigFailureEventArgs)e;
			if (ne.UserData != this)
			{
				return;
			}

			Log.Error("Can not load config '{0}' from '{1}' with error message '{2}'.", ne.ConfigAssetName, ne.ConfigAssetName, ne.ErrorMessage);
		}

		private void OnLoadDictionarySuccess(object sender, GameEventArgs e)
		{
			LoadDictionarySuccessEventArgs ne = (LoadDictionarySuccessEventArgs)e;
			if (ne.UserData != this)
			{
				return;
			}

			m_LoadedFlag[ne.DictionaryAssetName] = true;
			Log.Info("Load dictionary '{0}' OK.", ne.DictionaryAssetName);
		}

		private void OnLoadDictionaryFailure(object sender, GameEventArgs e)
		{
			LoadDictionaryFailureEventArgs ne = (LoadDictionaryFailureEventArgs)e;
			if (ne.UserData != this)
			{
				return;
			}

			Log.Error("Can not load dictionary '{0}' from '{1}' with error message '{2}'.", ne.DictionaryAssetName, ne.DictionaryAssetName, ne.ErrorMessage);
		}

	}
}
