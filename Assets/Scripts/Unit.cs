using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject gmObj;

    private void Awake()
    {
        gmObj = GameObject.FindGameObjectWithTag("GM");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Player Clicked");
        List<GameObject> unitList = gmObj.GetComponent<GameManager>().selectedUnits;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!unitList.Contains(this.gameObject))
                unitList.Add(this.gameObject);
        }
        else
        {
            unitList.RemoveRange(0, unitList.Count);
            unitList.Add(this.gameObject);
        }
    }

    

    public void Move(Vector3 d)
    {
        Debug.Log(d);
        transform.position = new Vector3(d.x, d.y, transform.position.z);
    }
}
