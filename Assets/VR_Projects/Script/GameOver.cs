using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Image Image;

    private void Awake()
    {
        Image = GetComponent<Image>();
    }

    void Update()
    {
        Color color = Image.color;

        if (color.a < 1)
        {
            color.a += Time.deltaTime;
        }
        
        Image.color = color;
    }
}
