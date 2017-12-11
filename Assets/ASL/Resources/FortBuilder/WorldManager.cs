using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL.Manipulation.Objects;

public class WorldManager : MonoBehaviour {

    private static fortBuilderObj _selected = null;

    public static fortBuilderObj Selected {
        get {
            return _selected;
        }

        set {
            if(_selected != null)
            {
                _selected.Deselect();
            }
            _selected = value;
            _selected.Select();
        }
    }

    public static ObjectInteractionManager objManager;

    private void Awake()
    {
        objManager = GameObject.Find("ObjectInteractionManager").GetComponent<ObjectInteractionManager>();
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
