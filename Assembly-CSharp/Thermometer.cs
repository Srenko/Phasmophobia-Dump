using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200016A RID: 362
public class Thermometer : MonoBehaviour
{
	// Token: 0x060009A9 RID: 2473 RVA: 0x0003B874 File Offset: 0x00039A74
	private void Awake()
	{
		this.noise = base.GetComponentInChildren<Noise>();
		this.source = base.GetComponent<AudioSource>();
		this.view = base.GetComponent<PhotonView>();
		this.rend = base.GetComponent<MeshRenderer>();
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		this.noise.gameObject.SetActive(false);
		this.isOn = false;
		this.timer = 0.8f;
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0003B8E0 File Offset: 0x00039AE0
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
		this.temperatureText.text = "";
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0003B909 File Offset: 0x00039B09
	private void Update()
	{
		if (this.isOn)
		{
			this.timer -= Time.deltaTime;
			if (this.timer < 0f)
			{
				this.UpdateSpotPosition();
				this.timer = 0.8f;
			}
		}
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x0003B943 File Offset: 0x00039B43
	private void Use()
	{
		this.view.RPC("NetworkedUse", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x0003B95C File Offset: 0x00039B5C
	[PunRPC]
	private void NetworkedUse()
	{
		this.isOn = !this.isOn;
		base.StartCoroutine(this.PlayNoiseObject());
		this.source.Play();
		if (this.isOn)
		{
			this.rend.material.EnableKeyword("_EMISSION");
			this.canvasObj.SetActive(true);
			return;
		}
		this.rend.material.DisableKeyword("_EMISSION");
		this.canvasObj.SetActive(false);
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x0003B9DC File Offset: 0x00039BDC
	private void UpdateSpotPosition()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.back), out raycastHit, 6f, this.mask, QueryTriggerInteraction.Ignore))
		{
			this.spot.position = raycastHit.point;
			this.currentTemp = ((PlayerPrefs.GetInt("degreesValue") == 0) ? this.room.temperature : (this.room.temperature * 1.8f + 32f));
			this.temperatureText.text = (this.currentTemp + Random.Range(-2f, 2f)).ToString("0.0") + ((PlayerPrefs.GetInt("degreesValue") == 0) ? "C" : "F");
		}
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x0003BAB4 File Offset: 0x00039CB4
	public void SetTemperatureValue(LevelRoom room)
	{
		if (room == LevelController.instance.outsideRoom && GameController.instance.myPlayer.player.currentRoom != room)
		{
			return;
		}
		if (room.floorType != GameController.instance.myPlayer.player.currentRoom.floorType)
		{
			return;
		}
		this.room = room;
		if (room.temperature < 10f && MissionTemperature.instance != null && room != LevelController.instance.outsideRoom && !MissionTemperature.instance.completed)
		{
			MissionTemperature.instance.CompleteMission();
		}
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x0003BB5B File Offset: 0x00039D5B
	private IEnumerator PlayNoiseObject()
	{
		this.noise.gameObject.SetActive(true);
		yield return new WaitForEndOfFrame();
		this.noise.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x040009C0 RID: 2496
	private AudioSource source;

	// Token: 0x040009C1 RID: 2497
	private PhotonView view;

	// Token: 0x040009C2 RID: 2498
	private PhotonObjectInteract photonInteract;

	// Token: 0x040009C3 RID: 2499
	public LayerMask mask;

	// Token: 0x040009C4 RID: 2500
	private Noise noise;

	// Token: 0x040009C5 RID: 2501
	private MeshRenderer rend;

	// Token: 0x040009C6 RID: 2502
	private bool isOn;

	// Token: 0x040009C7 RID: 2503
	private float timer = 0.5f;

	// Token: 0x040009C8 RID: 2504
	[SerializeField]
	private Text temperatureText;

	// Token: 0x040009C9 RID: 2505
	[SerializeField]
	private Transform raycastSpot;

	// Token: 0x040009CA RID: 2506
	[SerializeField]
	private GameObject canvasObj;

	// Token: 0x040009CB RID: 2507
	[SerializeField]
	private Transform spot;

	// Token: 0x040009CC RID: 2508
	private float currentTemp;

	// Token: 0x040009CD RID: 2509
	private LevelRoom room;
}
