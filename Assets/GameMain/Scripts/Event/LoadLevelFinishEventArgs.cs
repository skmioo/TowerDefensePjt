using GameFramework;
using GameFramework.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDF
{
	public class LoadLevelFinishEventArgs : GameEventArgs
	{
		public static readonly int EventId = typeof(LoadLevelFinishEventArgs).GetHashCode();
		public override int Id {
			get {
				return EventId;
			}
		}

		public int LevelId
		{
			get;
			private set;
		}

		public object UserData
		{
			get;
			private set;
		}

		public LoadLevelFinishEventArgs()
		{
			LevelId = -1;
		}

		public static LoadLevelFinishEventArgs Create(int levelId, object userData = null)
		{
			LoadLevelFinishEventArgs loadLevelEventArgs = ReferencePool.Acquire<LoadLevelFinishEventArgs>();
			loadLevelEventArgs.LevelId = levelId;
			loadLevelEventArgs.UserData = userData;
			return loadLevelEventArgs;
		}

		public override void Clear()
		{
			LevelId = -1;
		}
	}
}
