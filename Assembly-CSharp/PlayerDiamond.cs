using System;
using UnityEngine;

// Token: 0x0200007B RID: 123
public class PlayerDiamond : MonoBehaviour
{
	// Token: 0x1700002D RID: 45
	// (get) Token: 0x060002B6 RID: 694 RVA: 0x00011E0E File Offset: 0x0001000E
	private PhotonView PhotonView
	{
		get
		{
			if (this.m_PhotonView == null)
			{
				this.m_PhotonView = base.transform.parent.GetComponent<PhotonView>();
			}
			return this.m_PhotonView;
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060002B7 RID: 695 RVA: 0x00011E3A File Offset: 0x0001003A
	private Renderer DiamondRenderer
	{
		get
		{
			if (this.m_DiamondRenderer == null)
			{
				this.m_DiamondRenderer = base.GetComponentInChildren<Renderer>();
			}
			return this.m_DiamondRenderer;
		}
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x00011E5C File Offset: 0x0001005C
	private void Start()
	{
		this.m_Height = this.HeightOffset;
		if (this.HeadTransform != null)
		{
			this.m_Height += this.HeadTransform.position.y;
		}
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x00011E95 File Offset: 0x00010095
	private void Update()
	{
		this.UpdateDiamondPosition();
		this.UpdateDiamondRotation();
		this.UpdateDiamondVisibility();
	}

	// Token: 0x060002BA RID: 698 RVA: 0x00011EAC File Offset: 0x000100AC
	private void UpdateDiamondPosition()
	{
		Vector3 vector = Vector3.zero;
		if (this.HeadTransform != null)
		{
			vector = this.HeadTransform.position;
		}
		vector.y = this.m_Height;
		if (!float.IsNaN(vector.x) && !float.IsNaN(vector.z))
		{
			base.transform.position = Vector3.Lerp(base.transform.position, vector, Time.deltaTime * 10f);
		}
	}

	// Token: 0x060002BB RID: 699 RVA: 0x00011F28 File Offset: 0x00010128
	private void UpdateDiamondRotation()
	{
		this.m_Rotation += Time.deltaTime * 180f;
		this.m_Rotation %= 360f;
		base.transform.rotation = Quaternion.Euler(0f, this.m_Rotation, 0f);
	}

	// Token: 0x060002BC RID: 700 RVA: 0x00011F7F File Offset: 0x0001017F
	private void UpdateDiamondVisibility()
	{
		this.DiamondRenderer.enabled = true;
		if (this.PhotonView == null || !this.PhotonView.isMine)
		{
			this.DiamondRenderer.enabled = false;
		}
	}

	// Token: 0x040002F9 RID: 761
	public Transform HeadTransform;

	// Token: 0x040002FA RID: 762
	public float HeightOffset = 0.5f;

	// Token: 0x040002FB RID: 763
	private PhotonView m_PhotonView;

	// Token: 0x040002FC RID: 764
	private Renderer m_DiamondRenderer;

	// Token: 0x040002FD RID: 765
	private float m_Rotation;

	// Token: 0x040002FE RID: 766
	private float m_Height;
}
