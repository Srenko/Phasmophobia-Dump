using System;
using Photon;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class ClickAndDrag : Photon.MonoBehaviour
{
	// Token: 0x06000174 RID: 372 RVA: 0x0000A694 File Offset: 0x00008894
	private void Update()
	{
		if (!base.photonView.isMine)
		{
			return;
		}
		InputToEvent component = Camera.main.GetComponent<InputToEvent>();
		if (component == null)
		{
			return;
		}
		if (!this.following)
		{
			if (component.Dragging)
			{
				this.camOnPress = base.transform.position;
				this.following = true;
				return;
			}
			return;
		}
		else
		{
			if (component.Dragging)
			{
				Vector3 b = this.camOnPress - new Vector3(component.DragVector.x, 0f, component.DragVector.y) * this.factor;
				base.transform.position = Vector3.Lerp(base.transform.position, b, Time.deltaTime * 0.5f);
				return;
			}
			this.camOnPress = Vector3.zero;
			this.following = false;
			return;
		}
	}

	// Token: 0x040001AF RID: 431
	private Vector3 camOnPress;

	// Token: 0x040001B0 RID: 432
	private bool following;

	// Token: 0x040001B1 RID: 433
	private float factor = -0.1f;
}
