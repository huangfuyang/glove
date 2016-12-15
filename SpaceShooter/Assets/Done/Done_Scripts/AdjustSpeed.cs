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

    public float hModeSliderValue;
    public static int ModeSliderValue;

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
        hModeSliderValue= ReadWriteXmlFile.Mode;

        SpeedSliderValue = (int)hSpeedSliderValue;
        HardSliderValue = (int)hHardSliderValue;
        ModeSliderValue = (int)hModeSliderValue;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
       // if (Input.GetKeyDown("escape"))
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

        hModeSliderValue = GUI.HorizontalSlider(new Rect(40, 65, 80, 30), hModeSliderValue, 1.0F, 2.0F);
        GUI.Label(new Rect(40, 30, 150, 30), "游戏模式：" + (int)hModeSliderValue);

        hSpeedSliderValue = GUI.HorizontalSlider(new Rect(160, 65, 80, 30), hSpeedSliderValue, 1.0F, 5.0F);
        GUI.Label(new Rect(160, 30, 150, 30), "游戏速度：" + (int)hSpeedSliderValue);

        hHardSliderValue = GUI.HorizontalSlider(new Rect(280, 65, 80, 30), hHardSliderValue, 1.0F, 5.0F);
        GUI.Label(new Rect(280, 30, 150, 30), "游戏难度：" + (int)hHardSliderValue);


        if (GUI.Button(new Rect(40, 120, 100, 20), "确定(重新开始)"))
        {           
            SpeedSliderValue = (int)hSpeedSliderValue;
            HardSliderValue = (int)hHardSliderValue;
            ModeSliderValue = (int)hModeSliderValue;
            ReadWriteXmlFile.updateXML(SpeedSliderValue, HardSliderValue, ModeSliderValue);
            windowSwitch = 0;
            Application.LoadLevel(Application.loadedLevel);
            Client.GetInstance().ReStart();

        }


        if (GUI.Button(new Rect(170, 120, 80, 20), "取消"))
        {
            windowSwitch = 0;
        }

        if (GUI.Button(new Rect(280, 120, 80, 20), "退出游戏"))
        {
            windowSwitch = 0;
            Client.GetInstance().Destroy();
            Application.Quit();
        }

        GUI.DragWindow();
    }

}