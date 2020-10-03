using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR;
using VRTK;

// Token: 0x0200012B RID: 299
public class TrainingController : MonoBehaviour
{
	// Token: 0x060007FC RID: 2044 RVA: 0x0002FC5E File Offset: 0x0002DE5E
	private void Awake()
	{
		TrainingController.instance = this;
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x0002FC68 File Offset: 0x0002DE68
	private void Start()
	{
		if (FileBasedPrefs.GetInt("isTutorial", 0) == 1)
		{
			GameController.instance.OnGhostSpawned.AddListener(new UnityAction(this.GhostSpawned));
			GameController.instance.isTutorial = true;
			this.missionWhiteBoard.SetActive(false);
			this.tutorialWhiteBoard.SetActive(true);
			SetupPhaseController.instance.BeginHuntingPhase();
			this.tvLight.enabled = true;
			this.tvLight.intensity = 0.4f;
			this.trainingTVCanvas.gameObject.SetActive(true);
			Object.Destroy(this.tvRemote);
			this.trainingRemote.enabled = true;
			for (int i = 0; i < this.probes.Length; i++)
			{
				this.probes[i].RenderProbe();
			}
			for (int j = 0; j < this.slides.Length; j++)
			{
				this.slides[j].SetActive(false);
			}
			this.slides[0].SetActive(true);
			if (XRDevice.isPresent)
			{
				this.VRControls.SetActive(true);
				VRTK_DeviceFinder.Headsets headsetType = VRTK_DeviceFinder.GetHeadsetType(true);
				if (headsetType == VRTK_DeviceFinder.Headsets.OculusRift)
				{
					this.OculusControls.SetActive(true);
					return;
				}
				if (headsetType == VRTK_DeviceFinder.Headsets.Vive)
				{
					this.ViveControls.SetActive(true);
					return;
				}
			}
			else
			{
				this.NonVRControls.SetActive(true);
			}
		}
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x0002FDAA File Offset: 0x0002DFAA
	public void GhostSpawned()
	{
		this.tutorialMissionText.text = LocalisationSystem.GetLocalisedValue("Training_MissionFirstPart") + LevelController.instance.currentGhost.ghostInfo.favouriteRoom.roomName + ".";
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x0002FDE4 File Offset: 0x0002DFE4
	public void NextSlide()
	{
		this.currentIndex++;
		if (this.currentIndex >= this.slides.Length)
		{
			this.currentIndex = 0;
		}
		for (int i = 0; i < this.slides.Length; i++)
		{
			this.slides[i].SetActive(false);
		}
		this.slides[this.currentIndex].SetActive(true);
	}

	// Token: 0x040007F0 RID: 2032
	public static TrainingController instance;

	// Token: 0x040007F1 RID: 2033
	[SerializeField]
	private GameObject tutorialWhiteBoard;

	// Token: 0x040007F2 RID: 2034
	[SerializeField]
	private GameObject missionWhiteBoard;

	// Token: 0x040007F3 RID: 2035
	[SerializeField]
	private Text tutorialMissionText;

	// Token: 0x040007F4 RID: 2036
	[SerializeField]
	private GameObject[] slides;

	// Token: 0x040007F5 RID: 2037
	private int currentIndex;

	// Token: 0x040007F6 RID: 2038
	[SerializeField]
	private Light tvLight;

	// Token: 0x040007F7 RID: 2039
	[SerializeField]
	private LightSwitch tvRemote;

	// Token: 0x040007F8 RID: 2040
	[SerializeField]
	private Canvas trainingTVCanvas;

	// Token: 0x040007F9 RID: 2041
	[SerializeField]
	private TrainingRemote trainingRemote;

	// Token: 0x040007FA RID: 2042
	[SerializeField]
	private ReflectionProbe[] probes;

	// Token: 0x040007FB RID: 2043
	[SerializeField]
	private GameObject NonVRControls;

	// Token: 0x040007FC RID: 2044
	[SerializeField]
	private GameObject VRControls;

	// Token: 0x040007FD RID: 2045
	[SerializeField]
	private GameObject ViveControls;

	// Token: 0x040007FE RID: 2046
	[SerializeField]
	private GameObject OculusControls;
}
