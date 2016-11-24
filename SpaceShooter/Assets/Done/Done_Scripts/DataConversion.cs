using System;

public class DataConversion
{
    public DataConversion()
    {

    }

    public static float GetHorizontalMove()
	{
        Client client = Client.GetInstance();
        if (client.GetControldata() != null && client.GetControldata().Equals("right"))
        {
			return +1.0f;
        }
        else if (client.GetControldata() != null && client.GetControldata().Equals("left"))
        {
			return -1.0f;
        }
        else if (client.GetControldata() != null && client.GetControldata().Equals("hold"))
        {
			return 0.0f;
		} else {
			return 0.0f;
		}

	}
	
}
