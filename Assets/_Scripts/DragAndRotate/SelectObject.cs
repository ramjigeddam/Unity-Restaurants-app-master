

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectObject : MonoBehaviour
{

    private Ray ray;
    private RaycastHit raycastHit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int touches = Input.touchCount;

        if (touches > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
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
                                RotateObject[] rotateObject1 = GameObject.FindObjectsOfType<RotateObject>();

                                foreach (RotateObject ro in rotateObject1)
                                {
                                    Destroy(ro);
                                }

                                GetComponent<DragObject>().player = raycastHit.collider.gameObject;
                                raycastHit.collider.gameObject.AddComponent<RotateObject>();
                            }
                        }
                        break;

                    case TouchPhase.Ended:

                        RotateObject[] rotateObject = GameObject.FindObjectsOfType<RotateObject>();

                        foreach (RotateObject ro in rotateObject)
                        {
                            Destroy(ro);
                        }

                        GetComponent<DragObject>().player = null;
                        break;
                }
            }
        }
    }
}
