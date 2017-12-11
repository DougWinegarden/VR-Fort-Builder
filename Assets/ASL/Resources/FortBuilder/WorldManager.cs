using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL.Manipulation.Objects;

public class WorldManager : MonoBehaviour {

    private static fortBuilderObj selected = null;

    public static fortBuilderObj Selected {
        get {
            return selected;
        }

        set {
            if(selected != null)
            {
                selected.Deselect();
            }
            selected = value;
            selected.Select();
        }
    }

    public static ObjectInteractionManager objManager;

    private void Awake()
    {
        objManager = GameObject.Find("ObjectInteractionManager").GetComponent<ObjectInteractionManager>();
        objManager.FocusObjectChangedEvent += delegate (ObjectSelectedEventArgs e)
        {
            ; ;
        };
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
