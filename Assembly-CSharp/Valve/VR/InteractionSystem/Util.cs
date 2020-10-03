using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200043C RID: 1084
	public static class Util
	{
		// Token: 0x06002124 RID: 8484 RVA: 0x000A36B5 File Offset: 0x000A18B5
		public static float RemapNumber(float num, float low1, float high1, float low2, float high2)
		{
			return low2 + (num - low1) * (high2 - low2) / (high1 - low1);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x000A36C5 File Offset: 0x000A18C5
		public static float RemapNumberClamped(float num, float low1, float high1, float low2, float high2)
		{
			return Mathf.Clamp(Util.RemapNumber(num, low1, high1, low2, high2), Mathf.Min(low2, high2), Mathf.Max(low2, high2));
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x000A36E8 File Offset: 0x000A18E8
		public static float Approach(float target, float value, float speed)
		{
			float num = target - value;
			if (num > speed)
			{
				value += speed;
			}
			else if (num < -speed)
			{
				value -= speed;
			}
			else
			{
				value = target;
			}
			return value;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000A3714 File Offset: 0x000A1914
		public static Vector3 BezierInterpolate3(Vector3 p0, Vector3 c0, Vector3 p1, float t)
		{
			Vector3 a = Vector3.Lerp(p0, c0, t);
			Vector3 b = Vector3.Lerp(c0, p1, t);
			return Vector3.Lerp(a, b, t);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000A373C File Offset: 0x000A193C
		public static Vector3 BezierInterpolate4(Vector3 p0, Vector3 c0, Vector3 c1, Vector3 p1, float t)
		{
			Vector3 a = Vector3.Lerp(p0, c0, t);
			Vector3 vector = Vector3.Lerp(c0, c1, t);
			Vector3 b = Vector3.Lerp(c1, p1, t);
			Vector3 a2 = Vector3.Lerp(a, vector, t);
			Vector3 b2 = Vector3.Lerp(vector, b, t);
			return Vector3.Lerp(a2, b2, t);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x000A3780 File Offset: 0x000A1980
		public static Vector3 Vector3FromString(string szString)
		{
			string[] array = szString.Substring(1, szString.Length - 1).Split(new char[]
			{
				','
			});
			float x = float.Parse(array[0]);
			float y = float.Parse(array[1]);
			float z = float.Parse(array[2]);
			return new Vector3(x, y, z);
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x000A37D0 File Offset: 0x000A19D0
		public static Vector2 Vector2FromString(string szString)
		{
			string[] array = szString.Substring(1, szString.Length - 1).Split(new char[]
			{
				','
			});
			float x = float.Parse(array[0]);
			float y = float.Parse(array[1]);
			return new Vector2(x, y);
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000A381E File Offset: 0x000A1A1E
		public static float Normalize(float value, float min, float max)
		{
			return (value - min) / (max - min);
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000A3827 File Offset: 0x000A1A27
		public static Vector3 Vector2AsVector3(Vector2 v)
		{
			return new Vector3(v.x, 0f, v.y);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x000A383F File Offset: 0x000A1A3F
		public static Vector2 Vector3AsVector2(Vector3 v)
		{
			return new Vector2(v.x, v.z);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x000A3854 File Offset: 0x000A1A54
		public static float AngleOf(Vector2 v)
		{
			float magnitude = v.magnitude;
			if (v.y >= 0f)
			{
				return Mathf.Acos(v.x / magnitude);
			}
			return Mathf.Acos(-v.x / magnitude) + 3.14159274f;
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x000A3898 File Offset: 0x000A1A98
		public static float YawOf(Vector3 v)
		{
			float magnitude = v.magnitude;
			if (v.z >= 0f)
			{
				return Mathf.Acos(v.x / magnitude);
			}
			return Mathf.Acos(-v.x / magnitude) + 3.14159274f;
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000A38DC File Offset: 0x000A1ADC
		public static void Swap<T>(ref T lhs, ref T rhs)
		{
			T t = lhs;
			lhs = rhs;
			rhs = t;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000A3904 File Offset: 0x000A1B04
		public static void Shuffle<T>(T[] array)
		{
			for (int i = array.Length - 1; i > 0; i--)
			{
				int num = Random.Range(0, i);
				Util.Swap<T>(ref array[i], ref array[num]);
			}
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000A393C File Offset: 0x000A1B3C
		public static void Shuffle<T>(List<T> list)
		{
			for (int i = list.Count - 1; i > 0; i--)
			{
				int index = Random.Range(0, i);
				T value = list[i];
				list[i] = list[index];
				list[index] = value;
			}
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000A3984 File Offset: 0x000A1B84
		public static int RandomWithLookback(int min, int max, List<int> history, int historyCount)
		{
			int num = Random.Range(min, max - history.Count);
			for (int i = 0; i < history.Count; i++)
			{
				if (num >= history[i])
				{
					num++;
				}
			}
			history.Add(num);
			if (history.Count > historyCount)
			{
				history.RemoveRange(0, history.Count - historyCount);
			}
			return num;
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x000A39E0 File Offset: 0x000A1BE0
		public static Transform FindChild(Transform parent, string name)
		{
			if (parent.name == name)
			{
				return parent;
			}
			foreach (object obj in parent)
			{
				Transform transform = Util.FindChild((Transform)obj, name);
				if (transform != null)
				{
					return transform;
				}
			}
			return null;
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x000A3A54 File Offset: 0x000A1C54
		public static bool IsNullOrEmpty<T>(T[] array)
		{
			return array == null || array.Length == 0;
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x000A3A62 File Offset: 0x000A1C62
		public static bool IsValidIndex<T>(T[] array, int i)
		{
			return array != null && i >= 0 && i < array.Length;
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x000A3A75 File Offset: 0x000A1C75
		public static bool IsValidIndex<T>(List<T> list, int i)
		{
			return list != null && list.Count != 0 && i >= 0 && i < list.Count;
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x000A3A94 File Offset: 0x000A1C94
		public static int FindOrAdd<T>(List<T> list, T item)
		{
			int num = list.IndexOf(item);
			if (num == -1)
			{
				list.Add(item);
				num = list.Count - 1;
			}
			return num;
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x000A3ABE File Offset: 0x000A1CBE
		public static List<T> FindAndRemove<T>(List<T> list, Predicate<T> match)
		{
			List<T> result = list.FindAll(match);
			list.RemoveAll(match);
			return result;
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x000A3AD0 File Offset: 0x000A1CD0
		public static T FindOrAddComponent<T>(GameObject gameObject) where T : Component
		{
			T component = gameObject.GetComponent<T>();
			if (component)
			{
				return component;
			}
			return gameObject.AddComponent<T>();
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x000A3AF9 File Offset: 0x000A1CF9
		public static void FastRemove<T>(List<T> list, int index)
		{
			list[index] = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000A3B1E File Offset: 0x000A1D1E
		public static void ReplaceGameObject<T, U>(T replace, U replaceWith) where T : MonoBehaviour where U : MonoBehaviour
		{
			replace.gameObject.SetActive(false);
			replaceWith.gameObject.SetActive(true);
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000A3B44 File Offset: 0x000A1D44
		public static void SwitchLayerRecursively(Transform transform, int fromLayer, int toLayer)
		{
			if (transform.gameObject.layer == fromLayer)
			{
				transform.gameObject.layer = toLayer;
			}
			int childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Util.SwitchLayerRecursively(transform.GetChild(i), fromLayer, toLayer);
			}
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000A3B8C File Offset: 0x000A1D8C
		public static void DrawCross(Vector3 origin, Color crossColor, float size)
		{
			Vector3 start = origin + Vector3.right * size;
			Vector3 end = origin - Vector3.right * size;
			Debug.DrawLine(start, end, crossColor);
			Vector3 start2 = origin + Vector3.up * size;
			Vector3 end2 = origin - Vector3.up * size;
			Debug.DrawLine(start2, end2, crossColor);
			Vector3 start3 = origin + Vector3.forward * size;
			Vector3 end3 = origin - Vector3.forward * size;
			Debug.DrawLine(start3, end3, crossColor);
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000A3C17 File Offset: 0x000A1E17
		public static void ResetTransform(Transform t, bool resetScale = true)
		{
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			if (resetScale)
			{
				t.localScale = new Vector3(1f, 1f, 1f);
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x000A3C4C File Offset: 0x000A1E4C
		public static Vector3 ClosestPointOnLine(Vector3 vA, Vector3 vB, Vector3 vPoint)
		{
			Vector3 rhs = vPoint - vA;
			Vector3 normalized = (vB - vA).normalized;
			float num = Vector3.Distance(vA, vB);
			float num2 = Vector3.Dot(normalized, rhs);
			if (num2 <= 0f)
			{
				return vA;
			}
			if (num2 >= num)
			{
				return vB;
			}
			Vector3 b = normalized * num2;
			return vA + b;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x000A3CA3 File Offset: 0x000A1EA3
		public static void AfterTimer(GameObject go, float _time, Action callback, bool trigger_if_destroyed_early = false)
		{
			go.AddComponent<AfterTimer_Component>().Init(_time, callback, trigger_if_destroyed_early);
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000A3CB4 File Offset: 0x000A1EB4
		public static void SendPhysicsMessage(Collider collider, string message, SendMessageOptions sendMessageOptions)
		{
			Rigidbody attachedRigidbody = collider.attachedRigidbody;
			if (attachedRigidbody && attachedRigidbody.gameObject != collider.gameObject)
			{
				attachedRigidbody.SendMessage(message, sendMessageOptions);
			}
			collider.SendMessage(message, sendMessageOptions);
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000A3CF4 File Offset: 0x000A1EF4
		public static void SendPhysicsMessage(Collider collider, string message, object arg, SendMessageOptions sendMessageOptions)
		{
			Rigidbody attachedRigidbody = collider.attachedRigidbody;
			if (attachedRigidbody && attachedRigidbody.gameObject != collider.gameObject)
			{
				attachedRigidbody.SendMessage(message, arg, sendMessageOptions);
			}
			collider.SendMessage(message, arg, sendMessageOptions);
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x000A3D38 File Offset: 0x000A1F38
		public static void IgnoreCollisions(GameObject goA, GameObject goB)
		{
			Collider[] componentsInChildren = goA.GetComponentsInChildren<Collider>();
			Collider[] componentsInChildren2 = goB.GetComponentsInChildren<Collider>();
			if (componentsInChildren.Length == 0 || componentsInChildren2.Length == 0)
			{
				return;
			}
			foreach (Collider collider in componentsInChildren)
			{
				foreach (Collider collider2 in componentsInChildren2)
				{
					if (collider.enabled && collider2.enabled)
					{
						Physics.IgnoreCollision(collider, collider2, true);
					}
				}
			}
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x000A3DAA File Offset: 0x000A1FAA
		public static IEnumerator WrapCoroutine(IEnumerator coroutine, Action onCoroutineFinished)
		{
			while (coroutine.MoveNext())
			{
				object obj = coroutine.Current;
				yield return obj;
			}
			onCoroutineFinished();
			yield break;
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000A3DC0 File Offset: 0x000A1FC0
		public static Color ColorWithAlpha(this Color color, float alpha)
		{
			color.a = alpha;
			return color;
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000A3DCB File Offset: 0x000A1FCB
		public static void Quit()
		{
			Process.GetCurrentProcess().Kill();
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x000A3DD7 File Offset: 0x000A1FD7
		public static decimal FloatToDecimal(float value, int decimalPlaces = 2)
		{
			return Math.Round((decimal)value, decimalPlaces);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000A3DE8 File Offset: 0x000A1FE8
		public static T Median<T>(this IEnumerable<T> source)
		{
			if (source == null)
			{
				throw new ArgumentException("Argument cannot be null.", "source");
			}
			int num = source.Count<T>();
			if (num == 0)
			{
				throw new InvalidOperationException("Enumerable must contain at least one element.");
			}
			return (from x in source
			orderby x
			select x).ElementAt(num / 2);
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000A3E4C File Offset: 0x000A204C
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source == null)
			{
				throw new ArgumentException("Argument cannot be null.", "source");
			}
			foreach (T obj in source)
			{
				action(obj);
			}
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x000A3EA8 File Offset: 0x000A20A8
		public static string FixupNewlines(string text)
		{
			bool flag = true;
			while (flag)
			{
				int num = text.IndexOf("\\n");
				if (num == -1)
				{
					flag = false;
				}
				else
				{
					text = text.Remove(num - 1, 3);
					text = text.Insert(num - 1, "\n");
				}
			}
			return text;
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000A3EF0 File Offset: 0x000A20F0
		public static float PathLength(NavMeshPath path)
		{
			if (path.corners.Length < 2)
			{
				return 0f;
			}
			Vector3 a = path.corners[0];
			float num = 0f;
			for (int i = 1; i < path.corners.Length; i++)
			{
				Vector3 vector = path.corners[i];
				num += Vector3.Distance(a, vector);
				a = vector;
			}
			return num;
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000A3F50 File Offset: 0x000A2150
		public static bool HasCommandLineArgument(string argumentName)
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			for (int i = 0; i < commandLineArgs.Length; i++)
			{
				if (commandLineArgs[i].Equals(argumentName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x000A3F80 File Offset: 0x000A2180
		public static int GetCommandLineArgValue(string argumentName, int nDefaultValue)
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			int i = 0;
			while (i < commandLineArgs.Length)
			{
				if (commandLineArgs[i].Equals(argumentName))
				{
					if (i == commandLineArgs.Length - 1)
					{
						return nDefaultValue;
					}
					return int.Parse(commandLineArgs[i + 1]);
				}
				else
				{
					i++;
				}
			}
			return nDefaultValue;
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x000A3FC4 File Offset: 0x000A21C4
		public static float GetCommandLineArgValue(string argumentName, float flDefaultValue)
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			int i = 0;
			while (i < commandLineArgs.Length)
			{
				if (commandLineArgs[i].Equals(argumentName))
				{
					if (i == commandLineArgs.Length - 1)
					{
						return flDefaultValue;
					}
					return (float)double.Parse(commandLineArgs[i + 1]);
				}
				else
				{
					i++;
				}
			}
			return flDefaultValue;
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x000A4007 File Offset: 0x000A2207
		public static void SetActive(GameObject gameObject, bool active)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(active);
			}
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x000A401C File Offset: 0x000A221C
		public static string CombinePaths(params string[] paths)
		{
			if (paths.Length == 0)
			{
				return "";
			}
			string text = paths[0];
			for (int i = 1; i < paths.Length; i++)
			{
				text = Path.Combine(text, paths[i]);
			}
			return text;
		}

		// Token: 0x04001EA9 RID: 7849
		public const float FeetToMeters = 0.3048f;

		// Token: 0x04001EAA RID: 7850
		public const float FeetToCentimeters = 30.48f;

		// Token: 0x04001EAB RID: 7851
		public const float InchesToMeters = 0.0254f;

		// Token: 0x04001EAC RID: 7852
		public const float InchesToCentimeters = 2.54f;

		// Token: 0x04001EAD RID: 7853
		public const float MetersToFeet = 3.28084f;

		// Token: 0x04001EAE RID: 7854
		public const float MetersToInches = 39.3701f;

		// Token: 0x04001EAF RID: 7855
		public const float CentimetersToFeet = 0.0328084f;

		// Token: 0x04001EB0 RID: 7856
		public const float CentimetersToInches = 0.393701f;

		// Token: 0x04001EB1 RID: 7857
		public const float KilometersToMiles = 0.621371f;

		// Token: 0x04001EB2 RID: 7858
		public const float MilesToKilometers = 1.60934f;
	}
}
