using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDF.Data
{
	public class SceneData
	{
		public string AssetPath { get; internal set; }
	}

	public sealed class DataScene : DataBase
	{

		protected override void onPreload()
		{
			base.onPreload();
			LoadDataTable("Scene");
		}

		protected override void onLoad()
		{
			base.onLoad();
			//GameEntry.DataTable.GetDataTable();
		}

		internal SceneData GetSceneData(int loadingSceneId)
		{
			return null;
		}
	}

}