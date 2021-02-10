using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{ 
    public int ammountNumbers;

    private float fadeColor;

    public TextMeshPro numberText;

    MeshRenderer renderer;

    private void Start()
    {
        ammountNumbers = Random.Range(5, 30);
        renderer = gameObject.GetComponent<MeshRenderer>();
        fadeColor = 1f;
    }

    private void FixedUpdate()
    {
        numberText.text = "" + ammountNumbers;
        Color color = renderer.material.color;
        color.a = fadeColor;
        renderer.material.color = color;

        if (ammountNumbers <= 0)
        {
            Destroy(gameObject);
            Handheld.Vibrate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.Translate(0f, 0f, 0.1f);
            ammountNumbers--;
            fadeColor = 0.5f;
        }
    }
}
