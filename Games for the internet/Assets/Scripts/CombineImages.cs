using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombineImages : MonoBehaviour
{

    public Texture2D textureEye;
    public Texture2D textureBody;
    public Texture2D NewTexture;

    private Color EyeColour;
    private Color BodyColour;

    public GameObject bodyObject;
    public GameObject eyeObject;

    // Start is called before the first frame update
    public void Combine()
    {
        EyeColour = eyeObject.GetComponent<SpriteRenderer>().color;
        BodyColour = bodyObject.GetComponent<SpriteRenderer>().color;
        NewTexture = MergeImage(textureBody, textureEye, NewTexture); 
    }

    public void SwitcLevel()
    {
        SceneManager.LoadScene("Level 1");
    }


    // Update is called once per frame
  


    //public void CreateTexture(Texture2D texture, int width, int height, Color32 color)
    //{
    //    Color32[] newColors = new Color32[width * height];
    //    for (int i = 0; i < newColors.Length; i++)
    //    {
    //        newColors[i] = color;

    //    }

    //    texture.SetPixels32(newColors);
    //}



    public Texture2D MergeImage(Texture2D Body, Texture2D Eyes, Texture2D NewText)
    {
        //BodyColour.a = 1f;
        //EyeColour.a = 1f;

        int startX = 0;
        int startY = 0;

        for (int x = startX; x < Body.width; x++)
        {

            for (int y = startY; y < Body.height; y++)
            {
                Color bgColor = Body.GetPixel(x, y);
                Color wmColor = Eyes.GetPixel(x, y);
                if (wmColor.a > 0.9f)
                {
                    NewText.SetPixel(x, y, wmColor * EyeColour);
                }
                else
                {
                    NewText.SetPixel(x, y, bgColor * BodyColour);
                }



                //Color final_color = Color.Lerp(bgColor, wmColor, wmColor.a / 1.0f);

            }
        }

        NewText.Apply();
        return NewText;
    }

}
