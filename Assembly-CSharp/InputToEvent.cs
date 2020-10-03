using System;
using UnityEngine;

// Token: 0x020000CD RID: 205
public class InputToEvent : MonoBehaviour
{
	// Token: 0x170000DD RID: 221
	// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00020BF2 File Offset: 0x0001EDF2
	// (set) Token: 0x060005C3 RID: 1475 RVA: 0x00020BF9 File Offset: 0x0001EDF9
	public static GameObject goPointedAt { get; private set; }

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00020C01 File Offset: 0x0001EE01
	public Vector2 DragVector
	{
		get
		{
			if (!this.Dragging)
			{
				return Vector2.zero;
			}
			return this.currentPos - this.pressedPosition;
		}
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x00020C22 File Offset: 0x0001EE22
	private void Start()
	{
		this.m_Camera = base.GetComponent<Camera>();
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x00020C30 File Offset: 0x0001EE30
	private void Update()
	{
		if (this.DetectPointedAtGameObject)
		{
			InputToEvent.goPointedAt = this.RaycastObject(Input.mousePosition);
		}
		if (Input.touchCount <= 0)
		{
			this.currentPos = Input.mousePosition;
			if (Input.GetMouseButtonDown(0))
			{
				this.Press(Input.mousePosition);
			}
			if (Input.GetMouseButtonUp(0))
			{
				this.Release(Input.mousePosition);
			}
			if (Input.GetMouseButtonDown(1))
			{
				this.pressedPosition = Input.mousePosition;
				this.lastGo = this.RaycastObject(this.pressedPosition);
				if (this.lastGo != null)
				{
					this.lastGo.SendMessage("OnPressRight", SendMessageOptions.DontRequireReceiver);
				}
			}
			return;
		}
		Touch touch = Input.GetTouch(0);
		this.currentPos = touch.position;
		if (touch.phase == TouchPhase.Began)
		{
			this.Press(touch.position);
			return;
		}
		if (touch.phase == TouchPhase.Ended)
		{
			this.Release(touch.position);
		}
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x00020D2E File Offset: 0x0001EF2E
	private void Press(Vector2 screenPos)
	{
		this.pressedPosition = screenPos;
		this.Dragging = true;
		this.lastGo = this.RaycastObject(screenPos);
		if (this.lastGo != null)
		{
			this.lastGo.SendMessage("OnPress", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x00020D6C File Offset: 0x0001EF6C
	private void Release(Vector2 screenPos)
	{
		if (this.lastGo != null)
		{
			if (this.RaycastObject(screenPos) == this.lastGo)
			{
				this.lastGo.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
			}
			this.lastGo.SendMessage("OnRelease", SendMessageOptions.DontRequireReceiver);
			this.lastGo = null;
		}
		this.pressedPosition = Vector2.zero;
		this.Dragging = false;
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x00020DD8 File Offset: 0x0001EFD8
	private GameObject RaycastObject(Vector2 screenPos)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(this.m_Camera.ScreenPointToRay(screenPos), out raycastHit, 200f))
		{
			InputToEvent.inputHitPos = raycastHit.point;
			return raycastHit.collider.gameObject;
		}
		return null;
	}

	// Token: 0x040005DE RID: 1502
	private GameObject lastGo;

	// Token: 0x040005DF RID: 1503
	public static Vector3 inputHitPos;

	// Token: 0x040005E0 RID: 1504
	public bool DetectPointedAtGameObject;

	// Token: 0x040005E2 RID: 1506
	private Vector2 pressedPosition = Vector2.zero;

	// Token: 0x040005E3 RID: 1507
	private Vector2 currentPos = Vector2.zero;

	// Token: 0x040005E4 RID: 1508
	public bool Dragging;

	// Token: 0x040005E5 RID: 1509
	private Camera m_Camera;
}
