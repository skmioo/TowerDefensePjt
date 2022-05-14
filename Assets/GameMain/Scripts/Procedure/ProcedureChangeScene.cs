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

		protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
		{
			base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
			if (loadSceneCompleted)
			{
				Type type = Type.GetType(string.Format("TowerDF.{0}", sceneData.Procedure));
				if (type != null)
				{
					ChangeState(procedureOwner,type);
				}
				else
					Log.Warning("Can not change state,scene procedure '{0}' error, from scene '{1}.{2}'.", sceneData.Procedure.ToString(), sceneData.Id, sceneData.AssetPath);
			}
		}

		protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
		{
			base.OnLeave(procedureOwner, isShutdown);
			GameEntry.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, onLoadSceneSuccess);
			GameEntry.Event.Unsubscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
			GameEntry.Event.Unsubscribe(LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
			GameEntry.Event.Unsubscribe(LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);
		}

		private void OnLoadSceneDependencyAsset(object sender, GameEventArgs e)
		{
			
		}

		private void OnLoadSceneUpdate(object sender, GameEventArgs e)
		{
			
		}

		private void OnLoadSceneFailure(object sender, GameEventArgs e)
		{
			LoadSceneFailureEventArgs ne = e as LoadSceneFailureEventArgs;
			if (ne.UserData != this)
				return;
			Log.Error("Load scene '{0}' failure, error message '{1}'.", ne.SceneAssetName, ne.ErrorMessage);

		}

		private void onLoadSceneSuccess(object sender, GameEventArgs e)
		{
			LoadSceneSuccessEventArgs ne = e as LoadSceneSuccessEventArgs;
			if (ne.UserData != this)
				return;
			loadSceneCompleted = true;
			GameEntry.Event.Fire(this, LoadLevelFinishEventArgs.Create(loadingSceneId));
			Log.Info("Load scene '{0}' OK.", ne.SceneAssetName);
		}
	}
}
