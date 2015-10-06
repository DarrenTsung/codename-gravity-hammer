using DT;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
﻿using UnityEngine;
﻿using UnityEngine.Assertions;

public class TestGaussianDist : MonoBehaviour, ICustomEditor {
	// PRAGMA MARK - INTERNAL
	[SerializeField]
	protected GameObject _dot;
	
	[SerializeField]
	protected float _mean = 0.0f;
	[SerializeField]
	protected float _standardDeviation = 2.0f;
	[SerializeField]
	protected int _pointsToGenerate = 100;
	
	[MakeButtonAttribute]
	protected void ResampleGaussian() {
		while (transform.childCount != 0) {
			GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
		}
		
		Assert.IsTrue(_dot != null);
		
		Dictionary<int, int> distributionMapping = new Dictionary<int, int>();
		if (_dot != null) {
			for (int i = 0; i < _pointsToGenerate; i++) {
				int val = (int)Mathf.Round(MathUtil.SampleGaussian(_mean, _standardDeviation));
				int oldCount = distributionMapping.SafeGet(val, 0);
				
				GameObject dotClone = MonoBehaviour.Instantiate(_dot, Vector3.zero, Quaternion.identity) as GameObject;
				this.SetSelfAsParent(dotClone);
				dotClone.transform.localPosition = new Vector3(val, oldCount, 0.0f);
				dotClone.GetComponent<SpriteRenderer>().color = ColorExtensions.RandomPleasingColor();
				
				distributionMapping[val] = oldCount + 1;
			}
		}
	}
}
