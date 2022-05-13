using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDF
{
	/// <summary>
	/// 游戏通过Procedure来进行游戏逻辑执行
	/// </summary>
	public partial class GameEntry : MonoBehaviour
	{
		// Start is called before the first frame update
		void Start()
		{
			InitBuiltinComponents();
			InitCustomComponents();
		}

	}
}