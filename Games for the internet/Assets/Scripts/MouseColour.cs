﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MouseColour : MonoBehaviour
{
    public Texture2D texture;

    public GameObject BodySelection;
    public GameObject EyeSelection;

    public GameObject body;
    public GameObject eye;
    public GameObject colourSelector;
  
    void ChangeColour(GameObject change, GameObject parent)
    {
        Color selectedColour;

        if (GetSpritePixelColorUnderMousePointer(GetComponent<SpriteRenderer>(), out selectedColour))
        {
            change.GetComponent<SpriteRenderer>().color = selectedColour;
            colourSelector.GetComponent<SpriteRenderer>().color = selectedColour;
            colourSelector.SetActive(true);
            if(Input.GetMouseButton(0))
            {
                parent.GetComponent<ToggleFunctions>().ChangeButtonColour(selectedColour);
                parent.GetComponent<ToggleFunctions>().toggle = false;
            }
        }
        else
        {
            change.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);            
            colourSelector.SetActive(false);
        }
    }

    private void Update()
    {

       

        if(BodySelection.GetComponent<ToggleFunctions>().toggle)
        {
            ChangeColour(body, BodySelection);
        }

        if (EyeSelection.GetComponent<ToggleFunctions>().toggle)
        {
            ChangeColour(eye, EyeSelection);
        }


        if(!EyeSelection.GetComponent<ToggleFunctions>().toggle && !BodySelection.GetComponent<ToggleFunctions>().toggle)
        {
            colourSelector.SetActive(false);
        }

    }


    public bool GetSpritePixelColorUnderMousePointer(SpriteRenderer spriteRenderer, out Color color)
    {
        color = new Color();
        Camera cam = Camera.main;
        Vector2 mousePos = Input.mousePosition;
        Vector2 viewportPos = cam.ScreenToViewportPoint(mousePos);
        if (viewportPos.x < 0.0f || viewportPos.x > 1.0f || viewportPos.y < 0.0f || viewportPos.y > 1.0f) return false; // out of viewport bounds
                                                                                                                        // Cast a ray from viewport point into world
        Ray ray = cam.ViewportPointToRay(viewportPos);

        // Check for intersection with sprite and get the color
        return IntersectsSprite(spriteRenderer, ray, out color);
    }
    private bool IntersectsSprite(SpriteRenderer spriteRenderer, Ray ray, out Color color)
    {
        color = new Color();
        if (spriteRenderer == null) return false;
        Sprite sprite = spriteRenderer.sprite;
        if (sprite == null) return false;
        Texture2D texture = sprite.texture;
        if (texture == null) return false;
        // Check atlas packing mode
        if (sprite.packed && sprite.packingMode == SpritePackingMode.Tight)
        {
            // Cannot use textureRect on tightly packed sprites
            Debug.LogError("SpritePackingMode.Tight atlas packing is not supported!");
            // TODO: support tightly packed sprites
            return false;
        }
        // Craete a plane so it has the same orientation as the sprite transform
        Plane plane = new Plane(transform.forward, transform.position);
        // Intersect the ray and the plane
        float rayIntersectDist; // the distance from the ray origin to the intersection point
        if (!plane.Raycast(ray, out rayIntersectDist)) return false; // no intersection
                                                                     // Convert world position to sprite position
                                                                     // worldToLocalMatrix.MultiplyPoint3x4 returns a value from based on the texture dimensions (+/- half texDimension / pixelsPerUnit) )
                                                                     // 0, 0 corresponds to the center of the TEXTURE ITSELF, not the center of the trimmed sprite textureRect
        Vector3 spritePos = spriteRenderer.worldToLocalMatrix.MultiplyPoint3x4(ray.origin + (ray.direction * rayIntersectDist));
        Rect textureRect = sprite.textureRect;
        float pixelsPerUnit = sprite.pixelsPerUnit;
        float halfRealTexWidth = texture.width * 0.5f; // use the real texture width here because center is based on this -- probably won't work right for atlases
        float halfRealTexHeight = texture.height * 0.5f;
        // Convert to pixel position, offsetting so 0,0 is in lower left instead of center
        int texPosX = (int)(spritePos.x * pixelsPerUnit + halfRealTexWidth);
        int texPosY = (int)(spritePos.y * pixelsPerUnit + halfRealTexHeight);
        // Check if pixel is within texture
        if (texPosX < 0 || texPosX < textureRect.x || texPosX >= Mathf.FloorToInt(textureRect.xMax)) return false; // out of bounds
        if (texPosY < 0 || texPosY < textureRect.y || texPosY >= Mathf.FloorToInt(textureRect.yMax)) return false; // out of bounds
                                                                                                                   // Get pixel color
        color = texture.GetPixel(texPosX, texPosY);
        return true;
    }

}
