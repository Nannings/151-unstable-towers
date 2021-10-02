using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAME
{
    public class CameraController : MonoBehaviour
    {
        public Transform toFollow;

        private void Update()
        {
            Vector3 tempPos = transform.position;
            tempPos.y = toFollow.transform.position.y + 2;
            if (transform.position.y < tempPos.y)
            {
                transform.position = Vector3.Lerp(transform.position, tempPos, Time.deltaTime);
            }
        }
    }

}