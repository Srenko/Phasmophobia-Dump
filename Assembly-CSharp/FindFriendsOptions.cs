using System;

// Token: 0x0200009E RID: 158
public class FindFriendsOptions
{
	// Token: 0x0600033A RID: 826 RVA: 0x00013B14 File Offset: 0x00011D14
	internal int ToIntFlags()
	{
		int num = 0;
		if (this.CreatedOnGs)
		{
			num |= 1;
		}
		if (this.Visible)
		{
			num |= 2;
		}
		if (this.Open)
		{
			num |= 4;
		}
		return num;
	}

	// Token: 0x04000449 RID: 1097
	public bool CreatedOnGs;

	// Token: 0x0400044A RID: 1098
	public bool Visible;

	// Token: 0x0400044B RID: 1099
	public bool Open;
}
