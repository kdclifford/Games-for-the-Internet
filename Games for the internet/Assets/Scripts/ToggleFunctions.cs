using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ToggleFunctions : MonoBehaviour
{
    public bool toggle = false;
  public void selectColour()
    {           
            toggle = !toggle;
    }

    public void ChangeButtonColour(Color newColour)
    {
        GetComponent<Image>().color = newColour;

    }

    private void Update()
    {
        if (toggle)
        {
            transform.parent.GetComponent<Image>().color = Color.blue;
          
        }
        else
        {
            transform.parent.GetComponent<Image>().color = Color.black;
           
        }
    }



}
