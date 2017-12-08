using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControlScript : MonoBehaviour {
	
	public SteamVR_TrackedObject LeftController;
	public SteamVR_TrackedObject RightController;

	public SteamVR_Controller.Device LeftControllerDevice
    {
        get
        {
            return SteamVR_Controller.Input((int)LeftController.index);
        }
    }

    public SteamVR_Controller.Device RightControllerDevice
    {
        get
        {
            return SteamVR_Controller.Input((int)RightController.index);
        }
    }

    public GameObject laser;

	// Use this for initialization
	void Start () {
        //SteamVR_Render.instance.pauseGameWhenDashboardIsVisible = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (RightControllerDevice.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;

            if (Physics.Raycast(RightController.transform.position, RightController.transform.forward, out hit, 100))
            {

            }
        }
	}

    private void ShowLaser(RaycastHit hit, SteamVR_TrackedObject controller)
    {
        laser.SetActive(true);

        laser.transform.position = Vector3.Lerp(controller.transform.position, hit.point, .5f);

        laser.transform.LookAt(hit.point);


        var temp = laser.transform.forward;
        laser.transform.forward = laser.transform.up;
        laser.transform.up = temp;

        laser.transform.localScale = new Vector3(laser.transform.localScale.x, (controller.transform.position - hit.point).magnitude, laser.transform.localScale.z);
    }
}
