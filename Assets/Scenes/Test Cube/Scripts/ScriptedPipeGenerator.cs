using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedPipeGenerator : MonoBehaviour {

    public List<GameObject> Points;
    public float Size;
    public float Speed;

    private GameObject startCap;
    private GameObject tube;
    private GameObject endCap;

    private void Start() {
        Debug.Log("Hello");
        Debug.LogError("Error!");
        Debug.LogWarning("Warning!");

        startCap = MakeSphereHemisphere.GenerateHemisphere(1, 1, true, Quaternion.Euler(-90, 0, 0));
        tube = MakeTube.GenerateTube(1, 1, 1, true);
        endCap = MakeSphereHemisphere.GenerateHemisphere(1, 1, true, Quaternion.Euler(90, 0, 0));

        ResetParts(Vector3.zero, Quaternion.identity, false, false, false);
        StartCoroutine(AnimatePipe());
    }

    private IEnumerator AnimatePipe() {
        foreach (GameObject node in Points) {
            PipeNode nodeType = node.GetComponent<PipeNode>();
            switch (nodeType.Type) {
            case PipeNode.NodeType.START:
                yield return StartCoroutine(AnimateStart(node));
                break;

            case PipeNode.NodeType.CORNER:
                yield return StartCoroutine(AnimateCorner(node));
                break;

            case PipeNode.NodeType.END:
            default:
                yield return StartCoroutine(AnimateEnd(node));
                break;
            }
        }

        Destroy(startCap);
        Destroy(tube);
        Destroy(endCap);
    }

    private IEnumerator AnimateStart(GameObject node) {
        yield return StartCoroutine(AnimateSpawn(node.transform.position, node.transform.rotation));
        yield return StartCoroutine(AnimateStraight(node.transform.position, node.transform.rotation, 0.5f));
    }

    private IEnumerator AnimateCorner(GameObject node) {
        Quaternion rotation = node.transform.rotation;
        rotation = rotation * Quaternion.Euler(0, 0, 180);

        Vector3 origin = node.transform.position;
        origin -= rotation * (Vector3.forward * 0.5f);

        yield return StartCoroutine(AnimateStraight(origin, rotation, 0.5f - Size));

        yield return StartCoroutine(AnimateCorner(node.transform.position, node.transform.rotation));

        rotation = node.transform.rotation;
        origin = node.transform.position +
            (rotation * (Vector3.forward * Size));

        yield return StartCoroutine(AnimateStraight(origin, rotation, 0.5f - Size));
    }

    private IEnumerator AnimateEnd(GameObject node) {
        Quaternion rotation = node.transform.rotation;
        Vector3 origin = node.transform.position;
        origin -= rotation * (Vector3.forward * 0.5f);

        yield return StartCoroutine(AnimateStraight(origin, rotation, 0.5f));

        Instantiate(endCap);
    }

    private void ResetParts(Vector3 position, Quaternion rotation, bool useStart, bool useTube, bool useEnd) {
        startCap.SetActive(useStart);
        startCap.transform.position = position;
        startCap.transform.rotation = rotation;
        startCap.transform.localScale = Vector3.one * Size;

        tube.SetActive(useTube);
        tube.transform.position = position;
        tube.transform.rotation = rotation;
        tube.transform.localScale = Vector3.one * Size;

        endCap.SetActive(useEnd);
        endCap.transform.position = position;
        endCap.transform.rotation = rotation;
        endCap.transform.localScale = Vector3.one * Size;
    }

    private IEnumerator AnimateSpawn(Vector3 position, Quaternion rotation) {
        ResetParts(position, rotation, true, false, true);

        Vector3 currentSize;
        float t = 0;
        while (t < Speed) {
            currentSize = Vector3.one * Size * (t / Speed);
            startCap.transform.localScale = currentSize;
            endCap.transform.localScale = currentSize;

            yield return null;
            t += Time.deltaTime;
        }

        startCap.transform.localScale = Vector3.one * Size;
        Instantiate(startCap);
    }

    private IEnumerator AnimateStraight(Vector3 position, Quaternion rotation, float length) {
        ResetParts(position, rotation, false, true, true);

        Vector3 start = position;
        Vector3 scale = Vector3.one * Size;
        Vector3 pos = start;

        float ttl = Speed * length;
        float t = 0;

        while (t < ttl) {
            scale.z = length * (t / ttl);
            tube.transform.localScale = scale;

            pos = start + (rotation * (Vector3.forward * scale.z));
            endCap.transform.position = pos;

            yield return null;
            t += Time.deltaTime;
        }

        scale.z = length;
        tube.transform.localScale = scale;
        Instantiate(tube);

        pos = start + (rotation * (Vector3.forward * scale.z));
        endCap.transform.position = pos;
    }

    private IEnumerator AnimateCorner(Vector3 position, Quaternion rotation) {
        ResetParts(position, rotation, false, false, true);

        GameObject torus = MakeTorus.GenerateTube(Size, 1, true, 0, 0.1f);

        Vector3 origin = position;
        origin.x -= Size;
        torus.transform.position = origin;
        torus.transform.rotation = Quaternion.Euler(90, 90, 0) * rotation;

        MeshFilter filter = torus.GetComponent<MeshFilter>();

        float angle = 0;
        float t = 0;
        Vector3 pivot = origin;
        pivot.y += Size;

        Vector3 rotationOffset = new Vector3(0, -Size, 0);
        float cornerSpeed = Speed * Mathf.PI * Size * 0.5f;

        while (t < cornerSpeed) {
            angle = 90 * (t / cornerSpeed);
            MakeTorus.GenerateMesh(filter, Size, 1, true, 0, angle);

            endCap.transform.rotation = torus.transform.rotation * Quaternion.Euler(angle, 0, 0);
            endCap.transform.position = pivot + (Quaternion.Euler(0, 0, angle) * rotationOffset);

            yield return null;
            t += Time.deltaTime;
        }

        MakeTorus.GenerateMesh(filter, Size, 1, true, 0, 90);
    }
}
