using UnityEngine;

public class ControllerSwitcher : MonoBehaviour
{
    SteamVR_Controller.Device device;
    Gripper gripper;
    Scaler scaler;
    Bender bender;
    void Start ()
    {
        device = GetComponent<InputDevice> ().device;
        gripper = GetComponent<Gripper> ();
        scaler = GetComponent<Scaler> ();
        bender = GetComponent<Bender> ();
    }
    void Update ()
    {
        if (!device.GetTouch (SteamVR_Controller.ButtonMask.Trigger) && !device.GetPress (SteamVR_Controller.ButtonMask.Trigger) && device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad))
        {
            var PadPos = device.GetAxis ();
            if (Mathf.Abs (PadPos.x) > Mathf.Abs (PadPos.y))
            {
                if (PadPos.x > 0) //Right
                {
                    ChangeMode (Modes.Gripper);
                }
                else // Left
                {
                    ChangeMode (Modes.Scaler);
                }
            }
            else
            {
                if (PadPos.y > 0) //Up
                {
                    ChangeMode (Modes.Bender);
                }
                else // Down
                {

                }
            }
        }
    }
    void ChangeMode (Modes mode)
    {
        gripper.enabled = false;
        scaler.enabled = false;
        bender.enabled = false;
        switch (mode)
        {
            case Modes.Gripper:
                gripper.enabled = true;
                break;
            case Modes.Scaler:
                scaler.enabled = true;
                break;
            case Modes.Bender:
                bender.enabled = true;
                break;
        }
    }
    enum Modes
    {
        Gripper,
        Scaler,
        Bender,
    }
}