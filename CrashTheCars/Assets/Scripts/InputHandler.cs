using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;
public class InputHandler : MonoBehaviour
{
    [Header("references")]
    [SerializeField] private RawImage player1Finger;
    [SerializeField] private RawImage player2Finger;
    [SerializeField] private CarMovement player1;
    [SerializeField] private CarMovement player2;
    [SerializeField] private Texture stop;
    [SerializeField] private Texture redFinger;
    [SerializeField] private Texture blueFinger;

    public UnityEvent PlayersGaveInput;

    private bool player1GaveInput = false;
    private bool player2GaveInput = false;
    
    private bool playersCanGiveInput = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) ResetPlayers();

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
            PlayersGaveInput?.Invoke();
            player1GaveInput = false;
            player2GaveInput = false;
            //playersCanGiveInput = false;
        }
    }
    public void ResetPlayers()
    {
        player1Finger.texture = stop;
        player1Finger.transform.localEulerAngles = Vector3.zero;

        player2Finger.texture = stop;
        player1Finger.transform.localEulerAngles = Vector3.zero;
    }
}
