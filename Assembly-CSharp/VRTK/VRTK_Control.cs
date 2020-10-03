using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK
{
	// Token: 0x0200028E RID: 654
	[ExecuteInEditMode]
	public abstract class VRTK_Control : MonoBehaviour
	{
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060013EF RID: 5103 RVA: 0x0006DEB8 File Offset: 0x0006C0B8
		// (remove) Token: 0x060013F0 RID: 5104 RVA: 0x0006DEF0 File Offset: 0x0006C0F0
		public event Control3DEventHandler ValueChanged;

		// Token: 0x060013F1 RID: 5105
		protected abstract void InitRequiredComponents();

		// Token: 0x060013F2 RID: 5106
		protected abstract bool DetectSetup();

		// Token: 0x060013F3 RID: 5107
		protected abstract VRTK_Control.ControlValueRange RegisterValueRange();

		// Token: 0x060013F4 RID: 5108 RVA: 0x0006DF25 File Offset: 0x0006C125
		public virtual void OnValueChanged(Control3DEventArgs e)
		{
			if (this.ValueChanged != null)
			{
				this.ValueChanged(this, e);
			}
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0006DF3C File Offset: 0x0006C13C
		public virtual float GetValue()
		{
			return this.value;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0006DF44 File Offset: 0x0006C144
		public virtual float GetNormalizedValue()
		{
			return Mathf.Abs(Mathf.Round((this.value - this.valueRange.controlMin) / (this.valueRange.controlMax - this.valueRange.controlMin) * 100f));
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0006DF80 File Offset: 0x0006C180
		public virtual void SetContent(GameObject content, bool hideContent)
		{
			this.controlContent = content;
			this.hideControlContent = hideContent;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0006DF90 File Offset: 0x0006C190
		public virtual GameObject GetContent()
		{
			return this.controlContent;
		}

		// Token: 0x060013F9 RID: 5113
		protected abstract void HandleUpdate();

		// Token: 0x060013FA RID: 5114 RVA: 0x0006DF98 File Offset: 0x0006C198
		protected virtual void Awake()
		{
			if (Application.isPlaying)
			{
				this.InitRequiredComponents();
				if (this.interactWithoutGrab)
				{
					this.CreateTriggerVolume();
				}
			}
			this.setupSuccessful = this.DetectSetup();
			if (Application.isPlaying)
			{
				this.valueRange = this.RegisterValueRange();
				this.HandleInteractables();
			}
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0006DFE8 File Offset: 0x0006C1E8
		protected virtual void Update()
		{
			if (!Application.isPlaying)
			{
				this.setupSuccessful = this.DetectSetup();
				return;
			}
			if (this.setupSuccessful)
			{
				float num = this.value;
				this.HandleUpdate();
				if (this.value != num)
				{
					this.HandleInteractables();
					this.defaultEvents.OnValueChanged.Invoke(this.GetValue(), this.GetNormalizedValue());
					this.OnValueChanged(this.SetControlEvent());
				}
			}
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0006E058 File Offset: 0x0006C258
		protected virtual Control3DEventArgs SetControlEvent()
		{
			Control3DEventArgs result;
			result.value = this.GetValue();
			result.normalizedValue = this.GetNormalizedValue();
			return result;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0006E080 File Offset: 0x0006C280
		protected virtual void OnDrawGizmos()
		{
			if (!base.enabled)
			{
				return;
			}
			this.bounds = VRTK_SharedMethods.GetBounds(base.transform, null, null);
			Gizmos.color = (this.setupSuccessful ? VRTK_Control.COLOR_OK : VRTK_Control.COLOR_ERROR);
			if (this.setupSuccessful)
			{
				Gizmos.DrawWireCube(this.bounds.center, this.bounds.size);
				return;
			}
			Gizmos.DrawCube(this.bounds.center, this.bounds.size * 1.01f);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0006E10C File Offset: 0x0006C30C
		protected virtual void CreateTriggerVolume()
		{
			GameObject gameObject = new GameObject(base.name + "-Trigger");
			gameObject.transform.SetParent(base.transform);
			this.autoTriggerVolume = gameObject.AddComponent<VRTK_ControllerRigidbodyActivator>();
			Bounds bounds = VRTK_SharedMethods.GetBounds(base.transform, null, null);
			bounds.Expand(bounds.size * 0.2f);
			gameObject.transform.position = bounds.center;
			BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
			boxCollider.isTrigger = true;
			boxCollider.size = bounds.size;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0006E1A0 File Offset: 0x0006C3A0
		protected Vector3 GetThirdDirection(Vector3 axis1, Vector3 axis2)
		{
			bool flag = axis1.x != 0f || axis2.x != 0f;
			bool flag2 = axis1.y != 0f || axis2.y != 0f;
			bool flag3 = axis1.z != 0f || axis2.z != 0f;
			if (flag && flag2)
			{
				return Vector3.forward;
			}
			if (flag && flag3)
			{
				return Vector3.up;
			}
			return Vector3.right;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0006E22C File Offset: 0x0006C42C
		protected virtual void HandleInteractables()
		{
			if (this.controlContent == null)
			{
				return;
			}
			if (this.hideControlContent)
			{
				this.controlContent.SetActive(this.value > 0f);
			}
			VRTK_InteractableObject[] componentsInChildren = this.controlContent.GetComponentsInChildren<VRTK_InteractableObject>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = (this.value > 20f);
			}
		}

		// Token: 0x04001125 RID: 4389
		[Tooltip("The default events for the control. This parameter is deprecated and will be removed in a future version of VRTK.")]
		[Obsolete("`VRTK_Control.defaultEvents` has been replaced with delegate events. `VRTK_Control_UnityEvents` is now required to access Unity events. This method will be removed in a future version of VRTK.")]
		public VRTK_Control.DefaultControlEvents defaultEvents;

		// Token: 0x04001126 RID: 4390
		[Tooltip("If active the control will react to the controller without the need to push the grab button.")]
		public bool interactWithoutGrab;

		// Token: 0x04001128 RID: 4392
		protected Bounds bounds;

		// Token: 0x04001129 RID: 4393
		protected bool setupSuccessful = true;

		// Token: 0x0400112A RID: 4394
		protected VRTK_ControllerRigidbodyActivator autoTriggerVolume;

		// Token: 0x0400112B RID: 4395
		protected float value;

		// Token: 0x0400112C RID: 4396
		protected static Color COLOR_OK = Color.yellow;

		// Token: 0x0400112D RID: 4397
		protected static Color COLOR_ERROR = new Color(1f, 0f, 0f, 0.9f);

		// Token: 0x0400112E RID: 4398
		protected const float MIN_OPENING_DISTANCE = 20f;

		// Token: 0x0400112F RID: 4399
		protected VRTK_Control.ControlValueRange valueRange;

		// Token: 0x04001130 RID: 4400
		protected GameObject controlContent;

		// Token: 0x04001131 RID: 4401
		protected bool hideControlContent;

		// Token: 0x020005C8 RID: 1480
		[Obsolete("`VRTK_Control.ValueChangedEvent` has been replaced with delegate events. `VRTK_Control_UnityEvents` is now required to access Unity events. This method will be removed in a future version of VRTK.")]
		[Serializable]
		public class ValueChangedEvent : UnityEvent<float, float>
		{
		}

		// Token: 0x020005C9 RID: 1481
		[Obsolete("`VRTK_Control.DefaultControlEvents` has been replaced with delegate events. `VRTK_Control_UnityEvents` is now required to access Unity events. This method will be removed in a future version of VRTK.")]
		[Serializable]
		public class DefaultControlEvents
		{
			// Token: 0x0400274A RID: 10058
			public VRTK_Control.ValueChangedEvent OnValueChanged;
		}

		// Token: 0x020005CA RID: 1482
		public struct ControlValueRange
		{
			// Token: 0x0400274B RID: 10059
			public float controlMin;

			// Token: 0x0400274C RID: 10060
			public float controlMax;
		}

		// Token: 0x020005CB RID: 1483
		public enum Direction
		{
			// Token: 0x0400274E RID: 10062
			autodetect,
			// Token: 0x0400274F RID: 10063
			x,
			// Token: 0x04002750 RID: 10064
			y,
			// Token: 0x04002751 RID: 10065
			z
		}
	}
}
