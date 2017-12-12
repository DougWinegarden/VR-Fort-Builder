using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftViveController : MonoBehaviour {

    public float launch_magnitude = 4;
    public GameObject rightController;

    private SteamVR_TrackedObject controller;
    public GameObject VR_Rig;
    public GameObject teleLaser;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)controller.index); }
    }

    private void Awake()
    {
        controller = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            //Debug.Log("Right Controller Grip Pressed");
            VR_Rig.transform.Rotate(new Vector3(0, -70 * Time.deltaTime, 0));
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if(rightController.GetComponent<ViveController>().currentObj != null)
            {
                Destroy(rightController.GetComponent<ViveController>().currentObj);
                rightController.GetComponent<ViveController>().currentObj = null;
                rightController.GetComponent<ViveController>().isObjSelected = false;
            }
            else {
                GameObject proj = WorldManager.objManager.Instantiate("Projectile");
                proj.transform.position = controller.transform.position;
                proj.GetComponent<Rigidbody>().AddForce(controller.transform.forward.normalized * launch_magnitude);
            }
            
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
                        if (!hit.transform.GetComponent<fortBuilderObj>().selectable)
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
}
