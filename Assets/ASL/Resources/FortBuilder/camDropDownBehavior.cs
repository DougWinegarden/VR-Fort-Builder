using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camDropDownBehavior : MonoBehaviour {

    public Dropdown camDropdown;

    private int lastVal = 0;

	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lastVal != camDropdown.value)
        {
            lastVal = camDropdown.value;


            if (camDropdown.value == 0)
            {
                Debug.Log("CamPos1");

            }
            if (camDropdown.value == 1)
            {
                Debug.Log("CamPos2");

            }
            if (camDropdown.value == 2)
            {
                Debug.Log("CamPos3");

            }
            if (camDropdown.value == 3)
            {
                Debug.Log("CamPos4");

            }
        }
    }
}
