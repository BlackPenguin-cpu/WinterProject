using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Node ParentNode;

    //G: �������κ��� �̵��ߴ� �Ÿ�,H : [����]+[����]��ֹ� �����Ͽ� ��ǥ������ �Ÿ�, F : G + H
    int x, y, G, H;
    public int F { get { return G + H; } }

}
public class AStarTest : MonoBehaviour
{
    public Vector2Int bottomLeft, topRight, startPos, targewtPos;
    public List<Node> FinalNodeList;
}