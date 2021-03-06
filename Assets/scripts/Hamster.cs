using UnityEngine;


public class Hamster : MonoBehaviour
{
    [SerializeField]
    Transform Top;
    [SerializeField]
    public Transform Bottom;
    float speed;
    [SerializeField]
    float normalSpeed;
    [SerializeField]
    float speedAtMurder;
    float waitingTimeBottom;
    [SerializeField]
    float normalWaitingTimeBottom;
    [SerializeField]
    float waitingTimeBottomMurder;
    [SerializeField]
    float waitingTimeTop;
    [SerializeField]
    int minScore;
    [SerializeField]
    int maxScore;
    bool stateKillHamster;
    public float timer;
    bool stopHamster = false;
    public bool addPointsKill = false;

    enum StateType
    {
        STATE_WAIT_DOWN,
        STATE_MOVE_DOWN,
        STATE_MOVE_UP,
        STATE_WAIT_UP,
        STATE_KILL_MOVE_DOWN
    }
    StateType stateHamster;

    private void Awake()
    {
        stateKillHamster = false;
    }

    void Start()
    {
        speed = normalSpeed;
        waitingTimeBottom = normalWaitingTimeBottom;
    }

    void Update()
    {
        CheckerStateHamster();
    }

    public void DownAndStopHamsters()
    {
        if (stateHamster == StateType.STATE_MOVE_DOWN)
        {
            stateHamster = StateType.STATE_MOVE_DOWN;
        }
        else if (stateHamster == StateType.STATE_KILL_MOVE_DOWN)
        {
            stateHamster = StateType.STATE_KILL_MOVE_DOWN;
        }
        else
        {
            stateHamster = StateType.STATE_MOVE_DOWN;
        }
        stopHamster = true;
    }

    public void ResetHamster()
    {
        stopHamster = false;
        this.enabled = true;
        stateHamster = StateType.STATE_WAIT_DOWN;
        timer = 0;
    }

    public void OnMouseDown()
    {
        Points.GetInstance().point += Random.Range(minScore, maxScore + 1);
        KillHamster();
    }

    void CheckerStateHamster()
    {
        if (stateHamster == StateType.STATE_WAIT_DOWN)
        {
            timer += Time.deltaTime;
            if (timer >= waitingTimeBottom)
            {
                stateHamster = StateType.STATE_MOVE_UP;
                timer = 0;
                if (stateKillHamster == true)
                {
                    speed = normalSpeed;
                    waitingTimeBottom = normalWaitingTimeBottom;
                    stateKillHamster = false;
                }
            }
            if (stopHamster == true)
            {
                this.enabled = false;
            }

        }
        else if (stateHamster == StateType.STATE_MOVE_UP)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                Top.position, speed * Time.deltaTime);
            if (transform.position.y >= Top.position.y)
            {
                stateHamster = StateType.STATE_WAIT_UP;
            }
        }
        else if (stateHamster == StateType.STATE_WAIT_UP)
        {
            timer += Time.deltaTime;
            if (timer >= waitingTimeTop)
            {
                stateHamster = StateType.STATE_MOVE_DOWN;
                timer = 0;
            }
        }
        else if (stateHamster == StateType.STATE_MOVE_DOWN)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                Bottom.position, speed * Time.deltaTime);

            if (transform.position.y <= Bottom.position.y)
            {
                stateHamster = StateType.STATE_WAIT_DOWN;
            }
        }
        else if (stateHamster == StateType.STATE_KILL_MOVE_DOWN)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            timer = 0;
            if (stateKillHamster == true)
            {
                speed = speedAtMurder;
                waitingTimeBottom = waitingTimeBottomMurder;
            }
            this.GetComponent<SpriteRenderer>().color = new Color(150, 0, 0, 255);
            transform.position = Vector3.MoveTowards(transform.position,
                Bottom.position, speed * Time.deltaTime);

            if (transform.position.y <= Bottom.position.y)
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
                stateHamster = StateType.STATE_WAIT_DOWN;
                this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
        }
    }

    void KillHamster()
    {
        addPointsKill = true;
        stateHamster = StateType.STATE_KILL_MOVE_DOWN;
        stateKillHamster = true;
    }
}




