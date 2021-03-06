using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Node ParentNode;

    //G: 시작으로부터 이동했던 거리,H : [가로]+[세로]장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }

}
public class AStarTest : Singleton<AStarTest>
{
    public Vector2Int bottomLeft, topRight, startPos, targetPos;
    public List<Node> FinalNodeList;
    public bool allowDiagonal, dontCrossCorner;

    int sizeX, sizeY;
    Node[,] NodeArray;
    public Node StartNode, TargetNode, CurNode;
    private Vector2Int realBottomLeft, realTopRight;

    List<Node> OpenList, ClosedList;
    public Transform StartTR;

    public void PathFinding()
    {
        startPos = Vector2Int.RoundToInt(StartTR.position);
        realBottomLeft = startPos + bottomLeft;
        realTopRight = startPos + topRight;

        //NodeArray의 크기 정해주고, isWall, x, y 대입
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + realBottomLeft.x, j + realBottomLeft.y), 0.4f))
                    if (col.gameObject.layer == LayerMask.NameToLayer("Wall")) isWall = true;

                NodeArray[i, j] = new Node(isWall, i + realBottomLeft.x, j + realBottomLeft.y);
            } 
        }
        // 시작과 끝 노드, 열린리스트와 닫힌 리스트, 마지막리스트 초기화
        StartNode = NodeArray[- bottomLeft.x,- bottomLeft.x];
        TargetNode = NodeArray[targetPos.x - realBottomLeft.x  , targetPos.y - realBottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();

        while (OpenList.Count > 0)
        {
            //열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린 리스트에서 닫힌 리스트로 옮기기
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);

            //마지막
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                //for (int i = 0; i < FinalNodeList.Count; i++) Debug.Log(i + "번째는" + FinalNodeList[i].x + "," + FinalNodeList[i].y);
                return;
            }

            //대각선 이동
            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }
            //십자 이동
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x - 1, CurNode.y);

        }
    }

    void OpenListAdd(int checkX, int checkY)
    {

        int realCheckX = checkX - startPos.x;
        int realCheckY = checkY - startPos.y;
        //Debug.Log(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].x + " " + NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].y + " " + NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall);
        //상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌 리스트에 없다면
        if (checkX >= realBottomLeft.x
            && checkX < realTopRight.x
            && checkY >= realBottomLeft.y
            && checkY < realTopRight.y
            && (!NodeArray[realCheckX - bottomLeft.x , realCheckY - bottomLeft.y ].isWall
            || NodeArray[realCheckX - bottomLeft.x , realCheckY - bottomLeft.y ] == TargetNode)
            && !ClosedList.Contains(NodeArray[realCheckX - bottomLeft.x , realCheckY - bottomLeft.y]))
        {
            //대각선 장애물 사이로 못지나감
            if (allowDiagonal)
                if (NodeArray[CurNode.x - bottomLeft.x, realCheckY - bottomLeft.y].isWall
                    || NodeArray[realCheckX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;
            // 대각선 장애물 옆으로 못지나감
            if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, realCheckY - bottomLeft.y].isWall
                     || NodeArray[realCheckX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            //이웃노드에 넣고, 직선은 10, 대각선은 14 코스트 
            Node NeighborNode = NodeArray[realCheckX - bottomLeft.x, realCheckY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);
            //이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G,H,ParentNode를 설정 후 열린 리스트에 추가
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }

        }

    }

    private void OnDrawGizmos()
    {
        if (FinalNodeList.Count != 0) for (int i = 0; i < FinalNodeList.Count - 1; i++)
                Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
    }

}
