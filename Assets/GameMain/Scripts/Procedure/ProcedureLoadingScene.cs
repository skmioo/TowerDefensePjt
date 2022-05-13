using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using System;
using TowerDF.Data;
using UnityGameFramework.Runtime;

namespace TowerDF
{
	public class ProcedureLoadingScene : ProcedureBase
	{
		private int loadingSceneId = -1;
		private bool loadSceneCompleted = false;
		private SceneData sceneData = null;

		protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
		{
			base.OnEnter(procedureOwner);
			loadSceneCompleted = false;
			loadingSceneId = -1;
			GameEntry.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, onLoadSceneSuccess);
			GameEntry.Event.Subscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
			GameEntry.Event.Subscribe(LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
			GameEntry.Event.Subscribe(LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);
			//卸载之前的场景
			string[] loadedSceneAssetName = GameEntry.Scene.GetLoadedSceneAssetNames();
			for (int i = 0; i < loadedSceneAssetName.Length; i++)
			{
				GameEntry.Scene.UnloadScene(loadedSceneAssetName[i]);
			}

			loadingSceneId = procedureOwner.GetData<VarInt32>(Constant.ProcedureData.NextSceneId).Value;
			sceneData = GameEntry.Data.GetData<DataScene>().GetSceneData(loadingSceneId);
			if (sceneData == null)
			{
				Log.Warning("Can not can scene data id :'{0}'.", loadingSceneId.ToString());
				return;
			}

			GameEntry.Scene.LoadScene(sceneData.AssetPath, Constant.AssetPriority.SceneAsset, this);
		}

		private void OnLoadSceneDependencyAsset(object sender, GameEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void OnLoadSceneUpdate(object sender, GameEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void OnLoadSceneFailure(object sender, GameEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void onLoadSceneSuccess(object sender, GameEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
