using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000139 RID: 313
public class FirstPersonInputModule : StandaloneInputModule
{
	// Token: 0x06000834 RID: 2100 RVA: 0x000311C4 File Offset: 0x0002F3C4
	protected override PointerInputModule.MouseState GetMousePointerEventData(int id)
	{
		CursorLockMode lockState = Cursor.lockState;
		Cursor.lockState = CursorLockMode.None;
		PointerInputModule.MouseState mousePointerEventData = base.GetMousePointerEventData(id);
		Cursor.lockState = lockState;
		return mousePointerEventData;
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x000311EA File Offset: 0x0002F3EA
	protected override void ProcessMove(PointerEventData pointerEvent)
	{
		CursorLockMode lockState = Cursor.lockState;
		Cursor.lockState = CursorLockMode.None;
		base.ProcessMove(pointerEvent);
		Cursor.lockState = lockState;
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x00031203 File Offset: 0x0002F403
	protected override void ProcessDrag(PointerEventData pointerEvent)
	{
		CursorLockMode lockState = Cursor.lockState;
		Cursor.lockState = CursorLockMode.None;
		base.ProcessDrag(pointerEvent);
		Cursor.lockState = lockState;
	}
}
