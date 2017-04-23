using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
		private GameObject bg;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
			bg = GameObject.FindGameObjectWithTag ("Background");
        }


        // Update is called once per frame
        private void LateUpdate()
        {
            // only update lookahead pos if accelerating or changed direction
			Vector3 moveDelta = target.position - m_LastTargetPosition;
			float xMoveDelta = moveDelta.x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right;
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);
			if (newPos.y > 17) {
				newPos.y = 17;
				moveDelta.y = 0;
			}

			// bgs
			Transform bg1 = bg.transform.GetChild (0);
			Transform bg2 = bg.transform.GetChild (1);
			Transform bg3 = bg.transform.GetChild (2);
			bg1.transform.position = bg1.transform.position + moveDelta / 5;
			bg2.transform.position = bg2.transform.position + moveDelta / 6;
			bg3.transform.position = bg3.transform.position + moveDelta / 10;
            transform.position = newPos;
            m_LastTargetPosition = target.position;
			// Move killzone
			moveDelta.y = 0;
			GameObject.FindGameObjectWithTag ("Killzone").transform.position += moveDelta;
        }
    }
}
