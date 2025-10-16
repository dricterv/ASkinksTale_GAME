using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public Sprite fullHeart, threeQuaterHeart, halfHeart, oneQuaterHeart, emptyHeart;
    private Image heartImage;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    public void SetHeartImage(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = emptyHeart;
                break;
            case HeartStatus.OneQuater:
                heartImage.sprite = oneQuaterHeart;
                break;
            case HeartStatus.Half:
                heartImage.sprite = halfHeart;
                break;
            case HeartStatus.ThreeQuater:
                heartImage.sprite = threeQuaterHeart;
                break;
            case HeartStatus.Full:
                heartImage.sprite = fullHeart;
                break;
        }
    }

}

public enum HeartStatus
{
    Empty = 0,
    OneQuater = 1,
    Half = 2,
    ThreeQuater = 3,
    Full = 4


}
