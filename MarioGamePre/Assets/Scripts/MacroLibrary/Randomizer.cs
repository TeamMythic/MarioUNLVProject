using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : GenericComponentFunctions
{
	public int randomizeMe(int min, int max)
	{//Return a value between min and max - 1:
		return Random.Range(min, max);
	}
}
