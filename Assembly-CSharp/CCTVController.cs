using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000111 RID: 273
[RequireComponent(typeof(PhotonView))]
public class CCTVController : MonoBehaviour
{
	// Token: 0x06000736 RID: 1846 RVA: 0x0002A114 File Offset: 0x00028314
	private void Awake()
	{
		CCTVController.instance = this;
		this.cctvCameras.Clear();
		this.view = base.GetComponent<PhotonView>();
		this.texture = this.screen.material.GetTexture("_EmissionMap");
		this.truckKeyboardPhotonInteract.AddUseEvent(new UnityAction(this.ChangeNightVision));
		this.truckMousePhotonInteract.AddUseEvent(new UnityAction(this.NextCamera));
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x0002A188 File Offset: 0x00028388
	private void Start()
	{
		GameController.instance.OnAllPlayersConnected.AddListener(new UnityAction(this.OnAllPlayersConnected));
		for (int i = 0; i < this.allFixedCCTVCameras.Count; i++)
		{
			this.allcctvCameras.Add(this.allFixedCCTVCameras[i]);
		}
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0002A1DD File Offset: 0x000283DD
	private void OnAllPlayersConnected()
	{
		if (PhotonNetwork.isMasterClient)
		{
			base.Invoke("ActivateCamerasAfterTimeLimit", 5f);
		}
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x0002A1F8 File Offset: 0x000283F8
	public void AddCamera(CCTV c)
	{
		if (!this.cctvCameras.Contains(c))
		{
			this.cctvCameras.Add(c);
		}
		if (this.screen)
		{
			this.screen.material.color = Color.white;
		}
		this.RemoveDeactivatedCameras();
		this.NextCamera();
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x0002A250 File Offset: 0x00028450
	public void RemoveCamera(CCTV c)
	{
		if (c.isFixedCamera)
		{
			return;
		}
		if (this.cctvCameras.Contains(c))
		{
			this.cctvCameras.Remove(c);
		}
		if (this.cctvCameras.Count == 0)
		{
			if (this.screen)
			{
				this.screen.material.color = Color.black;
				this.screen.material.SetTexture("_EmissionMap", this.texture);
			}
			if (this.screenText)
			{
				this.screenText.text = "00/00";
			}
		}
		this.RemoveDeactivatedCameras();
		this.NextCamera();
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x0002A2F4 File Offset: 0x000284F4
	private void RemoveDeactivatedCameras()
	{
		bool flag = false;
		for (int i = 0; i < this.cctvCameras.Count; i++)
		{
			if (this.cctvCameras[i] == null)
			{
				this.cctvCameras.RemoveAt(i);
				flag = true;
			}
			if (this.cctvCameras.Count == 0)
			{
				this.screen.material.color = Color.black;
				this.screen.material.SetTexture("_EmissionMap", this.texture);
				this.screenText.text = "00/00";
				return;
			}
			if (flag)
			{
				return;
			}
		}
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0002A390 File Offset: 0x00028590
	public void StartRendering()
	{
		this.isRendering = true;
		this.RemoveDeactivatedCameras();
		if (this.cctvCameras.Count > 0 && this.cctvCameras[this.index] != null)
		{
			this.cctvCameras[this.index].cam.enabled = true;
			this.cctvCameras[this.index].myLight.enabled = true;
		}
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0002A40C File Offset: 0x0002860C
	public void StopRendering()
	{
		this.isRendering = false;
		this.RemoveDeactivatedCameras();
		for (int i = 0; i < this.allcctvCameras.Count; i++)
		{
			if (this.allcctvCameras[i] != null)
			{
				this.allcctvCameras[i].cam.enabled = false;
				this.allcctvCameras[i].myLight.enabled = false;
			}
		}
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x0002A480 File Offset: 0x00028680
	private void ActivateCamerasAfterTimeLimit()
	{
		for (int i = 0; i < this.allFixedCCTVCameras.Count; i++)
		{
			this.allFixedCCTVCameras[i].Use();
		}
		this.view.RPC("ActivatedCameras", PhotonTargets.AllBuffered, Array.Empty<object>());
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x0002A4CA File Offset: 0x000286CA
	[PunRPC]
	private void ActivatedCameras()
	{
		this.activatedCCTVCameras = true;
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0002A4D4 File Offset: 0x000286D4
	private void ChangeNightVision()
	{
		if (GameController.instance.allPlayersAreConnected && !GameController.instance.myPlayer.player.isDead)
		{
			this.view.RPC("NetworkedChangeNightVision", PhotonTargets.All, new object[]
			{
				!CCTVController.showNightVisionEffect
			});
		}
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x0002A52C File Offset: 0x0002872C
	[PunRPC]
	private void NetworkedChangeNightVision(bool on)
	{
		CCTVController.showNightVisionEffect = on;
		for (int i = 0; i < this.allcctvCameras.Count; i++)
		{
			if (this.allcctvCameras[i] != null && this.allcctvCameras[i].cam != null && this.allcctvCameras[i].cam.GetComponent<Nightvision>())
			{
				this.allcctvCameras[i].cam.GetComponent<Nightvision>().enabled = on;
			}
		}
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x0002A5BC File Offset: 0x000287BC
	public void NextCamera()
	{
		if (GameController.instance && GameController.instance.myPlayer != null && !GameController.instance.myPlayer.player.isDead)
		{
			this.view.RPC("ChangeCameraNetworked", PhotonTargets.MasterClient, Array.Empty<object>());
		}
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x0002A610 File Offset: 0x00028810
	[PunRPC]
	private void ChangeCameraNetworked()
	{
		if (this.cctvCameras.Count == 0)
		{
			return;
		}
		this.index++;
		if (this.index >= this.cctvCameras.Count)
		{
			this.index = 0;
		}
		this.view.RPC("SyncCameraNetworked", PhotonTargets.All, new object[]
		{
			this.index
		});
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x0002A678 File Offset: 0x00028878
	[PunRPC]
	private void SyncCameraNetworked(int newIndex)
	{
		this.RemoveDeactivatedCameras();
		this.index = newIndex;
		if (this.index >= this.cctvCameras.Count)
		{
			this.index = 0;
		}
		for (int i = 0; i < this.cctvCameras.Count; i++)
		{
			if (this.cctvCameras[i] != null)
			{
				this.cctvCameras[i].cam.enabled = false;
				this.cctvCameras[i].myLight.enabled = false;
				this.cctvCameras[i].isThisCameraActiveOnACCTVScreen = false;
				if (this.cctvCameras[i].mapIcon != null)
				{
					this.cctvCameras[i].mapIcon.color = Color.yellow;
				}
			}
		}
		if (this.cctvCameras[this.index] != null)
		{
			if (this.isRendering)
			{
				this.cctvCameras[this.index].cam.enabled = true;
				this.cctvCameras[this.index].myLight.enabled = true;
			}
			this.cctvCameras[this.index].isThisCameraActiveOnACCTVScreen = true;
			if (this.cctvCameras[this.index].isFixedCamera)
			{
				this.cctvCameras[this.index].mapIcon.color = Color.green;
			}
			this.screen.material.mainTexture = this.cctvCameras[this.index].cam.targetTexture;
			this.screen.material.SetTexture("_EmissionMap", this.cctvCameras[this.index].cam.targetTexture);
		}
		this.screenText.text = (this.index + 1).ToString("00") + "/" + this.cctvCameras.Count.ToString("00");
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x0002A898 File Offset: 0x00028A98
	private void PreviousCamera()
	{
		if (this.cctvCameras.Count == 0)
		{
			return;
		}
		this.index--;
		if (this.index < 0)
		{
			this.index = this.cctvCameras.Count - 1;
		}
		for (int i = 0; i < this.cctvCameras.Count; i++)
		{
			if (this.cctvCameras[i] != null)
			{
				this.cctvCameras[i].isThisCameraActiveOnACCTVScreen = false;
			}
		}
		if (this.cctvCameras[this.index] != null)
		{
			this.cctvCameras[this.index].cam.Render();
			this.cctvCameras[this.index].isThisCameraActiveOnACCTVScreen = true;
		}
	}

	// Token: 0x040006DF RID: 1759
	public static CCTVController instance;

	// Token: 0x040006E0 RID: 1760
	[HideInInspector]
	public PhotonView view;

	// Token: 0x040006E1 RID: 1761
	public Renderer screen;

	// Token: 0x040006E2 RID: 1762
	[HideInInspector]
	public List<CCTV> cctvCameras = new List<CCTV>();

	// Token: 0x040006E3 RID: 1763
	[HideInInspector]
	public int index;

	// Token: 0x040006E4 RID: 1764
	private Texture texture;

	// Token: 0x040006E5 RID: 1765
	[SerializeField]
	private PhotonObjectInteract truckKeyboardPhotonInteract;

	// Token: 0x040006E6 RID: 1766
	[SerializeField]
	private PhotonObjectInteract truckMousePhotonInteract;

	// Token: 0x040006E7 RID: 1767
	[SerializeField]
	private Text screenText;

	// Token: 0x040006E8 RID: 1768
	public List<CCTV> allFixedCCTVCameras = new List<CCTV>();

	// Token: 0x040006E9 RID: 1769
	[HideInInspector]
	public bool activatedCCTVCameras;

	// Token: 0x040006EA RID: 1770
	private static bool showNightVisionEffect = true;

	// Token: 0x040006EB RID: 1771
	[HideInInspector]
	public List<CCTV> allcctvCameras = new List<CCTV>();

	// Token: 0x040006EC RID: 1772
	private bool isRendering = true;
}
