using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class IgnoreUiRaycastWhenInactive : MonoBehaviour, ICanvasRaycastFilter
{
	// Token: 0x060001B3 RID: 435 RVA: 0x0000BBC3 File Offset: 0x00009DC3
	public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
	{
		return base.gameObject.activeInHierarchy;
	}
}
