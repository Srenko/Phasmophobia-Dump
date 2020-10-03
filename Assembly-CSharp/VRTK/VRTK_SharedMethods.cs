using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

namespace VRTK
{
	// Token: 0x02000317 RID: 791
	public static class VRTK_SharedMethods
	{
		// Token: 0x06001BCE RID: 7118 RVA: 0x000914F0 File Offset: 0x0008F6F0
		public static Bounds GetBounds(Transform transform, Transform excludeRotation = null, Transform excludeTransform = null)
		{
			Quaternion rotation = Quaternion.identity;
			if (excludeRotation)
			{
				rotation = excludeRotation.rotation;
				excludeRotation.rotation = Quaternion.identity;
			}
			bool flag = false;
			Bounds result = new Bounds(transform.position, Vector3.zero);
			foreach (Renderer renderer in transform.GetComponentsInChildren<Renderer>())
			{
				if (!(excludeTransform != null) || !renderer.transform.IsChildOf(excludeTransform))
				{
					if (!flag)
					{
						result = new Bounds(renderer.transform.position, Vector3.zero);
						flag = true;
					}
					result.Encapsulate(renderer.bounds);
				}
			}
			if (result.size.magnitude == 0f)
			{
				foreach (BoxCollider boxCollider in transform.GetComponentsInChildren<BoxCollider>())
				{
					if (!(excludeTransform != null) || !boxCollider.transform.IsChildOf(excludeTransform))
					{
						if (!flag)
						{
							result = new Bounds(boxCollider.transform.position, Vector3.zero);
							flag = true;
						}
						result.Encapsulate(boxCollider.bounds);
					}
				}
			}
			if (excludeRotation)
			{
				excludeRotation.rotation = rotation;
			}
			return result;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00091620 File Offset: 0x0008F820
		public static bool IsLowest(float value, float[] others)
		{
			for (int i = 0; i < others.Length; i++)
			{
				if (others[i] <= value)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00091646 File Offset: 0x0008F846
		public static Transform AddCameraFade()
		{
			Transform transform = VRTK_DeviceFinder.HeadsetCamera();
			VRTK_SDK_Bridge.AddHeadsetFade(transform);
			return transform;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00091654 File Offset: 0x0008F854
		public static void CreateColliders(GameObject obj)
		{
			foreach (Renderer renderer in obj.GetComponentsInChildren<Renderer>())
			{
				if (!renderer.gameObject.GetComponent<Collider>())
				{
					renderer.gameObject.AddComponent<BoxCollider>();
				}
			}
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00091698 File Offset: 0x0008F898
		public static Component CloneComponent(Component source, GameObject destination, bool copyProperties = false)
		{
			Component component = destination.gameObject.AddComponent(source.GetType());
			if (copyProperties)
			{
				foreach (PropertyInfo propertyInfo in source.GetType().GetProperties())
				{
					if (propertyInfo.CanWrite)
					{
						propertyInfo.SetValue(component, propertyInfo.GetValue(source, null), null);
					}
				}
			}
			foreach (FieldInfo fieldInfo in source.GetType().GetFields())
			{
				fieldInfo.SetValue(component, fieldInfo.GetValue(source));
			}
			return component;
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00091723 File Offset: 0x0008F923
		public static Color ColorDarken(Color color, float percent)
		{
			return new Color(VRTK_SharedMethods.NumberPercent(color.r, percent), VRTK_SharedMethods.NumberPercent(color.g, percent), VRTK_SharedMethods.NumberPercent(color.b, percent), color.a);
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x00091754 File Offset: 0x0008F954
		public static float RoundFloat(float givenFloat, int decimalPlaces, bool rawFidelity = false)
		{
			float num = rawFidelity ? ((float)decimalPlaces) : Mathf.Pow(10f, (float)decimalPlaces);
			return Mathf.Round(givenFloat * num) / num;
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x000694A8 File Offset: 0x000676A8
		public static bool IsEditTime()
		{
			return false;
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x0009177F File Offset: 0x0008F97F
		[Obsolete("`VRTK_SharedMethods.TriggerHapticPulse(controllerIndex, strength)` has been replaced with `VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, strength)`. This method will be removed in a future version of VRTK.")]
		public static void TriggerHapticPulse(uint controllerIndex, float strength)
		{
			VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(controllerIndex), strength);
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0009178D File Offset: 0x0008F98D
		[Obsolete("`VRTK_SharedMethods.TriggerHapticPulse(controllerIndex, strength, duration, pulseInterval)` has been replaced with `VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, strength, duration, pulseInterval)`. This method will be removed in a future version of VRTK.")]
		public static void TriggerHapticPulse(uint controllerIndex, float strength, float duration, float pulseInterval)
		{
			VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(controllerIndex), strength, duration, pulseInterval);
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0009179D File Offset: 0x0008F99D
		[Obsolete("`VRTK_SharedMethods.CancelHapticPulse(controllerIndex)` has been replaced with `VRTK_SharedMethods.CancelHapticPulse(controllerReference)`. This method will be removed in a future version of VRTK.")]
		public static void CancelHapticPulse(uint controllerIndex)
		{
			VRTK_ControllerHaptics.CancelHapticPulse(VRTK_ControllerReference.GetControllerReference(controllerIndex));
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x000917AA File Offset: 0x0008F9AA
		[Obsolete("`VRTK_SharedMethods.SetOpacity(model, alpha, transitionDuration)` has been replaced with `VRTK_ObjectAppearance.SetOpacity(model, alpha, transitionDuration)`. This method will be removed in a future version of VRTK.")]
		public static void SetOpacity(GameObject model, float alpha, float transitionDuration = 0f)
		{
			VRTK_ObjectAppearance.SetOpacity(model, alpha, transitionDuration);
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000917B4 File Offset: 0x0008F9B4
		[Obsolete("`VRTK_SharedMethods.SetRendererVisible(model, ignoredModel)` has been replaced with `VRTK_ObjectAppearance.SetRendererVisible(model, ignoredModel)`. This method will be removed in a future version of VRTK.")]
		public static void SetRendererVisible(GameObject model, GameObject ignoredModel = null)
		{
			VRTK_ObjectAppearance.SetRendererVisible(model, ignoredModel);
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x000917BD File Offset: 0x0008F9BD
		[Obsolete("`VRTK_SharedMethods.SetRendererHidden(model, ignoredModel)` has been replaced with `VRTK_ObjectAppearance.SetRendererHidden(model, ignoredModel)`. This method will be removed in a future version of VRTK.")]
		public static void SetRendererHidden(GameObject model, GameObject ignoredModel = null)
		{
			VRTK_ObjectAppearance.SetRendererHidden(model, ignoredModel);
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x000917C6 File Offset: 0x0008F9C6
		[Obsolete("`VRTK_SharedMethods.ToggleRenderer(state, model, ignoredModel)` has been replaced with `VRTK_ObjectAppearance.ToggleRenderer(state, model, ignoredModel)`. This method will be removed in a future version of VRTK.")]
		public static void ToggleRenderer(bool state, GameObject model, GameObject ignoredModel = null)
		{
			VRTK_ObjectAppearance.ToggleRenderer(state, model, ignoredModel);
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x000917D0 File Offset: 0x0008F9D0
		[Obsolete("`VRTK_SharedMethods.HighlightObject(model, highlightColor, fadeDuration)` has been replaced with `VRTK_ObjectAppearance.HighlightObject(model, highlightColor, fadeDuration)`. This method will be removed in a future version of VRTK.")]
		public static void HighlightObject(GameObject model, Color? highlightColor, float fadeDuration = 0f)
		{
			VRTK_ObjectAppearance.HighlightObject(model, highlightColor, fadeDuration);
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x000917DA File Offset: 0x0008F9DA
		[Obsolete("`VRTK_SharedMethods.UnhighlightObject(model)` has been replaced with `VRTK_ObjectAppearance.UnhighlightObject(model)`. This method will be removed in a future version of VRTK.")]
		public static void UnhighlightObject(GameObject model)
		{
			VRTK_ObjectAppearance.UnhighlightObject(model);
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x000917E2 File Offset: 0x0008F9E2
		public static float Mod(float a, float b)
		{
			return a - b * Mathf.Floor(a / b);
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x000917F0 File Offset: 0x0008F9F0
		public static GameObject FindEvenInactiveGameObject<T>(string gameObjectName = null) where T : Component
		{
			if (!string.IsNullOrEmpty(gameObjectName))
			{
				Scene activeScene = SceneManager.GetActiveScene();
				return (from component in Resources.FindObjectsOfTypeAll<T>()
				select component.gameObject into gameObject
				where gameObject.scene == activeScene
				select gameObject).Select(delegate(GameObject gameObject)
				{
					Transform transform = gameObject.transform.Find(gameObjectName);
					if (!(transform == null))
					{
						return transform.gameObject;
					}
					return null;
				}).FirstOrDefault((GameObject gameObject) => gameObject != null);
			}
			T t = VRTK_SharedMethods.FindEvenInactiveComponent<T>();
			if (!(t == null))
			{
				return t.gameObject;
			}
			return null;
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x000918B4 File Offset: 0x0008FAB4
		public static T[] FindEvenInactiveComponents<T>() where T : Component
		{
			Scene activeScene = SceneManager.GetActiveScene();
			return (from @object in Resources.FindObjectsOfTypeAll<T>()
			where @object.gameObject.scene == activeScene
			select @object).ToArray<T>();
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x000918F0 File Offset: 0x0008FAF0
		public static T FindEvenInactiveComponent<T>() where T : Component
		{
			Scene activeScene = SceneManager.GetActiveScene();
			return (from @object in Resources.FindObjectsOfTypeAll<T>()
			where @object.gameObject.scene == activeScene
			select @object).FirstOrDefault<T>();
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x0009192C File Offset: 0x0008FB2C
		public static string GenerateVRTKObjectName(bool autoGen, params object[] replacements)
		{
			string text = "[VRTK]";
			if (autoGen)
			{
				text += "[AUTOGEN]";
			}
			for (int i = 0; i < replacements.Length; i++)
			{
				text = string.Concat(new object[]
				{
					text,
					"[{",
					i,
					"}]"
				});
			}
			return string.Format(text, replacements);
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0009198C File Offset: 0x0008FB8C
		public static float GetGPUTimeLastFrame()
		{
			float result;
			if (XRStats.TryGetGPUTimeLastFrame(out result))
			{
				return result;
			}
			return 0f;
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x000919AC File Offset: 0x0008FBAC
		public static bool Vector2ShallowCompare(Vector2 vectorA, Vector2 vectorB, int compareFidelity)
		{
			Vector2 vector = vectorA - vectorB;
			return Math.Round((double)Mathf.Abs(vector.x), compareFidelity, MidpointRounding.AwayFromZero) < 1.4012984643248171E-45 && Math.Round((double)Mathf.Abs(vector.y), compareFidelity, MidpointRounding.AwayFromZero) < 1.4012984643248171E-45;
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000919FF File Offset: 0x0008FBFF
		public static float NumberPercent(float value, float percent)
		{
			percent = Mathf.Clamp(percent, 0f, 100f);
			if (percent != 0f)
			{
				return value - percent / 100f;
			}
			return value;
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00091A28 File Offset: 0x0008FC28
		public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
		{
			transform.localScale = Vector3.one;
			transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00091A84 File Offset: 0x0008FC84
		public static Type GetTypeUnknownAssembly(string typeName)
		{
			Type type = Type.GetType(typeName);
			if (type != null)
			{
				return type;
			}
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				type = assemblies[i].GetType(typeName);
				if (type != null)
				{
					return type;
				}
			}
			return null;
		}
	}
}
