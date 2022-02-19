using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
public class AmongMove : Objects
{
    public string SceneName;
    public float x, y;
    public GameObject Cam;
    

    public override void Interaction()
    {
        base.Interaction();
        PlayerMove();
    }

    async void PlayerMove()
    {
        SoundManager.Instance.PlaySound("Warp");
        CameraManager.Instance.Stop = true;
        Cam = Camera.main.gameObject;
        Cam.transform.position = new Vector3(0, -2, -10);
        float alpah = 0;
        while (CameraManager.Instance.BlackScreen.color.a < 1)
        {
            CameraManager.Instance.BlackScreen.color = new Color(0, 0, 0, alpah);
            alpah += 0.01f;
            await Task.Delay(7);
        }
        Player.Instance.CoroutineQuit();
        Player.Instance.transform.position = new Vector3(x, y);
        SceneManager.LoadScene(SceneName);
        while (CameraManager.Instance.BlackScreen.color.a > 0)
        {
            CameraManager.Instance.BlackScreen.color = new Color(0, 0, 0, alpah);
            alpah -= 0.01f;
            await Task.Delay(7);
        }
    }
}
