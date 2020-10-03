using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x020001AF RID: 431
public class PCItemSway : MonoBehaviour
{
	// Token: 0x06000BB2 RID: 2994 RVA: 0x0004876E File Offset: 0x0004696E
	private void OnEnable()
	{
		this.SetPosition();
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x00048776 File Offset: 0x00046976
	public void SetPosition()
	{
		this.startPosition = base.transform.localPosition;
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x0004878C File Offset: 0x0004698C
	private void Update()
	{
		this.factorX = Mathf.Clamp(-this.horizontalLook * this.amount, -this.maxAmount, this.maxAmount);
		this.factorY = Mathf.Clamp(-this.verticalLook * this.amount, -this.maxAmount, this.maxAmount);
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, new Vector3(this.startPosition.x + this.factorX, this.startPosition.y + this.factorY, this.startPosition.z), Time.deltaTime * this.smooth);
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x00048840 File Offset: 0x00046A40
	public void Look(InputAction.CallbackContext context)
	{
		Vector2 vector = context.ReadValue<Vector2>();
		this.horizontalLook = vector.x;
		this.verticalLook = vector.y;
	}

	// Token: 0x04000C05 RID: 3077
	private float horizontalLook;

	// Token: 0x04000C06 RID: 3078
	private float verticalLook;

	// Token: 0x04000C07 RID: 3079
	[SerializeField]
	private float amount = 0.055f;

	// Token: 0x04000C08 RID: 3080
	[SerializeField]
	private float maxAmount = 0.055f;

	// Token: 0x04000C09 RID: 3081
	[SerializeField]
	private float smooth = 3f;

	// Token: 0x04000C0A RID: 3082
	private Vector3 startPosition;

	// Token: 0x04000C0B RID: 3083
	private float factorX;

	// Token: 0x04000C0C RID: 3084
	private float factorY;
}
