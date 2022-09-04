using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeautySound : MonoBehaviour
{
    [SerializeField] float playRate = 5f;
    float timeAfterPlay = 0f;
    bool canPlay = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && canPlay)
        {
            canPlay = false;
            SoundManager.Instance.PlayRandomSound(Sound.Beauty);
        }
    }

    private void Start()
    {
        canPlay = true;
    }

    private void Update()
    {
        if(!canPlay)
        {
            timeAfterPlay += Time.deltaTime;
            if(timeAfterPlay >= playRate)
            {
                canPlay = true;
                timeAfterPlay = 0f;
            }
        }
    }

}
