using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;
namespace TowerDF.Data
{
	public abstract class DataBase : GameFramework.Data.Data
	{
		private Dictionary<string, bool> m_LoadFlag = new Dictionary<string, bool>();
		/// <summary>
		/// 本地游戏数据注册事件管理
		/// </summary>
		private EventSubscriber eventSubscriber;

		public bool isPreloadLoadOk {
			get {

				foreach (bool v in m_LoadFlag.Values)
				{
					if (!v)
						return false;
				}
				return true;
			}
		}

		public override void Init()
		{
			OnInit();
		}

		public override void Preload()
		{
			GameEntry.Event.Subscribe(LoadConfigSuccessEventArgs.EventId, onLoadConfigSuccess);
			GameEntry.Event.Subscribe(LoadConfigFailureEventArgs.EventId, onLoadConfigFailure);
			GameEntry.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, onLoadDataTableSuccess);
			GameEntry.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, onLoadDataTableFailure);
			GameEntry.Event.Subscribe(LoadDictionarySuccessEventArgs.EventId, onLoadDictionarySuccess);
			GameEntry.Event.Subscribe(LoadDictionaryFailureEventArgs.EventId, onLoadDictionaryFailure);
			OnPreload();
		}

		/// <summary>
		/// 通过 isPreloadLoadOk 在所有数据加载完毕后再执行Load
		/// </summary>
		public override void Load()
		{
			GameEntry.Event.Unsubscribe(LoadConfigSuccessEventArgs.EventId, onLoadConfigSuccess);
			GameEntry.Event.Unsubscribe(LoadConfigFailureEventArgs.EventId, onLoadConfigFailure);
			GameEntry.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, onLoadDataTableSuccess);
			GameEntry.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, onLoadDataTableFailure);
			GameEntry.Event.Unsubscribe(LoadDictionarySuccessEventArgs.EventId, onLoadDictionarySuccess);
			GameEntry.Event.Unsubscribe(LoadDictionaryFailureEventArgs.EventId, onLoadDictionaryFailure);
			OnLoad();
		}

		public override void Unload()
		{
			if (eventSubscriber != null)
			{
				eventSubscriber.UnSubscribeAll();
				ReferencePool.Release(eventSubscriber);
				eventSubscriber = null;
			}

			OnUnload();
		}

		public override void Shutdown()
		{
			OnShutdown();
		}

		protected void Subscribe(int id, EventHandler<GameEventArgs> handler)
		{
			if (eventSubscriber == null)
				eventSubscriber = EventSubscriber.Create(this);

			eventSubscriber.Subscribe(id, handler);
		}

		protected void UnSubscribe(int id, EventHandler<GameEventArgs> handler)
		{
			if (eventSubscriber != null)
				eventSubscriber.UnSubscribe(id, handler);
		}

		protected void LoadConfig(string configName)
		{
			string configAssetName = AssetUtility.GetConfigAsset(configName, false);
			m_LoadFlag.Add(configAssetName, false);
			GameEntry.Config.ReadData(configAssetName, this);
		}

		protected void LoadDataTable(string dataTableName)
		{
			string dataTableAssetName = AssetUtility.GetDataTableAsset(dataTableName, true);
			m_LoadFlag.Add(dataTableAssetName, false);
			GameEntry.DataTable.LoadDataTable(dataTableName, dataTableAssetName, this);
		}

		protected void LoadDictionary(string dictionaryName)
		{
			string dictionaryAssetName = AssetUtility.GetDictionaryAsset(dictionaryName, false);
			m_LoadFlag.Add(dictionaryAssetName, false);
			GameEntry.Localization.ReadData(dictionaryAssetName, this);
		}

		private void onLoadConfigSuccess(object sender, GameEventArgs e)
		{
			LoadConfigSuccessEventArgs ne = (LoadConfigSuccessEventArgs)e;
			if (ne.UserData != this)
				return;
			m_LoadFlag[ne.ConfigAssetName] = true;
		}

		private void onLoadConfigFailure(object sender, GameEventArgs e)
		{
			LoadConfigFailureEventArgs ne = (LoadConfigFailureEventArgs)e;
			if (ne.UserData != this)
			{
				return;
			}

			Log.Error("Can not load config '{0}' from '{1}' with error message '{2}'.", ne.ConfigAssetName, ne.ConfigAssetName, ne.ErrorMessage);
		}


		private void onLoadDataTableSuccess(object sender, GameEventArgs e)
		{
			LoadDataTableSuccessEventArgs ne = (LoadDataTableSuccessEventArgs)e;
			if (ne.UserData != this)
			{
				return;
			}

			m_LoadFlag[ne.DataTableAssetName] = true;
			Log.Info("Load data table '{0}' OK.", ne.DataTableAssetName);
		}

		private void onLoadDataTableFailure(object sender, GameEventArgs e)
		{
			LoadDataTableFailureEventArgs ne = (LoadDataTableFailureEventArgs)e;
			if (ne.UserData != this)
			{
				return;
			}

			Log.Error("Can not load data table '{0}' from '{1}' with error message '{2}'.", ne.DataTableAssetName, ne.DataTableAssetName, ne.ErrorMessage);
		}

		private void onLoadDictionarySuccess(object sender, GameEventArgs e)
		{
			LoadDictionarySuccessEventArgs ne = (LoadDictionarySuccessEventArgs)e;
			if (ne.UserData != this)
			{
				return;
			}

			m_LoadFlag[ne.DictionaryAssetName] = true;
			Log.Info("Load dictionary '{0}' OK.", ne.DictionaryAssetName);
		}

		private void onLoadDictionaryFailure(object sender, GameEventArgs e)
		{
			LoadDictionaryFailureEventArgs ne = (LoadDictionaryFailureEventArgs)e;
			if (ne.UserData != this)
			{
				return;
			}

			Log.Error("Can not load dictionary '{0}' from '{1}' with error message '{2}'.", ne.DictionaryAssetName, ne.DictionaryAssetName, ne.ErrorMessage);
		}



		protected virtual void OnInit() { }
		protected virtual void OnPreload() { }
		protected virtual void OnLoad() { }
		protected virtual void OnUnload() { }
		protected virtual void OnShutdown() { }

		

	}
}
