using System;
using UnityEngine;
using VRTK;

// Token: 0x0200012A RID: 298
public class TrailerController : MonoBehaviour
{
	// Token: 0x040007CF RID: 1999
	public bool inTrailerMode;

	// Token: 0x040007D0 RID: 2000
	private int eventIndex = 1;

	// Token: 0x040007D1 RID: 2001
	[SerializeField]
	private Camera cam;

	// Token: 0x040007D2 RID: 2002
	public TrailerCamera trailerCamera;

	// Token: 0x040007D3 RID: 2003
	[SerializeField]
	private Nightvision cameraNightVision;

	// Token: 0x040007D4 RID: 2004
	[SerializeField]
	private AudioSource playerArrivedSound;

	// Token: 0x040007D5 RID: 2005
	[SerializeField]
	private AudioSource radioBeCarefulSound;

	// Token: 0x040007D6 RID: 2006
	[SerializeField]
	private AudioSource playerNothingSound;

	// Token: 0x040007D7 RID: 2007
	[SerializeField]
	private AudioSource playerDirtyWaterSound;

	// Token: 0x040007D8 RID: 2008
	[SerializeField]
	private AudioSource radioSpiritOrWraithSound;

	// Token: 0x040007D9 RID: 2009
	[SerializeField]
	private AudioSource playerGhostInTheBasementSound;

	// Token: 0x040007DA RID: 2010
	[SerializeField]
	private AudioSource radioDontGetAReadingSound;

	// Token: 0x040007DB RID: 2011
	[HideInInspector]
	public EVPRecorder evpRecorder;

	// Token: 0x040007DC RID: 2012
	[SerializeField]
	private AudioSource playerConfirmEVPEvidenceSound;

	// Token: 0x040007DD RID: 2013
	[SerializeField]
	private AudioSource radioConfirmSpririt;

	// Token: 0x040007DE RID: 2014
	[SerializeField]
	private AudioSource playerEVPSound1;

	// Token: 0x040007DF RID: 2015
	[SerializeField]
	private AudioSource playerEVPSound2;

	// Token: 0x040007E0 RID: 2016
	[SerializeField]
	private AudioSource playerLowVitalsSound;

	// Token: 0x040007E1 RID: 2017
	[SerializeField]
	private LightSwitch tvRemote;

	// Token: 0x040007E2 RID: 2018
	[SerializeField]
	private EMF tvEMFSpot;

	// Token: 0x040007E3 RID: 2019
	[SerializeField]
	private Door basementDoor;

	// Token: 0x040007E4 RID: 2020
	[SerializeField]
	private AudioSource basementDoorOpeningSound;

	// Token: 0x040007E5 RID: 2021
	[SerializeField]
	private EMF basementEMFSpot;

	// Token: 0x040007E6 RID: 2022
	[SerializeField]
	private LevelRoom basement;

	// Token: 0x040007E7 RID: 2023
	[SerializeField]
	private GameObject basementGhost;

	// Token: 0x040007E8 RID: 2024
	[SerializeField]
	private LightSwitch basementLight;

	// Token: 0x040007E9 RID: 2025
	[SerializeField]
	private Door mainDoor;

	// Token: 0x040007EA RID: 2026
	[SerializeField]
	private Animator mainDoorAnim;

	// Token: 0x040007EB RID: 2027
	[SerializeField]
	private GameObject hallwayGhost;

	// Token: 0x040007EC RID: 2028
	[HideInInspector]
	public Torch torch;

	// Token: 0x040007ED RID: 2029
	[HideInInspector]
	public VRTK_SlideObjectControlAction xAxisSlide;

	// Token: 0x040007EE RID: 2030
	[HideInInspector]
	public VRTK_SlideObjectControlAction yAxisSlide;

	// Token: 0x040007EF RID: 2031
	private bool isCCTVActive;
}
