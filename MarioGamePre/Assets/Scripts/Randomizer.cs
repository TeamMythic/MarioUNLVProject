using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
	public int randomizeMe(int min, int max)
	{
		return Random.Range(min, max);
	}
}
