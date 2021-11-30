
using UnityEngine;

namespace Utils {
    public class SpriteAndUI {

        public static void FitSpriteToUIImage(RectTransform constraintRect,
                UnityEngine.UI.Image targetImage, UnityEngine.Sprite sprite) {
            float width, height;
            if (sprite.rect.height > sprite.rect.width) {
                height = constraintRect.sizeDelta.y;
                width = height * sprite.rect.width / sprite.rect.height;
            }
            else {
                width = constraintRect.sizeDelta.x;
                height = width * sprite.rect.height / sprite.rect.width;
            }
            targetImage.rectTransform.sizeDelta = new Vector2(width, height);
            targetImage.sprite = sprite;
        }



    }
}