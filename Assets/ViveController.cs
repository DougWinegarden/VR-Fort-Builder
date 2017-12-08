using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveController : MonoBehaviour
{

    private SteamVR_TrackedObject controller;
    public GameObject VR_Rig;
    public GameObject laser;
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

        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            //Debug.Log("Right Controller Grip Pressed");
            VR_Rig.transform.Rotate(new Vector3(0, 70 * Time.deltaTime, 0));
        }

        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            //Debug.Log("Right Controller Trigger Pressed");
            //controller.transform.
        }



        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;

            if (Physics.Raycast(controller.transform.position, transform.forward, out hit, 100))
            {
                ShowLaser(hit);
            }
            else
            {
                laser.SetActive(false);
                laser.GetComponent<Renderer>().enabled = false;
            }
        }
        else if (!Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) // 3
        {
            laser.SetActive(false);
            laser.GetComponent<Renderer>().enabled = false;
            if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                RaycastHit hit;

                if (Physics.Raycast(controller.transform.position, transform.forward, out hit, 100))
                {
                    var newPosition = VR_Rig.transform.position;

                    newPosition.x = hit.point.x;
                    newPosition.z = hit.point.z;

                    VR_Rig.transform.position = newPosition;
                }

            }
        }
    }

    private void ShowLaser(RaycastHit hit)
    {
        Transform lzrTForm = laser.transform;

        laser.SetActive(true);
        laser.GetComponent<Renderer>().enabled = true;

        lzrTForm.position = Vector3.Lerp(controller.transform.position, hit.point, .5f);

        laser.transform.LookAt(hit.point);
        laser.transform.forward = laser.transform.up;
        
        lzrTForm.localScale = new Vector3(lzrTForm.localScale.x, hit.distance / 2, lzrTForm.localScale.z);
    }
}