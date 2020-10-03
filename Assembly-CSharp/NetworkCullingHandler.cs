using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D0 RID: 208
[RequireComponent(typeof(PhotonView))]
public class NetworkCullingHandler : MonoBehaviour, IPunObservable
{
	// Token: 0x060005D1 RID: 1489 RVA: 0x000210B0 File Offset: 0x0001F2B0
	private void OnEnable()
	{
		if (this.pView == null)
		{
			this.pView = base.GetComponent<PhotonView>();
			if (!this.pView.isMine)
			{
				return;
			}
		}
		if (this.cullArea == null)
		{
			this.cullArea = Object.FindObjectOfType<CullArea>();
		}
		this.previousActiveCells = new List<byte>(0);
		this.activeCells = new List<byte>(0);
		this.currentPosition = (this.lastPosition = base.transform.position);
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x00021130 File Offset: 0x0001F330
	private void Start()
	{
		if (!this.pView.isMine)
		{
			return;
		}
		if (PhotonNetwork.inRoom)
		{
			if (this.cullArea.NumberOfSubdivisions == 0)
			{
				this.pView.group = this.cullArea.FIRST_GROUP_ID;
				PhotonNetwork.SetInterestGroups(this.cullArea.FIRST_GROUP_ID, true);
				return;
			}
			this.pView.ObservedComponents.Add(this);
		}
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x00021198 File Offset: 0x0001F398
	private void Update()
	{
		if (!this.pView.isMine)
		{
			return;
		}
		this.lastPosition = this.currentPosition;
		this.currentPosition = base.transform.position;
		if (this.currentPosition != this.lastPosition && this.HaveActiveCellsChanged())
		{
			this.UpdateInterestGroups();
		}
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x000211F4 File Offset: 0x0001F3F4
	private void OnGUI()
	{
		if (!this.pView.isMine)
		{
			return;
		}
		string text = "Inside cells:\n";
		string text2 = "Subscribed cells:\n";
		for (int i = 0; i < this.activeCells.Count; i++)
		{
			if (i <= this.cullArea.NumberOfSubdivisions)
			{
				text = text + this.activeCells[i] + " | ";
			}
			text2 = text2 + this.activeCells[i] + " | ";
		}
		GUI.Label(new Rect(20f, (float)Screen.height - 120f, 200f, 40f), "<color=white>PhotonView Group: " + this.pView.group + "</color>", new GUIStyle
		{
			alignment = TextAnchor.UpperLeft,
			fontSize = 16
		});
		GUI.Label(new Rect(20f, (float)Screen.height - 100f, 200f, 40f), "<color=white>" + text + "</color>", new GUIStyle
		{
			alignment = TextAnchor.UpperLeft,
			fontSize = 16
		});
		GUI.Label(new Rect(20f, (float)Screen.height - 60f, 200f, 40f), "<color=white>" + text2 + "</color>", new GUIStyle
		{
			alignment = TextAnchor.UpperLeft,
			fontSize = 16
		});
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x00021364 File Offset: 0x0001F564
	private bool HaveActiveCellsChanged()
	{
		if (this.cullArea.NumberOfSubdivisions == 0)
		{
			return false;
		}
		this.previousActiveCells = new List<byte>(this.activeCells);
		this.activeCells = this.cullArea.GetActiveCells(base.transform.position);
		while (this.activeCells.Count <= this.cullArea.NumberOfSubdivisions)
		{
			this.activeCells.Add(this.cullArea.FIRST_GROUP_ID);
		}
		return this.activeCells.Count != this.previousActiveCells.Count || this.activeCells[this.cullArea.NumberOfSubdivisions] != this.previousActiveCells[this.cullArea.NumberOfSubdivisions];
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x00021428 File Offset: 0x0001F628
	private void UpdateInterestGroups()
	{
		List<byte> list = new List<byte>(0);
		foreach (byte item in this.previousActiveCells)
		{
			if (!this.activeCells.Contains(item))
			{
				list.Add(item);
			}
		}
		PhotonNetwork.SetInterestGroups(list.ToArray(), this.activeCells.ToArray());
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x000214A8 File Offset: 0x0001F6A8
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		while (this.activeCells.Count <= this.cullArea.NumberOfSubdivisions)
		{
			this.activeCells.Add(this.cullArea.FIRST_GROUP_ID);
		}
		if (this.cullArea.NumberOfSubdivisions == 1)
		{
			int num = this.orderIndex + 1;
			this.orderIndex = num;
			this.orderIndex = num % this.cullArea.SUBDIVISION_FIRST_LEVEL_ORDER.Length;
			this.pView.group = this.activeCells[this.cullArea.SUBDIVISION_FIRST_LEVEL_ORDER[this.orderIndex]];
			return;
		}
		if (this.cullArea.NumberOfSubdivisions == 2)
		{
			int num = this.orderIndex + 1;
			this.orderIndex = num;
			this.orderIndex = num % this.cullArea.SUBDIVISION_SECOND_LEVEL_ORDER.Length;
			this.pView.group = this.activeCells[this.cullArea.SUBDIVISION_SECOND_LEVEL_ORDER[this.orderIndex]];
			return;
		}
		if (this.cullArea.NumberOfSubdivisions == 3)
		{
			int num = this.orderIndex + 1;
			this.orderIndex = num;
			this.orderIndex = num % this.cullArea.SUBDIVISION_THIRD_LEVEL_ORDER.Length;
			this.pView.group = this.activeCells[this.cullArea.SUBDIVISION_THIRD_LEVEL_ORDER[this.orderIndex]];
		}
	}

	// Token: 0x040005EE RID: 1518
	private int orderIndex;

	// Token: 0x040005EF RID: 1519
	private CullArea cullArea;

	// Token: 0x040005F0 RID: 1520
	private List<byte> previousActiveCells;

	// Token: 0x040005F1 RID: 1521
	private List<byte> activeCells;

	// Token: 0x040005F2 RID: 1522
	private PhotonView pView;

	// Token: 0x040005F3 RID: 1523
	private Vector3 lastPosition;

	// Token: 0x040005F4 RID: 1524
	private Vector3 currentPosition;
}
