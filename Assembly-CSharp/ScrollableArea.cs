using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

// Token: 0x0200013C RID: 316
[RequireComponent(typeof(ScrollRect))]
public class ScrollableArea : MonoBehaviour, IMoveHandler, IEventSystemHandler
{
	// Token: 0x06000840 RID: 2112 RVA: 0x000314FA File Offset: 0x0002F6FA
	private void Awake()
	{
		if (XRDevice.isPresent)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x0003150C File Offset: 0x0002F70C
	public void OnMove(AxisEventData e)
	{
		this.xSpeed += e.moveVector.y * (Mathf.Abs(this.xSpeed) + 0.1f);
		this.ySpeed += e.moveVector.y * (Mathf.Abs(this.ySpeed) + 0.1f);
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x00031570 File Offset: 0x0002F770
	public void SetScroll(Vector2 scrollAmount)
	{
		if (scrollAmount.x != 0f)
		{
			this.xSpeed += scrollAmount.x * (Mathf.Abs(this.xSpeed) + 0.1f);
			this.hPos = this.scrollRect.horizontalNormalizedPosition + this.xSpeed * 0.4f;
			this.xSpeed = 0f;
			if (this.scrollRect.movementType == ScrollRect.MovementType.Clamped)
			{
				this.hPos = Mathf.Clamp01(this.hPos);
			}
		}
		else
		{
			this.hPos = this.scrollRect.normalizedPosition.x;
		}
		if (scrollAmount.y != 0f)
		{
			this.ySpeed += scrollAmount.y * (Mathf.Abs(this.ySpeed) + 0.1f);
			this.vPos = this.scrollRect.verticalNormalizedPosition + this.ySpeed * 0.4f;
			this.ySpeed = 0f;
			if (this.scrollRect.movementType == ScrollRect.MovementType.Clamped)
			{
				this.vPos = Mathf.Clamp01(this.vPos);
			}
		}
		else
		{
			this.vPos = this.scrollRect.normalizedPosition.y;
		}
		this.scrollRect.normalizedPosition = new Vector2(this.hPos, this.vPos);
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x000316C0 File Offset: 0x0002F8C0
	private void OnEnable()
	{
		if (!XRDevice.isPresent && MainManager.instance.localPlayer.playerInput.enabled)
		{
			MainManager.instance.localPlayer.playerInput.actions["Look"].performed += delegate(InputAction.CallbackContext ctx)
			{
				this.RightStickScroll(ctx);
			};
		}
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x0003171C File Offset: 0x0002F91C
	private void OnDisable()
	{
		if (!XRDevice.isPresent && MainManager.instance.localPlayer.playerInput.enabled)
		{
			MainManager.instance.localPlayer.playerInput.actions["Look"].performed -= delegate(InputAction.CallbackContext ctx)
			{
				this.RightStickScroll(ctx);
			};
		}
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x00031778 File Offset: 0x0002F978
	public void RightStickScroll(InputAction.CallbackContext context)
	{
		Vector2 scroll = context.ReadValue<Vector2>();
		if (MainManager.instance.localPlayer.playerInput.currentControlScheme == "Gamepad")
		{
			this.SetScroll(scroll);
		}
	}

	// Token: 0x0400085E RID: 2142
	[SerializeField]
	private ScrollRect scrollRect;

	// Token: 0x0400085F RID: 2143
	private const float speedMultiplier = 0.4f;

	// Token: 0x04000860 RID: 2144
	private float xSpeed;

	// Token: 0x04000861 RID: 2145
	private float ySpeed;

	// Token: 0x04000862 RID: 2146
	private float hPos;

	// Token: 0x04000863 RID: 2147
	private float vPos;
}
