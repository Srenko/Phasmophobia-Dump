using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000258 RID: 600
	public class VRTK_PointerDirectionIndicator : MonoBehaviour
	{
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060011B0 RID: 4528 RVA: 0x00066AA4 File Offset: 0x00064CA4
		// (remove) Token: 0x060011B1 RID: 4529 RVA: 0x00066ADC File Offset: 0x00064CDC
		public event PointerDirectionIndicatorEventHandler PointerDirectionIndicatorPositionSet;

		// Token: 0x060011B2 RID: 4530 RVA: 0x00066B11 File Offset: 0x00064D11
		public virtual void OnPointerDirectionIndicatorPositionSet()
		{
			if (this.PointerDirectionIndicatorPositionSet != null)
			{
				this.PointerDirectionIndicatorPositionSet(this);
			}
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00066B27 File Offset: 0x00064D27
		public virtual void Initialize(VRTK_ControllerEvents events)
		{
			this.controllerEvents = events;
			this.playArea = VRTK_DeviceFinder.PlayAreaTransform();
			this.headset = VRTK_DeviceFinder.HeadsetTransform();
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00066B46 File Offset: 0x00064D46
		public virtual void SetPosition(bool active, Vector3 position)
		{
			base.transform.position = position;
			base.gameObject.SetActive(this.isActive && active);
			this.OnPointerDirectionIndicatorPositionSet();
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00066B70 File Offset: 0x00064D70
		public virtual Quaternion GetRotation()
		{
			float num = this.includeHeadsetOffset ? (this.playArea.eulerAngles.y - this.headset.eulerAngles.y) : 0f;
			return Quaternion.Euler(0f, base.transform.localEulerAngles.y + num, 0f);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00066BD0 File Offset: 0x00064DD0
		public virtual void SetMaterialColor(Color color, bool validity)
		{
			this.validLocation.SetActive(validity);
			this.invalidLocation.SetActive(this.displayOnInvalidLocation ? (!validity) : validity);
			if (this.usePointerColor)
			{
				Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].material.color = color;
				}
			}
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00066C30 File Offset: 0x00064E30
		protected virtual void Awake()
		{
			this.validLocation = base.transform.Find("ValidLocation").gameObject;
			this.invalidLocation = base.transform.Find("InvalidLocation").gameObject;
			base.gameObject.SetActive(false);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00066C80 File Offset: 0x00064E80
		protected virtual void Update()
		{
			if (this.controllerEvents != null)
			{
				float touchpadAxisAngle = this.controllerEvents.GetTouchpadAxisAngle();
				float y = ((touchpadAxisAngle > 180f) ? (touchpadAxisAngle - 360f) : touchpadAxisAngle) + this.headset.eulerAngles.y;
				base.transform.localEulerAngles = new Vector3(0f, y, 0f);
			}
		}

		// Token: 0x04001057 RID: 4183
		[Header("Appearance Settings")]
		[Tooltip("If this is checked then the reported rotation will include the offset of the headset rotation in relation to the play area.")]
		public bool includeHeadsetOffset = true;

		// Token: 0x04001058 RID: 4184
		[Tooltip("If this is checked then the direction indicator will be displayed when the location is invalid.")]
		public bool displayOnInvalidLocation = true;

		// Token: 0x04001059 RID: 4185
		[Tooltip("If this is checked then the pointer valid/invalid colours will also be used to change the colour of the direction indicator.")]
		public bool usePointerColor;

		// Token: 0x0400105A RID: 4186
		[HideInInspector]
		public bool isActive = true;

		// Token: 0x0400105C RID: 4188
		protected VRTK_ControllerEvents controllerEvents;

		// Token: 0x0400105D RID: 4189
		protected Transform playArea;

		// Token: 0x0400105E RID: 4190
		protected Transform headset;

		// Token: 0x0400105F RID: 4191
		protected GameObject validLocation;

		// Token: 0x04001060 RID: 4192
		protected GameObject invalidLocation;
	}
}
