using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxEffect : MonoBehaviour
{
    [Header("Parallax Settings")]
    [SerializeField] private Vector2 parallaxDirection = new(1f, 0f);
    [SerializeField] private float speed = 0.5f;

    private RawImage rawImage;
    private Vector2 uvOffset = Vector2.zero;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        uvOffset += parallaxDirection * speed * Time.deltaTime;
        rawImage.uvRect = new Rect(uvOffset, rawImage.uvRect.size);
    }
}
