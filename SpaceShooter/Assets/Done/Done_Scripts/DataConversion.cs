using System;

public class DataConversion
{
    public DataConversion()
    {

    }
    //这个方法返回的值是（-7，7）的一个float.       String（0，100）
    public static float GetHorizontalMove()
	{
        int ValueInAngle = 0;
        float xValue = 0.0f;
        Client client = Client.GetInstance();

        int.TryParse(client.GetControldata(), out ValueInAngle);
        xValue = (float)((ValueInAngle - 50) * 7.0 / 50);
        return xValue;
    }

    //        if (client.GetControldata() != null && client.GetControldata().Equals("right"))
    //        {
    //			return +1.0f;
    //       }
    //       else if (client.GetControldata() != null && client.GetControldata().Equals("left"))
    //       {
    //			return -1.0f;
    //       }
    //       else if (client.GetControldata() != null && client.GetControldata().Equals("hold"))
    //       {
    //			return 0.0f;
    //		} else {
    //			return 0.0f;
    //		}

    //这个方法是用来检测拳头还是手掌，当检测到合拢时（100），发送指令1；当检测到完全打开时（0），发送指令2。
    public static int FistOrPlam()
    {
        Client client = Client.GetInstance();
        int ValueInAngle = 0;
        int.TryParse(client.GetControldata(), out ValueInAngle);
       
        if (ValueInAngle==100)
        {
            return 1;
        }
        if (ValueInAngle == 0)
        {
            return 2;
        }
        return 0;
    }


}
