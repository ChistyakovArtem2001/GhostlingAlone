using UnityEngine;

[ExecuteInEditMode]
public class RevealingLight : MonoBehaviour
{

    [SerializeField] private Material material;
    [SerializeField] private Light spotLight;
    private void Update()
    {
        float distance = Vector3.Distance(GameObject.Find("Hidden").transform.position, spotLight.transform.position);

        material.SetVector("_LightPosition", spotLight.transform.position);
        material.SetVector("_LightDirection", -spotLight.transform.forward);
        material.SetFloat("_LightAngle", spotLight.spotAngle);
        material.SetFloat("_StrengthFactor", (150 / distance));
        //Debug.Log(distance);
        //Debug.Log(GameObject.Find("Hidden").transform.position);
        //Debug.Log(spotLight.transform.position);
    }
}
