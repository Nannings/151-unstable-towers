using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAME
{
    public class CameraController : MonoBehaviour
    {
        public Transform toFollow;
        public bool goBack;

        float startSize;
        Vector3 startPos;

        private void Start()
        {
            startPos = transform.position;
            startSize = Camera.main.orthographicSize;
        }

        private void Update()
        {
            if (goBack)
            {
                transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime / 3);
                return;
            }


            Vector3 tempPos = transform.position;
            //tempPos.y = toFollow.transform.position.y + 2 - startSize / 4;
            tempPos.y = toFollow.transform.position.y + 2;
            if (transform.position.y < tempPos.y)
            {
                //if (Camera.main.orthographicSize < 8)
                //{
                //    Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, startSize + tempPos.y / 5, Time.deltaTime);
                //}
                transform.position = Vector3.Lerp(transform.position, tempPos, Time.deltaTime);
            }
        }
    }

}