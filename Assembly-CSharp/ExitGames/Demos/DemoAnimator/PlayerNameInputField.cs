using System;
using UnityEngine;
using UnityEngine.UI;

namespace ExitGames.Demos.DemoAnimator
{
	// Token: 0x0200048A RID: 1162
	[RequireComponent(typeof(InputField))]
	public class PlayerNameInputField : MonoBehaviour
	{
		// Token: 0x06002436 RID: 9270 RVA: 0x000B13FC File Offset: 0x000AF5FC
		private void Start()
		{
			string text = "";
			InputField component = base.GetComponent<InputField>();
			if (component != null && PlayerPrefs.HasKey(PlayerNameInputField.playerNamePrefKey))
			{
				text = PlayerPrefs.GetString(PlayerNameInputField.playerNamePrefKey);
				component.text = text;
			}
			PhotonNetwork.playerName = text;
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000B1443 File Offset: 0x000AF643
		public void SetPlayerName(string value)
		{
			PhotonNetwork.playerName = value + " ";
			PlayerPrefs.SetString(PlayerNameInputField.playerNamePrefKey, value);
		}

		// Token: 0x0400217C RID: 8572
		private static string playerNamePrefKey = "PlayerName";
	}
}
