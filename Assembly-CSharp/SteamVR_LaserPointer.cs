using System;
using UnityEngine;

// Token: 0x020001D7 RID: 471
public class SteamVR_LaserPointer : MonoBehaviour
{
	// Token: 0x14000004 RID: 4
	// (add) Token: 0x06000CCA RID: 3274 RVA: 0x000513C0 File Offset: 0x0004F5C0
	// (remove) Token: 0x06000CCB RID: 3275 RVA: 0x000513F8 File Offset: 0x0004F5F8
	public event PointerEventHandler PointerIn;

	// Token: 0x14000005 RID: 5
	// (add) Token: 0x06000CCC RID: 3276 RVA: 0x00051430 File Offset: 0x0004F630
	// (remove) Token: 0x06000CCD RID: 3277 RVA: 0x00051468 File Offset: 0x0004F668
	public event PointerEventHandler PointerOut;

	// Token: 0x06000CCE RID: 3278 RVA: 0x000514A0 File Offset: 0x0004F6A0
	private void Start()
	{
		this.holder = new GameObject();
		this.holder.transform.parent = base.transform;
		this.holder.transform.localPosition = Vector3.zero;
		this.holder.transform.localRotation = Quaternion.identity;
		this.pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
		this.pointer.transform.parent = this.holder.transform;
		this.pointer.transform.localScale = new Vector3(this.thickness, this.thickness, 100f);
		this.pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
		this.pointer.transform.localRotation = Quaternion.identity;
		BoxCollider component = this.pointer.GetComponent<BoxCollider>();
		if (this.addRigidBody)
		{
			if (component)
			{
				component.isTrigger = true;
			}
			this.pointer.AddComponent<Rigidbody>().isKinematic = true;
		}
		else if (component)
		{
			Object.Destroy(component);
		}
		Material material = new Material(Shader.Find("Unlit/Color"));
		material.SetColor("_Color", this.color);
		this.pointer.GetComponent<MeshRenderer>().material = material;
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x000515F4 File Offset: 0x0004F7F4
	public virtual void OnPointerIn(PointerEventArgs e)
	{
		if (this.PointerIn != null)
		{
			this.PointerIn(this, e);
		}
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0005160B File Offset: 0x0004F80B
	public virtual void OnPointerOut(PointerEventArgs e)
	{
		if (this.PointerOut != null)
		{
			this.PointerOut(this, e);
		}
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x00051624 File Offset: 0x0004F824
	private void Update()
	{
		if (!this.isActive)
		{
			this.isActive = true;
			base.transform.GetChild(0).gameObject.SetActive(true);
		}
		float num = 100f;
		SteamVR_TrackedController component = base.GetComponent<SteamVR_TrackedController>();
		RaycastHit raycastHit;
		bool flag = Physics.Raycast(new Ray(base.transform.position, base.transform.forward), out raycastHit);
		if (this.previousContact && this.previousContact != raycastHit.transform)
		{
			PointerEventArgs e = default(PointerEventArgs);
			if (component != null)
			{
				e.controllerIndex = component.controllerIndex;
			}
			e.distance = 0f;
			e.flags = 0U;
			e.target = this.previousContact;
			this.OnPointerOut(e);
			this.previousContact = null;
		}
		if (flag && this.previousContact != raycastHit.transform)
		{
			PointerEventArgs e2 = default(PointerEventArgs);
			if (component != null)
			{
				e2.controllerIndex = component.controllerIndex;
			}
			e2.distance = raycastHit.distance;
			e2.flags = 0U;
			e2.target = raycastHit.transform;
			this.OnPointerIn(e2);
			this.previousContact = raycastHit.transform;
		}
		if (!flag)
		{
			this.previousContact = null;
		}
		if (flag && raycastHit.distance < 100f)
		{
			num = raycastHit.distance;
		}
		if (component != null && component.triggerPressed)
		{
			this.pointer.transform.localScale = new Vector3(this.thickness * 5f, this.thickness * 5f, num);
		}
		else
		{
			this.pointer.transform.localScale = new Vector3(this.thickness, this.thickness, num);
		}
		this.pointer.transform.localPosition = new Vector3(0f, 0f, num / 2f);
	}

	// Token: 0x04000D71 RID: 3441
	public bool active = true;

	// Token: 0x04000D72 RID: 3442
	public Color color;

	// Token: 0x04000D73 RID: 3443
	public float thickness = 0.002f;

	// Token: 0x04000D74 RID: 3444
	public GameObject holder;

	// Token: 0x04000D75 RID: 3445
	public GameObject pointer;

	// Token: 0x04000D76 RID: 3446
	private bool isActive;

	// Token: 0x04000D77 RID: 3447
	public bool addRigidBody;

	// Token: 0x04000D78 RID: 3448
	public Transform reference;

	// Token: 0x04000D7B RID: 3451
	private Transform previousContact;
}
