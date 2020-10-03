using System;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x0200007C RID: 124
internal static class CustomTypes
{
	// Token: 0x060002BE RID: 702 RVA: 0x00011FC8 File Offset: 0x000101C8
	internal static void Register()
	{
		PhotonPeer.RegisterType(typeof(Vector2), 87, new SerializeStreamMethod(CustomTypes.SerializeVector2), new DeserializeStreamMethod(CustomTypes.DeserializeVector2));
		PhotonPeer.RegisterType(typeof(Vector3), 86, new SerializeStreamMethod(CustomTypes.SerializeVector3), new DeserializeStreamMethod(CustomTypes.DeserializeVector3));
		PhotonPeer.RegisterType(typeof(Quaternion), 81, new SerializeStreamMethod(CustomTypes.SerializeQuaternion), new DeserializeStreamMethod(CustomTypes.DeserializeQuaternion));
		PhotonPeer.RegisterType(typeof(PhotonPlayer), 80, new SerializeStreamMethod(CustomTypes.SerializePhotonPlayer), new DeserializeStreamMethod(CustomTypes.DeserializePhotonPlayer));
	}

	// Token: 0x060002BF RID: 703 RVA: 0x00012080 File Offset: 0x00010280
	private static short SerializeVector3(StreamBuffer outStream, object customobject)
	{
		Vector3 vector = (Vector3)customobject;
		int num = 0;
		byte[] obj = CustomTypes.memVector3;
		lock (obj)
		{
			byte[] array = CustomTypes.memVector3;
			Protocol.Serialize(vector.x, array, ref num);
			Protocol.Serialize(vector.y, array, ref num);
			Protocol.Serialize(vector.z, array, ref num);
			outStream.Write(array, 0, 12);
		}
		return 12;
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x00012104 File Offset: 0x00010304
	private static object DeserializeVector3(StreamBuffer inStream, short length)
	{
		Vector3 vector = default(Vector3);
		byte[] obj = CustomTypes.memVector3;
		lock (obj)
		{
			inStream.Read(CustomTypes.memVector3, 0, 12);
			int num = 0;
			Protocol.Deserialize(out vector.x, CustomTypes.memVector3, ref num);
			Protocol.Deserialize(out vector.y, CustomTypes.memVector3, ref num);
			Protocol.Deserialize(out vector.z, CustomTypes.memVector3, ref num);
		}
		return vector;
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x00012198 File Offset: 0x00010398
	private static short SerializeVector2(StreamBuffer outStream, object customobject)
	{
		Vector2 vector = (Vector2)customobject;
		byte[] obj = CustomTypes.memVector2;
		lock (obj)
		{
			byte[] array = CustomTypes.memVector2;
			int num = 0;
			Protocol.Serialize(vector.x, array, ref num);
			Protocol.Serialize(vector.y, array, ref num);
			outStream.Write(array, 0, 8);
		}
		return 8;
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x00012208 File Offset: 0x00010408
	private static object DeserializeVector2(StreamBuffer inStream, short length)
	{
		Vector2 vector = default(Vector2);
		byte[] obj = CustomTypes.memVector2;
		lock (obj)
		{
			inStream.Read(CustomTypes.memVector2, 0, 8);
			int num = 0;
			Protocol.Deserialize(out vector.x, CustomTypes.memVector2, ref num);
			Protocol.Deserialize(out vector.y, CustomTypes.memVector2, ref num);
		}
		return vector;
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x00012288 File Offset: 0x00010488
	private static short SerializeQuaternion(StreamBuffer outStream, object customobject)
	{
		Quaternion quaternion = (Quaternion)customobject;
		byte[] obj = CustomTypes.memQuarternion;
		lock (obj)
		{
			byte[] array = CustomTypes.memQuarternion;
			int num = 0;
			Protocol.Serialize(quaternion.w, array, ref num);
			Protocol.Serialize(quaternion.x, array, ref num);
			Protocol.Serialize(quaternion.y, array, ref num);
			Protocol.Serialize(quaternion.z, array, ref num);
			outStream.Write(array, 0, 16);
		}
		return 16;
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x00012318 File Offset: 0x00010518
	private static object DeserializeQuaternion(StreamBuffer inStream, short length)
	{
		Quaternion quaternion = default(Quaternion);
		byte[] obj = CustomTypes.memQuarternion;
		lock (obj)
		{
			inStream.Read(CustomTypes.memQuarternion, 0, 16);
			int num = 0;
			Protocol.Deserialize(out quaternion.w, CustomTypes.memQuarternion, ref num);
			Protocol.Deserialize(out quaternion.x, CustomTypes.memQuarternion, ref num);
			Protocol.Deserialize(out quaternion.y, CustomTypes.memQuarternion, ref num);
			Protocol.Deserialize(out quaternion.z, CustomTypes.memQuarternion, ref num);
		}
		return quaternion;
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x000123BC File Offset: 0x000105BC
	private static short SerializePhotonPlayer(StreamBuffer outStream, object customobject)
	{
		int id = ((PhotonPlayer)customobject).ID;
		byte[] obj = CustomTypes.memPlayer;
		short result;
		lock (obj)
		{
			byte[] array = CustomTypes.memPlayer;
			int num = 0;
			Protocol.Serialize(id, array, ref num);
			outStream.Write(array, 0, 4);
			result = 4;
		}
		return result;
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x00012424 File Offset: 0x00010624
	private static object DeserializePhotonPlayer(StreamBuffer inStream, short length)
	{
		byte[] obj = CustomTypes.memPlayer;
		int key;
		lock (obj)
		{
			inStream.Read(CustomTypes.memPlayer, 0, (int)length);
			int num = 0;
			Protocol.Deserialize(out key, CustomTypes.memPlayer, ref num);
		}
		if (PhotonNetwork.networkingPeer.mActors.ContainsKey(key))
		{
			return PhotonNetwork.networkingPeer.mActors[key];
		}
		return null;
	}

	// Token: 0x040002FF RID: 767
	public static readonly byte[] memVector3 = new byte[12];

	// Token: 0x04000300 RID: 768
	public static readonly byte[] memVector2 = new byte[8];

	// Token: 0x04000301 RID: 769
	public static readonly byte[] memQuarternion = new byte[16];

	// Token: 0x04000302 RID: 770
	public static readonly byte[] memPlayer = new byte[4];
}
