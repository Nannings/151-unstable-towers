using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAME
{
    public class HeightManager : MonoBehaviour
    {
        public float bestY = 0;
        public int score = 0;

        SpriteRenderer spriteRenderer;
        //CanvasGroup canvasGroup;
        Text text;
        Vector2 startPos;

        private void Awake()
        {
            text = GetComponentInChildren<Text>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            //canvasGroup = GetComponentInChildren<CanvasGroup>();
            text.enabled = spriteRenderer.enabled = false;
        }

        private void Start()
        {
            startPos = transform.position;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Block"))
            {
                if (collision.GetComponent<Rigidbody2D>().velocity.magnitude <= .5f)
                {
                    transform.Translate(Vector2.up * 5 * Time.deltaTime);
                    text.enabled = spriteRenderer.enabled = false;
                    score = (int)Vector2.Distance(startPos, transform.position);
                    text.text = "" + score;
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Block"))
            {
                if (score > 0)
                {
                    bestY = transform.position.y;
                    text.enabled = spriteRenderer.enabled = true;
                }
            }
        }
    }

}