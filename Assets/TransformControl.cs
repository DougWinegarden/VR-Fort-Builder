using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ASL.Manipulation.Objects;

public class TransformControl : MonoBehaviour {

    public GameObject focusObject;

    public SliderWithEcho scaleSlider;

    public SliderWithEcho translateXSlider;
    public SliderWithEcho translateYSlider;
    public SliderWithEcho translateZSlider;

    public SliderWithEcho rotateXSlider;
    public SliderWithEcho rotateYSlider;
    public SliderWithEcho rotateZSlider;

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
        initializeSliders();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateObjectFromSliders()
    {
        if (focusObject != null)
        {
            focusObject.transform.localScale = new Vector3(scaleSlider.GetSliderValue(),
            scaleSlider.GetSliderValue(), scaleSlider.GetSliderValue());

            focusObject.transform.position = new Vector3(translateXSlider.GetSliderValue(),
                translateYSlider.GetSliderValue(), translateZSlider.GetSliderValue());

            focusObject.transform.localEulerAngles = new Vector3(rotateXSlider.GetSliderValue(),
                rotateYSlider.GetSliderValue(), rotateZSlider.GetSliderValue());

        }
    }

    public void resetSlidersForNewObject()
    {
        if(focusObject != null)
        {
            scaleSlider.SetSliderValue(focusObject.transform.localScale.x);

            translateXSlider.SetSliderValue(focusObject.transform.position.x);
            translateYSlider.SetSliderValue(focusObject.transform.position.y);
            translateZSlider.SetSliderValue(focusObject.transform.position.z);

            rotateXSlider.SetSliderValue(focusObject.transform.localEulerAngles.x);
            rotateYSlider.SetSliderValue(focusObject.transform.localEulerAngles.y);
            rotateZSlider.SetSliderValue(focusObject.transform.localEulerAngles.z);
        }
    }

    void initializeSliders()
    {
        scaleSlider.InitSliderRange(0.5f, 4, 1);

        translateXSlider.InitSliderRange(-4, 4, 0);
        translateYSlider.InitSliderRange(0.5f, 15, 1);
        translateZSlider.InitSliderRange(-4, 4, 0);

        rotateXSlider.InitSliderRange(-180, 180, 0);
        rotateYSlider.InitSliderRange(-180, 180, 0);
        rotateZSlider.InitSliderRange(-180, 180, 0);
    }

}
