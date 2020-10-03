using System;
using Photon;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class DemoMecanimGUI : PunBehaviour
{
	// Token: 0x060001D9 RID: 473 RVA: 0x00003F60 File Offset: 0x00002160
	public void Awake()
	{
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0000CB40 File Offset: 0x0000AD40
	public void Update()
	{
		this.FindRemoteAnimator();
		this.m_SlideIn = Mathf.Lerp(this.m_SlideIn, this.m_IsOpen ? 1f : 0f, Time.deltaTime * 9f);
		this.m_FoundPlayerSlideIn = Mathf.Lerp(this.m_FoundPlayerSlideIn, (this.m_AnimatorView == null) ? 0f : 1f, Time.deltaTime * 5f);
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0000CBBC File Offset: 0x0000ADBC
	public void FindRemoteAnimator()
	{
		if (this.m_RemoteAnimator != null)
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
		for (int i = 0; i < array.Length; i++)
		{
			PhotonView component = array[i].GetComponent<PhotonView>();
			if (component != null && !component.isMine)
			{
				this.m_RemoteAnimator = array[i].GetComponent<Animator>();
			}
		}
	}

	// Token: 0x060001DC RID: 476 RVA: 0x0000CC1C File Offset: 0x0000AE1C
	public void OnGUI()
	{
		GUI.skin = this.Skin;
		string[] texts = new string[]
		{
			"Disabled",
			"Discrete",
			"Continuous"
		};
		GUILayout.BeginArea(new Rect((float)Screen.width - 200f * this.m_FoundPlayerSlideIn - 400f * this.m_SlideIn, 0f, 600f, (float)Screen.height), GUI.skin.box);
		GUILayout.Label("Mecanim Demo", GUI.skin.customStyles[0], Array.Empty<GUILayoutOption>());
		GUI.color = Color.white;
		string text = "Settings";
		if (this.m_IsOpen)
		{
			text = "Close";
		}
		if (GUILayout.Button(text, new GUILayoutOption[]
		{
			GUILayout.Width(110f)
		}))
		{
			this.m_IsOpen = !this.m_IsOpen;
		}
		string text2 = "";
		if (this.m_AnimatorView != null)
		{
			text2 += "Send Values:\n";
			for (int i = 0; i < this.m_AnimatorView.GetSynchronizedParameters().Count; i++)
			{
				PhotonAnimatorView.SynchronizedParameter synchronizedParameter = this.m_AnimatorView.GetSynchronizedParameters()[i];
				try
				{
					switch (synchronizedParameter.Type)
					{
					case PhotonAnimatorView.ParameterType.Float:
						text2 = string.Concat(new string[]
						{
							text2,
							synchronizedParameter.Name,
							" (",
							this.m_AnimatorView.GetComponent<Animator>().GetFloat(synchronizedParameter.Name).ToString("0.00"),
							")\n"
						});
						break;
					case PhotonAnimatorView.ParameterType.Int:
						text2 = string.Concat(new object[]
						{
							text2,
							synchronizedParameter.Name,
							" (",
							this.m_AnimatorView.GetComponent<Animator>().GetInteger(synchronizedParameter.Name),
							")\n"
						});
						break;
					case PhotonAnimatorView.ParameterType.Bool:
						text2 = string.Concat(new string[]
						{
							text2,
							synchronizedParameter.Name,
							" (",
							this.m_AnimatorView.GetComponent<Animator>().GetBool(synchronizedParameter.Name) ? "True" : "False",
							")\n"
						});
						break;
					}
				}
				catch
				{
					Debug.Log("derrrr for " + synchronizedParameter.Name);
				}
			}
		}
		if (this.m_RemoteAnimator != null)
		{
			text2 += "\nReceived Values:\n";
			for (int j = 0; j < this.m_AnimatorView.GetSynchronizedParameters().Count; j++)
			{
				PhotonAnimatorView.SynchronizedParameter synchronizedParameter2 = this.m_AnimatorView.GetSynchronizedParameters()[j];
				try
				{
					switch (synchronizedParameter2.Type)
					{
					case PhotonAnimatorView.ParameterType.Float:
						text2 = string.Concat(new string[]
						{
							text2,
							synchronizedParameter2.Name,
							" (",
							this.m_RemoteAnimator.GetFloat(synchronizedParameter2.Name).ToString("0.00"),
							")\n"
						});
						break;
					case PhotonAnimatorView.ParameterType.Int:
						text2 = string.Concat(new object[]
						{
							text2,
							synchronizedParameter2.Name,
							" (",
							this.m_RemoteAnimator.GetInteger(synchronizedParameter2.Name),
							")\n"
						});
						break;
					case PhotonAnimatorView.ParameterType.Bool:
						text2 = string.Concat(new string[]
						{
							text2,
							synchronizedParameter2.Name,
							" (",
							this.m_RemoteAnimator.GetBool(synchronizedParameter2.Name) ? "True" : "False",
							")\n"
						});
						break;
					}
				}
				catch
				{
					Debug.Log("derrrr for " + synchronizedParameter2.Name);
				}
			}
		}
		GUIStyle guistyle = new GUIStyle(GUI.skin.label);
		guistyle.alignment = TextAnchor.UpperLeft;
		GUI.color = new Color(1f, 1f, 1f, 1f - this.m_SlideIn);
		GUI.Label(new Rect(10f, 100f, 600f, (float)Screen.height), text2, guistyle);
		if (this.m_AnimatorView != null)
		{
			GUI.color = new Color(1f, 1f, 1f, this.m_SlideIn);
			GUILayout.Space(20f);
			GUILayout.Label("Synchronize Parameters", Array.Empty<GUILayoutOption>());
			for (int k = 0; k < this.m_AnimatorView.GetSynchronizedParameters().Count; k++)
			{
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				PhotonAnimatorView.SynchronizedParameter synchronizedParameter3 = this.m_AnimatorView.GetSynchronizedParameters()[k];
				GUILayout.Label(synchronizedParameter3.Name, new GUILayoutOption[]
				{
					GUILayout.Width(100f),
					GUILayout.Height(36f)
				});
				int synchronizeType = (int)synchronizedParameter3.SynchronizeType;
				int num = GUILayout.Toolbar(synchronizeType, texts, Array.Empty<GUILayoutOption>());
				if (num != synchronizeType)
				{
					this.m_AnimatorView.SetParameterSynchronized(synchronizedParameter3.Name, synchronizedParameter3.Type, (PhotonAnimatorView.SynchronizeType)num);
				}
				GUILayout.EndHorizontal();
			}
		}
		GUILayout.EndArea();
	}

	// Token: 0x060001DD RID: 477 RVA: 0x0000D198 File Offset: 0x0000B398
	public override void OnJoinedRoom()
	{
		this.CreatePlayerObject();
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
	private void CreatePlayerObject()
	{
		Vector3 position = new Vector3(-2f, 0f, 0f);
		position.x += Random.Range(-3f, 3f);
		position.z += Random.Range(-4f, 4f);
		GameObject gameObject = PhotonNetwork.Instantiate("Robot Kyle Mecanim", position, Quaternion.identity, 0);
		this.m_AnimatorView = gameObject.GetComponent<PhotonAnimatorView>();
	}

	// Token: 0x040001F9 RID: 505
	public GUISkin Skin;

	// Token: 0x040001FA RID: 506
	private PhotonAnimatorView m_AnimatorView;

	// Token: 0x040001FB RID: 507
	private Animator m_RemoteAnimator;

	// Token: 0x040001FC RID: 508
	private float m_SlideIn;

	// Token: 0x040001FD RID: 509
	private float m_FoundPlayerSlideIn;

	// Token: 0x040001FE RID: 510
	private bool m_IsOpen;
}
