using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

// Token: 0x0200011A RID: 282
[RequireComponent(typeof(PhotonView))]
public class JournalController : MonoBehaviour
{
	// Token: 0x0600077D RID: 1917 RVA: 0x0002BABF File Offset: 0x00029CBF
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.keyAmount = 0;
		this.CreateGhosts();
		this.CreateEvidence();
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x0002BAE0 File Offset: 0x00029CE0
	private void Start()
	{
		if (LevelController.instance)
		{
			LevelController.instance.journals.Add(this);
		}
		this.index = 0;
		for (int i = 0; i < this.pages.Length; i++)
		{
			this.pages[i].SetActive(false);
		}
		this.pages[this.index].SetActive(true);
		this.pages[this.index + 1].SetActive(true);
		this.SetGhostTypes();
		if (!this.isVRJournal && !XRDevice.isPresent)
		{
			if (GameController.instance.myPlayer == null)
			{
				GameController.instance.OnLocalPlayerSpawned.AddListener(new UnityAction(this.OnPlayerSpawned));
				return;
			}
			this.OnPlayerSpawned();
		}
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x0002BB9C File Offset: 0x00029D9C
	private void CreateEvidence()
	{
		this.evidenceNames.Add(LocalisationSystem.GetLocalisedValue("Journal_NoEvidence"));
		this.evidenceNames.Add(LocalisationSystem.GetLocalisedValue("Journal_EMFEvidence"));
		this.evidenceNames.Add(LocalisationSystem.GetLocalisedValue("Journal_SpiritBoxEvidence"));
		this.evidenceNames.Add(LocalisationSystem.GetLocalisedValue("Journal_FingerprintsEvidence"));
		this.evidenceNames.Add(LocalisationSystem.GetLocalisedValue("Journal_GhostOrbEvidence"));
		this.evidenceNames.Add(LocalisationSystem.GetLocalisedValue("Journal_GhostWritingEvidence"));
		this.evidenceNames.Add(LocalisationSystem.GetLocalisedValue("Journal_FreezingEvidence"));
		this.evidence1Text.text = this.evidenceNames[0];
		this.evidence2Text.text = this.evidenceNames[0];
		this.evidence3Text.text = this.evidenceNames[0];
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0002BC84 File Offset: 0x00029E84
	private void CreateGhosts()
	{
		JournalController.Ghost item = new JournalController.Ghost
		{
			type = GhostTraits.Type.none,
			localisedName = LocalisationSystem.GetLocalisedValue("Journal_NoGhostType")
		};
		JournalController.Ghost item2 = new JournalController.Ghost
		{
			type = GhostTraits.Type.Spirit,
			localisedName = LocalisationSystem.GetLocalisedValue("Journal_SpiritTitle"),
			evidence1 = JournalController.evidenceType.SpiritBox,
			evidence2 = JournalController.evidenceType.Fingerprints,
			evidence3 = JournalController.evidenceType.GhostWritingBook
		};
		JournalController.Ghost item3 = new JournalController.Ghost
		{
			type = GhostTraits.Type.Wraith,
			localisedName = LocalisationSystem.GetLocalisedValue("Journal_WraithTitle"),
			evidence1 = JournalController.evidenceType.SpiritBox,
			evidence2 = JournalController.evidenceType.Fingerprints,
			evidence3 = JournalController.evidenceType.Temperature
		};
		JournalController.Ghost item4 = new JournalController.Ghost
		{
			type = GhostTraits.Type.Phantom,
			localisedName = LocalisationSystem.GetLocalisedValue("Journal_PhantomTitle"),
			evidence1 = JournalController.evidenceType.EMF,
			evidence2 = JournalController.evidenceType.GhostOrb,
			evidence3 = JournalController.evidenceType.Temperature
		};
		JournalController.Ghost item5 = new JournalController.Ghost
		{
			type = GhostTraits.Type.Poltergeist,
			localisedName = LocalisationSystem.GetLocalisedValue("Journal_PoltergeistTitle"),
			evidence1 = JournalController.evidenceType.SpiritBox,
			evidence2 = JournalController.evidenceType.Fingerprints,
			evidence3 = JournalController.evidenceType.GhostOrb
		};
		JournalController.Ghost item6 = new JournalController.Ghost
		{
			type = GhostTraits.Type.Banshee,
			localisedName = LocalisationSystem.GetLocalisedValue("Journal_BansheeTitle"),
			evidence1 = JournalController.evidenceType.EMF,
			evidence2 = JournalController.evidenceType.Fingerprints,
			evidence3 = JournalController.evidenceType.Temperature
		};
		JournalController.Ghost item7 = new JournalController.Ghost
		{
			type = GhostTraits.Type.Jinn,
			localisedName = LocalisationSystem.GetLocalisedValue("Journal_JinnTitle"),
			evidence1 = JournalController.evidenceType.EMF,
			evidence2 = JournalController.evidenceType.SpiritBox,
			evidence3 = JournalController.evidenceType.GhostOrb
		};
		JournalController.Ghost item8 = new JournalController.Ghost
		{
			type = GhostTraits.Type.Mare,
			localisedName = LocalisationSystem.GetLocalisedValue("Journal_MareTitle"),
			evidence1 = JournalController.evidenceType.Temperature,
			evidence2 = JournalController.evidenceType.SpiritBox,
			evidence3 = JournalController.evidenceType.GhostOrb
		};
		JournalController.Ghost item9 = new JournalController.Ghost
		{
			type = GhostTraits.Type.Revenant,
			localisedName = LocalisationSystem.GetLocalisedValue("Journal_RevenantTitle"),
			evidence1 = JournalController.evidenceType.EMF,
			evidence2 = JournalController.evidenceType.Fingerprints,
			evidence3 = JournalController.evidenceType.GhostWritingBook
		};
		JournalController.Ghost item10 = new JournalController.Ghost
		{
			type = GhostTraits.Type.Shade,
			localisedName = LocalisationSystem.GetLocalisedValue("Journal_ShadeTitle"),
			evidence1 = JournalController.evidenceType.EMF,
			evidence2 = JournalController.evidenceType.GhostOrb,
			evidence3 = JournalController.evidenceType.GhostWritingBook
		};
		JournalController.Ghost item11 = new JournalController.Ghost
		{
			type = GhostTraits.Type.Demon,
			localisedName = LocalisationSystem.GetLocalisedValue("Journal_DemonTitle"),
			evidence1 = JournalController.evidenceType.SpiritBox,
			evidence2 = JournalController.evidenceType.GhostWritingBook,
			evidence3 = JournalController.evidenceType.Temperature
		};
		JournalController.Ghost item12 = new JournalController.Ghost
		{
			type = GhostTraits.Type.Yurei,
			localisedName = LocalisationSystem.GetLocalisedValue("Journal_YureiTitle"),
			evidence1 = JournalController.evidenceType.GhostOrb,
			evidence2 = JournalController.evidenceType.GhostWritingBook,
			evidence3 = JournalController.evidenceType.Temperature
		};
		JournalController.Ghost item13 = new JournalController.Ghost
		{
			type = GhostTraits.Type.Oni,
			localisedName = LocalisationSystem.GetLocalisedValue("Journal_OniTitle"),
			evidence1 = JournalController.evidenceType.EMF,
			evidence2 = JournalController.evidenceType.SpiritBox,
			evidence3 = JournalController.evidenceType.GhostWritingBook
		};
		this.ghosts.Add(item);
		this.ghosts.Add(item2);
		this.ghosts.Add(item3);
		this.ghosts.Add(item4);
		this.ghosts.Add(item5);
		this.ghosts.Add(item6);
		this.ghosts.Add(item7);
		this.ghosts.Add(item8);
		this.ghosts.Add(item9);
		this.ghosts.Add(item10);
		this.ghosts.Add(item11);
		this.ghosts.Add(item12);
		this.ghosts.Add(item13);
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x0002C038 File Offset: 0x0002A238
	private void OnEnable()
	{
		if (GameController.instance)
		{
			if (base.GetComponent<Canvas>().renderMode == RenderMode.WorldSpace && GameController.instance.myPlayer != null)
			{
				base.GetComponent<Canvas>().worldCamera = GameController.instance.myPlayer.player.cam;
				return;
			}
		}
		else if (base.GetComponent<Canvas>().renderMode == RenderMode.WorldSpace)
		{
			base.GetComponent<Canvas>().worldCamera = Object.FindObjectOfType<Player>().cam;
		}
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0002C0B0 File Offset: 0x0002A2B0
	private void OpenCloseJournal()
	{
		if (GameController.instance == null)
		{
			return;
		}
		if (this.isVRJournal)
		{
			return;
		}
		if (!this.isOpen && GameController.instance.myPlayer.player.pcCanvas.isPaused)
		{
			return;
		}
		this.isOpen = !this.isOpen;
		this.openSource.clip = (this.isOpen ? this.openClip : this.closeClip);
		if (!this.openSource.isPlaying)
		{
			this.openSource.Play();
		}
		this.content.SetActive(this.isOpen);
		if (GameController.instance)
		{
			GameController.instance.myPlayer.player.firstPersonController.enabled = !this.isOpen;
			GameController.instance.myPlayer.player.charAnim.SetFloat("speed", 0f);
		}
		else if (MainManager.instance)
		{
			MainManager.instance.localPlayer.firstPersonController.enabled = !this.isOpen;
		}
		if (GameController.instance.myPlayer.player.playerInput.currentControlScheme == "Keyboard")
		{
			Cursor.visible = this.isOpen;
		}
		else
		{
			Cursor.visible = false;
		}
		if (this.isOpen)
		{
			if (GameController.instance.myPlayer.player.playerInput.currentControlScheme == "Keyboard")
			{
				Cursor.lockState = CursorLockMode.None;
				return;
			}
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x0002C240 File Offset: 0x0002A440
	public void CloseJournal()
	{
		if (GameController.instance == null)
		{
			return;
		}
		if (this.isVRJournal)
		{
			return;
		}
		this.openSource.clip = this.closeClip;
		if (!this.openSource.isPlaying)
		{
			this.openSource.Play();
		}
		if (!this.isOpen)
		{
			return;
		}
		this.isOpen = false;
		this.content.SetActive(false);
		if (GameController.instance)
		{
			GameController.instance.myPlayer.player.firstPersonController.enabled = true;
		}
		else if (MainManager.instance)
		{
			MainManager.instance.localPlayer.firstPersonController.enabled = true;
		}
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0002C300 File Offset: 0x0002A500
	public void NextPage()
	{
		if (!this.isVRJournal)
		{
			this.PageSync(2);
			return;
		}
		if (!this.view.isMine)
		{
			return;
		}
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("PageSync", PhotonTargets.AllBuffered, new object[]
			{
				2
			});
			return;
		}
		this.PageSync(2);
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x0002C35C File Offset: 0x0002A55C
	public void PreviousPage()
	{
		if (!this.isVRJournal)
		{
			this.PageSync(-2);
			return;
		}
		if (!this.view.isMine)
		{
			return;
		}
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("PageSync", PhotonTargets.AllBuffered, new object[]
			{
				-2
			});
			return;
		}
		this.PageSync(-2);
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x0002C3BC File Offset: 0x0002A5BC
	public void EndPage()
	{
		if (!this.isVRJournal)
		{
			this.EndPageSync();
			return;
		}
		if (!this.view.isMine)
		{
			return;
		}
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("EndPageSync", PhotonTargets.AllBuffered, Array.Empty<object>());
			return;
		}
		this.EndPageSync();
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x0002C40C File Offset: 0x0002A60C
	[PunRPC]
	private void EndPageSync()
	{
		this.index = this.pages.Length - 2;
		for (int i = 0; i < this.pages.Length; i++)
		{
			this.pages[i].SetActive(false);
		}
		this.pages[this.index].SetActive(true);
		this.pages[this.index + 1].SetActive(true);
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x0002C474 File Offset: 0x0002A674
	[PunRPC]
	private void PageSync(int value)
	{
		this.index += value;
		for (int i = 0; i < this.pages.Length; i++)
		{
			this.pages[i].SetActive(false);
		}
		this.pages[this.index].SetActive(true);
		this.pages[this.index + 1].SetActive(true);
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x0002C4D8 File Offset: 0x0002A6D8
	public void AddPhoto(Texture2D tex, string evidenceName, int evidenceAmount)
	{
		if (this.photosAmount < 10)
		{
			EvidenceController.instance.totalEvidenceFoundInPhotos += evidenceAmount;
			this.photos[this.photosAmount].sprite = Sprite.Create(tex, new Rect(0f, 0f, (float)tex.width, (float)tex.height), new Vector2(0.5f, 0.5f));
			this.photosNames[this.photosAmount].text = evidenceName;
			this.photosAmount++;
			GameController.instance.myPlayer.player.evidenceAudioSource.Play();
		}
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x0002C584 File Offset: 0x0002A784
	public GhostTraits.Type GetGhostType()
	{
		GhostTraits.Type result = GhostTraits.Type.none;
		if (this.ghostTypeIndex != 0)
		{
			result = this.values[this.ghostTypeIndex].type;
		}
		return result;
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x0002C5B4 File Offset: 0x0002A7B4
	public void AddKey(string name)
	{
		this.keyAmount++;
		switch (this.keyAmount)
		{
		case 1:
			this.Key1Text.text = name;
			break;
		case 2:
			this.Key2Text.text = name;
			break;
		case 3:
			this.Key3Text.text = name;
			break;
		case 4:
			this.Key4Text.text = name;
			break;
		case 5:
			this.Key5Text.text = name;
			break;
		case 6:
			this.Key6Text.text = name;
			break;
		}
		GameController.instance.myPlayer.player.evidenceAudioSource.Play();
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x0002C664 File Offset: 0x0002A864
	public void SetGhostTypes()
	{
		this.values.Clear();
		this.values.Add(this.ghosts[0]);
		JournalController.evidenceType evidenceType = (JournalController.evidenceType)this.evidence1Index;
		JournalController.evidenceType evidenceType2 = (JournalController.evidenceType)this.evidence2Index;
		JournalController.evidenceType evidenceType3 = (JournalController.evidenceType)this.evidence3Index;
		for (int i = 0; i < this.ghosts.Count; i++)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (evidenceType != JournalController.evidenceType.None && (evidenceType == this.ghosts[i].evidence1 || evidenceType == this.ghosts[i].evidence2 || evidenceType == this.ghosts[i].evidence3))
			{
				flag = true;
			}
			if (evidenceType2 != JournalController.evidenceType.None && (evidenceType2 == this.ghosts[i].evidence1 || evidenceType2 == this.ghosts[i].evidence2 || evidenceType2 == this.ghosts[i].evidence3))
			{
				flag2 = true;
			}
			if (evidenceType3 != JournalController.evidenceType.None && (evidenceType3 == this.ghosts[i].evidence1 || evidenceType3 == this.ghosts[i].evidence2 || evidenceType3 == this.ghosts[i].evidence3))
			{
				flag3 = true;
			}
			if (flag && evidenceType2 == JournalController.evidenceType.None && evidenceType3 == JournalController.evidenceType.None)
			{
				this.values.Add(this.ghosts[i]);
			}
			else if (flag2 && evidenceType == JournalController.evidenceType.None && evidenceType3 == JournalController.evidenceType.None)
			{
				this.values.Add(this.ghosts[i]);
			}
			else if (flag3 && evidenceType == JournalController.evidenceType.None && evidenceType2 == JournalController.evidenceType.None)
			{
				this.values.Add(this.ghosts[i]);
			}
			else if (flag && flag2 && evidenceType3 == JournalController.evidenceType.None)
			{
				this.values.Add(this.ghosts[i]);
			}
			else if (flag && flag3 && evidenceType2 == JournalController.evidenceType.None)
			{
				this.values.Add(this.ghosts[i]);
			}
			else if (flag2 && flag3 && evidenceType == JournalController.evidenceType.None)
			{
				this.values.Add(this.ghosts[i]);
			}
			else if (flag && flag2 && flag3)
			{
				this.values.Add(this.ghosts[i]);
			}
			else if (evidenceType == JournalController.evidenceType.None && evidenceType2 == JournalController.evidenceType.None && evidenceType3 == JournalController.evidenceType.None)
			{
				this.values.Add(this.ghosts[i]);
			}
		}
		if (evidenceType == JournalController.evidenceType.None && evidenceType2 == JournalController.evidenceType.None && evidenceType3 == JournalController.evidenceType.None)
		{
			this.values.RemoveAt(0);
		}
		this.ghostTypeIndex = 0;
		this.ghostTypeText.text = this.values[0].localisedName;
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x0002C8EC File Offset: 0x0002AAEC
	public void ChangeGhostTypeButton(int value)
	{
		this.ghostTypeIndex += value;
		if (this.ghostTypeIndex < 0)
		{
			this.ghostTypeIndex = this.values.Count - 1;
		}
		if (this.ghostTypeIndex == this.values.Count)
		{
			this.ghostTypeIndex = 0;
		}
		this.ghostTypeText.text = this.values[this.ghostTypeIndex].localisedName;
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x0002C960 File Offset: 0x0002AB60
	public void ChangeEvidence1Button(int value)
	{
		this.evidence1Index += value;
		if (this.evidence1Index < 0)
		{
			this.evidence1Index = this.evidenceNames.Count - 1;
		}
		if (this.evidence1Index == this.evidenceNames.Count)
		{
			this.evidence1Index = 0;
		}
		this.evidence1Text.text = this.evidenceNames[this.evidence1Index];
		this.SetGhostTypes();
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0002C9D4 File Offset: 0x0002ABD4
	public void ChangeEvidence2Button(int value)
	{
		this.evidence2Index += value;
		if (this.evidence2Index < 0)
		{
			this.evidence2Index = this.evidenceNames.Count - 1;
		}
		if (this.evidence2Index == this.evidenceNames.Count)
		{
			this.evidence2Index = 0;
		}
		this.evidence2Text.text = this.evidenceNames[this.evidence2Index];
		this.SetGhostTypes();
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x0002CA48 File Offset: 0x0002AC48
	public void ChangeEvidence3Button(int value)
	{
		this.evidence3Index += value;
		if (this.evidence3Index < 0)
		{
			this.evidence3Index = this.evidenceNames.Count - 1;
		}
		if (this.evidence3Index == this.evidenceNames.Count)
		{
			this.evidence3Index = 0;
		}
		this.evidence3Text.text = this.evidenceNames[this.evidence3Index];
		this.SetGhostTypes();
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x0002CABC File Offset: 0x0002ACBC
	private void OnPlayerSpawned()
	{
		if (!XRDevice.isPresent && !this.isVRJournal && GameController.instance && GameController.instance.myPlayer != null && GameController.instance.myPlayer.player.playerInput)
		{
			GameController.instance.myPlayer.player.playerInput.actions["Journal"].performed += delegate(InputAction.CallbackContext ctx)
			{
				this.OpenCloseJournal();
			};
		}
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x0002CB40 File Offset: 0x0002AD40
	private void OnDisable()
	{
		if (!XRDevice.isPresent && !this.isVRJournal && GameController.instance && GameController.instance.myPlayer != null && GameController.instance.myPlayer.player.playerInput)
		{
			GameController.instance.myPlayer.player.playerInput.actions["Journal"].performed -= delegate(InputAction.CallbackContext ctx)
			{
				this.OpenCloseJournal();
			};
		}
	}

	// Token: 0x0400072D RID: 1837
	[SerializeField]
	private GameObject content;

	// Token: 0x0400072E RID: 1838
	[SerializeField]
	private AudioSource openSource;

	// Token: 0x0400072F RID: 1839
	[SerializeField]
	private AudioClip openClip;

	// Token: 0x04000730 RID: 1840
	[SerializeField]
	private AudioClip closeClip;

	// Token: 0x04000731 RID: 1841
	[HideInInspector]
	public bool isOpen;

	// Token: 0x04000732 RID: 1842
	[SerializeField]
	private bool isVRJournal;

	// Token: 0x04000733 RID: 1843
	[SerializeField]
	private GameObject[] pages;

	// Token: 0x04000734 RID: 1844
	private int index;

	// Token: 0x04000735 RID: 1845
	[SerializeField]
	private Image[] photos;

	// Token: 0x04000736 RID: 1846
	[SerializeField]
	private Text[] photosNames;

	// Token: 0x04000737 RID: 1847
	private int photosAmount;

	// Token: 0x04000738 RID: 1848
	private int resWidth = 256;

	// Token: 0x04000739 RID: 1849
	private int resHeight = 144;

	// Token: 0x0400073A RID: 1850
	[HideInInspector]
	public PhotonView view;

	// Token: 0x0400073B RID: 1851
	[Header("Keys Page")]
	private int keyAmount;

	// Token: 0x0400073C RID: 1852
	[SerializeField]
	private Text Key1Text;

	// Token: 0x0400073D RID: 1853
	[SerializeField]
	private Text Key2Text;

	// Token: 0x0400073E RID: 1854
	[SerializeField]
	private Text Key3Text;

	// Token: 0x0400073F RID: 1855
	[SerializeField]
	private Text Key4Text;

	// Token: 0x04000740 RID: 1856
	[SerializeField]
	private Text Key5Text;

	// Token: 0x04000741 RID: 1857
	[SerializeField]
	private Text Key6Text;

	// Token: 0x04000742 RID: 1858
	[Header("Evidence Page")]
	private List<JournalController.Ghost> values = new List<JournalController.Ghost>();

	// Token: 0x04000743 RID: 1859
	private List<JournalController.Ghost> ghosts = new List<JournalController.Ghost>();

	// Token: 0x04000744 RID: 1860
	private List<string> evidenceNames = new List<string>();

	// Token: 0x04000745 RID: 1861
	[SerializeField]
	private Text evidence1Text;

	// Token: 0x04000746 RID: 1862
	private int evidence1Index;

	// Token: 0x04000747 RID: 1863
	[SerializeField]
	private Text evidence2Text;

	// Token: 0x04000748 RID: 1864
	private int evidence2Index;

	// Token: 0x04000749 RID: 1865
	[SerializeField]
	private Text evidence3Text;

	// Token: 0x0400074A RID: 1866
	private int evidence3Index;

	// Token: 0x0400074B RID: 1867
	[SerializeField]
	private Text ghostTypeText;

	// Token: 0x0400074C RID: 1868
	private int ghostTypeIndex;

	// Token: 0x02000513 RID: 1299
	public enum evidenceType
	{
		// Token: 0x0400246A RID: 9322
		None,
		// Token: 0x0400246B RID: 9323
		EMF,
		// Token: 0x0400246C RID: 9324
		SpiritBox,
		// Token: 0x0400246D RID: 9325
		Fingerprints,
		// Token: 0x0400246E RID: 9326
		GhostOrb,
		// Token: 0x0400246F RID: 9327
		GhostWritingBook,
		// Token: 0x04002470 RID: 9328
		Temperature
	}

	// Token: 0x02000514 RID: 1300
	private struct Ghost
	{
		// Token: 0x04002471 RID: 9329
		public GhostTraits.Type type;

		// Token: 0x04002472 RID: 9330
		public string localisedName;

		// Token: 0x04002473 RID: 9331
		public JournalController.evidenceType evidence1;

		// Token: 0x04002474 RID: 9332
		public JournalController.evidenceType evidence2;

		// Token: 0x04002475 RID: 9333
		public JournalController.evidenceType evidence3;
	}
}
