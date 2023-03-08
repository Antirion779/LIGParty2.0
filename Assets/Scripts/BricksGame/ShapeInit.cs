using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShapeInit : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsUi;
    public int points;
    [SerializeField] private Color pointColor;
    [SerializeField] private SpriteRenderer brickSpriteRenderer;
    [SerializeField] private List<Color> brickColors;

    public GameObject Init()
    {
        pointsUi.text = points.ToString();
        //pointsUi.color = pointColor;
        brickSpriteRenderer.color = GetRandomColor();
        return this.gameObject;
    }
    private Color GetRandomColor()
    {
        int _randomIndex = Random.Range(0, brickColors.Count);
        return brickColors[_randomIndex];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fall")
        {
            Destroy(this.gameObject);
        }
    }
}
