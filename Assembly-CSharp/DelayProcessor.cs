using System;
using ExitGames.Client.Photon.Voice;
using UnityEngine;

// Token: 0x0200000F RID: 15
internal class DelayProcessor : MonoBehaviour
{
	// Token: 0x0600008F RID: 143 RVA: 0x000042F0 File Offset: 0x000024F0
	private void PhotonVoiceCreated(PhotonVoiceRecorder.PhotonVoiceCreatedParams p)
	{
		if (p.Voice is LocalVoiceAudioFloat)
		{
			((LocalVoiceAudioFloat)p.Voice).AddPreProcessor(new LocalVoiceFramed<float>.IProcessor[]
			{
				new DelayProcessor.ProcessorFloat(p.AudioSource.SamplingRate / 2, 0.3f)
			});
			Debug.Log("DelayProcessor: ProcessorFloat added to local voice pipeline");
			return;
		}
		if (p.Voice is LocalVoiceAudioShort)
		{
			((LocalVoiceAudioShort)p.Voice).AddPreProcessor(new LocalVoiceFramed<short>.IProcessor[]
			{
				new DelayProcessor.ProcessorShort(p.AudioSource.SamplingRate / 2, 0.3f)
			});
			Debug.Log("DelayProcessor: ProcessorShort added to local voice pipeline");
			return;
		}
		Debug.LogError("DelayProcessor: Only float and short voices are supported. Trying to add processor to " + p.Voice.GetType());
	}

	// Token: 0x020004CE RID: 1230
	private abstract class Processor<T> : LocalVoiceFramed<T>.IProcessor, IDisposable
	{
		// Token: 0x060025F2 RID: 9714 RVA: 0x000BD866 File Offset: 0x000BBA66
		public Processor(int delaySamples, float factor)
		{
			this.prevBuf = new T[delaySamples * 2];
			this.prevBufPosRead = delaySamples;
			this.factor = factor;
		}

		// Token: 0x060025F3 RID: 9715
		protected abstract void mix(float factor, T[] buf, T[] prevBuf, ref int prevBufPosRead);

		// Token: 0x060025F4 RID: 9716 RVA: 0x000BD88C File Offset: 0x000BBA8C
		public T[] Process(T[] buf)
		{
			this.mix(this.factor, buf, this.prevBuf, ref this.prevBufPosRead);
			if (buf.Length > this.prevBuf.Length - this.prevBufPosWrite)
			{
				Array.Copy(buf, 0, this.prevBuf, this.prevBufPosWrite, this.prevBuf.Length - this.prevBufPosWrite);
				int length = buf.Length - (this.prevBuf.Length - this.prevBufPosWrite);
				Array.Copy(buf, this.prevBuf.Length - this.prevBufPosWrite, this.prevBuf, 0, length);
				this.prevBufPosWrite = length;
			}
			else
			{
				Array.Copy(buf, 0, this.prevBuf, this.prevBufPosWrite, buf.Length);
				this.prevBufPosWrite += buf.Length;
			}
			return buf;
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x00003F60 File Offset: 0x00002160
		public void Dispose()
		{
		}

		// Token: 0x04002366 RID: 9062
		private float factor;

		// Token: 0x04002367 RID: 9063
		private T[] prevBuf;

		// Token: 0x04002368 RID: 9064
		private int prevBufPosWrite;

		// Token: 0x04002369 RID: 9065
		private int prevBufPosRead;
	}

	// Token: 0x020004CF RID: 1231
	private class ProcessorFloat : DelayProcessor.Processor<float>
	{
		// Token: 0x060025F6 RID: 9718 RVA: 0x000BD949 File Offset: 0x000BBB49
		public ProcessorFloat(int delaySamples, float factor) : base(delaySamples, factor)
		{
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x000BD954 File Offset: 0x000BBB54
		protected override void mix(float factor, float[] buf, float[] prevBuf, ref int prevBufPosRead)
		{
			for (int i = 0; i < buf.Length; i++)
			{
				int num = i;
				float num2 = buf[num];
				int num3 = prevBufPosRead;
				prevBufPosRead = num3 + 1;
				buf[num] = num2 + factor * prevBuf[num3 % prevBuf.Length];
			}
		}
	}

	// Token: 0x020004D0 RID: 1232
	private class ProcessorShort : DelayProcessor.Processor<short>
	{
		// Token: 0x060025F8 RID: 9720 RVA: 0x000BD98D File Offset: 0x000BBB8D
		public ProcessorShort(int delaySamples, float factor) : base(delaySamples, factor)
		{
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x000BD998 File Offset: 0x000BBB98
		protected override void mix(float factor, short[] buf, short[] prevBuf, ref int prevBufPosRead)
		{
			for (int i = 0; i < buf.Length; i++)
			{
				int num = i;
				short num2 = buf[num];
				float num3 = (float)buf[i];
				int num4 = prevBufPosRead;
				prevBufPosRead = num4 + 1;
				buf[num] = num2 + (short)(num3 + factor * (float)prevBuf[num4 % prevBuf.Length]);
			}
		}
	}
}
