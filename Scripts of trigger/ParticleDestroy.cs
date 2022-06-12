using UnityEngine;
using System.Collections;

public class ParticleDestroy : MonoBehaviour {

    private ParticleSystem ps;

	void Start () {

        ps = GetComponent<ParticleSystem>();
        Destroy(gameObject, ps.startLifetime + 0.5f);
	
	}
}
