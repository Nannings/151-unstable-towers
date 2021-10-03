using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GAME
{
    public class MenuButton : MonoBehaviour
    {
        public GameObject panel;
        public UnityEvent unityEvent;

        private void Start()
        {
            unityEvent.Invoke();
        }

        public void Toggle()
        {
            if (panel.activeInHierarchy)
            {
                panel.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                panel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}