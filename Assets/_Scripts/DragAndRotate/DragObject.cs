
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

        if (touches > 0 && touches != 2)
        {
            for (int i = 0; i < touches; i++)
            {
                Touch touch = Input.GetTouch(i);

                TouchPhase phase = touch.phase;

                switch (phase)
                {
                    case TouchPhase.Began:

                        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out raycastHit))
                        {
                            if (raycastHit.collider.gameObject.transform.tag == "Model")
                            {
                                player = GameObject.FindGameObjectWithTag("Model");
                            }
                        }
                        break;
                    case TouchPhase.Moved:
                        if (player != null)
                        {
                            float distanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                            Vector3 posMove = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, distanceToScreen));
                            player.transform.position = posMove;
                        }
                        break;
                }
            }
        }

    }
}