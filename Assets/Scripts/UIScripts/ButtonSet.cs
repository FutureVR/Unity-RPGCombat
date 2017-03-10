using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSet : MonoBehaviour
{
    public ButtonArray buttonArray;

    public Text fieldName;
    public Button upButton;
    public Button downButton;
    public Text text;

    public int value;

    void Start()
    {
        buttonArray = GetComponentInParent<ButtonArray>();
        value = 5;

        upButton.gameObject.GetComponentInChildren<Text>().text = "UP";
        downButton.gameObject.GetComponentInChildren<Text>().text = "DOWN";
        setText();

        upButton.onClick.AddListener(upClicked);
        downButton.onClick.AddListener(downClicked);
    }

    void upClicked()
    {
        if (buttonArray.totalCurrentStats < buttonArray.maxStats)
        {//Debug.Log("pressed up");
            value++;
            buttonArray.TotalCurrentStats++;
            setText();
        }
    }

    void downClicked()
    {
        if (value > 0)
        {//Debug.Log("pressed down");
            value--;
            buttonArray.TotalCurrentStats--;
            setText();
        }
    }

    void setText()
    {
        text.text = value.ToString();
    }
}
