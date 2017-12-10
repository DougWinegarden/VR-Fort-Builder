// This is the script for the right controller.
// the script for the left controller is called
// LeftViveController


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveController : MonoBehaviour
{

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
        controller = GetComponent<SteamVR_TrackedObject>();
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

                if (hit.transform.GetComponent<fortBuilderObj>().selectable)
                {
                    selectLaser.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
                }
            }
            else
            {
                //selectLaser.transform.up = transform.forward;
                //selectLaser.GetComponent<Renderer>().material.color = new Color(255, 0, 0);

                //selectLaser.SetActive(false);
                //selectLaser.GetComponent<Renderer>().enabled = false;
            }
            
        }
        else if (!Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            selectLaser.SetActive(false);
            selectLaser.GetComponent<Renderer>().enabled = false;
        }



        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;

            if (Physics.Raycast(controller.transform.position, transform.forward, out hit, 100))
            {
                ShowTeleLaser(hit);
            }
            else
            {
                teleLaser.SetActive(false);
                teleLaser.GetComponent<Renderer>().enabled = false;
            }
        }
        else if (!Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) // 3
        {
            teleLaser.SetActive(false);
            teleLaser.GetComponent<Renderer>().enabled = false;
            if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                RaycastHit hit;

                if (Physics.Raycast(controller.transform.position, transform.forward, out hit, 100))
                {
                    if (hit.transform.GetComponent<fortBuilderObj>())
                    {
                        var newPosition = VR_Rig.transform.position;

                        newPosition.x = hit.point.x;
                        newPosition.z = hit.point.z;

                        VR_Rig.transform.position = newPosition;
                    }
                }

            }
        }
    }

    private void ShowTeleLaser(RaycastHit hit)
    {
        Transform lzrTForm = teleLaser.transform;

        teleLaser.SetActive(true);
        teleLaser.GetComponent<Renderer>().enabled = true;

        lzrTForm.position = Vector3.Lerp(controller.transform.position, hit.point, .5f);

        teleLaser.transform.LookAt(hit.point);
        teleLaser.transform.forward = teleLaser.transform.up;
        
        lzrTForm.localScale = new Vector3(lzrTForm.localScale.x, hit.distance / 2, lzrTForm.localScale.z);
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