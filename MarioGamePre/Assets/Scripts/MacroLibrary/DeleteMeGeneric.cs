using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMeGeneric : MonoBehaviour
{
    [SerializeField] private float duration = 1.5f;
    private void Awake()
    {
        StartCoroutine(deleteMeAfterDuration());
    }
    private IEnumerator deleteMeAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
	}
}
