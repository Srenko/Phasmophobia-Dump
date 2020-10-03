using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CB RID: 459
public class SoundSensorData : MonoBehaviour
{
	// Token: 0x06000C91 RID: 3217 RVA: 0x00050365 File Offset: 0x0004E565
	private void Awake()
	{
		SoundSensorData.instance = this;
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x00050370 File Offset: 0x0004E570
	public void UpdateSensorValue(int id, float volume)
	{
		switch (id)
		{
		case 0:
			this.sensor1Bar.offsetMax = new Vector2(this.sensor1Bar.offsetMax.x, volume * 500f);
			return;
		case 1:
			this.sensor2Bar.offsetMax = new Vector2(this.sensor2Bar.offsetMax.x, volume * 500f);
			return;
		case 2:
			this.sensor3Bar.offsetMax = new Vector2(this.sensor3Bar.offsetMax.x, volume * 500f);
			return;
		case 3:
			this.sensor4Bar.offsetMax = new Vector2(this.sensor4Bar.offsetMax.x, volume * 500f);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x00050434 File Offset: 0x0004E634
	public void SetText(SoundSensor sensor)
	{
		if (!this.Sensors.Contains(sensor))
		{
			this.Sensors.Add(sensor);
			sensor.id = this.Sensors.Count - 1;
		}
		switch (sensor.id)
		{
		case 0:
			this.sensor_1_Text.text = this.Sensors[0].roomName;
			return;
		case 1:
			this.sensor_2_Text.text = this.Sensors[1].roomName;
			return;
		case 2:
			this.sensor_3_Text.text = this.Sensors[2].roomName;
			return;
		case 3:
			this.sensor_4_Text.text = this.Sensors[3].roomName;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x00050500 File Offset: 0x0004E700
	public void RemoveText(SoundSensor sensor)
	{
		switch (sensor.id)
		{
		case 0:
			this.sensor_1_Text.text = "";
			return;
		case 1:
			this.sensor_2_Text.text = "";
			return;
		case 2:
			this.sensor_3_Text.text = "";
			return;
		case 3:
			this.sensor_4_Text.text = "";
			return;
		default:
			return;
		}
	}

	// Token: 0x04000D35 RID: 3381
	public static SoundSensorData instance;

	// Token: 0x04000D36 RID: 3382
	private List<SoundSensor> Sensors = new List<SoundSensor>();

	// Token: 0x04000D37 RID: 3383
	[SerializeField]
	private RectTransform sensor1Bar;

	// Token: 0x04000D38 RID: 3384
	[SerializeField]
	private RectTransform sensor2Bar;

	// Token: 0x04000D39 RID: 3385
	[SerializeField]
	private RectTransform sensor3Bar;

	// Token: 0x04000D3A RID: 3386
	[SerializeField]
	private RectTransform sensor4Bar;

	// Token: 0x04000D3B RID: 3387
	[SerializeField]
	private Text sensor_1_Text;

	// Token: 0x04000D3C RID: 3388
	[SerializeField]
	private Text sensor_2_Text;

	// Token: 0x04000D3D RID: 3389
	[SerializeField]
	private Text sensor_3_Text;

	// Token: 0x04000D3E RID: 3390
	[SerializeField]
	private Text sensor_4_Text;
}
