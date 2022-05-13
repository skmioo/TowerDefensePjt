using GameFramework.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace TowerDF
{
	public static class DataTableExtension
	{
		private const string DataClassPrefixName = "TowerDF.DR";
		internal static readonly char[] DataSplitSeparators = new char[] { '\t' };
		internal static readonly char[] DataTrimSeparators = new char[] { '\"' };

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
			DataTableBase dataTable = dataTableComponent.CreateDataTable(type, "");
			dataTable.ReadData(AssetUtility.GetDataTableAsset(dataTableName), Constant.AssetPriority.DataTableAsset, userData);
		}

		public static Color32 ParseColor32(string value)
		{
			string[] splitValue = value.Split(',');
			return new Color32(byte.Parse(splitValue[0]), byte.Parse(splitValue[1]), byte.Parse(splitValue[2]), byte.Parse(splitValue[3]));
		}

		public static Color ParseColor(string value)
		{
			string[] splitValue = value.Split(',');
			return new Color(float.Parse(splitValue[0]), float.Parse(splitValue[1]), float.Parse(splitValue[2]), float.Parse(splitValue[3]));
		}

		public static Quaternion ParseQuaternion(string value)
		{
			string[] splitValue = value.Split(',');
			return new Quaternion(float.Parse(splitValue[0]), float.Parse(splitValue[1]), float.Parse(splitValue[2]), float.Parse(splitValue[3]));
		}

		public static Rect ParseRect(string value)
		{
			string[] splitValue = value.Split(',');
			return new Rect(float.Parse(splitValue[0]), float.Parse(splitValue[1]), float.Parse(splitValue[2]), float.Parse(splitValue[3]));
		}

		public static Vector2 ParseVector2(string value)
		{
			string[] splitValue = value.Split(',');
			return new Vector2(float.Parse(splitValue[0]), float.Parse(splitValue[1]));
		}

		public static Vector3 ParseVector3(string value)
		{
			string[] splitValue = value.Split(',');
			return new Vector3(float.Parse(splitValue[0]), float.Parse(splitValue[1]), float.Parse(splitValue[2]));
		}

		public static Vector4 ParseVector4(string value)
		{
			string[] splitValue = value.Split(',');
			return new Vector4(float.Parse(splitValue[0]), float.Parse(splitValue[1]), float.Parse(splitValue[2]), float.Parse(splitValue[3]));
		}

		public static int[] ParseInt32Array(string value)
		{
			string[] splitValue = value.Split(',');
			int[] result = new int[splitValue.Length];
			for (int i = 0; i < splitValue.Length; i++)
			{
				result[i] = int.Parse(splitValue[i]);
			}

			return result;
		}
	}
}
