using UnityEngine;
using System.Collections;

public class AdjustSpeed : MonoBehaviour
{
    // public
    public int windowWidth = 400;
    public int windowHight = 150;

    // private
    Rect windowRect;
    int windowSwitch = 0;
    float alpha = 0;
    public float hSpeedSliderValue;
    public static int SpeedSliderValue;

    public float hHardSliderValue;
    public static int HardSliderValue;

    void GUIAlphaColor_0_To_1()
    {
        if (alpha < 1)
        {
            alpha += Time.deltaTime;
            GUI.color = new Color(1, 1, 1, alpha);
        }
    }

    // Init
    void Awake()
    {
        windowRect = new Rect(
            (Screen.width - windowWidth) / 2,
            (Screen.height - windowHight) / 2,
            windowWidth,
            windowHight);

        ReadWriteXmlFile.LoadXml();
        hSpeedSliderValue = ReadWriteXmlFile.Speed;
        hHardSliderValue= ReadWriteXmlFile.Hard;

        SpeedSliderValue = (int)hSpeedSliderValue;
        HardSliderValue = (int)hHardSliderValue;

    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            windowSwitch = 1;
            alpha = 0; // Init Window Alpha Color
        }
    }

    void OnGUI()
    {
        if (windowSwitch == 1)
        {
            GUIAlphaColor_0_To_1();
            windowRect = GUI.Window(0, windowRect, QuitWindow, "游戏速度调节");
        }
    }

    

    void QuitWindow(int windowID)
    {
      //  GUI.Label(new Rect(20, 20, 300, 30), "游戏速度调节");

        hSpeedSliderValue = GUI.HorizontalSlider(new Rect(75, 65, 100, 30), hSpeedSliderValue, 1.0F, 5.0F);
        GUI.Label(new Rect(80, 30, 200, 30), "游戏速度：" + (int)hSpeedSliderValue);

        hHardSliderValue = GUI.HorizontalSlider(new Rect(225, 65, 100, 30), hHardSliderValue, 1.0F, 5.0F);
        GUI.Label(new Rect(230, 30, 200, 30), "游戏难度：" + (int)hHardSliderValue);


        if (GUI.Button(new Rect(75, 120, 100, 20), "确定"))
        {           
            SpeedSliderValue = (int)hSpeedSliderValue;
            HardSliderValue = (int)hHardSliderValue;
            ReadWriteXmlFile.updateXML(SpeedSliderValue, HardSliderValue);
            windowSwitch = 0;
        }


        if (GUI.Button(new Rect(225, 120, 100, 20), "返回"))
        {
            windowSwitch = 0;
        }

        GUI.DragWindow();
    }

}