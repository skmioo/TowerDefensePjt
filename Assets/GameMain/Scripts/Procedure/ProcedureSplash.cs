﻿using GameFramework.Procedure;
using GameFramework.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace TowerDF
{
	/// <summary>
	/// 
	/// </summary>
	public class ProcedureSplash : ProcedureBase
	{
		protected override void OnInit(ProcedureOwner procedureOwner)
		{
			base.OnInit(procedureOwner);
		}

		protected override void OnEnter(ProcedureOwner procedureOwner)
		{
			base.OnEnter(procedureOwner);
		}

		protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
		{
			base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
			if (GameEntry.Base.EditorResourceMode)
			{
				//编辑器模式
				Log.Info("Editor resource mode detected.");
				ChangeState<ProcedurePreload>(procedureOwner);
			}
			else if (GameEntry.Resource.ResourceMode == ResourceMode.Package)
			{
				// 单机模式
				Log.Info("Package resource mode detected.");
				ChangeState<ProcedureInitResources>(procedureOwner);
			}
			else
			{
				// 可更新模式
				Log.Info("Updatable resource mode detected.");
				ChangeState<ProcedureCheckVersion>(procedureOwner);
			}
		}

		protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
		{
			base.OnLeave(procedureOwner, isShutdown);
		}

		protected override void OnDestroy(ProcedureOwner procedureOwner)
		{
			base.OnDestroy(procedureOwner);
		}

	}
}
