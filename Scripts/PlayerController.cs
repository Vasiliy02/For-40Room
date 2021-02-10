using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject mainCamera, skin, restartTable, winTable;

    [SerializeField]
    Image bar;

    [SerializeField]
    Text textBar;

    public float speedMove;
    public float scaleMan;
    private float progress;
    private float swipeMove;

    public int weight;

    Animator anim;

    private void Start()
    {
        swipeMove = 1.6f;

        restartTable.SetActive(false);
        winTable.SetActive(false);

        anim = skin.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        anim.SetTrigger("Run");

        Transform();
        WeightnMan();
        WeightBar();
    }

    void Update()
    {
        if (SwipeController.Instance.SwipeRight == true)
        {
            if (transform.position.x >= 3.2f)
            {
                transform.Translate(0f, 0f, 0f);
            }
            else
            {
                transform.Translate(swipeMove, 0f, 0f);
            }
        }

        if (SwipeController.Instance.SwipeLeft == true)
        {
            if (transform.position.x <= -1.6f)
            {
                transform.Translate(0f, 0f, 0f);
            }
            else
            {
                transform.Translate(-swipeMove, 0f, 0f);
            }
        }
    }

    private void Transform()
    {
        transform.Translate(0f, 0f, speedMove);

        mainCamera.transform.position = new Vector3(3f, 4f, gameObject.transform.position.z - 11f);
        bar.transform.position = new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z - 1.2f);
    }

    private void WeightnMan()
    {
        if (scaleMan >= 0.2 || scaleMan <= 2)
        {
            skin.transform.localScale = new Vector3(scaleMan + 0.2f, scaleMan, scaleMan + 0.2f);
        }
        else
        {
            LoseGame();
        }

        if(weight <= 0)
        {
            LoseGame();
        }
    }

    private void WeightBar()
    { 
        if(bar.fillAmount == 0f)
        {
            textBar.text = "Худой";
        }
        else if(bar.fillAmount == 0.3f)
        {
            textBar.text = "Нормальный";
        }
        else if (bar.fillAmount == 0.6f)
        {
            textBar.text = "Толстый";
        }
        else if (bar.fillAmount == 1f)
        {
            bar.fillAmount = 1f;
            textBar.text = "Мега толстый";
        }
    }

    private void Win()
    {
        winTable.SetActive(true);
        speedMove = 0f;
        swipeMove = 0f;
        anim.SetTrigger("Win");
    }

    private void LoseGame()
    {
        speedMove = 0f;
        swipeMove = 0f;
        anim.SetTrigger("Lose");
        restartTable.SetActive(true);
        Handheld.Vibrate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            weight--;
            bar.fillAmount -= 0.1f;
            scaleMan -= 0.1f;
            anim.SetTrigger("Push");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Eat")
        {
            weight += 2;
            bar.fillAmount += 0.1f;
            scaleMan += 0.2f;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Spike")
        {
            LoseGame();
        }

        if(other.gameObject.tag == "Win")
        {
            Win();
        }
    }


}
