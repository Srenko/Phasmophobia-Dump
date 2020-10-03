using System;
using ExitGames.Client.Photon.Voice;
using UnityEngine;

// Token: 0x02000013 RID: 19
internal class ToneAudioReader : IAudioReader<float>, IDataReader<float>, IDisposable, IAudioSource
{
	// Token: 0x0600009A RID: 154 RVA: 0x00004E99 File Offset: 0x00003099
	public ToneAudioReader()
	{
		this.k = 2764.6015351590181 / (double)this.SamplingRate;
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x0600009B RID: 155 RVA: 0x00004EB8 File Offset: 0x000030B8
	public int Channels
	{
		get
		{
			return 2;
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x0600009C RID: 156 RVA: 0x00004EBB File Offset: 0x000030BB
	public int SamplingRate
	{
		get
		{
			return 24000;
		}
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00003F60 File Offset: 0x00002160
	public void Dispose()
	{
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00004EC4 File Offset: 0x000030C4
	public bool Read(float[] buf)
	{
		int num = buf.Length / this.Channels;
		long num2 = (long)(AudioSettings.dspTime * (double)this.SamplingRate);
		long num3 = num2 - this.timeSamples;
		if (Math.Abs(num3) > (long)(this.SamplingRate / 4))
		{
			Debug.LogWarningFormat("ToneAudioReader sample time is out: {0} / {1}", new object[]
			{
				this.timeSamples,
				num2
			});
			num3 = (long)num;
			this.timeSamples = num2 - (long)num;
		}
		if (num3 < (long)num)
		{
			return false;
		}
		int num4 = 0;
		for (int i = 0; i < num; i++)
		{
			long num5 = this.timeSamples;
			this.timeSamples = num5 + 1L;
			float num6 = (float)Math.Sin((double)num5 * this.k) * 0.2f;
			for (int j = 0; j < this.Channels; j++)
			{
				buf[num4++] = num6;
			}
		}
		return true;
	}

	// Token: 0x0400006C RID: 108
	private double k;

	// Token: 0x0400006D RID: 109
	private long timeSamples;
}
