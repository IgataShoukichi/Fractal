using UnityEngine;

public class JuliaSetGenerator : MonoBehaviour
{
    public int width = 512; // テクスチャの幅
    public int height = 512; // テクスチャの高さ
    public float zoom = 1.0f; // ズームレベル
    public Vector2 offset = Vector2.zero; // オフセット
    public float cRe = -0.7f; // ジュリア集合のパラメータ c
    public float cIm = 0.27015f; // ジュリア集合のパラメータ c

    public int maxIteration = 1;

    private Texture2D texture;

    void Start()
    {
        texture = new Texture2D(width, height);
        GetComponent<Renderer>().material.mainTexture = texture;
        GenerateJuliaSet();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            maxIteration += 10;
            zoom += 1.5f;
            GenerateJuliaSet();
        }
    }
    void GenerateJuliaSet()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float zx = 1.5f * (x - width / 2) / (0.5f * width) / zoom + offset.x;
                float zy = (y - height / 2) / (0.5f * height) / zoom + offset.y;
                int iteration = 0;

                while (zx * zx + zy * zy < 4 && iteration < maxIteration)
                {
                    float tmp = zx * zx - zy * zy + cRe;
                    zy = 2.0f * zx * zy + cIm;
                    zx = tmp;
                    iteration++;
                }

                float colorValue = (float)iteration / maxIteration;
                Color color = Color.Lerp(Color.black, Color.white, colorValue);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
    }
}
