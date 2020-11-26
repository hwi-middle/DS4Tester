using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectionController : MonoBehaviour
{
    public Text title;
    public Text info;
    public GameObject btnPanel;

    private bool needConnection = true;
    private bool needChecking = true;
    private Color lightBarColor = new Color(0, 0, 0);
    private DualShockGamepad gamepad;

    void Start()
    {
        gamepad = (DualShockGamepad)Gamepad.all[0];
        InitTexts();
        StartCoroutine("CheckMultipleController");
    }

    void Update()
    {
        if(needChecking)
        {
            bool find;
            try
            {
                gamepad = (DualShockGamepad)Gamepad.all[0];
                find = true;
            }
            catch (Exception e)
            {
                //Debug.Log(e);
                find = false;
            }
            if (!find)
            {
                needConnection = true;
                StopLightingAndDisableBtnPanel();
                InitTexts();
            }
            else
            {
                if (needConnection)
                {
                    needConnection = false;
                    btnPanel.SetActive(true);
                    StartCoroutine("Blink");
                }
            }
        }
    }

    void InitTexts()
    {
        title.text = "컨트롤러 인식 불가";
        info.text = "USB케이블 또는 블루투스를 통해 컨트롤러와 PC를 연결하십시오. 대체로 USB케이블을 통한 연결이 안정적입니다. 무선 어댑터 및 기타 서드파티 매핑 프로그램을 사용할 수 없습니다. 컨트롤러가 인식되면 자동으로 다음단계로 진행됩니다.";
    }

    IEnumerator CheckMultipleController()
    {
        bool multipleConnectionActivated = false;
        int num = 0;
        while (true)
        {
            bool check;
            try
            {
                var gamepad2 = (DualShockGamepad)Gamepad.all[1];                
                check = true;
            }
            catch (Exception e)
            {
                //Debug.Log(e);
                check = false;
            }
            if (check)
            {
                StopLightingAndDisableBtnPanel();
                title.text = "1개의 컨트롤러만 연결하십시오.";
                info.text = "여러개의 컨트롤러를 연결할 수 없습니다. 테스트할 컨트롤러 1개만 PC와 연결하여주십시오. 블루투스를 통해 연결하셨을 경우, Windows 설정에서 페어링을 해제해주십시오.";
                multipleConnectionActivated = true;
            }
            else
            {
                if(multipleConnectionActivated)
                {
                    needConnection = true;
                    multipleConnectionActivated = false;
                }
            }
            yield return null;
        }
    }

    public void BlinkNotWorking()
    {
        needChecking = false;
        StopLightingAndDisableBtnPanel();
        title.text = "재연결 시도";
        StartCoroutine("WaitAndRefresh");
    }

    IEnumerator WaitAndRefresh()
    {
        for (int i = 5; i > 0; i--)
        {
            info.text = "시스템은 정상연결을 감지했으나 라이트바가 정상적으로 점등되지 않았다고 보고하셨습니다. 컨트롤러와 연결을 끊고 다시 시도하십시오. " + i + "초 후 컨트롤러를 재탐색 합니다.";
            yield return new WaitForSecondsRealtime(1.0f);
        }
        needChecking = true;
        needConnection = true;
        yield break;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MoveToTestScene()
    {
        StopLightingAndDisableBtnPanel();
        SceneManager.LoadScene("Test");
    }

    IEnumerator CheckColor()
    {
        while (true)
        {
            info.text = "컨트롤러가 연결되었습니다. <b><color=#" + ColorUtility.ToHtmlStringRGB(lightBarColor) + ">이런색</color></b>" + "으로 라이트바가 점등 중인지 확인하세요." +
                "정상적으로 작동하고 있나요? \n<size=28>(\'예\'를 누르면 바로 테스트 화면으로 이동합니다)</size>";
            yield return null;
        }
    }

    IEnumerator Blink()
    {
        StartCoroutine("CheckColor");
        title.text = "컨트롤러 인식 완료";
        while (true)
        {
            int pattern = UnityEngine.Random.Range(0, 4) + 1;
            if (pattern == 1) //흰색 깜빡이기
            {
                for (int i = 0; i < 10; i++)
                {
                    lightBarColor = Color.black;
                    gamepad.SetLightBarColor(lightBarColor);
                    yield return new WaitForSecondsRealtime(0.05f);
                    lightBarColor = Color.white;
                    gamepad.SetLightBarColor(lightBarColor);
                    yield return new WaitForSecondsRealtime(0.05f);
                }
            }
            else if (pattern == 2) //컬러 랜덤 깜빡이기
            {
                for (int i = 0; i < 15; i++)
                {
                    int ran = UnityEngine.Random.Range(0, 5) + 1;
                    if (ran == 1)
                    {
                        lightBarColor = Color.red;
                    }
                    else if (ran == 2)
                    {
                        lightBarColor = Color.blue;
                    }
                    else if (ran == 3)
                    {
                        lightBarColor = Color.green;
                    }
                    else if (ran == 4)
                    {
                        lightBarColor = Color.cyan;
                    }
                    else if (ran == 5)
                    {
                        lightBarColor = Color.yellow;
                    }
                    else if (ran == 5)
                    {
                        lightBarColor = Color.magenta;
                    }
                    gamepad.SetLightBarColor(lightBarColor);
                    yield return new WaitForSecondsRealtime(0.8f);
                }
            }
            else if (pattern == 3) //경찰
            {
                for (int i = 0; i < 5; i++)
                {
                    lightBarColor = Color.red;
                    gamepad.SetLightBarColor(lightBarColor);
                    yield return new WaitForSecondsRealtime(0.2f);
                    lightBarColor = Color.blue;
                    gamepad.SetLightBarColor(lightBarColor);
                    yield return new WaitForSecondsRealtime(0.2f);
                }
            }
            else if (pattern == 4) //rgb 브레스
            {
                lightBarColor = Color.black;
                gamepad.SetLightBarColor(new Color(lightBarColor.r, lightBarColor.g, lightBarColor.b));
                while (lightBarColor.r < 0.9)
                {
                    lightBarColor.r += 0.01f;
                    gamepad.SetLightBarColor(new Color(lightBarColor.r, lightBarColor.g, lightBarColor.b));
                    yield return new WaitForSecondsRealtime(0.01f);
                }
                lightBarColor.r = 1;

                while (lightBarColor.r > 0.01)
                {
                    lightBarColor.r -= 0.01f;
                    gamepad.SetLightBarColor(new Color(lightBarColor.r, lightBarColor.g, lightBarColor.b));
                    yield return new WaitForSecondsRealtime(0.01f);
                }
                lightBarColor.r = 0;

                while (lightBarColor.g < 0.9)
                {
                    lightBarColor.g += 0.01f;
                    gamepad.SetLightBarColor(new Color(lightBarColor.r, lightBarColor.g, lightBarColor.b));
                    yield return new WaitForSecondsRealtime(0.01f);
                }
                lightBarColor.g = 1;

                while (lightBarColor.g > 0.01)
                {
                    lightBarColor.g -= 0.01f;
                    gamepad.SetLightBarColor(new Color(lightBarColor.r, lightBarColor.g, lightBarColor.b));
                    yield return new WaitForSecondsRealtime(0.01f);
                }
                lightBarColor.g = 0;

                while (lightBarColor.b < 0.9)
                {
                    lightBarColor.b += 0.01f;
                    gamepad.SetLightBarColor(new Color(lightBarColor.r, lightBarColor.g, lightBarColor.b));
                    yield return new WaitForSecondsRealtime(0.01f);
                }
                lightBarColor.b = 1;

                while (lightBarColor.b > 0.01)
                {
                    lightBarColor.b -= 0.01f;
                    gamepad.SetLightBarColor(new Color(lightBarColor.r, lightBarColor.g, lightBarColor.b));
                    yield return new WaitForSecondsRealtime(0.01f);
                }
                lightBarColor.b = 0;
            }
        }
    }

    void StopLightingAndDisableBtnPanel()
    {
        btnPanel.SetActive(false);
        StopCoroutine("CheckColor");
        StopCoroutine("Blink");
        lightBarColor = Color.black;
        gamepad.SetLightBarColor(lightBarColor);
    }
}
