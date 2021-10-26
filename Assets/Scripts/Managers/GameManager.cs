using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<GameObject> selectedUnits = new List<GameObject>();
    public Vector3 mousePos;
    public Vector3 stwPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        DetectCommand();
    }

    private void DetectCommand()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("RMB Down");
            stwPoint = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 1));
            Debug.Log(stwPoint);

            GameObject t = new GameObject();
            t.transform.position = stwPoint;
            foreach (GameObject unit in selectedUnits)
            {
                UnitAI unitAI = unit.GetComponent<UnitAI>();
                unitAI.target = t.transform;
            }
        }
    }
}
