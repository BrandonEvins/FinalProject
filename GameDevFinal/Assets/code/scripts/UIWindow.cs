using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    // Start is called before the first frame update
    public void Show(){
        GetComponent<Canvas>().enabled = true;
    }

    public void Hide(){
        GetComponent<Canvas>().enabled = false;
    }
}
