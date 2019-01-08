using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedPipeGenerator : MonoBehaviour {

    public float Size;
    public float Speed;

    public Material material;

    private GameObject startCap;
    private GameObject tube;
    private GameObject endCap;

    private void Start() {
        Debug.Log("Hello");
        Debug.LogError("Error!");
        Debug.LogWarning("Warning!");

        startCap = MakeSphereHemisphere.GenerateHemisphere(1, 1, true, Quaternion.Euler(-90, 0, 0));
        startCap.GetComponent<MeshRenderer>().material = material;

        tube = MakeTube.GenerateTube(1, 1, 1, true);
        tube.GetComponent<MeshRenderer>().material = material;

        endCap = MakeSphereHemisphere.GenerateHemisphere(1, 1, true, Quaternion.Euler(90, 0, 0));
        endCap.GetComponent<MeshRenderer>().material = material;

        ResetParts(Vector3.zero, Quaternion.identity, false, false, false);
        StartCoroutine(AnimatePipe());
    }

    private IEnumerator AnimatePipe() {
        for (int i = 0; i < transform.childCount; ++i) {
            GameObject node = transform.GetChild(i).gameObject;
            PipeNode nodeType = node.GetComponent<PipeNode>();
            if (nodeType == null) {
                continue;
            }

            switch (nodeType.Type) {
            case PipeNode.NodeType.START:
                yield return StartCoroutine(AnimateStart(i));
                break;

            case PipeNode.NodeType.CORNER:
                yield return StartCoroutine(AnimateCorner(i));
                break;

            case PipeNode.NodeType.END:
            default:
                yield return StartCoroutine(AnimateEnd(i));
                break;
            }
        }

        Destroy(startCap);
        Destroy(tube);
        Destroy(endCap);
    }

    private IEnumerator AnimateStart(int nodeIndex) {
        GameObject currentNode = transform.GetChild(nodeIndex).gameObject;
        GameObject nextNode = transform.GetChild(nodeIndex + 1).gameObject;
        float distanceToNext = (nextNode.transform.position - currentNode.transform.position).magnitude;

        yield return StartCoroutine(AnimateSpawn(currentNode.transform.position, currentNode.transform.rotation));
        yield return StartCoroutine(AnimateStraight(currentNode.transform.position, currentNode.transform.rotation, distanceToNext / 2.0f));
    }

    private IEnumerator AnimateCorner(int nodeIndex) {
        GameObject lastNode = transform.GetChild(nodeIndex - 1).gameObject;
        GameObject currentNode = transform.GetChild(nodeIndex).gameObject;
        GameObject nextNode = transform.GetChild(nodeIndex + 1).gameObject;
        float distanceToLast = (currentNode.transform.position - lastNode.transform.position).magnitude;
        float distanceToNext = (nextNode.transform.position - currentNode.transform.position).magnitude;

        float firstLegLength = distanceToLast / 2.0f;
        float secondLegLength = ((distanceToNext / 2.0f) - Size);

        Quaternion rotation = currentNode.transform.rotation;
        rotation = rotation * Quaternion.Euler(0, -90, 0);

        Vector3 origin = currentNode.transform.position;
        origin -= rotation * (Vector3.forward * (distanceToLast / 2.0f));

        yield return StartCoroutine(AnimateStraight(origin, rotation, (distanceToLast / 2.0f) - Size));

        yield return StartCoroutine(AnimateCorner(currentNode.transform.position, currentNode.transform.rotation));

        rotation = currentNode.transform.rotation;
        origin = currentNode.transform.position +
            (rotation * (Vector3.forward * Size));

        yield return StartCoroutine(AnimateStraight(origin, rotation, secondLegLength));
    }

    private IEnumerator AnimateEnd(int nodeIndex) {
        GameObject lastNode = transform.GetChild(nodeIndex - 1).gameObject;
        GameObject currentNode = transform.GetChild(nodeIndex).gameObject;
        float distanceToLast = (currentNode.transform.position - lastNode.transform.position).magnitude;

        Quaternion rotation = currentNode.transform.rotation;
        Vector3 origin = currentNode.transform.position;
        origin -= rotation * (Vector3.forward * (distanceToLast / 2.0f));

        yield return StartCoroutine(AnimateStraight(origin, rotation, distanceToLast / 2.0f));

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
        torus.GetComponent<MeshRenderer>().material = material;

        Vector3 origin = position - (rotation * (Vector3.left * Size));

        torus.transform.position = origin;
        torus.transform.rotation = rotation * Quaternion.Euler(0, 0, 90) * Quaternion.Euler(-90, 0, 0);

        MeshFilter filter = torus.GetComponent<MeshFilter>();

        float angle = 0;
        float t = 0;
        Vector3 pivot = origin + (rotation * (Vector3.forward * Size));
        Vector3 pivotOffset = rotation * (Vector3.left * Size);

        float cornerSpeed = Speed * Mathf.PI * Size * 0.5f;

        while (t < cornerSpeed) {
            angle = 90 * (t / cornerSpeed);
            MakeTorus.GenerateMesh(filter, Size, 1, true, 0, angle);

            endCap.transform.rotation = torus.transform.rotation * Quaternion.Euler(angle, 0, 0);
            endCap.transform.position = pivot + (endCap.transform.up * Size);

            yield return null;
            t += Time.deltaTime;
        }

        MakeTorus.GenerateMesh(filter, Size, 1, true, 0, 90);
    }
}
