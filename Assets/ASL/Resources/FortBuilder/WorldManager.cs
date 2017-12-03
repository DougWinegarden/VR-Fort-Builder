using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start () {
		
	}
	
	void Update () {
		
	}
}
