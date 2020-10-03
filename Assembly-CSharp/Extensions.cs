using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x02000085 RID: 133
public static class Extensions
{
	// Token: 0x060002C8 RID: 712 RVA: 0x000124D0 File Offset: 0x000106D0
	public static ParameterInfo[] GetCachedParemeters(this MethodInfo mo)
	{
		ParameterInfo[] parameters;
		if (!Extensions.ParametersOfMethods.TryGetValue(mo, out parameters))
		{
			parameters = mo.GetParameters();
			Extensions.ParametersOfMethods[mo] = parameters;
		}
		return parameters;
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x00012500 File Offset: 0x00010700
	public static PhotonView[] GetPhotonViewsInChildren(this GameObject go)
	{
		return go.GetComponentsInChildren<PhotonView>(true);
	}

	// Token: 0x060002CA RID: 714 RVA: 0x00012509 File Offset: 0x00010709
	public static PhotonView GetPhotonView(this GameObject go)
	{
		return go.GetComponent<PhotonView>();
	}

	// Token: 0x060002CB RID: 715 RVA: 0x00012514 File Offset: 0x00010714
	public static bool AlmostEquals(this Vector3 target, Vector3 second, float sqrMagnitudePrecision)
	{
		return (target - second).sqrMagnitude < sqrMagnitudePrecision;
	}

	// Token: 0x060002CC RID: 716 RVA: 0x00012534 File Offset: 0x00010734
	public static bool AlmostEquals(this Vector2 target, Vector2 second, float sqrMagnitudePrecision)
	{
		return (target - second).sqrMagnitude < sqrMagnitudePrecision;
	}

	// Token: 0x060002CD RID: 717 RVA: 0x00012553 File Offset: 0x00010753
	public static bool AlmostEquals(this Quaternion target, Quaternion second, float maxAngle)
	{
		return Quaternion.Angle(target, second) < maxAngle;
	}

	// Token: 0x060002CE RID: 718 RVA: 0x0001255F File Offset: 0x0001075F
	public static bool AlmostEquals(this float target, float second, float floatDiff)
	{
		return Mathf.Abs(target - second) < floatDiff;
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0001256C File Offset: 0x0001076C
	public static void Merge(this IDictionary target, IDictionary addHash)
	{
		if (addHash == null || target.Equals(addHash))
		{
			return;
		}
		foreach (object key in addHash.Keys)
		{
			target[key] = addHash[key];
		}
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x000125D4 File Offset: 0x000107D4
	public static void MergeStringKeys(this IDictionary target, IDictionary addHash)
	{
		if (addHash == null || target.Equals(addHash))
		{
			return;
		}
		foreach (object obj in addHash.Keys)
		{
			if (obj is string)
			{
				target[obj] = addHash[obj];
			}
		}
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x00012644 File Offset: 0x00010844
	public static string ToStringFull(this IDictionary origin)
	{
		return SupportClass.DictionaryToString(origin, false);
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x00012650 File Offset: 0x00010850
	public static string ToStringFull(this object[] data)
	{
		if (data == null)
		{
			return "null";
		}
		string[] array = new string[data.Length];
		for (int i = 0; i < data.Length; i++)
		{
			object obj = data[i];
			array[i] = ((obj != null) ? obj.ToString() : "null");
		}
		return string.Join(", ", array);
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x000126A0 File Offset: 0x000108A0
	public static Hashtable StripToStringKeys(this IDictionary original)
	{
		Hashtable hashtable = new Hashtable();
		if (original != null)
		{
			foreach (object obj in original.Keys)
			{
				if (obj is string)
				{
					hashtable[obj] = original[obj];
				}
			}
		}
		return hashtable;
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x00012710 File Offset: 0x00010910
	public static void StripKeysWithNullValues(this IDictionary original)
	{
		object[] array = new object[original.Count];
		int num = 0;
		foreach (object obj in original.Keys)
		{
			array[num++] = obj;
		}
		foreach (object key in array)
		{
			if (original[key] == null)
			{
				original.Remove(key);
			}
		}
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x000127A0 File Offset: 0x000109A0
	public static bool Contains(this int[] target, int nr)
	{
		if (target == null)
		{
			return false;
		}
		for (int i = 0; i < target.Length; i++)
		{
			if (target[i] == nr)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000355 RID: 853
	public static Dictionary<MethodInfo, ParameterInfo[]> ParametersOfMethods = new Dictionary<MethodInfo, ParameterInfo[]>();
}
