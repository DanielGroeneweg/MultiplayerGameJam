using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;
using System.Collections;
using TMPro;
using System.Collections.Generic;
public class InputHandler : MonoBehaviour
{
    public List<Coroutine> mCoroutines = new List<Coroutine>();
    public UnityEvent FailureEvent;
    [DoNotSerialize] public bool didSuccessOrFailure = false;

    [Header("references")]
    [SerializeField] private RawImage player1Finger;
    [SerializeField] private RawImage player2Finger;
    [SerializeField] private CarMovement player1;
    [SerializeField] private CarMovement player2;
    [SerializeField] private Texture stop;
    [SerializeField] private Texture redFinger;
    [SerializeField] private Texture blueFinger;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;

    public UnityEvent PlayersGaveInput;

    private bool player1GaveInput = false;
    private bool player2GaveInput = false;
    
    private bool playersCanGiveInput = false;

    [DoNotSerialize] public static InputHandler Instance;

    [Header("Stats")]
    [SerializeField] private float time = 10;
    [SerializeField] private int streak = 0;
    [SerializeField] private int score = 0;
    [SerializeField] private float timeMin = 2;
    [SerializeField] private int scoreIncrease = 100;
    [SerializeField] private float streakScoremultiplier = 0.1f;
    [SerializeField] private float timeDecrease = 0.1f;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Update()
    {
        if (!playersCanGiveInput) return;

        #region Player1
        if (Input.GetKeyDown(KeyCode.W) && player1.spawnLocation != CarMovement.targets.Up)
        {
            player1Finger.texture = redFinger;
            player1GaveInput = true;
            player1.target = CarMovement.targets.Up;
            player1Finger.transform.localEulerAngles = new Vector3(0, 0, -90);
        }

        if (Input.GetKeyDown(KeyCode.A) && player1.spawnLocation != CarMovement.targets.Left)
        {
            player1Finger.texture = redFinger;
            player1GaveInput = true;
            player1.target = CarMovement.targets.Left;
            player1Finger.transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.S) && player1.spawnLocation != CarMovement.targets.Down)
        {
            player1Finger.texture = redFinger;
            player1GaveInput = true;
            player1.target = CarMovement.targets.Down;
            player1Finger.transform.localEulerAngles = new Vector3(0, 0, 90);
        }

        if (Input.GetKeyDown(KeyCode.D) && player1.spawnLocation != CarMovement.targets.Right)
        {
            player1Finger.texture = redFinger;
            player1GaveInput = true;
            player1.target = CarMovement.targets.Right;
            player1Finger.transform.localEulerAngles = new Vector3(0, 0, 180);
        }
        #endregion

        #region Player2
        if (Input.GetKeyDown(KeyCode.UpArrow) && player2.spawnLocation != CarMovement.targets.Up)
        {
            player2Finger.texture = blueFinger;
            player2GaveInput = true;
            player2.target = CarMovement.targets.Up;
            player2Finger.transform.localEulerAngles = new Vector3(0, 0, -90);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && player2.spawnLocation != CarMovement.targets.Left)
        {
            player2Finger.texture = blueFinger;
            player2GaveInput = true;
            player2.target = CarMovement.targets.Left;
            player2Finger.transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && player2.spawnLocation != CarMovement.targets.Down)
        {
            player2Finger.texture = blueFinger;
            player2GaveInput = true;
            player2.target = CarMovement.targets.Down;
            player2Finger.transform.localEulerAngles = new Vector3(0, 0, 90);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && player2.spawnLocation != CarMovement.targets.Right)
        {
            player2Finger.texture = blueFinger;
            player2GaveInput = true;
            player2.target = CarMovement.targets.Right;
            player2Finger.transform.localEulerAngles = new Vector3(0, 0, 180);
        }
        #endregion

        if (player1GaveInput && player2GaveInput)
        {
            StopCoroutine(InputTimer());
            PlayersGaveInput?.Invoke();
            player1GaveInput = false;
            player2GaveInput = false;
            playersCanGiveInput = false;
        }
    }
    bool resettedPlayer = false;
    public void ResetPlayers()
    {
        if (!resettedPlayer)
        {

            player1Finger.texture = stop;
            player1Finger.transform.localEulerAngles = Vector3.zero;

            player2Finger.texture = stop;
            player2Finger.transform.localEulerAngles = Vector3.zero;

            resettedPlayer = true;
            playersCanGiveInput = true;
            StartCoroutine(SuccessOrFailure());

            Coroutine coroutine = StartCoroutine(InputTimer());
            mCoroutines.Add(coroutine);
        }
    }
    private IEnumerator SuccessOrFailure()
    {
        yield return new WaitForEndOfFrame();
        didSuccessOrFailure = false;
        resettedPlayer = false;
    }
    public IEnumerator InputTimer()
    {
        Debug.Log("Started Timer");
        float timer = time;
        timerText.text = timer.ToString();
        yield return null;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = (Mathf.Floor(timer * 100f) / 100f).ToString();
            yield return null;
        }

        if (!player1GaveInput && !player2GaveInput)
        {
            Debug.Log("failed by time");
            FailureEvent?.Invoke();
        }
    }
    public void Failure()
    {
        score -= scoreIncrease;
        scoreText.text = score.ToString();
        streak = 0;
        didSuccessOrFailure = true;
    }
    public void Success()
    {
        streak++;
        score += (int)(scoreIncrease * (1 + streakScoremultiplier * (streak - 1)));
        time -= timeDecrease;
        if (time < timeMin) time = timeMin;
        didSuccessOrFailure = true;
    }
}
