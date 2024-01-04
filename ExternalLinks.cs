using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ExternalLinks : MonoBehaviour
{
    public void OpenML(){
        Application.OpenURL("https://machinelearningforkids.co.uk");
    }

    public void OpenCO(){
        Application.OpenURL("https://code.org");
    }
}
