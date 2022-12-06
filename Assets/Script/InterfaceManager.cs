using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text TextTurn;

    public GameObject WinObject;
    public Text TextWin;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeInterface(string color)
    {
        if(color == "Blue")
        {
            TextTurn.text = "Blue";
            TextTurn.color = Color.blue;
        }
        else if(color == "Red")
        {
            TextTurn.text = "Red";
            TextTurn.color = Color.red;
        }
    }

    public void InterfaceWin(string color)
    {
        WinObject.SetActive(true);
        if (color == "Blue")
        {
            TextWin.text = "Blue wins";
            TextWin.color = Color.blue;
        }
        else if (color == "Red")
        {
            TextWin.text = "Red wins";
            TextWin.color = Color.red;
        }
    }

    public void InterfaceDraw()
    {
        WinObject.SetActive(true);
            TextWin.text = "Draw";
            TextWin.color = Color.white;
    }
}
