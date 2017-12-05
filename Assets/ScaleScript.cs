using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ASL.Manipulation.Objects;



public class ScaleScript : MonoBehaviour {

    public GameObject focusObject;
    //public Slider scaleSlider;

    public SliderWithEcho scaleSliderWithEcho;

    public virtual void Awake()
    {
        GameObject.Find("ObjectInteractionManager").GetComponent<ObjectInteractionManager>().FocusObjectChangedEvent += SetObject;
    }

    private void SetObject(ObjectSelectedEventArgs e)
    {
        focusObject = e.FocusObject;
    }

    // Use this for initialization
    void Start () {
        scaleSliderWithEcho.InitSliderRange(0.5f, 4, 1);

    }
	
	// Update is called once per frame
	void Update () {
        focusObject.transform.localScale = new Vector3(scaleSliderWithEcho.GetSliderValue(),
            scaleSliderWithEcho.GetSliderValue(), scaleSliderWithEcho.GetSliderValue());
	}
}
