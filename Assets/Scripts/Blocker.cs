using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blocker : MonoBehaviour
{
    [SerializeField] private Sprite _blockEndSprite;
    [SerializeField] private Transform _destination;
    [SerializeField] private float _moveSpeed;

    
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DoBlock();
        }
    }

    private void DoBlock()
    {
        ﻿﻿﻿﻿﻿﻿﻿﻿transform.DOMove(_destination.position, _moveSpeed)
            .OnComplete(() => OnDoBlockComplete());
    }

    private void OnDoBlockComplete()
    {
        GetComponent<SpriteRenderer>().sprite = _blockEndSprite;
        this.enabled = false;
    }
}
