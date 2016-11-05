using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;

    [Header("Prefabs for Pooling")]
    public GameObject m_shot;
    public GameObject m_grunt;
    public GameObject m_grunt_explodes;
    public GameObject m_hulk_vertical;
    public GameObject m_hulk_horizontal;
    public GameObject m_blood_mark;

    public int CurrentLevel { get { return m_current_level; } }
    private int m_current_level = 0;

    [Header("Loading Screen")]
    public GameObject m_loading_screen;
    public Text m_level_text;

    [Header("Gameplay Screen")]
    public GameObject m_gameplay_screen;
    public GameObject m_score_screen;
    public Text m_score_text;
    public GameObject m_gameover_screen;
    public Text m_gameover_text;

    public Camera camera;

    public int CurrentScore { get { return m_current_score; } }
    private int m_current_score = 0;

    private int m_no_enemies_alive = 0;

    [Range(0f, 4f)]
    public float m_loading_time = 2f;

    [Header("Levels")]
    public Level[] m_levels;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        ObjectPoolingManager.Instance.CreatePool(m_shot, 100, 100);
        ObjectPoolingManager.Instance.CreatePool(m_grunt, 100, 100);
        ObjectPoolingManager.Instance.CreatePool(m_grunt_explodes, 100, 100);
        ObjectPoolingManager.Instance.CreatePool(m_hulk_vertical, 100, 100);
        ObjectPoolingManager.Instance.CreatePool(m_hulk_horizontal, 100, 100);
        ObjectPoolingManager.Instance.CreatePool(m_blood_mark, 100, 100);

        camera.clearFlags = CameraClearFlags.SolidColor;

        m_gameplay_screen.SetActive(false);
        m_score_screen.SetActive(false);
        m_loading_screen.SetActive(false);
        m_gameover_screen.SetActive(false);

        m_current_score = 0;
        StartCoroutine(NextLevel());

    }

    IEnumerator NextLevel()
    {
        m_current_level = m_current_level + 1;
        m_level_text.text = "LEVEL " + m_current_level.ToString();
        m_loading_screen.SetActive(true);
        m_gameplay_screen.SetActive(false);
        m_score_screen.SetActive(false);
        m_gameover_screen.SetActive(false);

        yield return new WaitForSeconds(m_loading_time);
        m_loading_screen.SetActive(false);
        InitLevel(m_current_level);
        m_gameplay_screen.SetActive(true);
        m_score_screen.SetActive(true);
        m_gameover_screen.SetActive(false);
    }

    void InitLevel(int level)
    {
        Player.Instance.Start();
        int no_grunts = 0;
        int no_hulks_vertical = 0;
        int no_hulks_horizontal = 0;

        if (level <= 5)
        {
            no_grunts = m_levels[level - 1].m_no_grunts;
            no_hulks_vertical = m_levels[level - 1].m_no_hulks_vertical;
            no_hulks_horizontal = m_levels[level - 1].m_no_hulks_horizontal;
            camera.backgroundColor = m_levels[level - 1].background;
        }
        else
        {
            no_grunts = m_levels[4].m_no_grunts;
            no_hulks_vertical = m_levels[4].m_no_hulks_vertical;
            no_hulks_horizontal = m_levels[4].m_no_hulks_horizontal;
        }


        //no_grunts = m_levels [(int)Mathf.Min (level - 1, 3)].m_no_grunts;

        m_no_enemies_alive = no_grunts + no_hulks_vertical + no_hulks_horizontal;


        //set enemies position
        Vector2 camera_boundaries = CameraBoundaries();
        float width = camera_boundaries.x;
        float height = camera_boundaries.y;

        for (int i = 0; i < no_grunts; i++)
        {
            GameObject go = ObjectPoolingManager.Instance.GetObject(m_grunt.name);

            float a = Random.Range(0, Mathf.PI * 2);
            float r = Random.Range(Mathf.Min(width, height), 2*Mathf.Min(width, height));

            Vector3 np = new Vector3();
            np.x = Mathf.Cos(a) * r;
            np.y = Mathf.Sin(a) * r;
            np.z = transform.position.z;

            go.transform.position = np;
            go.transform.rotation = Quaternion.identity;
        }

        for (int i = 0; i < no_hulks_vertical; i++)
        {
            GameObject go = ObjectPoolingManager.Instance.GetObject(m_hulk_vertical.name);
            

            float a = Random.Range(0, Mathf.PI * 2);
            float r = Random.Range(Mathf.Min(width, height)/(2), Mathf.Min(width,height));
            Vector3 np = new Vector3();
            np.x = Mathf.Cos(a) * r;
            np.y = Mathf.Sin(a) * r;
            np.z = transform.position.z;

            Hulk hulk = go.GetComponent<Hulk>();
            hulk.transform.position = np;
            hulk.transform.rotation = Quaternion.identity;
            hulk.SetOrigin();
        }

        for (int i = 0; i < no_hulks_horizontal; i++)
        {
            GameObject go = ObjectPoolingManager.Instance.GetObject(m_hulk_horizontal.name);


            float a = Random.Range(0, Mathf.PI * 2);
            float r = Random.Range(Mathf.Min(width, height) / (2), Mathf.Min(width, height));

            Vector3 np = new Vector3();
            np.x = Mathf.Cos(a) * r;
            np.y = Mathf.Sin(a) * r;
            np.z = transform.position.z;

            Hulk hulk = go.GetComponent<Hulk>();
            hulk.transform.position = np;
            hulk.transform.rotation = Quaternion.identity;
            hulk.SetOrigin();
        }
    }

    public void Scored(int score)
    {
        m_current_score = m_current_score + score;
        m_score_text.text = m_current_score.ToString();
        m_no_enemies_alive = m_no_enemies_alive - 1;

        if (m_no_enemies_alive == 0)
            StartCoroutine(NextLevel());
    }

    //active gameover screen, deactivate all enemies instances, set the gameover text to display
    public void GameOver()
    {
        m_loading_screen.SetActive(false);
        m_gameplay_screen.SetActive(false);
        m_score_screen.SetActive(false);
        m_gameover_screen.SetActive(true);
        m_gameover_text.text = "GAME OVER !!!" + System.Environment.NewLine + "Score : " + m_current_score.ToString() + System.Environment.NewLine + "Press SPACE to try again";
        var activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in activeEnemies)
        {
            enemy.gameObject.SetActive(false);
        }
        var bloodMarks = GameObject.FindGameObjectsWithTag("Blood");
        foreach (var blood in bloodMarks)
        {
            blood.gameObject.SetActive(false);
        }
		SoundManager.Instance.PlayerDies ();
    }

    //reinitiates the game for restart
    public void Restart()
    {
        m_current_score = 0;
        m_score_text.text = m_current_score.ToString();
        m_current_level = 0;
        StartCoroutine(NextLevel());
    }

    
    void Update()
    {
        //restart for any key pressed on gameover screen
        if (Input.GetKeyDown("space") && m_gameover_screen.activeSelf)
        {
            Restart();
        }
    }

    public Vector2 CameraBoundaries()
    {
        float height = camera.orthographicSize;
        float width = height * camera.aspect;
        Vector2 camera_boundaries = new Vector2(width, height);
        return camera_boundaries;
    }
}
