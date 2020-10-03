using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ExitGames.Demos
{
	// Token: 0x02000482 RID: 1154
	public class DemoHubManager : MonoBehaviour
	{
		// Token: 0x060023FB RID: 9211 RVA: 0x000B02E0 File Offset: 0x000AE4E0
		private void Awake()
		{
			this.OpenSceneButton.SetActive(false);
			this.OpenTutorialLinkButton.SetActive(false);
			this.OpenDocLinkButton.SetActive(false);
			this._data.Add("DemoBoxes", new DemoHubManager.DemoData
			{
				Title = "Demo Boxes",
				Description = "Uses ConnectAndJoinRandom script.\n(joins a random room or creates one)\n\nInstantiates simple prefabs.\nSynchronizes positions without smoothing.\nShows that RPCs target a specific object.",
				Scene = "DemoBoxes-Scene"
			});
			this._data.Add("DemoWorker", new DemoHubManager.DemoData
			{
				Title = "Demo Worker",
				Description = "Joins the default lobby and shows existing rooms.\nLets you create or join a room.\nInstantiates an animated character.\nSynchronizes position and animation state of character with smoothing.\nImplements simple in-room Chat via RPC calls.",
				Scene = "DemoWorker-Scene"
			});
			this._data.Add("MovementSmoothing", new DemoHubManager.DemoData
			{
				Title = "Movement Smoothing",
				Description = "Uses ConnectAndJoinRandom script.\nShows several basic ways to synchronize positions between controlling client and remote ones.\nThe TransformView is a good default to use.",
				Scene = "DemoSynchronization-Scene"
			});
			this._data.Add("BasicTutorial", new DemoHubManager.DemoData
			{
				Title = "Basic Tutorial",
				Description = "All custom code for connection, player and scene management.\nAuto synchronization of room levels.\nUses PhotonAnimatoView for Animator synch.\nNew Unity UI all around, for Menus and player health HUD.\nFull step by step tutorial available online.",
				Scene = "PunBasics-Launcher",
				TutorialLink = "http://j.mp/2dibZIM"
			});
			this._data.Add("OwnershipTransfer", new DemoHubManager.DemoData
			{
				Title = "Ownership Transfer",
				Description = "Shows how to transfer the ownership of a PhotonView.\nThe owner will send position updates of the GameObject.\nTransfer can be edited per PhotonView and set to Fixed (no transfer), Request (owner has to agree) or Takeover (owner can't object).",
				Scene = "DemoChangeOwner-Scene"
			});
			this._data.Add("PickupTeamsScores", new DemoHubManager.DemoData
			{
				Title = "Pickup, Teams, Scores",
				Description = "Uses ConnectAndJoinRandom script.\nImplements item pickup with RPCs.\nUses Custom Properties for Teams.\nCounts score per player and team.\nUses PhotonPlayer extension methods for easy Custom Property access.",
				Scene = "DemoPickup-Scene"
			});
			this._data.Add("Chat", new DemoHubManager.DemoData
			{
				Title = "Chat",
				Description = "Uses the Chat API (now part of PUN).\nSimple UI.\nYou can enter any User ID.\nAutomatically subscribes some channels.\nAllows simple commands via text.\n\nRequires configuration of Chat App ID in scene.",
				Scene = "DemoChat-Scene",
				DocLink = "http://j.mp/2iwQkPJ"
			});
			this._data.Add("RPGMovement", new DemoHubManager.DemoData
			{
				Title = "RPG Movement",
				Description = "Demonstrates how to use the PhotonTransformView component to synchronize position updates smoothly using inter- and extrapolation.\n\nThis demo also shows how to setup a Mecanim Animator to update animations automatically based on received position updates (without sending explicit animation updates).",
				Scene = "DemoRPGMovement-Scene"
			});
			this._data.Add("MecanimAnimations", new DemoHubManager.DemoData
			{
				Title = "Mecanim Animations",
				Description = "This demo shows how to use the PhotonAnimatorView component to easily synchronize Mecanim animations.\n\nIt also demonstrates another feature of the PhotonTransformView component which gives you more control how position updates are inter-/extrapolated by telling the component how fast the object moves and turns using SetSynchronizedValues().",
				Scene = "DemoMecanim-Scene"
			});
			this._data.Add("2DGame", new DemoHubManager.DemoData
			{
				Title = "2D Game Demo",
				Description = "Synchronizes animations, positions and physics in a 2D scene.",
				Scene = "Demo2DJumpAndRunWithPhysics-Scene"
			});
			this._data.Add("FriendsAndAuth", new DemoHubManager.DemoData
			{
				Title = "Friends & Authentication",
				Description = "Shows connect with or without (server-side) authentication.\n\nAuthentication requires minor server-side setup (in Dashboard).\n\nOnce connected, you can find (made up) friends.\nJoin a room just to see how that gets visible in friends list.",
				Scene = "DemoFriends-Scene"
			});
			this._data.Add("TurnBasedGame", new DemoHubManager.DemoData
			{
				Title = "'Rock Paper Scissor' Turn Based Game",
				Description = "Demonstrate TurnBased Game Mechanics using PUN.\n\nIt makes use of the TurnBasedManager Utility Script",
				Scene = "DemoRPS-Scene"
			});
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000B0608 File Offset: 0x000AE808
		public void SelectDemo(string Reference)
		{
			this.currentSelection = Reference;
			this.TitleText.text = this._data[this.currentSelection].Title;
			this.DescriptionText.text = this._data[this.currentSelection].Description;
			this.OpenSceneButton.SetActive(!string.IsNullOrEmpty(this._data[this.currentSelection].Scene));
			this.OpenTutorialLinkButton.SetActive(!string.IsNullOrEmpty(this._data[this.currentSelection].TutorialLink));
			this.OpenDocLinkButton.SetActive(!string.IsNullOrEmpty(this._data[this.currentSelection].DocLink));
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x000B06D9 File Offset: 0x000AE8D9
		public void OpenScene()
		{
			if (string.IsNullOrEmpty(this.currentSelection))
			{
				Debug.LogError("Bad setup, a CurrentSelection is expected at this point");
				return;
			}
			SceneManager.LoadScene(this._data[this.currentSelection].Scene);
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000B070E File Offset: 0x000AE90E
		public void OpenTutorialLink()
		{
			if (string.IsNullOrEmpty(this.currentSelection))
			{
				Debug.LogError("Bad setup, a CurrentSelection is expected at this point");
				return;
			}
			Application.OpenURL(this._data[this.currentSelection].TutorialLink);
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000B0743 File Offset: 0x000AE943
		public void OpenDocLink()
		{
			if (string.IsNullOrEmpty(this.currentSelection))
			{
				Debug.LogError("Bad setup, a CurrentSelection is expected at this point");
				return;
			}
			Application.OpenURL(this._data[this.currentSelection].DocLink);
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000B0778 File Offset: 0x000AE978
		public void OpenMainWebLink()
		{
			Application.OpenURL(this.MainDemoWebLink);
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000B0788 File Offset: 0x000AE988
		private void OnGUI()
		{
			GUI.SetNextControlName(base.gameObject.GetHashCode().ToString());
			GUI.TextField(this.BugFixbounds, string.Empty, 0);
		}

		// Token: 0x04002151 RID: 8529
		public Text TitleText;

		// Token: 0x04002152 RID: 8530
		public Text DescriptionText;

		// Token: 0x04002153 RID: 8531
		public GameObject OpenSceneButton;

		// Token: 0x04002154 RID: 8532
		public GameObject OpenTutorialLinkButton;

		// Token: 0x04002155 RID: 8533
		public GameObject OpenDocLinkButton;

		// Token: 0x04002156 RID: 8534
		private string MainDemoWebLink = "http://bit.ly/2f8OFu8";

		// Token: 0x04002157 RID: 8535
		private Dictionary<string, DemoHubManager.DemoData> _data = new Dictionary<string, DemoHubManager.DemoData>();

		// Token: 0x04002158 RID: 8536
		private string currentSelection;

		// Token: 0x04002159 RID: 8537
		private Rect BugFixbounds = new Rect(0f, 0f, 0f, 0f);

		// Token: 0x02000797 RID: 1943
		private struct DemoData
		{
			// Token: 0x0400299F RID: 10655
			public string Title;

			// Token: 0x040029A0 RID: 10656
			public string Description;

			// Token: 0x040029A1 RID: 10657
			public string Scene;

			// Token: 0x040029A2 RID: 10658
			public string TutorialLink;

			// Token: 0x040029A3 RID: 10659
			public string DocLink;
		}
	}
}
