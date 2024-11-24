using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    // Let camera follow target
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float lerpSpeed = 1.0f;
        public bool useDynamicOffset = false;

        private Vector3 offset;
        private Vector3 targetPos;

        private void Start()
        {
            if (target == null)
            {
                Debug.LogWarning("Target is not assigned in CameraFollow script.", this);
                return;
            }

            offset = transform.position - target.position;
        }

        private void LateUpdate()
        {
            if (target == null) return;

            if (useDynamicOffset)
            {
                offset = transform.position - target.position;
            }

            targetPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
        }
    }
}
