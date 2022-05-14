using GameFramework.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDF.Data
{
	public sealed class UIData
	{
		private DRUIForm dRUIForm;
		private DRAssetsPath dRAssetsPath;
		private UIGroupData uiGroupData;

		public int Id
		{
			get
			{
				return dRUIForm.Id;
			}
		}

		public UIGroupData UIGroupData
		{
			get
			{
				return uiGroupData;
			}
		}

		public string AssetPath
		{
			get
			{
				return dRAssetsPath.AssetPath;
			}
		}

		public bool AllowMultiInstance
		{
			get
			{
				return dRUIForm.AllowMultiInstance;
			}
		}

		public bool PauseCoveredUIForm
		{
			get
			{
				return dRUIForm.PauseCoveredUIForm;
			}
		}

		public UIData(DRUIForm dRUIForm, DRAssetsPath dRAssetsPath, UIGroupData uiGroupData)
		{
			this.dRUIForm = dRUIForm;
			this.dRAssetsPath = dRAssetsPath;
			this.uiGroupData = uiGroupData;
		}

	}

	public sealed class UIGroupData
	{
		private DRUIGroup dRUIGroup;

		public int Id
		{
			get
			{
				return dRUIGroup.Id;
			}
		}

		public string Name
		{
			get
			{
				return dRUIGroup.Name;
			}
		}

		public int Depth
		{
			get
			{
				return dRUIGroup.Depth;
			}
		}

		public UIGroupData(DRUIGroup dRUIGroup)
		{
			this.dRUIGroup = dRUIGroup;
		}
	}


	public sealed class DataUI : DataBase
	{
		IDataTable<DRUIForm> dtUIForm;
		IDataTable<DRUIGroup> dtUIGroup;

		private Dictionary<int, UIData> dicUIData;
		private Dictionary<int, UIGroupData> dicUIGroupData;

		protected override void OnPreload()
		{
			base.OnPreload();
			LoadDataTable("UIForm");
			LoadDataTable("UIGroup");
		}


		protected override void OnLoad()
		{
			base.OnLoad();
			dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
			if (dtUIForm == null)
				throw new System.Exception("Can not get data table UIForm");

			dtUIGroup = GameEntry.DataTable.GetDataTable<DRUIGroup>();
			if (dtUIGroup == null)
				throw new System.Exception("Can not get data table UIGroup");
			dicUIData = new Dictionary<int, UIData>();
			dicUIGroupData = new Dictionary<int, UIGroupData>();
			DRUIForm[] dRUIForms = dtUIForm.GetAllDataRows();
			foreach (var drUIForm in dRUIForms)
			{
				UIGroupData uiGroupData = null;

				if (!dicUIGroupData.TryGetValue(drUIForm.UIGroupId, out uiGroupData))
				{
					DRUIGroup dRUIGroup = dtUIGroup.GetDataRow(drUIForm.UIGroupId);
					if (dRUIGroup == null)
					{
						throw new System.Exception("Can not find UIGroup id :" + drUIForm.UIGroupId);
					}

					uiGroupData = new UIGroupData(dRUIGroup);
					dicUIGroupData.Add(drUIForm.UIGroupId, uiGroupData);
				}


				DRAssetsPath dRAssetsPath = GameEntry.Data.GetData<DataAssetsPath>().GetDRAssetsPathByAssetsId(drUIForm.AssetId);

				UIData uIData = new UIData(drUIForm, dRAssetsPath, uiGroupData);
				dicUIData.Add(uIData.Id,uIData);
			}
		}

		public UIData GetUIData(int id)
		{
			if (dicUIData.ContainsKey(id))
			{
				return dicUIData[id];
			}

			return null;
		}

		public UIGroupData GetUIGroupData(int id)
		{
			if (dicUIGroupData.ContainsKey(id))
			{
				return dicUIGroupData[id];
			}

			return null;
		}

		public UIData[] GetAllUIData()
		{
			int index = 0;
			UIData[] results = new UIData[dicUIData.Count];
			foreach (var uiData
				in dicUIData.Values)
			{
				results[index++] = uiData;
			}

			return results;
		}

		public UIGroupData[] GetAllUIGroupData()
		{
			int index = 0;
			UIGroupData[] results = new UIGroupData[dicUIGroupData.Count];
			foreach (var uiGroupData in dicUIGroupData.Values)
			{
				results[index++] = uiGroupData;
			}

			return results;
		}

		protected override void OnUnload()
		{
			GameEntry.DataTable.DestroyDataTable<DRUIForm>();
			GameEntry.DataTable.DestroyDataTable<DRUIGroup>();

			dtUIForm = null;
			dtUIGroup = null;
			dicUIData = null;
			dicUIGroupData = null;
		}

		protected override void OnShutdown()
		{
		}
	}
}
