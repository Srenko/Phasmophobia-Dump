using System;
using ExitGames.UtilityScripts;
using Photon;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class ColorPerPlayer : PunBehaviour
{
	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000155 RID: 341 RVA: 0x0000A0FC File Offset: 0x000082FC
	// (set) Token: 0x06000156 RID: 342 RVA: 0x0000A104 File Offset: 0x00008304
	public bool ColorPicked { get; set; }

	// Token: 0x06000157 RID: 343 RVA: 0x0000A10D File Offset: 0x0000830D
	private void OnEnable()
	{
		if (!this.isInitialized)
		{
			this.Init();
		}
	}

	// Token: 0x06000158 RID: 344 RVA: 0x0000A10D File Offset: 0x0000830D
	private void Start()
	{
		if (!this.isInitialized)
		{
			this.Init();
		}
	}

	// Token: 0x06000159 RID: 345 RVA: 0x0000A120 File Offset: 0x00008320
	private void Init()
	{
		if (!this.isInitialized && PlayerRoomIndexing.instance != null)
		{
			PlayerRoomIndexing instance = PlayerRoomIndexing.instance;
			instance.OnRoomIndexingChanged = (PlayerRoomIndexing.RoomIndexingChanged)Delegate.Combine(instance.OnRoomIndexingChanged, new PlayerRoomIndexing.RoomIndexingChanged(this.Refresh));
			this.isInitialized = true;
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0000A16F File Offset: 0x0000836F
	private void OnDisable()
	{
		PlayerRoomIndexing instance = PlayerRoomIndexing.instance;
		instance.OnRoomIndexingChanged = (PlayerRoomIndexing.RoomIndexingChanged)Delegate.Remove(instance.OnRoomIndexingChanged, new PlayerRoomIndexing.RoomIndexingChanged(this.Refresh));
	}

	// Token: 0x0600015B RID: 347 RVA: 0x0000A198 File Offset: 0x00008398
	private void Refresh()
	{
		int roomIndex = PhotonNetwork.player.GetRoomIndex();
		if (roomIndex == -1)
		{
			this.Reset();
			return;
		}
		this.MyColor = this.Colors[roomIndex];
		this.ColorPicked = true;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x0000A10D File Offset: 0x0000830D
	public override void OnJoinedRoom()
	{
		if (!this.isInitialized)
		{
			this.Init();
		}
	}

	// Token: 0x0600015D RID: 349 RVA: 0x0000A1D4 File Offset: 0x000083D4
	public override void OnLeftRoom()
	{
		this.Reset();
	}

	// Token: 0x0600015E RID: 350 RVA: 0x0000A1DC File Offset: 0x000083DC
	public void Reset()
	{
		this.MyColor = Color.grey;
		this.ColorPicked = false;
	}

	// Token: 0x0600015F RID: 351 RVA: 0x0000A1F0 File Offset: 0x000083F0
	private void OnGUI()
	{
		if (!this.ColorPicked || !this.ShowColorLabel)
		{
			return;
		}
		GUILayout.BeginArea(this.ColorLabelArea);
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		Color color = GUI.color;
		GUI.color = this.MyColor;
		GUILayout.Label(this.img, Array.Empty<GUILayoutOption>());
		GUI.color = color;
		GUILayout.Label(PhotonNetwork.isMasterClient ? "is your color\nyou are the Master Client" : "is your color", Array.Empty<GUILayoutOption>());
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	// Token: 0x04000198 RID: 408
	public Color[] Colors = new Color[]
	{
		Color.red,
		Color.blue,
		Color.yellow,
		Color.green
	};

	// Token: 0x04000199 RID: 409
	public const string ColorProp = "pc";

	// Token: 0x0400019A RID: 410
	public bool ShowColorLabel;

	// Token: 0x0400019B RID: 411
	public Rect ColorLabelArea = new Rect(0f, 50f, 100f, 200f);

	// Token: 0x0400019C RID: 412
	public Texture2D img;

	// Token: 0x0400019D RID: 413
	public Color MyColor = Color.grey;

	// Token: 0x0400019F RID: 415
	private bool isInitialized;
}
