using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<GameObject> selectedUnits = new List<GameObject>();
    public List<GameObject> generators = new List<GameObject>();
    public Vector3 mousePos;
    public Vector3 stwPoint;

    private int gensDone = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        DetectCommand();

        foreach(GameObject gen in generators)
        {
            Generator genScript = gen.GetComponent<Generator>();
            if (genScript.isComplete())
            {
                gensDone += 1;
                if(gensDone == generators.Count)
                {
                    activateDoor();
                }
            }
        }
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
                unitAI.target = t;
            }
        }
    }

    private void activateDoor()
    {
        Debug.Log("All Generators Complete.");
    }
}
