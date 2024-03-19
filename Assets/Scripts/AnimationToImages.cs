using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToImages : MonoBehaviour
{
    public Animation animationComponent; // �ִϸ��̼� ������Ʈ
    public int frameRate = 60; // ������ �ӵ�
    public string outputFolder = "Assets/AnimationPng"; // �̹��� ���� ����

    private void Start()
    {
        // �������� �ʴ� ������� ����
        if (!System.IO.Directory.Exists(outputFolder))
        {
            System.IO.Directory.CreateDirectory(outputFolder);
        }

        // �ִϸ��̼� Ŭ���� ���� ���
        float animationLength = animationComponent.clip.length;
        int totalFrames = Mathf.RoundToInt(animationLength * frameRate);

        // �� �������� �������ϰ� �̹����� ����
        for (int i = 0; i < totalFrames; i++)
        {
            // �ش� �ð��� �ִϸ��̼��� ����
            float time = i / (float)frameRate;
            animationComponent.clip.SampleAnimation(gameObject, time);

            // �������� �������Ͽ� �̹����� ����
            RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
            Camera.main.targetTexture = renderTexture;
            Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            Camera.main.Render();
            RenderTexture.active = renderTexture;
            screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            byte[] bytes = screenshot.EncodeToPNG();
            System.IO.File.WriteAllBytes(outputFolder + "/frame_" + i.ToString("0000") + ".png", bytes);
            Destroy(renderTexture);
        }

        Debug.Log("Image extraction complete!");
    }
}
