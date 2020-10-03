using System;
using UnityEngine;

// Token: 0x02000145 RID: 325
public class Drawer : MonoBehaviour
{
	// Token: 0x0600087E RID: 2174 RVA: 0x00032990 File Offset: 0x00030B90
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.body = base.GetComponent<Rigidbody>();
		this.startPos = base.transform.localPosition;
		this.startWorldPos = base.transform.position;
		this.closed = true;
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x000329E0 File Offset: 0x00030BE0
	private void Start()
	{
		if (this.loopSource)
		{
			this.loopSource.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		}
		if (this.closedSource)
		{
			this.closedSource.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		}
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x00032A54 File Offset: 0x00030C54
	private void Update()
	{
		if (this.isZ)
		{
			if (base.transform.localPosition.z < this.startPos.z)
			{
				this.pos = base.transform.localPosition;
				this.pos.z = this.startPos.z;
				base.transform.localPosition = this.pos;
			}
		}
		else if (this.isX)
		{
			if (base.transform.localPosition.x < this.startPos.x)
			{
				this.pos = base.transform.localPosition;
				this.pos.x = this.startPos.x;
				base.transform.localPosition = this.pos;
			}
		}
		else if (this.isY && base.transform.localPosition.y < this.startPos.y)
		{
			this.pos = base.transform.localPosition;
			this.pos.y = this.startPos.y;
			base.transform.localPosition = this.pos;
		}
		if (Time.frameCount % 3 == 0)
		{
			if (!this.closed)
			{
				if (!this.loopSource.isPlaying)
				{
					this.loopSource.Play();
				}
				this.velocity = (base.transform.position - this.oldPos).magnitude / Time.deltaTime;
				this.oldPos = base.transform.position;
				this.loopSource.volume = this.velocity / 6f;
				return;
			}
			if (this.loopSource.isPlaying)
			{
				this.loopSource.Stop();
			}
		}
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x00032C1C File Offset: 0x00030E1C
	public void UnGrab()
	{
		Vector3 localPosition = base.transform.localPosition;
		if (Vector3.Distance(base.transform.position, this.startWorldPos) <= 0.03f)
		{
			if (this.isZ)
			{
				localPosition.z = 0f;
			}
			else if (this.isY)
			{
				localPosition.y = 0f;
			}
			else if (this.isX)
			{
				localPosition.z = 0f;
			}
			this.view.RPC("NetworkedPlayClosedSound", PhotonTargets.All, Array.Empty<object>());
			base.transform.localPosition = localPosition;
		}
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x00032CB4 File Offset: 0x00030EB4
	public void Grab()
	{
		this.view.RPC("NetworkedGrab", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x00032CCC File Offset: 0x00030ECC
	[PunRPC]
	private void NetworkedGrab()
	{
		this.closed = false;
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x00032CD8 File Offset: 0x00030ED8
	[PunRPC]
	private void NetworkedPlayClosedSound()
	{
		this.closed = true;
		if (this.closedSource.isPlaying)
		{
			return;
		}
		if (this.doorClosedClips.Length != 0)
		{
			this.closedSource.clip = this.doorClosedClips[Random.Range(0, this.doorClosedClips.Length)];
			this.closedSource.Play();
			return;
		}
		Debug.LogError(base.gameObject.name + " needs a drawer closing audio clip");
	}

	// Token: 0x04000895 RID: 2197
	private Vector3 startPos;

	// Token: 0x04000896 RID: 2198
	public bool isX;

	// Token: 0x04000897 RID: 2199
	public bool isY;

	// Token: 0x04000898 RID: 2200
	public bool isZ = true;

	// Token: 0x04000899 RID: 2201
	private Vector3 pos;

	// Token: 0x0400089A RID: 2202
	private Rigidbody body;

	// Token: 0x0400089B RID: 2203
	private PhotonView view;

	// Token: 0x0400089C RID: 2204
	[HideInInspector]
	public bool closed;

	// Token: 0x0400089D RID: 2205
	[SerializeField]
	private AudioClip[] doorClosedClips;

	// Token: 0x0400089E RID: 2206
	[SerializeField]
	private AudioSource loopSource;

	// Token: 0x0400089F RID: 2207
	[SerializeField]
	private AudioSource closedSource;

	// Token: 0x040008A0 RID: 2208
	private Vector3 startWorldPos;

	// Token: 0x040008A1 RID: 2209
	private Vector3 oldPos;

	// Token: 0x040008A2 RID: 2210
	private float velocity;
}
