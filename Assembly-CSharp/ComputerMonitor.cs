using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000143 RID: 323
[RequireComponent(typeof(Evidence))]
public class ComputerMonitor : MonoBehaviour
{
	// Token: 0x0600085E RID: 2142 RVA: 0x00031DDF File Offset: 0x0002FFDF
	private void Awake()
	{
		this.isOn = false;
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		this.view = base.GetComponent<PhotonView>();
		this.evidence = base.GetComponent<Evidence>();
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x00031E0C File Offset: 0x0003000C
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
		this.screen.material.mainTexture = this.tex;
		this.screen.material.SetTexture("_EmissionMap", this.tex);
		this.screen.material.DisableKeyword("_EMISSION");
		this.screen.material.color = Color.black;
		this.myLight.enabled = false;
		this.evidence.enabled = false;
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x00031EA3 File Offset: 0x000300A3
	private void Use()
	{
		this.view.RPC("ComputerMonitorNetworkedUse", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x00031EBC File Offset: 0x000300BC
	[PunRPC]
	private void ComputerMonitorNetworkedUse()
	{
		this.isOn = !this.isOn;
		this.evidence.enabled = this.isOn;
		this.myLight.enabled = this.isOn;
		this.screen.material.color = (this.isOn ? Color.white : Color.black);
		if (this.isOn)
		{
			this.screen.material.EnableKeyword("_EMISSION");
		}
		else
		{
			this.screen.material.DisableKeyword("_EMISSION");
		}
		this.mainRoomLight.ResetReflectionProbes();
	}

	// Token: 0x04000876 RID: 2166
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000877 RID: 2167
	private PhotonView view;

	// Token: 0x04000878 RID: 2168
	[SerializeField]
	private Renderer screen;

	// Token: 0x04000879 RID: 2169
	[SerializeField]
	private Texture tex;

	// Token: 0x0400087A RID: 2170
	[SerializeField]
	private Light myLight;

	// Token: 0x0400087B RID: 2171
	private Evidence evidence;

	// Token: 0x0400087C RID: 2172
	[SerializeField]
	private LightSwitch mainRoomLight;

	// Token: 0x0400087D RID: 2173
	private bool isOn;
}
