using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
public class Clock : MonoBehaviour
{
	// Token: 0x060000E2 RID: 226 RVA: 0x00003F60 File Offset: 0x00002160
	private void Start()
	{
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x000071F8 File Offset: 0x000053F8
	private void Update()
	{
		if (this.analog)
		{
			TimeSpan timeOfDay = DateTime.Now.TimeOfDay;
			this.hours.localRotation = Quaternion.Euler(0f, 0f, (float)timeOfDay.TotalHours * 30f);
			this.minutes.localRotation = Quaternion.Euler(0f, 0f, (float)timeOfDay.TotalMinutes * 6f);
			this.seconds.localRotation = Quaternion.Euler(0f, 0f, (float)timeOfDay.TotalSeconds * 6f);
			return;
		}
		DateTime now = DateTime.Now;
		this.hours.localRotation = Quaternion.Euler(0f, 0f, (float)now.Hour * 30f);
		this.minutes.localRotation = Quaternion.Euler(0f, 0f, (float)now.Minute * 6f);
		this.seconds.localRotation = Quaternion.Euler(0f, 0f, (float)now.Second * 6f);
	}

	// Token: 0x040000D5 RID: 213
	private const float hoursToDegrees = 30f;

	// Token: 0x040000D6 RID: 214
	private const float minutesToDegrees = 6f;

	// Token: 0x040000D7 RID: 215
	private const float secondsToDegrees = 6f;

	// Token: 0x040000D8 RID: 216
	public Transform hours;

	// Token: 0x040000D9 RID: 217
	public Transform minutes;

	// Token: 0x040000DA RID: 218
	public Transform seconds;

	// Token: 0x040000DB RID: 219
	public bool analog = true;
}
