using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinchZoomForImage : MonoBehaviour
{
    public Canvas canvas; // The canvas
    public float zoomSpeed = 0.5f;        // The rate of change of the canvas scale factor
    public float scale = .25f;
    public ScrollRect scrollRect;

    private void Start ()
    {
        scale = canvas.rootCanvas.scaleFactor;
    }

    void Update ()
    {
        // If there are two touches on the device...
        if ( Input.touchCount == 2 )
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch( 0 );
            Touch touchOne = Input.GetTouch( 1 );

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = ( touchZeroPrevPos - touchOnePrevPos ).magnitude;
            float touchDeltaMag = ( touchZero.position - touchOne.position ).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ... change the canvas size based on the change in distance between the touches.
            canvas.scaleFactor -= deltaMagnitudeDiff * zoomSpeed;

            // Make sure the canvas size never drops below 0.1
            canvas.scaleFactor = Mathf.Max( canvas.scaleFactor , scale );
            scrollRect.horizontal = false;
            scrollRect.vertical = false;
        }
        else
        {
            scrollRect.horizontal = true;
            scrollRect.vertical = true;
        }
    }
}
