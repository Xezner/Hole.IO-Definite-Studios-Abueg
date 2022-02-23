using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyColor : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public Material material;
    public SpriteRenderer indicator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        material.SetColor("Color_54d0debc8fe243ba9038d2f1938b1252", fcp.color);
        indicator.color = fcp.color;
        
    }
}
