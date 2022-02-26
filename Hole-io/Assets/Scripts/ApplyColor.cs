using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyColor : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public HolePlayer hole;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplyOnClick()
    {
        //changes the color of the hole
        hole.holeMaterial.SetColor("Color_54d0debc8fe243ba9038d2f1938b1252", fcp.color);
        //changes the color of the arrow indicator
        hole.arrowMaterial.SetColor("Color_32efba62d64f4116b07dd2f84049a09c", fcp.color);
    }

}
