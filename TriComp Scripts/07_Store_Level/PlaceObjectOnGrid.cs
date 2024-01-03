using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceObjectOnGrid : MonoBehaviour
{
    public Transform gridCellPrefab;
    public Transform cube;
    public Transform triangle;
    public Transform onMousePrefab;
    public Vector3 smoothMousePosition;
    [SerializeField] private int height;
    [SerializeField] private int width;

    public int BoxCounter;
    private static int count;
    Vector3 mousePosition;
    private Node[,] nodes;
    private Plane plane;

    int SceneLoad;
    int nextSceneLoad;

    bool TimerOn;
    float timeleft = 10f;
    public GameObject YouFailPanel;

    public AudioSource YouFailAudio;
    public AudioSource BGM;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Physics.gravity = new Vector3(0, 0, 0);
        CreateGrid();
        plane = new Plane(inNormal: Vector3.up, inPoint: transform.position);
        count = BoxCounter;
        SceneLoad = SceneManager.GetActiveScene().buildIndex;
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;

    }

    // Update is called once per frame
    void Update()
    {
        GetMousePositionOnGrid();
    }

    void GetMousePositionOnGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out var enter))
        {
            mousePosition = ray.GetPoint(enter);
            smoothMousePosition = mousePosition;
            mousePosition.y = 0;
            mousePosition = Vector3Int.RoundToInt(mousePosition);
            foreach (var node in nodes)
            {
                if (node.cellPosition == mousePosition && node.isPlaceable)
                {
                    if (Input.GetMouseButtonUp(0) && onMousePrefab != null)
                    {
                        node.isPlaceable = false;
                        onMousePrefab.GetComponent<ObjFollowMouse>().isOnGrid = true;
                        onMousePrefab.position = node.cellPosition + new Vector3(x: 0, y: 0.6f, z: 0);
                        onMousePrefab = null;
                    }
                }
            }
        }

        if (TimerOn)
        {
            if (timeleft > 0)
            {
                timeleft -= Time.deltaTime;
                updateTimer(timeleft);
                //Debug.Log(timeleft);
            }
            else
            {

                PlayerPrefs.SetInt("currentEnergy", PlayerPrefs.GetInt("currentEnergy") - 1);
                //Debug.Log("time is up");
                timeleft = 0;
                TimerOn = false;
                YouFailPanel.SetActive(true);

                BGM.Stop();
                YouFailAudio.Play();

                Time.timeScale = 0f;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float seconds = Mathf.FloorToInt(currentTime % 60);
    }



    public int Decrement()
    {
        count = count - 1;
        return count;
    }

    public void start()
    {
        Time.timeScale = 1f;
        Physics.gravity = new Vector3(0, 0, -9.8f);
        TimerOn = true;
    }

    public void stop()
    {
        Time.timeScale = 0f;
    }

    public void retry()
    {
        if (PlayerPrefs.GetInt("currentEnergy") != 0)
        {
            SceneManager.LoadScene(SceneLoad);
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MapSelector");
        }
    }

    public void nextlevel()
    {
        SceneManager.LoadScene(nextSceneLoad);
    }

    public void OnMouseClickOnUI()
    {
        if (onMousePrefab == null && count != 0)
        {
            onMousePrefab = Instantiate(cube, mousePosition, Quaternion.identity);
            //Debug.Log("Boxes left: " + (count - 1));
            Decrement();

        }
        else
        {
            //Debug.Log("you can only spawn " + BoxCounter + " box.");
        }
    }

    public void reset()
    {
        SceneManager.LoadScene(SceneLoad);
    }

    public void RightTriangleOnMouseClickOnUI()
    {
        if (onMousePrefab == null && count != 0)
        {
            onMousePrefab = Instantiate(triangle, mousePosition, transform.rotation);
            onMousePrefab.transform.Rotate(0, -90f, 0);
            //Debug.Log("Boxes left: " + (count - 1));
            Decrement();

        }
        else
        {
            //Debug.Log("you can only spawn " + BoxCounter + " box.");
        }
    }

    public void LeftTriangleOnMouseClickOnUI()
    {
        if (onMousePrefab == null && count != 0)
        {
            onMousePrefab = Instantiate(triangle, mousePosition, transform.rotation);
            onMousePrefab.transform.Rotate(0, 180f, 0);

            //Debug.Log("Boxes left: " + (count - 1));
            Decrement();

        }
        else
        {
            //Debug.Log("you can only spawn " + BoxCounter + " box.");
        }
    }

    private void CreateGrid()
    {
        nodes = new Node[width, height];
        var name = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 worldPosition = new Vector3(x: i, y: 0, z: j);
                Transform obj = Instantiate(gridCellPrefab, worldPosition, Quaternion.identity);
                obj.name = "Cell" + name;
                nodes[i, j] = new Node(isPlaceable: true, worldPosition, obj);
                name++;
            }
        }
    }

   
}

public class Node
{
    public bool isPlaceable;
    public Vector3 cellPosition;
    public Transform obj;

    public Node(bool isPlaceable, Vector3 cellPosition, Transform obj)
    {
        this.isPlaceable = isPlaceable;
        this.cellPosition = cellPosition;
        this.obj = obj;
    }
}