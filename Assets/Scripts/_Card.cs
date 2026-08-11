using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Card : MonoBehaviour
{

    private int spriteID;
    private int id;
    private bool flipped;
    private bool turning;
    [SerializeField]
    private UnityEngine.UI.Image img;

    private IEnumerator Flip90(Transform thisTransform, float time, bool changeSprite)
    {
        Quaternion startRotation = thisTransform.localRotation;

        float elapsed = 0f;
        bool spriteChanged = false;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsed / time);
            float angle = progress * 180f;

            thisTransform.localRotation =
                startRotation * Quaternion.AngleAxis(angle, Vector3.up);

            // Cambiar la imagen cuando la carta está de canto
            if (changeSprite && !spriteChanged && progress >= 0.5f)
            {
                flipped = !flipped;
                ChangeSprite();
                spriteChanged = true;
            }

            yield return null;
        }

        thisTransform.localRotation =
            startRotation * Quaternion.AngleAxis(180f, Vector3.up);

        turning = false;
    }

    public void Flip()
    {
        if (turning)
            return;

        turning = true;

        if (AudioPlayer.Instance != null)
            AudioPlayer.Instance.PlayAudio(0);

        StartCoroutine(Flip90(transform, 0.25f, true));
    }

    private void ChangeSprite()
    {
        if (spriteID == -1 || img == null) return;
        if (flipped)
            img.sprite = _CardGameManager.Instance.GetSprite(spriteID);
        else
            img.sprite = _CardGameManager.Instance.CardBack();
    }

    public void Inactive()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        float rate = 1.0f / 2.5f;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            img.color = Color.Lerp(img.color, Color.clear, t);

            yield return null;
        }
    }

    public void Active()
    {
        if (img)
            img.color = Color.white;
    }

    public int SpriteID
    {
        set
        {
            spriteID = value;
            flipped = true;
            ChangeSprite();
        }
        get { return spriteID; }
    }

    public int ID
    {
        set { id = value; }
        get { return id; }
    }

    public void ResetRotation()
    {
        transform.localRotation = Quaternion.identity;
        flipped = true;
        ChangeSprite();
    }

    public void CardBtn()
    {
        if (flipped || turning) return;
        if (!_CardGameManager.Instance.canClick()) return;
        Flip();
        StartCoroutine(SelectionEvent());
    }

    private IEnumerator SelectionEvent()
    {
        yield return new WaitForSeconds(0.5f);
        _CardGameManager.Instance.cardClicked(spriteID, id);
    }
}