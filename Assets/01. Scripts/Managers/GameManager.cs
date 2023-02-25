using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isMove = false;
    const float AllowDistance = 5f;
    Block firstTouchBlock = null;
    Block secondTouchBlock = null;
    Camera mainCam = null;
    Vector3 firstInputPos;
    Vector3 secondInputPos;
    public void Init()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (isMove)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            firstInputPos = Input.mousePosition;
            Ray ray1 = mainCam.ScreenPointToRay(firstInputPos);
            RaycastHit2D hit = Physics2D.Raycast(ray1.origin, ray1.direction);
            if (hit.collider != null)
            {
                firstTouchBlock = hit.collider.GetComponent<Block>();
                if (firstTouchBlock.Type == Define.BlockType.TOP)
                    firstTouchBlock = null;
            }
        }

        if (firstTouchBlock != null && Input.GetMouseButton(0))
        {
            secondInputPos = Input.mousePosition;
            Debug.Log($"Dist : {Vector3.Distance(firstInputPos, secondInputPos)}");
            if (Vector3.Distance(firstInputPos, secondInputPos) >= AllowDistance)
            {
                Ray ray2 = mainCam.ScreenPointToRay(secondInputPos);
                RaycastHit2D hit = Physics2D.Raycast(ray2.origin, ray2.direction);
                if (hit.collider != null)
                {
                    secondTouchBlock = hit.collider.GetComponent<Block>();
                    if (firstTouchBlock != null && secondTouchBlock != null)
                    {
                        Debug.Log("Move Start!!");
                        StartCoroutine(MoveAndCheck());
                    }
                }
            }
        }
    }

    IEnumerator MoveAndCheck()
    {
        isMove = true;
        yield return Managers.Block.SwapBlocks(firstTouchBlock, secondTouchBlock);
        isMove = false;
    }
}