using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class Level : ScriptableObject {

	[Range(0,100)]
	public int m_no_grunts = 0;
    [Range(0, 100)]
    public int m_no_hulks_vertical = 0;
    [Range(0, 100)]
    public int m_no_hulks_horizontal = 0;

    public Color background = Color.black;

	void OnEnable() {
		/// do we need to init anyhow?
	}

	void OnDisable() {
		/// do we need to destroy/disable something?
	}
}
