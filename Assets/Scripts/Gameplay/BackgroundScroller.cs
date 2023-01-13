using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float SpeedIncreaser;
    public float Speed;
    public GameManager GameManager;

    [SerializeField]
    private Renderer bgRenderer;

    void Update()
    {
        Speed = (Mathf.Sqrt(GameManager.Difficulty) / 2) * SpeedIncreaser;
        bgRenderer.material.mainTextureOffset += new Vector2(0, (Speed * Time.deltaTime));
    }
}
