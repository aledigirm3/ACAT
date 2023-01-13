using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float Speed;
    public GameManager GameManager;

    [SerializeField]
    private Renderer bgRenderer;

    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(0, ((Mathf.Sqrt(GameManager.Difficulty)/2) * Speed) * Time.deltaTime);
    }
}
