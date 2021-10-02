using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GAME
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject best;
        public GameObject psDestroyPrefab;
        public GameObject panelGamover;

        HeightManager heightManager;
        bool restart = false;
        bool canResart = false;
        BlockManager blockManager;
        LevelManager levelManager;

        private void Awake()
        {
            blockManager = FindObjectOfType<BlockManager>();
            //PlayerPrefs.SetInt("bestScore", 0);
            //PlayerPrefs.SetFloat("bestY", 0);
            heightManager = FindObjectOfType<HeightManager>();
            levelManager = FindObjectOfType<LevelManager>();
        }

        private void Start()
        {
            int s = PlayerPrefs.GetInt("bestScore", 0);
            if (s <= 0)
            {
                best.SetActive(false);
            }
            else
            {
                best.SetActive(true);
                best.GetComponentInChildren<Text>().text = "best " + s;
                float y = PlayerPrefs.GetFloat("bestY", 0);
                Vector3 pos = best.transform.position;
                pos.y = y;
                best.transform.position = pos;
            }
        }


        private void Update()
        {
            if (canResart)
            {
                if (Input.anyKeyDown)
                {
                    canResart = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Block"))
            {
                Rigidbody2D rigidbody2D = collision.GetComponent<Rigidbody2D>();
                if (rigidbody2D.velocity.magnitude > 1.5f)
                {
                    if (!restart)
                    {
                        int s = PlayerPrefs.GetInt("bestScore", 0);
                        if (s < heightManager.score)
                        {
                            PlayerPrefs.SetInt("bestScore", heightManager.score);
                            PlayerPrefs.SetFloat("bestY", heightManager.bestY);
                        }
                        print("game over....");
                        Invoke("Restart2", 1f);
                        Invoke("Restart", 3f);
                        restart = true;
                        blockManager.Stop();
                    }
                    GameObject psDestroy = Instantiate(psDestroyPrefab);
                    Vector3 vec = collision.transform.position;
                    Bounds b = Camera.main.OrthographicBounds();
                    vec.y = b.min.y;
                    //vec.y = levelManager.transform.position.y + heightManager.score;
                    psDestroy.transform.position = vec;
                    Destroy(collision.gameObject);
                }
                else
                {
                    rigidbody2D.transform.tag = "Untagged";
                }
            }
        }

        public void Restart2()
        {
            FindObjectOfType<CameraController>().goBack = true;
        }

        public void Restart()
        {
            panelGamover.SetActive(true);
            canResart = true;
        }
    }
}
