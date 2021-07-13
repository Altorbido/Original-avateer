using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class TimedObjectDestructor1 : Photon.MonoBehaviour
    {
        [SerializeField] private float m_TimeOut = 1.0f;
        [SerializeField] private bool m_DetachChildren = false;


        private void Awake()
        {
			if (photonView.isMine) {

				Invoke ("DestroyNow", m_TimeOut);
			}
			}


        private void DestroyNow()
        {
            if (m_DetachChildren)
            {
                transform.DetachChildren();
            }
			PhotonNetwork.Destroy(gameObject);
        }
    }
}
