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

        HeightManager heightManager;
        bool restart = false;

        private void Awake()
        {
            //PlayerPrefs.SetInt("bestScore", 0);
            //PlayerPrefs.SetFloat("bestY", 0);
            heightManager = FindObjectOfType<HeightManager>();
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



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Block") && !restart)
            {
                Rigidbody2D rigidbody2D = collision.GetComponent<Rigidbody2D>();
                if (rigidbody2D.velocity.magnitude > 1.5f)
                {
                    int s = PlayerPrefs.GetInt("bestScore", 0);
                    if (s < heightManager.score)
                    {
                        PlayerPrefs.SetInt("bestScore", heightManager.score);
                        PlayerPrefs.SetFloat("bestY", heightManager.bestY);
                    }
                    print("game over....");
                    Invoke("Restart", 3f);
                    restart = true;
                    FindObjectOfType<BlockManager>().Stop();
                }
                else
                {
                    rigidbody2D.transform.tag = "Untagged";
                }
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
