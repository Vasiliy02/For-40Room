 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private const float STOP = 100.0f;

    public static SwipeController Instance { set; get; }

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;

    private Vector2 swipeDelta, startTouch;

    public bool Tap { get { return tap; } }
    
    public Vector2 SwipeDelta { get { return swipeDelta; } }

    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeLeft { get { return swipeLeft; } }


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        swipeRight = swipeLeft = false;

        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTouch = swipeDelta = Vector2.zero;
        }

        swipeDelta = Vector2.zero;

        if(startTouch != Vector2.zero)
        {
            if(Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if(Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        if(swipeDelta.magnitude > STOP)
        {
            float x = swipeDelta.x;

            if(Mathf.Abs(x) > 0)
            {
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }

            startTouch = swipeDelta = Vector2.zero;
        }

    }




}
