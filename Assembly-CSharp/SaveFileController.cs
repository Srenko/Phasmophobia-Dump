﻿using System;
using UnityEngine;

// Token: 0x02000124 RID: 292
public class SaveFileController : MonoBehaviour
{
	// Token: 0x060007DE RID: 2014 RVA: 0x0002EEB0 File Offset: 0x0002D0B0
	public void Awake()
	{
		if (!FileBasedPrefs.HasKey("myTotalExp"))
		{
			FileBasedPrefs.SetInt("myTotalExp", PlayerPrefs.GetInt("myTotalExp"));
		}
		if (!FileBasedPrefs.HasKey("PlayersMoney"))
		{
			FileBasedPrefs.SetInt("PlayersMoney", PlayerPrefs.GetInt("PlayersMoney"));
		}
		if (!FileBasedPrefs.HasKey("EMFReaderInventory"))
		{
			FileBasedPrefs.SetInt("EMFReaderInventory", PlayerPrefs.GetInt("EMFReaderInventory"));
		}
		if (!FileBasedPrefs.HasKey("FlashlightInventory"))
		{
			FileBasedPrefs.SetInt("FlashlightInventory", PlayerPrefs.GetInt("FlashlightInventory"));
		}
		if (!FileBasedPrefs.HasKey("CameraInventory"))
		{
			FileBasedPrefs.SetInt("CameraInventory", PlayerPrefs.GetInt("CameraInventory"));
		}
		if (!FileBasedPrefs.HasKey("LighterInventory"))
		{
			FileBasedPrefs.SetInt("LighterInventory", PlayerPrefs.GetInt("LighterInventory"));
		}
		if (!FileBasedPrefs.HasKey("CandleInventory"))
		{
			FileBasedPrefs.SetInt("CandleInventory", PlayerPrefs.GetInt("CandleInventory"));
		}
		if (!FileBasedPrefs.HasKey("UVFlashlightInventory"))
		{
			FileBasedPrefs.SetInt("UVFlashlightInventory", PlayerPrefs.GetInt("UVFlashlightInventory"));
		}
		if (!FileBasedPrefs.HasKey("CrucifixInventory"))
		{
			FileBasedPrefs.SetInt("CrucifixInventory", PlayerPrefs.GetInt("CrucifixInventory"));
		}
		if (!FileBasedPrefs.HasKey("DSLRCameraInventory"))
		{
			FileBasedPrefs.SetInt("DSLRCameraInventory", PlayerPrefs.GetInt("DSLRCameraInventory"));
		}
		if (!FileBasedPrefs.HasKey("EVPRecorderInventory"))
		{
			FileBasedPrefs.SetInt("EVPRecorderInventory", PlayerPrefs.GetInt("EVPRecorderInventory"));
		}
		if (!FileBasedPrefs.HasKey("SaltInventory"))
		{
			FileBasedPrefs.SetInt("SaltInventory", PlayerPrefs.GetInt("SaltInventory"));
		}
		if (!FileBasedPrefs.HasKey("SageInventory"))
		{
			FileBasedPrefs.SetInt("SageInventory", PlayerPrefs.GetInt("SageInventory"));
		}
		if (!FileBasedPrefs.HasKey("TripodInventory"))
		{
			FileBasedPrefs.SetInt("TripodInventory", PlayerPrefs.GetInt("TripodInventory"));
		}
		if (!FileBasedPrefs.HasKey("StrongFlashlightInventory"))
		{
			FileBasedPrefs.SetInt("StrongFlashlightInventory", PlayerPrefs.GetInt("StrongFlashlightInventory"));
		}
		if (!FileBasedPrefs.HasKey("MotionSensorInventory"))
		{
			FileBasedPrefs.SetInt("MotionSensorInventory", PlayerPrefs.GetInt("MotionSensorInventory"));
		}
		if (!FileBasedPrefs.HasKey("SoundSensorInventory"))
		{
			FileBasedPrefs.SetInt("SoundSensorInventory", PlayerPrefs.GetInt("SoundSensorInventory"));
		}
		if (!FileBasedPrefs.HasKey("SanityPillsInventory"))
		{
			FileBasedPrefs.SetInt("SanityPillsInventory", PlayerPrefs.GetInt("SanityPillsInventory"));
		}
		if (!FileBasedPrefs.HasKey("ThermometerInventory"))
		{
			FileBasedPrefs.SetInt("ThermometerInventory", PlayerPrefs.GetInt("ThermometerInventory"));
		}
		if (!FileBasedPrefs.HasKey("GhostWritingBookInventory"))
		{
			FileBasedPrefs.SetInt("GhostWritingBookInventory", PlayerPrefs.GetInt("GhostWritingBookInventory"));
		}
		if (!FileBasedPrefs.HasKey("IRLightSensorInventory"))
		{
			FileBasedPrefs.SetInt("IRLightSensorInventory", PlayerPrefs.GetInt("IRLightSensorInventory"));
		}
		if (!FileBasedPrefs.HasKey("ParabolicMicrophoneInventory"))
		{
			FileBasedPrefs.SetInt("ParabolicMicrophoneInventory", PlayerPrefs.GetInt("ParabolicMicrophoneInventory"));
		}
		if (!FileBasedPrefs.HasKey("GlowstickInventory"))
		{
			FileBasedPrefs.SetInt("GlowstickInventory", PlayerPrefs.GetInt("GlowstickInventory"));
		}
		if (!FileBasedPrefs.HasKey("HeadMountedCameraInventory"))
		{
			FileBasedPrefs.SetInt("HeadMountedCameraInventory", PlayerPrefs.GetInt("HeadMountedCameraInventory"));
		}
		if (!FileBasedPrefs.HasKey("PlayerDied"))
		{
			FileBasedPrefs.SetInt("PlayerDied", PlayerPrefs.GetInt("PlayerDied"));
		}
		if (!FileBasedPrefs.HasKey("MissionStatus"))
		{
			FileBasedPrefs.SetInt("MissionStatus", PlayerPrefs.GetInt("MissionStatus"));
		}
		if (!FileBasedPrefs.HasKey("LevelDifficulty"))
		{
			FileBasedPrefs.SetInt("LevelDifficulty", PlayerPrefs.GetInt("LevelDifficulty"));
		}
		if (!FileBasedPrefs.HasKey("completedTraining"))
		{
			FileBasedPrefs.SetInt("completedTraining", PlayerPrefs.GetInt("completedTraining"));
		}
		PlayerPrefs.DeleteKey("myTotalExp");
		PlayerPrefs.DeleteKey("PlayersMoney");
		PlayerPrefs.DeleteKey("EMFReaderInventory");
		PlayerPrefs.DeleteKey("FlashlightInventory");
		PlayerPrefs.DeleteKey("CameraInventory");
		PlayerPrefs.DeleteKey("LighterInventory");
		PlayerPrefs.DeleteKey("CandleInventory");
		PlayerPrefs.DeleteKey("UVFlashlightInventory");
		PlayerPrefs.DeleteKey("CrucifixInventory");
		PlayerPrefs.DeleteKey("DSLRCameraInventory");
		PlayerPrefs.DeleteKey("EVPRecorderInventory");
		PlayerPrefs.DeleteKey("SaltInventory");
		PlayerPrefs.DeleteKey("SageInventory");
		PlayerPrefs.DeleteKey("TripodInventory");
		PlayerPrefs.DeleteKey("StrongFlashlightInventory");
		PlayerPrefs.DeleteKey("MotionSensorInventory");
		PlayerPrefs.DeleteKey("SoundSensorInventory");
		PlayerPrefs.DeleteKey("SanityPillsInventory");
		PlayerPrefs.DeleteKey("ThermometerInventory");
		PlayerPrefs.DeleteKey("GhostWritingBookInventory");
		PlayerPrefs.DeleteKey("IRLightSensorInventory");
		PlayerPrefs.DeleteKey("ParabolicMicrophoneInventory");
		PlayerPrefs.DeleteKey("GlowstickInventory");
		PlayerPrefs.DeleteKey("HeadMountedCameraInventory");
		this.SetDefaultValues();
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x0002F334 File Offset: 0x0002D534
	private void SetDefaultValues()
	{
		if (!FileBasedPrefs.HasKey("myTotalExp"))
		{
			FileBasedPrefs.SetInt("myTotalExp", 0);
		}
		if (!FileBasedPrefs.HasKey("PlayersMoney"))
		{
			FileBasedPrefs.SetInt("PlayersMoney", 0);
		}
		if (!FileBasedPrefs.HasKey("PlayersMoney"))
		{
			FileBasedPrefs.SetInt("PlayersMoney", 0);
		}
		if (!FileBasedPrefs.HasKey("EMFReaderInventory"))
		{
			FileBasedPrefs.SetInt("EMFReaderInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("FlashlightInventory"))
		{
			FileBasedPrefs.SetInt("FlashlightInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("CameraInventory"))
		{
			FileBasedPrefs.SetInt("CameraInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("LighterInventory"))
		{
			FileBasedPrefs.SetInt("LighterInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("CandleInventory"))
		{
			FileBasedPrefs.SetInt("CandleInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("UVFlashlightInventory"))
		{
			FileBasedPrefs.SetInt("UVFlashlightInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("CrucifixInventory"))
		{
			FileBasedPrefs.SetInt("CrucifixInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("DSLRCameraInventory"))
		{
			FileBasedPrefs.SetInt("DSLRCameraInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("EVPRecorderInventory"))
		{
			FileBasedPrefs.SetInt("EVPRecorderInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("SaltInventory"))
		{
			FileBasedPrefs.SetInt("SaltInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("SageInventory"))
		{
			FileBasedPrefs.SetInt("SageInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("TripodInventory"))
		{
			FileBasedPrefs.SetInt("TripodInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("StrongFlashlightInventory"))
		{
			FileBasedPrefs.SetInt("StrongFlashlightInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("MotionSensorInventory"))
		{
			FileBasedPrefs.SetInt("MotionSensorInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("SoundSensorInventory"))
		{
			FileBasedPrefs.SetInt("SoundSensorInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("SanityPillsInventory"))
		{
			FileBasedPrefs.SetInt("SanityPillsInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("ThermometerInventory"))
		{
			FileBasedPrefs.SetInt("ThermometerInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("GhostWritingBookInventory"))
		{
			FileBasedPrefs.SetInt("GhostWritingBookInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("IRLightSensorInventory"))
		{
			FileBasedPrefs.SetInt("IRLightSensorInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("ParabolicMicrophoneInventory"))
		{
			FileBasedPrefs.SetInt("ParabolicMicrophoneInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("GlowstickInventory"))
		{
			FileBasedPrefs.SetInt("GlowstickInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("HeadMountedCameraInventory"))
		{
			FileBasedPrefs.SetInt("HeadMountedCameraInventory", 0);
		}
		if (!FileBasedPrefs.HasKey("MissionStatus"))
		{
			FileBasedPrefs.SetInt("MissionStatus", 0);
		}
		if (!FileBasedPrefs.HasKey("setupPhase"))
		{
			FileBasedPrefs.SetInt("setupPhase", 0);
		}
		if (!FileBasedPrefs.HasKey("completedTraining"))
		{
			FileBasedPrefs.SetInt("completedTraining", 0);
		}
		if (!FileBasedPrefs.HasKey("StayInServerRoom"))
		{
			FileBasedPrefs.SetInt("StayInServerRoom", 0);
		}
		if (!FileBasedPrefs.HasKey("LevelDifficulty"))
		{
			FileBasedPrefs.SetInt("LevelDifficulty", 0);
		}
		if (!FileBasedPrefs.HasKey("PlayerDied"))
		{
			FileBasedPrefs.SetInt("PlayerDied", 0);
		}
		if (!FileBasedPrefs.HasKey("totalExp"))
		{
			FileBasedPrefs.SetInt("totalExp", 0);
		}
		this.playerStatsManager.UpdateExperience();
		this.playerStatsManager.UpdateLevel();
		this.playerStatsManager.UpdateMoney();
		this.storeManager.UpdatePlayerMoneyText();
	}

	// Token: 0x040007A6 RID: 1958
	[SerializeField]
	private StoreSDKManager storeSDKManager;

	// Token: 0x040007A7 RID: 1959
	[SerializeField]
	private PlayerStatsManager playerStatsManager;

	// Token: 0x040007A8 RID: 1960
	[SerializeField]
	private StoreManager storeManager;
}
