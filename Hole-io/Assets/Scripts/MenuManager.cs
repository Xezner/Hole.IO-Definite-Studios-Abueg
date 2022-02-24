using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    [SerializeField] GameObject fcp = null;
    public bool isSkinsClicked;
    // Start is called before the first frame update
    void Start()
    {
        
        fcp.gameObject.SetActive(false);
        isSkinsClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    

    public void SkinsButtonOnClick()
    {
        if (isSkinsClicked)
        {
            fcp.gameObject.SetActive(false);
            isSkinsClicked = false;
        }
        else
        {
            fcp.gameObject.SetActive(true);
            isSkinsClicked = true;
        }
    }

    public void ClosedByMenu()
    {
        fcp.gameObject.SetActive(false);
        isSkinsClicked = false;
    }
    
}
