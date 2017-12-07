using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ASL.Manipulation.Objects;
//using ASL.Manipulation.Controllers;

public class DeleteButtonBehavior : MonoBehaviour {
    private ObjectInteractionManager objManager;
    private MoveBehavior _moveBehavior;


    public Button delButton;
    public Button copyButton;

    void Awake()
    {
        objManager = GameObject.Find("ObjectInteractionManager").GetComponent<ObjectInteractionManager>();
        _moveBehavior = objManager.RegisterBehavior<MoveBehavior>();
    }

    // Use this for initialization
    void Start () {
        delButton.onClick.AddListener(() => { DeleteButtonListener(); });
        copyButton.onClick.AddListener(() => { CopyButtonListener(); });
    }
	
	// Update is called once per frame
	void DeleteButtonListener () {
        _moveBehavior.Delete();
    }

    void CopyButtonListener()
    {
        _moveBehavior.Copy();
    }
}
