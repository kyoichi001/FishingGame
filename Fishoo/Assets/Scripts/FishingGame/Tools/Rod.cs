using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame.Tools
{
    [RequireComponent(typeof(Animation))]
    /// <summary>
    /// 釣り竿の動作を制御するクラス
    /// </summary>
    public class Rod : MonoBehaviour, FishingTool
    {
        /// <summary>
        /// 釣り竿の画像
        /// </summary>
        public SpriteRenderer sprite;

        [Header("Animations")]
        [SerializeField] AnimationClip throwRodClip;
        [SerializeField] AnimationClip retrieveRodClip;

        Animation throwRodAnimation;

        /// <summary>
        /// 釣り具を展開する
        /// </summary>
        public void ExpandTools()
        {
            if (throwRodAnimation == null) return;
            throwRodAnimation.clip = throwRodClip;
            throwRodAnimation.Play();
        }

        /// <summary>
        /// 釣りが終わり釣り具を収納する
        /// </summary>
        public void RetrieveTools()
        {
            throwRodAnimation.clip = retrieveRodClip;
            throwRodAnimation.Play();
        }

        public void SetInvisible()
        {
            var color = sprite.color;
            color.a = 0;
            sprite.color = color;
        }

        // Start is called before the first frame update
        void Start()
        {
            throwRodAnimation = GetComponent<Animation>();
        }
    }
}



// Update is called once per frame
/*
        /// <summary>
        /// 釣り竿を回転する軸
        /// </summary>
        public GameObject shaft;
 * void Update()
{
    //  if (!linkMouse) return;
    //マウスに棒が追従するようにしている。画面をMaximizeしないとずれるので注意
    // Vector3でマウス位置座標を取得する
    var position = Input.mousePosition;
    // Z軸修正
    position.z = 10f;
    // マウス位置座標をスクリーン座標からワールド座標に変換する
    var screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
    // ワールド座標に変換されたマウス座標を代入
    //rotator.transform.position = screenToWorldPointPosition;
}*/
