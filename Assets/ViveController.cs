// This is the script for the right controller.
// the script for the left controller is called
// LeftViveController


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL.Manipulation.Objects;
using TMPro;

public class ViveController : MonoBehaviour
{
    private Vector3 initTranslateDist;

    private float? initScaleDistance;
    public Transform leftController;

    public GameObject transformModeText;
    // translate, rotate, scale
    private string transformMode = "translate";

    public bool isObjSelected = false;

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
                // or the object being pointed at is on the build board
                else if (hit.transform.GetComponent<selectionObject>())
                {
                    selectLaser.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
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
                            initTranslateDist = controller.transform.position - hit.transform.position;

                            isObjSelected = true;
                            transformMode = "translate";

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
                    // or the object being selected is from the selection board
                    else if (hit.transform.GetComponent<selectionObject>())
                    {
                        //isObjSelected = true;
                        transformMode = "translate";

                        if (hit.transform.gameObject.name == "Rectangular Prism")
                        {
                            string prefabName = "FBRectPrism1_3";
                            Vector3 position = controller.transform.position + controller.transform.forward * 2;
                            Quaternion rotation = Quaternion.identity;
                            objManager.Instantiate(prefabName, position, rotation);
                        }
                        if (hit.transform.gameObject.name == "Cylinder")
                        {
                            string prefabName = "FortBuilderCylinder";
                            Vector3 position = controller.transform.position + controller.transform.forward * 2;
                            Quaternion rotation = Quaternion.identity;
                            objManager.Instantiate(prefabName, position, rotation);
                        }
                        if (hit.transform.gameObject.name == "Cube")
                        {
                            string prefabName = "FortBuilderCube 1";
                            Vector3 position = controller.transform.position + controller.transform.forward * 2;
                            Quaternion rotation = Quaternion.identity;
                            objManager.Instantiate(prefabName, position, rotation);
                        }
                        if (hit.transform.gameObject.name == "Cone")
                        {
                            string prefabName = "Cone";
                            Vector3 position = controller.transform.position + controller.transform.forward * 2;
                            Quaternion rotation = Quaternion.identity;
                            objManager.Instantiate(prefabName, position, rotation);
                        }
                        if (hit.transform.gameObject.name == "Arch")
                        {
                            string prefabName = "Arch";
                            Vector3 position = controller.transform.position + controller.transform.forward * 2;
                            Quaternion rotation = Quaternion.identity;
                            objManager.Instantiate(prefabName, position, rotation);
                        }
                        if (hit.transform.gameObject.name == "Prism")
                        {
                            string prefabName = "Prism";
                            Vector3 position = controller.transform.position + controller.transform.forward * 2;
                            Quaternion rotation = Quaternion.identity;
                            objManager.Instantiate(prefabName, position, rotation);
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
            initScaleDistance = null;
            if(transformMode == "translate")
            {
                transformMode = "rotate";
            }
            else if(transformMode == "rotate")
            {
                transformMode = "scale";
                Vector3 controllerVect = controller.transform.position - leftController.position;
                var controllerDist = controllerVect.magnitude;
                initScaleDistance = controllerDist;

            }
            else if(transformMode == "scale")
            {
                transformMode = "translate";
            }
        }

        //Debug.Log(currentObj);
        // updates the object
        if (currentObj != null)
        {
            updateCurrentObj();
        }




        //update the transformModeText in-app
        transformModeText.GetComponent<TextMeshPro>().SetText(transformMode);
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
            
            currentObj.transform.position = controller.transform.position + controller.transform.forward * initTranslateDist.magnitude;
                //new Vector3(0, 0, initTranslateDist.magnitude);
        }
        else if(transformMode == "scale")
        {
            // this one is more tricky because it requires the transforms for both controllers.

            if (initScaleDistance == null) return; ; ; ;
            Vector3 controllerVect = controller.transform.position - leftController.position;
            float controllerDist = controllerVect.magnitude;
            float newScale = controllerDist - (float)initScaleDistance + currentObj.transform.localScale.x;

            if(newScale >= 0.5 && newScale <= 4)
            {
                currentObj.transform.localScale = new Vector3(newScale, newScale, newScale);
            }
            
        
        }

        
    }

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