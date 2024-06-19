using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExtension
{
    public static void SetWidth(this RectTransform transform, float width)
    {
        transform.sizeDelta = new Vector2(width, transform.rect.height);
    }
}
