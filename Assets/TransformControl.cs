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
        focusObject = null;
        setSlidersToObject(e.FocusObject);
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
            //Debug.Log(focusObject.transform);
            scaleSlider.SetSliderValue(focusObject.transform.localScale.x);

            translateXSlider.SetSliderValue(focusObject.transform.localPosition.x);
            translateYSlider.SetSliderValue(focusObject.transform.localPosition.y);
            translateZSlider.SetSliderValue(focusObject.transform.localPosition.z);

            rotateXSlider.SetSliderValue(focusObject.transform.localEulerAngles.x);
            rotateYSlider.SetSliderValue(focusObject.transform.localEulerAngles.y);
            rotateZSlider.SetSliderValue(focusObject.transform.localEulerAngles.z);
        }
    }

    public void setSlidersToObject(GameObject obj)
    {
        if (obj != null)
        {
            //Debug.Log(obj.transform);
            scaleSlider.SetSliderValue(obj.transform.localScale.x);

            translateXSlider.SetSliderValue(obj.transform.localPosition.x);
            translateYSlider.SetSliderValue(obj.transform.localPosition.y);
            translateZSlider.SetSliderValue(obj.transform.localPosition.z);

            rotateXSlider.SetSliderValue(obj.transform.localEulerAngles.x);
            rotateYSlider.SetSliderValue(obj.transform.localEulerAngles.y);
            rotateZSlider.SetSliderValue(obj.transform.localEulerAngles.z);
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
