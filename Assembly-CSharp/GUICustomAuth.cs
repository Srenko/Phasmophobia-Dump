using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
public class GUICustomAuth : MonoBehaviour
{
	// Token: 0x060001B9 RID: 441 RVA: 0x0000BC6A File Offset: 0x00009E6A
	public void Start()
	{
		this.GuiRect = new Rect((float)(Screen.width / 4), 80f, (float)(Screen.width / 2), (float)(Screen.height - 100));
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0000BC95 File Offset: 0x00009E95
	public void OnJoinedLobby()
	{
		base.enabled = false;
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000BC95 File Offset: 0x00009E95
	public void OnConnectedToMaster()
	{
		base.enabled = false;
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000BC9E File Offset: 0x00009E9E
	public void OnCustomAuthenticationFailed(string debugMessage)
	{
		this.authDebugMessage = debugMessage;
		this.SetStateAuthFailed();
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000BCAD File Offset: 0x00009EAD
	public void SetStateAuthInput()
	{
		this.RootOf3dButtons.SetActive(false);
		this.guiState = GUICustomAuth.GuiState.AuthInput;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000BCC2 File Offset: 0x00009EC2
	public void SetStateAuthHelp()
	{
		this.RootOf3dButtons.SetActive(false);
		this.guiState = GUICustomAuth.GuiState.AuthHelp;
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000BCD7 File Offset: 0x00009ED7
	public void SetStateAuthOrNot()
	{
		this.RootOf3dButtons.SetActive(true);
		this.guiState = GUICustomAuth.GuiState.AuthOrNot;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000BCEC File Offset: 0x00009EEC
	public void SetStateAuthFailed()
	{
		this.RootOf3dButtons.SetActive(false);
		this.guiState = GUICustomAuth.GuiState.AuthFailed;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000BD04 File Offset: 0x00009F04
	public void ConnectWithNickname()
	{
		this.RootOf3dButtons.SetActive(false);
		PhotonNetwork.AuthValues = new AuthenticationValues
		{
			UserId = PhotonNetwork.playerName
		};
		PhotonNetwork.playerName += "Nick";
		PhotonNetwork.ConnectUsingSettings("1.0");
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000BD54 File Offset: 0x00009F54
	private void OnGUI()
	{
		if (PhotonNetwork.connected)
		{
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString(), Array.Empty<GUILayoutOption>());
			return;
		}
		GUILayout.BeginArea(this.GuiRect);
		switch (this.guiState)
		{
		case GUICustomAuth.GuiState.AuthInput:
			GUILayout.Label("Authenticate yourself", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			this.authName = GUILayout.TextField(this.authName, new GUILayoutOption[]
			{
				GUILayout.Width((float)(Screen.width / 4 - 5))
			});
			GUILayout.FlexibleSpace();
			this.authToken = GUILayout.TextField(this.authToken, new GUILayoutOption[]
			{
				GUILayout.Width((float)(Screen.width / 4 - 5))
			});
			GUILayout.EndHorizontal();
			if (GUILayout.Button("Authenticate", Array.Empty<GUILayoutOption>()))
			{
				PhotonNetwork.AuthValues = new AuthenticationValues();
				PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Custom;
				PhotonNetwork.AuthValues.AddAuthParameter("username", this.authName);
				PhotonNetwork.AuthValues.AddAuthParameter("token", this.authToken);
				PhotonNetwork.ConnectUsingSettings("1.0");
			}
			GUILayout.Space(10f);
			if (GUILayout.Button("Help", new GUILayoutOption[]
			{
				GUILayout.Width(100f)
			}))
			{
				this.SetStateAuthHelp();
			}
			break;
		case GUICustomAuth.GuiState.AuthHelp:
			GUILayout.Label("By default, any player can connect to Photon.\n'Custom Authentication' can be enabled to reject players without valid user-account.", Array.Empty<GUILayoutOption>());
			GUILayout.Label("The actual authentication must be done by a web-service which you host and customize. Example sourcecode for these services is available on the docs page.", Array.Empty<GUILayoutOption>());
			GUILayout.Label("For this demo set the Authentication URL in the Dashboard to:\nhttps://wt-e4c18d407aa73a40e4182aaf00a2a2eb-0.run.webtask.io/auth/auth-demo-equals", Array.Empty<GUILayoutOption>());
			GUILayout.Label("That authentication-service has no user-database. It confirms any user if 'name equals password'.", Array.Empty<GUILayoutOption>());
			GUILayout.Space(10f);
			if (GUILayout.Button("Configure Authentication (Dashboard)", Array.Empty<GUILayoutOption>()))
			{
				Application.OpenURL("https://www.photonengine.com/dashboard");
			}
			if (GUILayout.Button("Authentication Docs", Array.Empty<GUILayoutOption>()))
			{
				Application.OpenURL("https://doc.photonengine.com/en-us/pun/current/demos-and-tutorials/pun-and-facebook-custom-authentication");
			}
			GUILayout.Space(10f);
			if (GUILayout.Button("Back to input", Array.Empty<GUILayoutOption>()))
			{
				this.SetStateAuthInput();
			}
			break;
		case GUICustomAuth.GuiState.AuthFailed:
			GUILayout.Label("Authentication Failed", Array.Empty<GUILayoutOption>());
			GUILayout.Space(10f);
			GUILayout.Label("Error message:\n'" + this.authDebugMessage + "'", Array.Empty<GUILayoutOption>());
			GUILayout.Space(10f);
			GUILayout.Label("For this demo set the Authentication URL in the Dashboard to:\nhttps://wt-e4c18d407aa73a40e4182aaf00a2a2eb-0.run.webtask.io/auth/auth-demo-equals", Array.Empty<GUILayoutOption>());
			GUILayout.Label("That authentication-service has no user-database. It confirms any user if 'name equals password'.", Array.Empty<GUILayoutOption>());
			GUILayout.Label("The error message comes from that service and can be customized.", Array.Empty<GUILayoutOption>());
			GUILayout.Space(10f);
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			if (GUILayout.Button("Back", Array.Empty<GUILayoutOption>()))
			{
				this.SetStateAuthInput();
			}
			if (GUILayout.Button("Help", Array.Empty<GUILayoutOption>()))
			{
				this.SetStateAuthHelp();
			}
			GUILayout.EndHorizontal();
			break;
		}
		GUILayout.EndArea();
	}

	// Token: 0x040001E0 RID: 480
	public Rect GuiRect;

	// Token: 0x040001E1 RID: 481
	private string authName = "usr";

	// Token: 0x040001E2 RID: 482
	private string authToken = "usr";

	// Token: 0x040001E3 RID: 483
	private string authDebugMessage = string.Empty;

	// Token: 0x040001E4 RID: 484
	private GUICustomAuth.GuiState guiState;

	// Token: 0x040001E5 RID: 485
	public GameObject RootOf3dButtons;

	// Token: 0x020004E1 RID: 1249
	private enum GuiState
	{
		// Token: 0x040023A2 RID: 9122
		AuthOrNot,
		// Token: 0x040023A3 RID: 9123
		AuthInput,
		// Token: 0x040023A4 RID: 9124
		AuthHelp,
		// Token: 0x040023A5 RID: 9125
		AuthFailed
	}
}
