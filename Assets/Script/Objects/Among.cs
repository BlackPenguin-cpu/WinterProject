using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Among : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Check1;
    public GameObject Check2;
    public GameObject Check3;
    public GameObject Text;
    public GameObject x;
    public float a  = 1;
    public GameObject MainCamera;
    public GameObject game;
    public float rotSpeed = 5;
    public bool b = false;
    public bool c = false;
    public bool d = false;
    public bool g = false;
    public bool h = false;
    public GameObject target;
    Rigidbody2D rb;
    void Start()
    {
        game = Player.Instance.gameObject;
        Player1.SetActive(false);
        Player2.SetActive(false);
        Player3.SetActive(false);
        Check1.SetActive(false);
        Check2.SetActive(false);
        Check3.SetActive(false);
        x.SetActive(false);

    }

    void Update()
    {
        if(MainCamera.GetComponent<AmongMic>().Ok == true)
        {
            if(g == false)
            {
                ss();
            }
            Player1.SetActive(true);
            Player2.SetActive(true);
            Player3.SetActive(true);
            if(a >= 0)
            {
                Invoke("aaa", 1);
                Invoke("bbb", 2);
            }
        }
        if(b == true)
        {
            c = true;
            rb = Player.Instance.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.gravityScale = 0;
            rb.AddTorque(1);
           // game.transform.position = Vector3.Lerp(game.transform.position, target.transform.position, 2);
        }
    }

    void aaa()
    {
        Check1.SetActive(true);
        Check2.SetActive(true);
        Check3.SetActive(true);
        x.SetActive(true);
    }
    void bbb()
    {
        a = 0;
        Invoke("Textt", 2);
        if(d == true)
        {
            Sound();
        }
        game.transform.position = new Vector3(0.39f, 20.19f, -5);
        b = true;
        if(h == false)
        {
            Invoke("Die", 5);
        }
    }
    void Die()
    {
        DeathManager.Instance.OnDeathUI(DeathManager.Instance.DeathList[21]);
        b = false;
        rb.isKinematic = true;
        rb.gravityScale = 1;
        rb.AddTorque(0);
        h = true;
    }

    void Sound()
    {
        SoundManager.Instance.PlaySound("Amongg");
        d = false;
    }
    void ss()
    {
        SoundManager.Instance.PlaySound("Amonga");
        g = true;
    }

    void Textt()
    {
        Text.SetActive(true);
    }
}
