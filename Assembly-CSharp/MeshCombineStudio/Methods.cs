using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MeshCombineStudio
{
	// Token: 0x020004B7 RID: 1207
	public static class Methods
	{
		// Token: 0x06002598 RID: 9624 RVA: 0x000BBDBC File Offset: 0x000B9FBC
		public static void SetTag(GameObject go, string tag)
		{
			Transform[] componentsInChildren = go.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].tag = tag;
			}
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000BBDE8 File Offset: 0x000B9FE8
		public static void SetTagWhenCollider(GameObject go, string tag)
		{
			Transform[] componentsInChildren = go.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].GetComponent<Collider>() != null)
				{
					componentsInChildren[i].tag = tag;
				}
			}
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000BBE24 File Offset: 0x000BA024
		public static void SetTagAndLayer(GameObject go, string tag, int layer)
		{
			Transform[] componentsInChildren = go.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].tag = tag;
				componentsInChildren[i].gameObject.layer = layer;
			}
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000BBE60 File Offset: 0x000BA060
		public static void SetLayer(GameObject go, int layer)
		{
			go.layer = layer;
			Transform[] componentsInChildren = go.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = layer;
			}
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000BBE97 File Offset: 0x000BA097
		public static bool LayerMaskContainsLayer(int layerMask, int layer)
		{
			return (1 << layer & layerMask) != 0;
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x000BBEA4 File Offset: 0x000BA0A4
		public static int GetFirstLayerInLayerMask(int layerMask)
		{
			for (int i = 0; i < 32; i++)
			{
				if ((layerMask & Mathw.bits[i]) != 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000BBECC File Offset: 0x000BA0CC
		public static bool Contains(string compare, string name)
		{
			List<string> list = new List<string>();
			int num;
			do
			{
				num = name.IndexOf("*");
				if (num != -1)
				{
					if (num != 0)
					{
						list.Add(name.Substring(0, num));
					}
					if (num == name.Length - 1)
					{
						break;
					}
					name = name.Substring(num + 1);
				}
			}
			while (num != -1);
			list.Add(name);
			for (int i = 0; i < list.Count; i++)
			{
				if (!compare.Contains(list[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000BBF44 File Offset: 0x000BA144
		public static T[] Search<T>(GameObject parentGO = null)
		{
			GameObject[] array;
			if (parentGO == null)
			{
				array = SceneManager.GetActiveScene().GetRootGameObjects();
			}
			else
			{
				array = new GameObject[]
				{
					parentGO
				};
			}
			if (array == null)
			{
				return null;
			}
			if (typeof(T) == typeof(GameObject))
			{
				List<GameObject> list = new List<GameObject>();
				for (int i = 0; i < array.Length; i++)
				{
					Transform[] componentsInChildren = array[i].GetComponentsInChildren<Transform>(true);
					for (int j = 0; j < componentsInChildren.Length; j++)
					{
						list.Add(componentsInChildren[j].gameObject);
					}
				}
				return list.ToArray() as T[];
			}
			if (parentGO == null)
			{
				List<T> list2 = new List<T>();
				for (int k = 0; k < array.Length; k++)
				{
					list2.AddRange(array[k].GetComponentsInChildren<T>(true));
				}
				return list2.ToArray();
			}
			return parentGO.GetComponentsInChildren<T>(true);
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000BC028 File Offset: 0x000BA228
		public static FastList<GameObject> GetAllRootGameObjects()
		{
			FastList<GameObject> fastList = new FastList<GameObject>();
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				fastList.AddRange(SceneManager.GetSceneAt(i).GetRootGameObjects());
			}
			return fastList;
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000BC060 File Offset: 0x000BA260
		public static T[] SearchParent<T>(GameObject parentGO, bool searchInActiveGameObjects) where T : Component
		{
			if (parentGO == null)
			{
				return Methods.SearchAllScenes<T>(searchInActiveGameObjects).ToArray();
			}
			if (!searchInActiveGameObjects && !parentGO.activeInHierarchy)
			{
				return null;
			}
			if (typeof(T) == typeof(GameObject))
			{
				Transform[] componentsInChildren = parentGO.GetComponentsInChildren<Transform>(searchInActiveGameObjects);
				GameObject[] array = new GameObject[componentsInChildren.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = componentsInChildren[i].gameObject;
				}
				return array as T[];
			}
			return parentGO.GetComponentsInChildren<T>(searchInActiveGameObjects);
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000BC0E4 File Offset: 0x000BA2E4
		public static T[] SearchScene<T>(Scene scene, bool searchInActiveGameObjects) where T : Component
		{
			GameObject[] rootGameObjects = scene.GetRootGameObjects();
			FastList<T> fastList = new FastList<T>();
			foreach (GameObject parentGO in rootGameObjects)
			{
				fastList.AddRange(Methods.SearchParent<T>(parentGO, searchInActiveGameObjects));
			}
			return fastList.ToArray();
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000BC124 File Offset: 0x000BA324
		public static FastList<T> SearchAllScenes<T>(bool searchInActiveGameObjects) where T : Component
		{
			FastList<T> fastList = new FastList<T>();
			FastList<GameObject> allRootGameObjects = Methods.GetAllRootGameObjects();
			for (int i = 0; i < allRootGameObjects.Count; i++)
			{
				T[] arrayItems = Methods.SearchParent<T>(allRootGameObjects.items[i], searchInActiveGameObjects);
				fastList.AddRange(arrayItems);
			}
			return fastList;
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x000BC168 File Offset: 0x000BA368
		public static T Find<T>(GameObject parentGO, string name) where T : Component
		{
			T[] array = Methods.SearchParent<T>(parentGO, true);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].name == name)
				{
					return array[i];
				}
			}
			return default(T);
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000BC1B8 File Offset: 0x000BA3B8
		public static void SetCollidersActive(Collider[] colliders, bool active, string[] nameList)
		{
			for (int i = 0; i < colliders.Length; i++)
			{
				for (int j = 0; j < nameList.Length; j++)
				{
					if (colliders[i].name.Contains(nameList[j]))
					{
						colliders[i].enabled = active;
					}
				}
			}
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x00003F60 File Offset: 0x00002160
		public static void SelectChildrenWithMeshRenderer(Transform t)
		{
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000BC1FC File Offset: 0x000BA3FC
		public static void DestroyChildren(Transform t)
		{
			while (t.childCount > 0)
			{
				Transform child = t.GetChild(0);
				child.parent = null;
				Object.DestroyImmediate(child.gameObject);
			}
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000BC221 File Offset: 0x000BA421
		public static void Destroy(GameObject go)
		{
			if (go == null)
			{
				return;
			}
			Object.Destroy(go);
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000BC234 File Offset: 0x000BA434
		public static void SetChildrenActive(Transform t, bool active)
		{
			for (int i = 0; i < t.childCount; i++)
			{
				t.GetChild(i).gameObject.SetActive(active);
			}
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000BC264 File Offset: 0x000BA464
		public static void SnapBoundsAndPreserveArea(ref Bounds bounds, float snapSize, Vector3 offset)
		{
			Vector3 vector = Mathw.Snap(bounds.center, snapSize) + offset;
			bounds.size += Mathw.Abs(vector - bounds.center) * 2f;
			bounds.center = vector;
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000BC2B7 File Offset: 0x000BA4B7
		public static void ListRemoveAt<T>(List<T> list, int index)
		{
			list[index] = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000BC2DC File Offset: 0x000BA4DC
		public static void CopyComponent(Component component, GameObject target)
		{
			Type type = component.GetType();
			target.AddComponent(type);
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public))
			{
				propertyInfo.SetValue(target.GetComponent(type), propertyInfo.GetValue(component, null), null);
			}
		}
	}
}
