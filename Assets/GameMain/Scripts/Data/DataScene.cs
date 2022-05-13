using GameFramework.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDF.Data
{
	public class SceneData
	{
		private DRScene dRScene;
		private DRAssetsPath dRAssetsPath;

		public int Id
		{
			get
			{
				return dRScene.Id;
			}
		}

		public string AssetPath
		{
			get
			{
				return dRAssetsPath.AssetPath;
			}
		}

		public string Procedure
		{
			get
			{
				return dRScene.Procedure;
			}
		}

		public SceneData(DRScene dRScene, DRAssetsPath dRAssetsPath)
		{
			this.dRScene = dRScene;
			this.dRAssetsPath = dRAssetsPath;
		}
	}

	public sealed class DataScene : DataBase
	{
		IDataTable<DRScene> dtScene;
		private Dictionary<int, SceneData> dicSceneData;
		protected override void OnPreload()
		{
			base.OnPreload();
			LoadDataTable("Scene");
		}

		protected override void OnLoad()
		{
			base.OnLoad();
			dtScene = GameEntry.DataTable.GetDataTable<DRScene>();
			if(dtScene == null)
				throw new System.Exception("Can not get data table Scene");
			dicSceneData = new Dictionary<int, SceneData>();
			DRScene[] dRScenes = dtScene.GetAllDataRows();
			foreach (DRScene scene in dRScenes)
			{
				DRAssetsPath dRAssetsPath = GameEntry.Data.GetData<DataAssetsPath>().GetDRAssetsPathByAssetsId(scene.AssetId);
				SceneData sceneData = new SceneData(scene, dRAssetsPath);
				dicSceneData.Add(sceneData.Id, sceneData);
			}
		}

		internal SceneData GetSceneData(int id)
		{
			if (dicSceneData.ContainsKey(id))
			{
				return dicSceneData[id];
			}
			return null;
		}
	}

}