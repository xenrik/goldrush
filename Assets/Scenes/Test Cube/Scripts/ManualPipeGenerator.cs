using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualPipeGenerator : MonoBehaviour {

    public List<GameObject> Points;

    public float HeadSize;
    public float BodySize;
    public float TailSize;

    public float GrowSpeed;
    public float MoveSpeed;

    private GameObject startCap;
    private GameObject tube;
    private GameObject endCap;

	// Use this for initialization
	void Start () {
        startCap = MakeSphereHemisphere.GenerateHemisphere(HeadSize, 1, true, Quaternion.Euler(-90, 0, 0));
        tube = MakeTube.GenerateTube(1, HeadSize, 1, true);
        endCap = MakeSphereHemisphere.GenerateHemisphere(HeadSize, 1, true, Quaternion.Euler(90, 0, 0));

        StartCoroutine(AnimateStart(0));
	}

    private void ResetParts(Transform transform, bool useStart, bool useTube, bool useEnd) {
        startCap.SetActive(useStart);
        startCap.transform.position = transform.position;
        startCap.transform.rotation = transform.rotation;
        startCap.transform.localScale = Vector3.one;

        tube.SetActive(useTube);
        tube.transform.position = transform.position;
        tube.transform.rotation = transform.rotation;
        tube.transform.localScale = Vector3.one;

        endCap.SetActive(useEnd);
        endCap.transform.position = transform.position;
        endCap.transform.rotation = transform.rotation;
        endCap.transform.localScale = Vector3.one;
    }

    private IEnumerator AnimateStart(int point) {
        ResetParts(Points[point].transform, true, false, true);

        Vector3 size;
        float t = 0;
        while (t < GrowSpeed) {
            size = Vector3.one * (t / GrowSpeed);
            startCap.transform.localScale = size;
            endCap.transform.localScale = size;

            yield return null;
            t += Time.deltaTime;
        }

        startCap.transform.localScale = Vector3.one;
        Instantiate(startCap);
        StartCoroutine(AnimateStraight(point));
    }

    private IEnumerator AnimateStraight(int point) {
        ResetParts(Points[point].transform, false, true, true);

        Vector3 start = Points[point].transform.position;
        Vector3 delta = Points[point + 1].transform.position - start;
        delta.x -= HeadSize;

        Vector3 pos = start;
        Vector3 scale = Vector3.one;
        endCap.transform.localScale = scale;

        float t = 0;
        while (t < MoveSpeed) {
            scale.z = delta.x * (t / MoveSpeed);
            tube.transform.localScale = scale;

            pos.x = start.x + scale.z;
            endCap.transform.position = pos;

            yield return null;
            t += Time.deltaTime;
        }

        scale.z = delta.x;
        tube.transform.localScale = scale;
        Instantiate(tube);

        StartCoroutine(AnimateCorner(point + 1));
    }

    private IEnumerator AnimateCorner(int point) {
        ResetParts(Points[point].transform, false, false, true);

        GameObject torus = MakeTorus.GenerateTube(HeadSize, 1, true, 0, 0.1f);

        Vector3 origin = Points[point].transform.position;
        origin.x -= HeadSize;
        torus.transform.position = origin;

        torus.transform.rotation = Points[point].transform.rotation;

        MeshFilter filter = torus.GetComponent<MeshFilter>();

        float angle = 0;
        float t = 0;
        Vector3 pivot = origin;
        pivot.y += HeadSize;

        Vector3 rotationOffset = new Vector3(0, -HeadSize, 0);
        float cornerSpeed = MoveSpeed * Mathf.PI * HeadSize * 0.5f;

        while (t < cornerSpeed) {
            angle = 90 * (t / cornerSpeed);
            MakeTorus.GenerateMesh(filter, HeadSize, 1, true, 0, angle);

            endCap.transform.rotation = Points[point].transform.rotation * Quaternion.Euler(angle, 0, 0);
            endCap.transform.position = pivot + (Quaternion.Euler(0, 0, angle) * rotationOffset);

            yield return null;
            t += Time.deltaTime;
        }

        MakeTorus.GenerateMesh(filter, HeadSize, 1, true, 0, 90);
        StartCoroutine(AnimateEnd(point));
    }

    private IEnumerator AnimateEnd(int point) {
        ResetParts(Points[point+1].transform, false, true, true);

        Vector3 origin = Points[point].transform.position;
        origin.y += HeadSize;

        Vector3 target = Points[point+1].transform.position;

        tube.transform.position = origin;
        endCap.transform.position = origin;

        Vector3 delta = target - origin;
        Vector3 width;
        Vector3 scale = Vector3.one;
        endCap.transform.localScale = scale;

        float t = 0;
        while (t < MoveSpeed) {
            width = delta * (t / MoveSpeed);

            scale.z = width.magnitude;
            tube.transform.localScale = scale;
            endCap.transform.position = origin + width;

            yield return null;
            t += Time.deltaTime;
        }

        yield return null;
    }

}
