using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000172 RID: 370
[RequireComponent(typeof(Rigidbody))]
public class ChangeMixer : MonoBehaviour
{
	// Token: 0x060009D3 RID: 2515 RVA: 0x0003C5AE File Offset: 0x0003A7AE
	private void Start()
	{
		if (this.isDefaultSnapshot)
		{
			this.snapshot.TransitionTo(0f);
		}
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x0003C5C8 File Offset: 0x0003A7C8
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<EVPRecorder>())
		{
			other.GetComponent<EVPRecorder>().loopSource.outputAudioMixerGroup = SoundController.instance.GetAudioGroupFromSnapshot(this.snapshot);
			other.GetComponent<EVPRecorder>().soundSource.outputAudioMixerGroup = SoundController.instance.GetAudioGroupFromSnapshot(this.snapshot);
		}
		else if (other.GetComponent<EMFReader>())
		{
			other.GetComponent<EMFReader>().source.outputAudioMixerGroup = SoundController.instance.GetAudioGroupFromSnapshot(this.snapshot);
		}
		if (other.transform.root.CompareTag("Player"))
		{
			if (other.isTrigger)
			{
				return;
			}
			if (other.GetComponent<PhotonObjectInteract>() && !other.GetComponent<WalkieTalkie>())
			{
				return;
			}
			if (other.GetComponent<ThermometerSpot>())
			{
				return;
			}
			if (GameController.instance == null)
			{
				return;
			}
			if (GameController.instance.myPlayer == null)
			{
				return;
			}
			Player component = other.transform.root.GetComponent<Player>();
			if (component.currentHeldObject != null)
			{
				if (component.currentHeldObject.GetComponent<EVPRecorder>())
				{
					component.currentHeldObject.GetComponent<EVPRecorder>().loopSource.outputAudioMixerGroup = SoundController.instance.GetAudioGroupFromSnapshot(this.snapshot);
					component.currentHeldObject.GetComponent<EVPRecorder>().soundSource.outputAudioMixerGroup = SoundController.instance.GetAudioGroupFromSnapshot(this.snapshot);
				}
				else if (component.currentHeldObject.GetComponent<EMFReader>())
				{
					component.currentHeldObject.GetComponent<EMFReader>().source.outputAudioMixerGroup = SoundController.instance.GetAudioGroupFromSnapshot(this.snapshot);
				}
			}
			if (component.view.isMine)
			{
				if (this.reverbZone)
				{
					this.reverbZone.gameObject.SetActive(this.activateReverbZone);
				}
				this.snapshot.TransitionTo(0.4f);
			}
			component.currentPlayerSnapshot = this.snapshot;
			component.voiceOcclusion.SetVoiceMixer();
			if (MapController.instance && !this.isDeadZoneMixer)
			{
				MapController.instance.ChangePlayerFloor(component, this.floorType);
			}
		}
	}

	// Token: 0x040009F4 RID: 2548
	[SerializeField]
	private AudioMixerSnapshot snapshot;

	// Token: 0x040009F5 RID: 2549
	[SerializeField]
	private AudioReverbZone reverbZone;

	// Token: 0x040009F6 RID: 2550
	[SerializeField]
	private bool activateReverbZone;

	// Token: 0x040009F7 RID: 2551
	[SerializeField]
	private bool isDefaultSnapshot;

	// Token: 0x040009F8 RID: 2552
	[SerializeField]
	private LevelRoom.Type floorType = LevelRoom.Type.firstFloor;

	// Token: 0x040009F9 RID: 2553
	[SerializeField]
	private bool isDeadZoneMixer;
}
