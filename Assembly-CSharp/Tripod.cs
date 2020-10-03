using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200016E RID: 366
public class Tripod : MonoBehaviour
{
	// Token: 0x060009C2 RID: 2498 RVA: 0x0003C106 File Offset: 0x0003A306
	private void Awake()
	{
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		this.body = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0003C120 File Offset: 0x0003A320
	private void Start()
	{
		this.photonInteract.AddUnGrabbedEvent(new UnityAction(this.UnGrab));
		this.photonInteract.AddPCUnGrabbedEvent(new UnityAction(this.UnGrab));
		this.photonInteract.AddGrabbedEvent(new UnityAction(this.Grab));
		this.body.constraints = (RigidbodyConstraints)122;
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x0003C180 File Offset: 0x0003A380
	public void UnGrab()
	{
		this.body.constraints = (RigidbodyConstraints)122;
		Quaternion rotation = base.transform.rotation;
		Vector3 eulerAngles = rotation.eulerAngles;
		eulerAngles = new Vector3(0f, eulerAngles.y, 0f);
		rotation.eulerAngles = eulerAngles;
		base.transform.rotation = rotation;
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0003C1D9 File Offset: 0x0003A3D9
	private void Grab()
	{
		this.body.constraints = RigidbodyConstraints.None;
	}

	// Token: 0x040009DD RID: 2525
	private PhotonObjectInteract photonInteract;

	// Token: 0x040009DE RID: 2526
	private Rigidbody body;

	// Token: 0x040009DF RID: 2527
	public Transform snapZone;
}
