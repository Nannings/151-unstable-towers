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

        bool left;
        float speed;

        public void InitValues()
        {
            speed = 5;
        }

        private void Start()
        {
            InitValues();
            GetNewBlock();
        }

        private void GetNewBlock()
        {
            if (currentBlock == null)
            {
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
                currentBlock = newBlock;
            }
        }

        private void Update()
        {
            MoveTopBlock();

            DropBlock();
        }

        private void DropBlock()
        {
            if (currentBlock != null)
            {
                if (Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(0))
                {
                    Rigidbody2D rigidbody2D = currentBlock.GetComponent<Rigidbody2D>();
                    if (rigidbody2D)
                    {
                        rigidbody2D.isKinematic = false;
                    }
                    currentBlock = null;
                    Invoke("GetNewBlock", 2);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    
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
                        currentBlock.transform.Translate(Vector2.right * speed * Time.deltaTime);
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
                        currentBlock.transform.Translate(Vector2.left * speed * Time.deltaTime);
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