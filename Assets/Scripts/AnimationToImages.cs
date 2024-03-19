using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToImages : MonoBehaviour
{
    public Animation animationComponent; // 애니메이션 컴포넌트
    public int frameRate = 60; // 프레임 속도
    public string outputFolder = "Assets/AnimationPng"; // 이미지 저장 폴더

    private void Start()
    {
        // 존재하지 않는 폴더라면 생성
        if (!System.IO.Directory.Exists(outputFolder))
        {
            System.IO.Directory.CreateDirectory(outputFolder);
        }

        // 애니메이션 클립의 길이 계산
        float animationLength = animationComponent.clip.length;
        int totalFrames = Mathf.RoundToInt(animationLength * frameRate);

        // 각 프레임을 렌더링하고 이미지로 저장
        for (int i = 0; i < totalFrames; i++)
        {
            // 해당 시간에 애니메이션을 설정
            float time = i / (float)frameRate;
            animationComponent.clip.SampleAnimation(gameObject, time);

            // 프레임을 렌더링하여 이미지로 저장
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
