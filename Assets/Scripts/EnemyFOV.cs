using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;

public class EnemyFOV : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    public Vector3 origin;
    public float startingAngle;
    public float startingAngleOffset;
    public float fov;
    public float viewDistance;
    public EdgeCollider2D fovTrigger;
    public MeshCollider meshFovTrigger;
    public Transform enemy;

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
        origin = enemy.position;
        //startingAngle = enemy.localEulerAngles.z - startingAngleOffset;

        Vector3 dir = enemy.transform.right;
        startingAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + (fov / 2f);
        //startingAngle += (fov / 2f);
        if (startingAngle < 0) startingAngle += 360; //maybe needed? added because tutorial had lol
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

        meshFovTrigger.sharedMesh = mesh;

        Vector2[] vertices2D = new Vector2[vertices.Length];
        for(int i=0; i<vertices.Length; i++)
        {
            vertices2D[i] = new Vector2(vertices[i].x, vertices[i].y);
        }

        fovTrigger.points = vertices2D;

        //StartCoroutine(FOVFade(mesh));

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

    public IEnumerator FOVFade(Mesh fov)
    {
        //light.intensity = 0f;
        //while (light.intensity > 0)
        //{
        //light.intensity -= forgetRate;
        //if (light.intensity <= 0.5f)
        //{
        //light.lightOrder -= 1;
        //}
        //else light.intensity -= 5 * forgetRate;

        //yield return new WaitForSeconds(0.01f);
        //}
        //Destroy(light.gameObject);

        yield return new WaitForSeconds(2);
        Destroy(fov);

        //return null;
    } 
   

    void SetFieldValue<T>(object obj, string name, T val)
    {
        var field = obj.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        field?.SetValue(obj, val);
    }

    /*void SetShapePath(Light2D light, Vector3[] path)
    {
        SetFieldValue<Vector3[]>(light, "m_ShapePath", path);
    }*/
    void SetAimDirection(float aimDirection) //Vector3 aimDirection)
    {
        //Vector3 dir = aimDirection.normalized;
        //float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; //Stuff for getting angle (float) from vector
        //if (n < 0) n += 360; 

        startingAngle = aimDirection + (fov / 2f);  //n - fov / 2f;
    }
}
