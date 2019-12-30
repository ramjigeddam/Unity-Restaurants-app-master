
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour
{
    public GameObject player;
    private bool isDragging = false;
    private Ray ray;
    private RaycastHit raycastHit;


    private void Update()
    {
        int touches = Input.touchCount;

        if (touches > 0 && touches != 2 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            for (int i = 0; i < touches; i++)
            {
                Touch touch = Input.GetTouch(i);

                TouchPhase phase = touch.phase;

                switch (phase)
                {

                    case TouchPhase.Moved:
                        if (player != null)
                        {
                            float distanceToScreen = Camera.main.WorldToScreenPoint(player.transform.position).z;
                            Vector3 posMove = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, distanceToScreen));
                            player.transform.position = posMove;
                        }
                        break;
                }
            }
        }

    }
}