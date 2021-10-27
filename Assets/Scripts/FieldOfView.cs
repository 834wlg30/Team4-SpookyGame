using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    private Vector3 origin;
    private float startingAngle;
    public float fov;
    public float viewDistance;

    //public GameObject freeLight;
    //public Light2D shapeLight;
    public GameObject emptyGameObject;
    //private int numLights = 0;

    public float forgetRate;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        //fov = 90f;
        origin = Vector3.zero;
    }

    private void LateUpdate()
    {
        //origin = 
        //bool flash = false;

        int rayCount = 50;
        float angle = startingAngle; //0f;
        float angleIncrease = fov / rayCount;
        //float viewDistance = 50f;

        Vector3[] vertices = new Vector3[rayCount + 1 +1]; //new Vector3[3];
        Vector2[] uv = new Vector2[vertices.Length]; //new Vector2[3];
        int[] triangles = new int[rayCount * 3]; //new int[3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i = 0; i <= rayCount; i++)
        {
            float angleRad = angle * (Mathf.PI / 180f);
            Vector3 currentAngle = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, currentAngle, viewDistance, layerMask);
            if(raycastHit2D.collider == null)
            {
                //No hit
                vertex = origin + currentAngle * viewDistance;
            }
            else
            {
                //Hit object
                vertex = raycastHit2D.point;// - new Vector2(origin.x, origin.y);
            }

            vertices[vertexIndex] = vertex - origin;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        //vertices[0] = Vector3.zero;
        //vertices[1] = new Vector3(50, 0);
        //vertices[2] = new Vector3(0, -50);

        //triangles[0] = 0;
        //triangles[1] = 1;
        //triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        Vector2[] vertices2D = new Vector2[vertices.Length];
        for(int i=0; i<vertices.Length; i++)
        {
            vertices2D[i] = new Vector2(vertices[i].x, vertices[i].y);
        }

        //var shapeLightClone = Instantiate(freeLight.gameObject);
        //shapeLightClone.name =  numLights.ToString();
        //shapeLightClone.transform.SetSiblingIndex(0);
        //shapeLightClone.transform.position = origin;
        //UnityEngine.Experimental.Rendering.Universal.Light2D

        //numLights++;

    /*  Light2D cloneLight = shapeLightClone.GetComponent<Light2D>();
        shapeLightClone.transform.position = origin;
        SetShapePath(cloneLight, vertices);
        shapeLightClone.GetComponent<EdgeCollider2D>().points = vertices2D;
        shapeLightClone.transform.parent = emptyGameObject.transform;

        shapeLightClone.layer = 8;
        cloneLight.lightOrder = 0; //numLights;

        if (flash)
        {
            cloneLight.intensity = 10;
            cloneLight.lightOrder = 1;
        }
        else cloneLight.intensity = 0.5f;

        StartCoroutine(LightFade(cloneLight));

        //shapeLightClone.GetComponent<LightShape>() = new Vector3[];
        //shapeLight.shapePath = vertices;
    */
    } 

    /*public IEnumerator LightFade(Light2D light)
    {
        //light.intensity = 0f;
        while (light.intensity > 0)
        {
            light.intensity -= forgetRate;
            if (light.intensity <= 0.5f)
            {
                light.lightOrder -= 1;
            }
            else light.intensity -= 5 * forgetRate;

            yield return new WaitForSeconds(0.01f);
        }
        Destroy(light.gameObject);
        //return null;
    } */
   

    void SetFieldValue<T>(object obj, string name, T val)
    {
        var field = obj.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        field?.SetValue(obj, val);
    }

    /*void SetShapePath(Light2D light, Vector3[] path)
    {
        SetFieldValue<Vector3[]>(light, "m_ShapePath", path);
    }*/

    void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }
    void SetAimDirection(float aimDirection) //Vector3 aimDirection)
    {
        //Vector3 dir = aimDirection.normalized;
        //float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; //Stuff for getting angle (float) from vector
        //if (n < 0) n += 360; 

        startingAngle = aimDirection + (fov / 2f);  //n - fov / 2f;
    }
}
