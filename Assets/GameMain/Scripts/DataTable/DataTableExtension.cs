using GameFramework.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityGameFramework.Runtime;

namespace TowerDF
{
	public static class DataTableExtension
	{
		private const string DataClassPrefixName = "TowerDF.DR";
		public static void LoadDataTable(this DataTableComponent dataTableComponent, string dataTableName,  string dataTableAssetName, object userData = null)
		{
			if (string.IsNullOrEmpty(dataTableAssetName))
			{
				Log.Warning("Data table name is invalid.");
				return;
			}

			string dataClassName = DataClassPrefixName + dataTableName;
			Type type = Type.GetType(dataClassName);
			if (type == null)
			{
				Log.Warning("Can not get data row type with class name '{0}'", dataClassName);
				return;
			}
			DataTableBase dataTable = dataTableComponent.CreateDataTable(type, dataTableName);
			dataTable.ReadData(AssetUtility.GetDataTableAsset(dataTableName), Constant.AssetPriority.DataTableAsset, userData);
		}

	}
}
