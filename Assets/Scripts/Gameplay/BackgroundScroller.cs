using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float SpeedFactorIncreaser;
    public float Speed;
    public GameManager GameManager;
    public Renderer BackgroundRenderer;

    void Update()
    {
        Speed = (Mathf.Sqrt(GameManager.Difficulty) / 2) * SpeedFactorIncreaser;
        BackgroundRenderer.material.mainTextureOffset += new Vector2(0, (Speed * Time.deltaTime));
    }
}
