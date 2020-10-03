using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CA RID: 458
public class MotionSensorData : MonoBehaviour
{
	// Token: 0x06000C88 RID: 3208 RVA: 0x000500A3 File Offset: 0x0004E2A3
	private void Awake()
	{
		MotionSensorData.instance = this;
		this.startColor = ((this.image1 == null) ? Color.red : this.image1.color);
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x000500D4 File Offset: 0x0004E2D4
	public void Detected(MotionSensor sensor)
	{
		this.source.Play();
		if (sensor.id == 0)
		{
			this.detected1 = true;
			base.StartCoroutine(this.DisableMotionSensor1Colour());
			if (this.image1)
			{
				this.image1.color = Color.green;
				return;
			}
		}
		else if (sensor.id == 1)
		{
			this.detected2 = true;
			base.StartCoroutine(this.DisableMotionSensor2Colour());
			if (this.image2)
			{
				this.image2.color = Color.green;
				return;
			}
		}
		else if (sensor.id == 2)
		{
			this.detected3 = true;
			base.StartCoroutine(this.DisableMotionSensor3Colour());
			if (this.image3)
			{
				this.image3.color = Color.green;
				return;
			}
		}
		else if (sensor.id == 3)
		{
			this.detected4 = true;
			base.StartCoroutine(this.DisableMotionSensor4Colour());
			if (this.image4)
			{
				this.image4.color = Color.green;
			}
		}
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x000501DC File Offset: 0x0004E3DC
	private IEnumerator DisableMotionSensor1Colour()
	{
		yield return new WaitForSeconds(6f);
		if (this.detected1)
		{
			yield return null;
		}
		this.detected1 = false;
		if (this.image1)
		{
			this.image1.color = this.startColor;
		}
		yield break;
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x000501EB File Offset: 0x0004E3EB
	private IEnumerator DisableMotionSensor2Colour()
	{
		yield return new WaitForSeconds(6f);
		if (this.detected2)
		{
			yield return null;
		}
		this.detected2 = false;
		if (this.image2)
		{
			this.image2.color = this.startColor;
		}
		yield break;
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x000501FA File Offset: 0x0004E3FA
	private IEnumerator DisableMotionSensor3Colour()
	{
		yield return new WaitForSeconds(6f);
		if (this.detected3)
		{
			yield return null;
		}
		this.detected3 = false;
		if (this.image3)
		{
			this.image3.color = this.startColor;
		}
		yield break;
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x00050209 File Offset: 0x0004E409
	private IEnumerator DisableMotionSensor4Colour()
	{
		yield return new WaitForSeconds(6f);
		if (this.detected4)
		{
			yield return null;
		}
		this.detected4 = false;
		if (this.image4)
		{
			this.image4.color = this.startColor;
		}
		yield break;
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x00050218 File Offset: 0x0004E418
	public void SetText(MotionSensor sensor)
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

	// Token: 0x06000C8F RID: 3215 RVA: 0x000502E4 File Offset: 0x0004E4E4
	public void RemoveText(MotionSensor sensor)
	{
		switch (sensor.id)
		{
		case 0:
			this.sensor_1_Text.text = "Sensor 1";
			return;
		case 1:
			this.sensor_2_Text.text = "Sensor 2";
			return;
		case 2:
			this.sensor_3_Text.text = "Sensor 3";
			return;
		case 3:
			this.sensor_4_Text.text = "Sensor 4";
			return;
		default:
			return;
		}
	}

	// Token: 0x04000D25 RID: 3365
	public static MotionSensorData instance;

	// Token: 0x04000D26 RID: 3366
	[HideInInspector]
	public List<MotionSensor> Sensors = new List<MotionSensor>();

	// Token: 0x04000D27 RID: 3367
	private bool detected1;

	// Token: 0x04000D28 RID: 3368
	private bool detected2;

	// Token: 0x04000D29 RID: 3369
	private bool detected3;

	// Token: 0x04000D2A RID: 3370
	private bool detected4;

	// Token: 0x04000D2B RID: 3371
	public Image image1;

	// Token: 0x04000D2C RID: 3372
	public Image image2;

	// Token: 0x04000D2D RID: 3373
	public Image image3;

	// Token: 0x04000D2E RID: 3374
	public Image image4;

	// Token: 0x04000D2F RID: 3375
	private Color startColor;

	// Token: 0x04000D30 RID: 3376
	[SerializeField]
	private Text sensor_1_Text;

	// Token: 0x04000D31 RID: 3377
	[SerializeField]
	private Text sensor_2_Text;

	// Token: 0x04000D32 RID: 3378
	[SerializeField]
	private Text sensor_3_Text;

	// Token: 0x04000D33 RID: 3379
	[SerializeField]
	private Text sensor_4_Text;

	// Token: 0x04000D34 RID: 3380
	[SerializeField]
	private AudioSource source;
}
