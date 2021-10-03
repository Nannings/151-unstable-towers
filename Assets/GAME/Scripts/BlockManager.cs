using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAME
{
    public class BlockManager : MonoBehaviour
    {
        public List<GameObject> blockPrefabs = new List<GameObject>();
        public GameObject currentBlock;
        public Transform leftSide, rightSide;
        public Color[] colors;
        public AudioSource audioSource;

        bool left;
        float speed;
        bool statered;

        public void InitValues()
        {
            speed = 5;
        }

        private void Start()
        {
            InitValues();
            Invoke("GetNewBlock", .5f);
        }

        private void GetNewBlock()
        {
            if (currentBlock == null)
            {
                statered = true;
                left = false;
                if (Random.value > .5f)
                {
                    left = true;
                }
                speed += .25f;
                blockPrefabs.Shuffle();
                GameObject newBlock = Instantiate(blockPrefabs[0]);
                Vector3 vec = transform.position;
                vec.z = 0;
                newBlock.transform.position = vec;
                newBlock.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];

                int[] r = {0, 90, -90, 180};
                newBlock.transform.rotation = Quaternion.Euler(new Vector3(0,0,r[Random.Range(0, r.Length)]));

                currentBlock = newBlock;

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }

        private void Update()
        {
            if (!statered)
            {
                return;
            }

            MoveTopBlock();

            DropBlock();
        }

        private void DropBlock()
        {
            if (currentBlock != null)
            {
                if (Input.GetKeyDown(KeyCode.Space) || 
                    Input.GetMouseButtonDown(0) ||
                    Input.GetKeyDown(KeyCode.DownArrow) ||
                    Input.GetKeyDown(KeyCode.S))
                {
                    Rigidbody2D rigidbody2D = currentBlock.GetComponent<Rigidbody2D>();
                    if (rigidbody2D)
                    {
                        rigidbody2D.isKinematic = false;
                    }
                    currentBlock = null;
                    Invoke("GetNewBlock", 1);
                }
                else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    Quaternion quaternion = currentBlock.transform.localRotation;
                    quaternion *= Quaternion.Euler(0, 0, 90);
                    currentBlock.transform.localRotation = quaternion;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    Quaternion quaternion = currentBlock.transform.localRotation;
                    quaternion *= Quaternion.Euler(0, 0, -90);
                    currentBlock.transform.localRotation = quaternion;
                }
            }
        }

        private void MoveTopBlock()
        {
            if (currentBlock != null)
            {
                if (!left)
                {
                    if (currentBlock.transform.position.x < rightSide.position.x)
                    {
                        currentBlock.transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);
                    }
                    else
                    {
                        left = true;
                    }
                }
                else
                {
                    if (currentBlock.transform.position.x > leftSide.position.x)
                    {
                        currentBlock.transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
                    }
                    else
                    {
                        left = false;
                    }
                }
            }
        }

        public void Stop()
        {
            CancelInvoke();
            currentBlock = null;
            enabled = false;
            gameObject.SetActive(false);
        }
    }
}