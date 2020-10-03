using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VRTK
{
	// Token: 0x020002B7 RID: 695
	public class VRTK_EventSystem : EventSystem
	{
		// Token: 0x0600171A RID: 5914 RVA: 0x0007BF54 File Offset: 0x0007A154
		protected override void OnEnable()
		{
			this.previousEventSystem = EventSystem.current;
			if (this.previousEventSystem != null)
			{
				this.previousEventSystem.enabled = false;
				VRTK_EventSystem.CopyValuesFrom(this.previousEventSystem, this);
			}
			this.vrInputModule = base.gameObject.AddComponent<VRTK_VRInputModule>();
			base.OnEnable();
			base.StartCoroutine(VRTK_EventSystem.SetEventSystemOfBaseInputModulesAfterFrameDelay(this));
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0007BFB8 File Offset: 0x0007A1B8
		protected override void OnDisable()
		{
			base.OnDisable();
			Object.Destroy(this.vrInputModule);
			if (this.previousEventSystem != null)
			{
				this.previousEventSystem.enabled = true;
				VRTK_EventSystem.CopyValuesFrom(this, this.previousEventSystem);
				VRTK_EventSystem.SetEventSystemOfBaseInputModules(this.previousEventSystem);
			}
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0007C007 File Offset: 0x0007A207
		protected override void Update()
		{
			base.Update();
			if (EventSystem.current == this)
			{
				this.vrInputModule.Process();
			}
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00003F60 File Offset: 0x00002160
		protected override void OnApplicationFocus(bool hasFocus)
		{
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0007C028 File Offset: 0x0007A228
		private static void CopyValuesFrom(EventSystem fromEventSystem, EventSystem toEventSystem)
		{
			foreach (FieldInfo fieldInfo in VRTK_EventSystem.EVENT_SYSTEM_FIELD_INFOS)
			{
				fieldInfo.SetValue(toEventSystem, fieldInfo.GetValue(fromEventSystem));
			}
			foreach (PropertyInfo propertyInfo in VRTK_EventSystem.EVENT_SYSTEM_PROPERTY_INFOS)
			{
				if (propertyInfo.CanWrite)
				{
					propertyInfo.SetValue(toEventSystem, propertyInfo.GetValue(fromEventSystem, null), null);
				}
			}
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0007C08F File Offset: 0x0007A28F
		private static IEnumerator SetEventSystemOfBaseInputModulesAfterFrameDelay(EventSystem eventSystem)
		{
			yield return null;
			VRTK_EventSystem.SetEventSystemOfBaseInputModules(eventSystem);
			yield break;
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0007C0A0 File Offset: 0x0007A2A0
		private static void SetEventSystemOfBaseInputModules(EventSystem eventSystem)
		{
			foreach (BaseInputModule obj in Object.FindObjectsOfType<BaseInputModule>())
			{
				VRTK_EventSystem.BASE_INPUT_MODULE_EVENT_SYSTEM_FIELD_INFO.SetValue(obj, eventSystem);
			}
			eventSystem.UpdateModules();
		}

		// Token: 0x040012F0 RID: 4848
		protected EventSystem previousEventSystem;

		// Token: 0x040012F1 RID: 4849
		protected VRTK_VRInputModule vrInputModule;

		// Token: 0x040012F2 RID: 4850
		private static readonly FieldInfo[] EVENT_SYSTEM_FIELD_INFOS = typeof(EventSystem).GetFields(BindingFlags.Instance | BindingFlags.Public);

		// Token: 0x040012F3 RID: 4851
		private static readonly PropertyInfo[] EVENT_SYSTEM_PROPERTY_INFOS = typeof(EventSystem).GetProperties(BindingFlags.Instance | BindingFlags.Public).Except(new PropertyInfo[]
		{
			typeof(EventSystem).GetProperty("enabled")
		}).ToArray<PropertyInfo>();

		// Token: 0x040012F4 RID: 4852
		private static readonly FieldInfo BASE_INPUT_MODULE_EVENT_SYSTEM_FIELD_INFO = typeof(BaseInputModule).GetField("m_EventSystem", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
