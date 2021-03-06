using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mario : MonoBehaviour
{
    private Rigidbody2D rigid;
    public int number;
    public string text;

    void Start()
    {
        Camera.main.transform.position = Vector3.zero;
        rigid = Player.Instance.gameObject.GetComponent<Rigidbody2D>();
        rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        rigid.bodyType = RigidbodyType2D.Dynamic;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.Instance.CoroutineQuit();
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
            rigid.AddForce(new Vector3(0, 5, 0), ForceMode2D.Impulse);
            Invoke(nameof(die), 0.5f);
        }

    }

    void die()
    {
        this.gameObject.SetActive(false);
        Player.Instance._State = PlayerState.DIE;
        Invoke(nameof(death), 3f);
    }

    void death()
    {
        DeathManager.Instance.OnDeathUI(number, text);
    }
}
