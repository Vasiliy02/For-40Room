using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    private void Start()
    {
        player.GetComponent<PlayerController>().enabled = false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            player.GetComponent<PlayerController>().enabled = true;
            Destroy(gameObject);
        }
    }
}
