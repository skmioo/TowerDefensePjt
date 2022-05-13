﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityGameFramework.Runtime;

namespace TowerDF
{
	/// <summary>
	/// 场景配置表
	/// </summary>
	public class DRScene : DataRowBase
	{
		private int m_Id = 0;

		/// <summary>
		/// 获取场景编号。
		/// </summary>
		public override int Id
		{
			get
			{
				return m_Id;
			}
		}

		/// <summary>
		/// 获取资源Id。
		/// </summary>
		public int AssetId
		{
			get;
			private set;
		}

		/// <summary>
		/// 获取流程名称。
		/// </summary>
		public string Procedure
		{
			get;
			private set;
		}

		public override bool ParseDataRow(string dataRowString, object userData)
		{
			string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
			for (int i = 0; i < columnStrings.Length; i++)
			{
				columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
			}

			int index = 0;
			index++;
			m_Id = int.Parse(columnStrings[index++]);
			index++;
			AssetId = int.Parse(columnStrings[index++]);
			Procedure = columnStrings[index++];

			GeneratePropertyArray();
			return true;
		}

		public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
		{
			using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
				{
					m_Id = binaryReader.Read7BitEncodedInt32();
					AssetId = binaryReader.Read7BitEncodedInt32();
					Procedure = binaryReader.ReadString();
				}
			}

			GeneratePropertyArray();
			return true;
		}

		private void GeneratePropertyArray()
		{

		}
	}
}