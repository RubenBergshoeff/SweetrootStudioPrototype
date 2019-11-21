using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillTestWaypoint : MonoBehaviour {
    public Skill TargetSkill = null;
    public int RequiredSkillScore = 0;
    public List<UnlockResult> unlockResults = new List<UnlockResult>();
    public float ResolveTime = 2;

    [SerializeField] private int itemSpawnCount = 5;
    [SerializeField] private SpriteRenderer spawnTemplate = null;

    private void Awake() {
        spawnTemplate.gameObject.SetActive(false);
    }

    public void ShowResolveAnimation() {
        float timePerItem = ResolveTime / itemSpawnCount;
        StartCoroutine(SpawnItems(timePerItem, itemSpawnCount));
    }

    private IEnumerator SpawnItems(float timePerItem, int itemSpawnCount) {
        while (itemSpawnCount > 0) {
            SpawnItem();
            itemSpawnCount--;
            yield return new WaitForSeconds(timePerItem);
        }
    }

    private void SpawnItem() {
        spawnTemplate.gameObject.SetActive(true);
        GameObject spawnItem = Instantiate(spawnTemplate.gameObject, spawnTemplate.transform.parent);
        spawnTemplate.gameObject.SetActive(false);
        spawnItem.GetComponent<SpriteRenderer>().sprite = TargetSkill.Icon;
        spawnItem.transform.DOScale(0, 0.35f).SetEase(Ease.InBack);
        spawnItem.transform.DOPunchPosition(Vector3.up * spawnItem.transform.localScale.y * 4, 0.6f, 5, 0.2f).OnComplete(() => Destroy(spawnItem.gameObject));
        //spawnItem.transform.DOPunchScale(spawnItem.transform.localScale * 2, 0.6f).
    }
}
