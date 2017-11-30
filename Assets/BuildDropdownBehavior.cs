﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ASL.Manipulation.Objects;

public class BuildDropdownBehavior : MonoBehaviour {
    public Dropdown buildDropdown;
    private ObjectInteractionManager objManager;

    public void Awake()
    {
        objManager = GameObject.Find("ObjectInteractionManager").GetComponent<ObjectInteractionManager>();
    }

    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(buildDropdown.value == 1)
        {
            //Debug.Log("spawn 1 cube");
            string prefabName = "FortBuilderCube 1";
            Vector3 position = new Vector3(0, 0, 2);
            Quaternion rotation = Quaternion.identity;
            objManager.Instantiate(prefabName, position, rotation);
        }

        if (buildDropdown.value == 2)
        {
            //Debug.Log("spawn 1 rect prism");
            string prefabName = "FortBuilderRectPrism1 1";
            Vector3 position = new Vector3(0, 0, 2);
            Quaternion rotation = Quaternion.identity;
            objManager.Instantiate(prefabName, position, rotation);
        }

        if (buildDropdown.value == 3)
        {
            //Debug.Log("spawn 1 rect prism");
            string prefabName = "FortBuilderCylinder";
            Vector3 position = new Vector3(0, 0, 2);
            Quaternion rotation = Quaternion.identity;
            objManager.Instantiate(prefabName, position, rotation);
        }

        // set it back to 0 so it only builds 1 of whatever was selected
        buildDropdown.value = 0;


    }
}
