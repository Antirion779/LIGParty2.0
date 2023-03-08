using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using TMPro;

public class BrickFallManager : MonoBehaviour
{
    [Header("> Source Objects")]
    [SerializeField] private List<ShapeInit> shapesList = new();
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject mainCamera;

    [Header("> Parameters")]
    [SerializeField] private float brickMoveSpeed = 1;
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;

    [Header("> Score")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int score;

    private bool canMove;
    private GameObject currentBlock;
    private FollowBricks cameraScript;

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        MoveBrick(playerInput.currentActionMap.FindAction("Move").ReadValue<Vector2>());
        UpdateScore();
    }

    public void ListenFall(InputAction.CallbackContext context)
    {
        if (context.performed && canMove)
            StartCoroutine(Fall());
    }
    public void ListenTurn(InputAction.CallbackContext context)
    {
        if (context.performed && canMove)
            Rotate();
    }

    private void MoveBrick(Vector2 value)
    {
        if (canMove)
        {
            currentBlock.transform.Translate(value.x * brickMoveSpeed * Time.deltaTime, 0, 0, Space.World);
        }
    }
    private void Rotate()
    {
        if (canMove)
        {
            currentBlock.transform.Rotate(new Vector3(0, 0, -90));
        }
    }

    private void Turn() 
    {
        currentBlock = SpawnRandomShape(shapesList);
    }

    GameObject SpawnRandomShape(List<ShapeInit> shapesList)
    {
        int _randomIndex = Random.Range(0, shapesList.Count);
        GameObject _newShape = Instantiate(shapesList[_randomIndex], new Vector3(leftBoundary.position.x + (rightBoundary.position.x - leftBoundary.position.x)/2, leftBoundary.position.y), Quaternion.identity).Init();
        return(_newShape);
    }

    private IEnumerator Fall()
    {
        if (canMove)
        {
            canMove = false;
            currentBlock.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            yield return new WaitForSeconds(1f);

            cameraScript.brickList.Add(currentBlock);

            yield return new WaitForSeconds(1f);

            canMove = true;
            SwitchPlayer();
            Turn();
        }
    }

    private void SwitchPlayer()
    {
        switch (playerInput.currentActionMap.name.ToString())
        {
            default:
                break;
            case "Player1":
                playerInput.SwitchCurrentActionMap("Player2");
                return;
            case "Player2":
                playerInput.SwitchCurrentActionMap("Player1");
                return;
        }
    }
    private void Init()
    {
        cameraScript = mainCamera.GetComponent<FollowBricks>();
        canMove = true;
        Turn();
    }
    private void UpdateScore()
    {
        score = 0;
        foreach (GameObject _brick in cameraScript.brickList)
        {
            //Debug.Log(brick.GetComponent<ShapeInit>().points);
            if(_brick != null)
                score += _brick.GetComponent<ShapeInit>().points;
        }
        scoreText.text = score.ToString();
    }
}
