using UnityEngine;
using System.Collections;

public class LockCursor : Photon.MonoBehaviour
{
    void Start()
    {
        if (!photonView.isMine) {
			this.enabled = false;
		} else {
			Screen.lockCursor = !Screen.lockCursor;

		}
    }

    void Update()
    {
        if (photonView.isMine)
        {
            if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Escape))
            {
                Screen.lockCursor = !Screen.lockCursor;
            }
        }
        else
        {
            this.enabled = false;
        }
    }
}
