using System;
using ExitGames.UtilityScripts;
using Photon;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class ColorPerPlayerApply : PunBehaviour
{
	// Token: 0x06000161 RID: 353 RVA: 0x0000A2E9 File Offset: 0x000084E9
	private void OnEnable()
	{
		if (!this.isInitialized)
		{
			this.Init();
		}
	}

	// Token: 0x06000162 RID: 354 RVA: 0x0000A2E9 File Offset: 0x000084E9
	private void Start()
	{
		if (!this.isInitialized)
		{
			this.Init();
		}
	}

	// Token: 0x06000163 RID: 355 RVA: 0x0000A2FC File Offset: 0x000084FC
	private void Init()
	{
		if (!this.isInitialized && PlayerRoomIndexing.instance != null)
		{
			PlayerRoomIndexing instance = PlayerRoomIndexing.instance;
			instance.OnRoomIndexingChanged = (PlayerRoomIndexing.RoomIndexingChanged)Delegate.Combine(instance.OnRoomIndexingChanged, new PlayerRoomIndexing.RoomIndexingChanged(this.ApplyColor));
			this.isInitialized = true;
		}
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0000A34B File Offset: 0x0000854B
	private void OnDisable()
	{
		this.isInitialized = false;
		if (PlayerRoomIndexing.instance != null)
		{
			PlayerRoomIndexing instance = PlayerRoomIndexing.instance;
			instance.OnRoomIndexingChanged = (PlayerRoomIndexing.RoomIndexingChanged)Delegate.Remove(instance.OnRoomIndexingChanged, new PlayerRoomIndexing.RoomIndexingChanged(this.ApplyColor));
		}
	}

	// Token: 0x06000165 RID: 357 RVA: 0x0000A388 File Offset: 0x00008588
	public void Awake()
	{
		if (ColorPerPlayerApply.colorPickerCache == null)
		{
			ColorPerPlayerApply.colorPickerCache = Object.FindObjectOfType<ColorPerPlayer>();
		}
		if (ColorPerPlayerApply.colorPickerCache == null)
		{
			base.enabled = false;
		}
		if (base.photonView.isSceneView)
		{
			base.enabled = false;
		}
		this.rendererComponent = base.GetComponent<Renderer>();
	}

	// Token: 0x06000166 RID: 358 RVA: 0x0000A3E0 File Offset: 0x000085E0
	public override void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		this.ApplyColor();
	}

	// Token: 0x06000167 RID: 359 RVA: 0x0000A3E8 File Offset: 0x000085E8
	public void ApplyColor()
	{
		if (base.photonView.owner == null)
		{
			return;
		}
		int roomIndex = base.photonView.owner.GetRoomIndex();
		if (roomIndex >= 0 && roomIndex <= ColorPerPlayerApply.colorPickerCache.Colors.Length)
		{
			this.rendererComponent.material.color = ColorPerPlayerApply.colorPickerCache.Colors[roomIndex];
		}
	}

	// Token: 0x040001A0 RID: 416
	private static ColorPerPlayer colorPickerCache;

	// Token: 0x040001A1 RID: 417
	private Renderer rendererComponent;

	// Token: 0x040001A2 RID: 418
	private bool isInitialized;
}
