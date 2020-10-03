using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000140 RID: 320
public class WorldUIPointer : StandaloneInputModule
{
	// Token: 0x0600084E RID: 2126 RVA: 0x00031967 File Offset: 0x0002FB67
	public void UpdateCursorPosition()
	{
		this.ActivateModule();
		this.Process();
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x00031978 File Offset: 0x0002FB78
	protected override PointerInputModule.MouseState GetMousePointerEventData(int id = 0)
	{
		PointerEventData pointerEventData;
		bool pointerData = base.GetPointerData(-1, out pointerEventData, true);
		pointerEventData.Reset();
		if (pointerData)
		{
			pointerEventData.position = Input.mousePosition;
		}
		pointerEventData.position = Input.mousePosition;
		Vector2 vector = Input.mousePosition;
		pointerEventData.delta = vector - pointerEventData.position;
		pointerEventData.position = vector;
		pointerEventData.scrollDelta = Input.mouseScrollDelta;
		pointerEventData.button = PointerEventData.InputButton.Left;
		base.eventSystem.RaycastAll(pointerEventData, this.m_RaycastResultCache);
		RaycastResult pointerCurrentRaycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
		pointerEventData.pointerCurrentRaycast = pointerCurrentRaycast;
		this.m_RaycastResultCache.Clear();
		PointerEventData pointerEventData2;
		base.GetPointerData(-2, out pointerEventData2, true);
		base.CopyFromTo(pointerEventData, pointerEventData2);
		pointerEventData2.button = PointerEventData.InputButton.Right;
		PointerEventData pointerEventData3;
		base.GetPointerData(-3, out pointerEventData3, true);
		base.CopyFromTo(pointerEventData, pointerEventData3);
		pointerEventData3.button = PointerEventData.InputButton.Middle;
		this.m_MouseState.SetButtonState(PointerEventData.InputButton.Left, base.StateForMouseButton(0), pointerEventData);
		this.m_MouseState.SetButtonState(PointerEventData.InputButton.Right, base.StateForMouseButton(1), pointerEventData2);
		this.m_MouseState.SetButtonState(PointerEventData.InputButton.Middle, base.StateForMouseButton(2), pointerEventData3);
		return this.m_MouseState;
	}

	// Token: 0x04000864 RID: 2148
	private readonly PointerInputModule.MouseState m_MouseState = new PointerInputModule.MouseState();
}
