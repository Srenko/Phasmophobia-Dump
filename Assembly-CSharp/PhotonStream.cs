using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A9 RID: 169
public class PhotonStream
{
	// Token: 0x060003E4 RID: 996 RVA: 0x000199CF File Offset: 0x00017BCF
	public PhotonStream(bool write, object[] incomingData)
	{
		this.write = write;
		if (incomingData == null)
		{
			this.writeData = new Queue<object>(10);
			return;
		}
		this.readData = incomingData;
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x000199F6 File Offset: 0x00017BF6
	public void SetReadStream(object[] incomingData, byte pos = 0)
	{
		this.readData = incomingData;
		this.currentItem = pos;
		this.write = false;
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x00019A0D File Offset: 0x00017C0D
	internal void ResetWriteStream()
	{
		this.writeData.Clear();
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x060003E7 RID: 999 RVA: 0x00019A1A File Offset: 0x00017C1A
	public bool isWriting
	{
		get
		{
			return this.write;
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00019A22 File Offset: 0x00017C22
	public bool isReading
	{
		get
		{
			return !this.write;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00019A2D File Offset: 0x00017C2D
	public int Count
	{
		get
		{
			if (!this.isWriting)
			{
				return this.readData.Length;
			}
			return this.writeData.Count;
		}
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x00019A4B File Offset: 0x00017C4B
	public object ReceiveNext()
	{
		if (this.write)
		{
			Debug.LogError("Error: you cannot read this stream that you are writing!");
			return null;
		}
		object result = this.readData[(int)this.currentItem];
		this.currentItem += 1;
		return result;
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x00019A7D File Offset: 0x00017C7D
	public object PeekNext()
	{
		if (this.write)
		{
			Debug.LogError("Error: you cannot read this stream that you are writing!");
			return null;
		}
		return this.readData[(int)this.currentItem];
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x00019AA0 File Offset: 0x00017CA0
	public void SendNext(object obj)
	{
		if (!this.write)
		{
			Debug.LogError("Error: you cannot write/send to this stream that you are reading!");
			return;
		}
		this.writeData.Enqueue(obj);
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00019AC1 File Offset: 0x00017CC1
	public object[] ToArray()
	{
		if (!this.isWriting)
		{
			return this.readData;
		}
		return this.writeData.ToArray();
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00019AE0 File Offset: 0x00017CE0
	public void Serialize(ref bool myBool)
	{
		if (this.write)
		{
			this.writeData.Enqueue(myBool);
			return;
		}
		if (this.readData.Length > (int)this.currentItem)
		{
			myBool = (bool)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00019B3C File Offset: 0x00017D3C
	public void Serialize(ref int myInt)
	{
		if (this.write)
		{
			this.writeData.Enqueue(myInt);
			return;
		}
		if (this.readData.Length > (int)this.currentItem)
		{
			myInt = (int)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00019B98 File Offset: 0x00017D98
	public void Serialize(ref string value)
	{
		if (this.write)
		{
			this.writeData.Enqueue(value);
			return;
		}
		if (this.readData.Length > (int)this.currentItem)
		{
			value = (string)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00019BF0 File Offset: 0x00017DF0
	public void Serialize(ref char value)
	{
		if (this.write)
		{
			this.writeData.Enqueue(value);
			return;
		}
		if (this.readData.Length > (int)this.currentItem)
		{
			value = (char)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00019C4C File Offset: 0x00017E4C
	public void Serialize(ref short value)
	{
		if (this.write)
		{
			this.writeData.Enqueue(value);
			return;
		}
		if (this.readData.Length > (int)this.currentItem)
		{
			value = (short)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00019CA8 File Offset: 0x00017EA8
	public void Serialize(ref float obj)
	{
		if (this.write)
		{
			this.writeData.Enqueue(obj);
			return;
		}
		if (this.readData.Length > (int)this.currentItem)
		{
			obj = (float)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00019D04 File Offset: 0x00017F04
	public void Serialize(ref PhotonPlayer obj)
	{
		if (this.write)
		{
			this.writeData.Enqueue(obj);
			return;
		}
		if (this.readData.Length > (int)this.currentItem)
		{
			obj = (PhotonPlayer)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x00019D5C File Offset: 0x00017F5C
	public void Serialize(ref Vector3 obj)
	{
		if (this.write)
		{
			this.writeData.Enqueue(obj);
			return;
		}
		if (this.readData.Length > (int)this.currentItem)
		{
			obj = (Vector3)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x00019DC0 File Offset: 0x00017FC0
	public void Serialize(ref Vector2 obj)
	{
		if (this.write)
		{
			this.writeData.Enqueue(obj);
			return;
		}
		if (this.readData.Length > (int)this.currentItem)
		{
			obj = (Vector2)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x00019E24 File Offset: 0x00018024
	public void Serialize(ref Quaternion obj)
	{
		if (this.write)
		{
			this.writeData.Enqueue(obj);
			return;
		}
		if (this.readData.Length > (int)this.currentItem)
		{
			obj = (Quaternion)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x040004DB RID: 1243
	private bool write;

	// Token: 0x040004DC RID: 1244
	private Queue<object> writeData;

	// Token: 0x040004DD RID: 1245
	private object[] readData;

	// Token: 0x040004DE RID: 1246
	internal byte currentItem;
}
