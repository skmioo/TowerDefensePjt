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
		internal SceneData GetSceneData(int loadingSceneId)
		{
			
		}
	}

}