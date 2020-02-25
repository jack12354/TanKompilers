using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            if (hit.collider != null && hit.collider.GetComponent<Popup>() != null)
            {
                hit.collider.GetComponent<Popup>().ShowPopup();
            }
        }
    }
}