// This is the script for the right controller.
// the script for the left controller is called
// LeftViveController


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL.Manipulation.Objects;

public class ViveController : MonoBehaviour
{
    // translate, rotate, scale
    private string transformMode = "translate";

    private bool isObjSelected = false;

    public GameObject currentObj;

    public GameObject focusObject;

    private ObjectInteractionManager objManager;
    private fortBuilderObj previousObj;

    private SteamVR_TrackedObject controller;
    public GameObject VR_Rig;
    public GameObject teleLaser;

    public GameObject selectLaser;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)controller.index); }
    }

    void Awake()
    {
        objManager = GameObject.Find("ObjectInteractionManager").GetComponent<ObjectInteractionManager>();
        objManager.FocusObjectChangedEvent += SetObject;

        controller = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetObject(ObjectSelectedEventArgs e)
    {
        focusObject = e.FocusObject;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // rotate the player to the right. Makes me a bit dizzy if 
        // I hold it down too long. I recommend just turning around in
        // the room if you can lol
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            //Debug.Log("Right Controller Grip Pressed");
            VR_Rig.transform.Rotate(new Vector3(0, 70 * Time.deltaTime, 0));
        }



        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            RaycastHit hit;
            if (Physics.Raycast(controller.transform.position, transform.forward, out hit, 100))
            {
                ShowSelectLaser(hit);
                selectLaser.GetComponent<Renderer>().material.color = new Color(255, 0, 0);

                if (hit.transform.GetComponent<fortBuilderObj>())
                {
                    if (hit.transform.GetComponent<fortBuilderObj>().selectable)
                    {
                        selectLaser.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
                    }
                }
                
            }
            else
            {
                Debug.Log("this should never happen.. I mean I added walls and everything");
            }
            
        }
        else if (!Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            selectLaser.SetActive(false);
            selectLaser.GetComponent<Renderer>().enabled = false;

            if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger) && !isObjSelected)
            {
                RaycastHit hit;
                if (Physics.Raycast(controller.transform.position, transform.forward, out hit, 100))
                {
                    if (hit.transform.GetComponent<fortBuilderObj>())
                    {
                        if (hit.transform.GetComponent<fortBuilderObj>().selectable && hit.collider.gameObject != null)
                        {
                            isObjSelected = true;

                            GameObject selectedObject = hit.collider.gameObject;
                            currentObj = hit.collider.gameObject;


                            fortBuilderObj fbComponent = selectedObject.GetComponent<fortBuilderObj>();

                            // recommended code by Thomas. Fixed a bug where object was unselectable
                            if(selectedObject.GetPhotonView() != null)
                            {
                                selectedObject.GetPhotonView().RPC("Grab", PhotonTargets.Others);
                            }
                            else
                            {
                                objManager.RequestOwnership(selectedObject, PhotonNetwork.player.ID);
                            }
                            WorldManager.Selected = fbComponent;

                            // changes object's color
                            fbComponent.Select();


                            // deselect the old object
                            if (fbComponent != previousObj)
                            {
                                if (previousObj != null)
                                {
                                    previousObj.Deselect();
                                    previousObj = fbComponent;
                                }
                            }
                        }
                    }
                }
            }
            else if(Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger) && isObjSelected)
            {
                // deselect currentObject
                currentObj.GetComponent<fortBuilderObj>().Deselect();
                //previousObj = currentObj.GetComponent<fortBuilderObj>();

                isObjSelected = false;
                currentObj = null;
            }
        }

        // pressing the touchpad on the right controller toggles transformMode
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if(transformMode == "translate")
            {
                transformMode = "rotate";
            }
            else if(transformMode == "rotate")
            {
                transformMode = "scale";
            }
            else if(transformMode == "scale")
            {
                transformMode = "translate";
            }
        }

        //if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        //{
        //    RaycastHit hit;

        //    if (Physics.Raycast(controller.transform.position, transform.forward, out hit, 100))
        //    {
        //        ShowTeleLaser(hit);
        //    }
        //    else
        //    {
        //        teleLaser.SetActive(false);
        //        teleLaser.GetComponent<Renderer>().enabled = false;
        //    }
        //}
        //else if (!Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) // 3
        //{
        //    teleLaser.SetActive(false);
        //    teleLaser.GetComponent<Renderer>().enabled = false;
        //    if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        //    {
        //        RaycastHit hit;

        //        if (Physics.Raycast(controller.transform.position, transform.forward, out hit, 100))
        //        {
        //            if (hit.transform.GetComponent<fortBuilderObj>())
        //            {
        //                var newPosition = VR_Rig.transform.position;

        //                newPosition.x = hit.point.x;
        //                newPosition.z = hit.point.z;

        //                VR_Rig.transform.position = newPosition;
        //            }
        //        }

        //    }
        //}







        //Debug.Log(currentObj);
        // updates the object
        if (currentObj != null)
        {
            updateCurrentObj();
        }



    }

    // only happens if currentObject is not null
    private void updateCurrentObj()
    {
        if(transformMode == "rotate")
        {
            // matches focus object rotation to controller rotation
            currentObj.transform.up = controller.transform.forward;
        }
        else if(transformMode == "translate")
        {
            // puts the current selected obj in front of the controller position
            currentObj.transform.position = controller.transform.position + controller.transform.forward * 2;
        }
        else if(transformMode == "scale")
        {
            // this one is more tricky because it requires the transforms for both controllers.

        
        }

        
    }

    //private void ShowTeleLaser(RaycastHit hit)
    //{
    //    Transform lzrTForm = teleLaser.transform;

    //    teleLaser.SetActive(true);
    //    teleLaser.GetComponent<Renderer>().enabled = true;

    //    lzrTForm.position = Vector3.Lerp(controller.transform.position, hit.point, .5f);

    //    teleLaser.transform.LookAt(hit.point);
    //    teleLaser.transform.forward = teleLaser.transform.up;
        
    //    lzrTForm.localScale = new Vector3(lzrTForm.localScale.x, hit.distance / 2, lzrTForm.localScale.z);
    //}

    private void ShowSelectLaser(RaycastHit hit)
    {
        Transform lzrTForm = selectLaser.transform;

        selectLaser.SetActive(true);
        selectLaser.GetComponent<Renderer>().enabled = true;

        lzrTForm.position = Vector3.Lerp(controller.transform.position, hit.point, .5f);

        selectLaser.transform.LookAt(hit.point);
        selectLaser.transform.forward = selectLaser.transform.up;

        lzrTForm.localScale = new Vector3(lzrTForm.localScale.x, hit.distance / 2, lzrTForm.localScale.z);
    }
}