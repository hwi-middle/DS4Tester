using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TestController : MonoBehaviour
{
    public RectTransform LeftStickRect;
    private Vector3 LeftStickLoc;
    public RectTransform RightStickRect;
    private Vector3 RightStickLoc;

    public Text outputText;

    public Image triangleImg;
    public Image circleImg;
    public Image crossImg;
    public Image squareImg;

    public Image DpadUpImg;
    public Image DpadRightImg;
    public Image DpadLeftImg;
    public Image DpadDownImg;

    public Image optionsImg;
    public Image shareImg;
    public Image systemImg;
    public Image touchPadImg;

    public Image LeftStickBgImg;
    public Image RightStickBgImg;

    public Image LeftStickCoverImg;
    public Image RightStickCoverImg;

    public Image L1Img;
    public Image R1Img;
    public Image L2Img;
    public Image R2Img;

    public Sprite[] RightBtnSprites;
    public Sprite[] optionsNshareSprites;
    public Sprite[] systemSprites;
    public Sprite[] touchPadSprites;
    public Sprite[] stickBgSprites;
    public Sprite[] stickCoverSprites;
    public Sprite[] DpadSprites;
    public Sprite[] L1Sprites;
    public Sprite[] L2Sprites;
    public Sprite[] R1Sprites;
    public Sprite[] R2Sprites;

    private float leftX;
    private float leftY;
    private float rightX;
    private float rightY;

    private bool circle;
    private bool triangle;
    private bool square;
    private bool cross;

    private bool DpadUp;
    private bool DpadDown;
    private bool DpadLeft;
    private bool DpadRight;

    private bool L1;
    private float L2Axis;
    private bool L2Btn;
    private bool L3;

    private bool R1;
    private float R2Axis;
    private bool R2Btn;
    private bool R3;

    private bool options;
    private bool share;
    private bool system;
    private bool touchPad;

    private TestControls control;

    private void Awake()
    {
        LeftStickLoc = LeftStickRect.position;
        RightStickLoc = RightStickRect.position;
        control = new TestControls();
        control.DS4.Enable();

        //L스틱
        control.DS4.LeftStickX.performed += ctx => leftX = ctx.ReadValue<float>();
        control.DS4.LeftStickX.canceled += ctx => leftX = 0;
        control.DS4.LeftStickY.performed += ctx => leftY = ctx.ReadValue<float>();
        control.DS4.LeftStickY.canceled += ctx => leftY = 0;

        //R스틱
        control.DS4.RightStickX.performed += ctx => rightX = ctx.ReadValue<float>();
        control.DS4.RightStickX.canceled += ctx => rightX = 0;
        control.DS4.RightStickY.performed += ctx => rightY = ctx.ReadValue<float>();
        control.DS4.RightStickY.canceled += ctx => rightY = 0;

        //L버튼 및 트리거
        control.DS4.L1.started += ctx => L1 = true;
        control.DS4.L1.canceled += ctx => L1 = false;

        control.DS4.L2Axis.performed += ctx => L2Axis = ctx.ReadValue<float>();
        control.DS4.L2Axis.canceled += ctx => L2Axis = 0;

        control.DS4.L2Btn.started += ctx => L2Btn = true;
        control.DS4.L2Btn.canceled += ctx => L2Btn = false;

        control.DS4.L3.started += ctx => L3 = true;
        control.DS4.L3.canceled += ctx => L3 = false;

        //R버튼 및 트리거
        control.DS4.R1.started += ctx => R1 = true;
        control.DS4.R1.canceled += ctx => R1 = false;

        control.DS4.R2Axis.performed += ctx => R2Axis = ctx.ReadValue<float>();
        control.DS4.R2Axis.canceled += ctx => R2Axis = 0;

        control.DS4.R2Btn.started += ctx => R2Btn = true;
        control.DS4.R2Btn.canceled += ctx => R2Btn = false;

        control.DS4.R3.started += ctx => R3 = true;
        control.DS4.R3.canceled += ctx => R3 = false;

        //좌측 D패드
        control.DS4.DPadDown.started += ctx => DpadDown = true;
        control.DS4.DPadDown.canceled += ctx => DpadDown = false;

        control.DS4.DPadLeft.started += ctx => DpadLeft = true;
        control.DS4.DPadLeft.canceled += ctx => DpadLeft = false;

        control.DS4.DPadRight.started += ctx => DpadRight = true;
        control.DS4.DPadRight.canceled += ctx => DpadRight = false;

        control.DS4.DPadUp.started += ctx => DpadUp = true;
        control.DS4.DPadUp.canceled += ctx => DpadUp = false;

        //우측 4종 버튼
        control.DS4.Circle.started += ctx => circle = true;
        control.DS4.Circle.canceled += ctx => circle = false;

        control.DS4.Triangle.started += ctx => triangle = true;
        control.DS4.Triangle.canceled += ctx => triangle = false;

        control.DS4.Square.started += ctx => square = true;
        control.DS4.Square.canceled += ctx => square = false;

        control.DS4.Cross.started += ctx => cross = true;
        control.DS4.Cross.canceled += ctx => cross = false;

        //기타버튼
        control.DS4.Options.started += ctx => options = true;
        control.DS4.Options.canceled += ctx => options = false;

        control.DS4.Share.started += ctx => share = true;
        control.DS4.Share.canceled += ctx => share = false;

        control.DS4.System.started += ctx => system = true;
        control.DS4.System.canceled += ctx => system = false;

        control.DS4.Pad.started += ctx => touchPad = true;
        control.DS4.Pad.canceled += ctx => touchPad = false;
    }

    public void MoveToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Update()
    {
        outputText.text = "아날로그 L스틱: (" + leftX + ", " + leftY + ")\n";
        outputText.text += "아날로그 R스틱: (" + rightX + ", " + rightY + ")\n";
        outputText.text += "L1: " + L1 + "\nL2: " + L2Axis + "\nL3: " + L3 + "\n";
        outputText.text += "R1: " + R1 + "\nR2: " + R2Axis + "\nR3: " + R3 + "\n";
        outputText.text += "D-Pad Up: " + DpadUp + "\n";
        outputText.text += "D-Pad Right: " + DpadRight + "\n";
        outputText.text += "D-Pad Down: " + DpadDown + "\n";
        outputText.text += "D-Pad Left: " + DpadLeft + "\n";
        outputText.text += "△: " + triangle + "\n";
        outputText.text += "○: " + circle + "\n";
        outputText.text += "×: " + cross + "\n";
        outputText.text += "□: " + square + "\n";
        outputText.text += "options: " + options + "\n";
        outputText.text += "share: " + share + "\n";
        outputText.text += "system: " + system + "\n";
        outputText.text += "touchpad: " + touchPad + "\n";

        if (leftX != 0 || leftY != 0)
        {
            LeftStickBgImg.sprite = stickBgSprites[1];
            LeftStickBgImg.color = Color.red;
            LeftStickRect.position = new Vector3(LeftStickLoc.x + leftX * 35, LeftStickLoc.y + leftY * 35, 0);
        }
        else
        {
            LeftStickBgImg.sprite = stickBgSprites[0];
            LeftStickBgImg.color = Color.white;
            LeftStickRect.position = new Vector3(LeftStickLoc.x, LeftStickLoc.y, 0);
        }

        if (rightX != 0 || rightY != 0)
        {
            RightStickBgImg.sprite = stickBgSprites[1];
            RightStickBgImg.color = Color.red;
            RightStickRect.position = new Vector3(RightStickLoc.x + rightX * 35, RightStickLoc.y + rightY * 35, 0);
        }
        else
        {
            RightStickBgImg.sprite = stickBgSprites[0];
            RightStickBgImg.color = Color.white;
            RightStickRect.position = new Vector3(RightStickLoc.x, RightStickLoc.y, 0);
        }

        if (triangle)
        {
            triangleImg.sprite = RightBtnSprites[1];
            triangleImg.color = Color.red;
        }
        else
        {
            triangleImg.sprite = RightBtnSprites[0];
            triangleImg.color = Color.white;
        }

        if (circle)
        {
            circleImg.sprite = RightBtnSprites[1];
            circleImg.color = Color.red;
        }
        else
        {
            circleImg.sprite = RightBtnSprites[0];
            circleImg.color = Color.white;
        }

        if (cross)
        {
            crossImg.sprite = RightBtnSprites[1];
            crossImg.color = Color.red;
        }
        else
        {
            crossImg.sprite = RightBtnSprites[0];
            crossImg.color = Color.white;
        }

        if (square)
        {
            squareImg.sprite = RightBtnSprites[1];
            squareImg.color = Color.red;
        }
        else
        {
            squareImg.sprite = RightBtnSprites[0];
            squareImg.color = Color.white;
        }

        if (DpadUp)
        {
            DpadUpImg.sprite = DpadSprites[1];
            DpadUpImg.color = Color.red;
        }
        else
        {
            DpadUpImg.sprite = DpadSprites[0];
            DpadUpImg.color = Color.white;
        }

        if (DpadRight)
        {
            DpadRightImg.sprite = DpadSprites[1];
            DpadRightImg.color = Color.red;
        }
        else
        {
            DpadRightImg.sprite = DpadSprites[0];
            DpadRightImg.color = Color.white;
        }

        if (DpadDown)
        {
            DpadDownImg.sprite = DpadSprites[1];
            DpadDownImg.color = Color.red;
        }
        else
        {
            DpadDownImg.sprite = DpadSprites[0];
            DpadDownImg.color = Color.white;
        }

        if (DpadLeft)
        {
            DpadLeftImg.sprite = DpadSprites[1];
            DpadLeftImg.color = Color.red;
        }
        else
        {
            DpadLeftImg.sprite = DpadSprites[0];
            DpadLeftImg.color = Color.white;
        }

        if (L1)
        {
            L1Img.sprite = L1Sprites[1];
            L1Img.color = Color.red;
        }
        else
        {
            L1Img.sprite = L1Sprites[0];
            L1Img.color = Color.white;
        }

        if (L2Btn)
        {
            L2Img.sprite = L2Sprites[1];
            L2Img.color = Color.red;
        }
        else
        {
            L2Img.sprite = L2Sprites[0];
            L2Img.color = Color.white;
        }

        if (L3)
        {
            LeftStickCoverImg.sprite = stickCoverSprites[1];
            LeftStickCoverImg.color = Color.red;
        }
        else
        {
            LeftStickCoverImg.sprite = stickCoverSprites[0];
            LeftStickCoverImg.color = Color.white;
        }

        if (R1)
        {
            R1Img.sprite = R1Sprites[1];
            R1Img.color = Color.red;
        }
        else
        {
            R1Img.sprite = R1Sprites[0];
            R1Img.color = Color.white;
        }

        if (R2Btn)
        {
            R2Img.sprite = R2Sprites[1];
            R2Img.color = Color.red;
        }
        else
        {
            R2Img.sprite = R2Sprites[0];
            R2Img.color = Color.white;
        }

        if (R3)
        {
            RightStickCoverImg.sprite = stickCoverSprites[1];
            RightStickCoverImg.color = Color.red;
        }
        else
        {
            RightStickCoverImg.sprite = stickCoverSprites[0];
            RightStickCoverImg.color = Color.white;
        }

        if (options)
        {
            optionsImg.sprite = optionsNshareSprites[1];
            optionsImg.color = Color.red;
        }
        else
        {
            optionsImg.sprite = optionsNshareSprites[0];
            optionsImg.color = Color.white;
        }

        if (share)
        {
            shareImg.sprite = optionsNshareSprites[1];
            shareImg.color = Color.red;
        }
        else
        {
            shareImg.sprite = optionsNshareSprites[0];
            shareImg.color = Color.white;
        }

        if (system)
        {
            systemImg.sprite = systemSprites[1];
            systemImg.color = Color.red;
        }
        else
        {
            systemImg.sprite = systemSprites[0];
            systemImg.color = Color.white;
        }

        if (touchPad)
        {
            touchPadImg.sprite = touchPadSprites[1];
            touchPadImg.color = Color.red;
        }
        else
        {
            touchPadImg.sprite = touchPadSprites[0];
            touchPadImg.color = Color.white;
        }
    }
}
