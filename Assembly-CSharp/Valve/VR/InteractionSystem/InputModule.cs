using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000422 RID: 1058
	public class InputModule : BaseInputModule
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x000A0F0F File Offset: 0x0009F10F
		public static InputModule instance
		{
			get
			{
				if (InputModule._instance == null)
				{
					InputModule._instance = Object.FindObjectOfType<InputModule>();
				}
				return InputModule._instance;
			}
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000A0F2D File Offset: 0x0009F12D
		public override bool ShouldActivateModule()
		{
			return base.ShouldActivateModule() && this.submitObject != null;
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000A0F48 File Offset: 0x0009F148
		public void HoverBegin(GameObject gameObject)
		{
			PointerEventData eventData = new PointerEventData(base.eventSystem);
			ExecuteEvents.Execute<IPointerEnterHandler>(gameObject, eventData, ExecuteEvents.pointerEnterHandler);
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000A0F70 File Offset: 0x0009F170
		public void HoverEnd(GameObject gameObject)
		{
			ExecuteEvents.Execute<IPointerExitHandler>(gameObject, new PointerEventData(base.eventSystem)
			{
				selectedObject = null
			}, ExecuteEvents.pointerExitHandler);
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000A0F9D File Offset: 0x0009F19D
		public void Submit(GameObject gameObject)
		{
			this.submitObject = gameObject;
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x000A0FA8 File Offset: 0x0009F1A8
		public override void Process()
		{
			if (this.submitObject)
			{
				BaseEventData baseEventData = this.GetBaseEventData();
				baseEventData.selectedObject = this.submitObject;
				ExecuteEvents.Execute<ISubmitHandler>(this.submitObject, baseEventData, ExecuteEvents.submitHandler);
				this.submitObject = null;
			}
		}

		// Token: 0x04001E14 RID: 7700
		private GameObject submitObject;

		// Token: 0x04001E15 RID: 7701
		private static InputModule _instance;
	}
}
