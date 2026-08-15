using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int playerScore;
    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }

    [SerializeField]
    private GameObject[] ballPositions;

    [SerializeField]
    private GameObject ballLine;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private TextMeshProUGUI notiText;

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private GameObject cueBall;

    [SerializeField]
    private float xInput = 0f;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CameraBehindCueBall();

        SetBall(BallColor.Red, 1);
        SetBall(BallColor.Yellow, 2);
        SetBall(BallColor.Green, 3);
        SetBall(BallColor.Brown, 4);
        SetBall(BallColor.Blue, 5);
        SetBall(BallColor.Pink, 6);
        SetBall(BallColor.Black, 7);
    }

    // Update is called once per frame
    void Update()

    {
        RotateBall();

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            ShootBall();

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            xInput = -0.15f;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            xInput = 0.15f;
        else
            xInput = 0f;

        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
            StopBall();
    }

    private void SetBall(BallColor col, int i)
    {
        GameObject obj = Instantiate(ballPrefab,
                                     ballPositions[i].transform.position,
                                     Quaternion.identity);

        Ball b = obj.GetComponent<Ball>();
        b.SetColorAndPoint(col);
    }

    private void ShootBall()
    {
        Rigidbody rb = cueBall.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * 50, ForceMode.Impulse);

        ballLine.SetActive(false);
        cam.transform.parent = null;
        cam.transform.position = new Vector3(0, 30f, -42f);
        cam.transform.eulerAngles = new Vector3(45f, 0f, 0f);
    }

    private void RotateBall()
    {
        if (cueBall != null)
            cueBall.transform.Rotate(new Vector3(0, xInput, 0));
    }

    private void StopBall()

    {
        Rigidbody rb = cueBall.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        cueBall.transform.eulerAngles = new Vector3(0f, 0f, 0f);

        ballLine.SetActive(true);
    }

    private void CameraBehindCueBall()
    {
        cam.transform.parent = cueBall.transform;
        cam.transform.position = cueBall.transform.position + new Vector3(0, 7f, -15f);
        cam.transform.eulerAngles = new Vector3(30f, 0f, 0f);
    }

    public void ShowScoreText(int n)
    {
        playerScore += n;
        notiText.text = $"Ball Point:{n}\nTotal Point:{playerScore}";
    }

    public void ShowString(string s)
    {
        notiText.text = s;
    }
}