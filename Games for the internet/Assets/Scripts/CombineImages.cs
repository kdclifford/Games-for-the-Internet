using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineImages : MonoBehaviour
{

    public Texture2D texture1;
    public Texture2D texture2;

    // Start is called before the first frame update
    void Start()
    {
        texture1 = AddWatermark(texture1, texture2); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //public void CreateTexture(Texture2D texture, int width, int height, Color32 color)
    //{
    //    Color32[] newColors = new Color32[width * height];
    //    for (int i = 0; i < newColors.Length; i++)
    //    {
    //        newColors[i] = color;

    //    }

    //    texture.SetPixels32(newColors);
    //}



    public Texture2D AddWatermark(Texture2D background, Texture2D watermark)
    {

        int startX = 0;
        int startY = background.height - watermark.height;

        for (int x = startX; x < background.width; x++)
        {

            for (int y = startY; y < background.height; y++)
            {
                Color bgColor = background.GetPixel(x, y);
                Color wmColor = watermark.GetPixel(x - startX, y - startY);

                Color final_color = Color.Lerp(bgColor, wmColor, wmColor.a / 1.0f);

                background.SetPixel(x, y, final_color);
            }
        }

        background.Apply();
        return background;
    }

}
