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
        startCap = MakeSphereHemisphere.GenerateHemisphere(1, 1, true, Quaternion.Euler(-90, 0, 0));
        startCap.GetComponent<MeshRenderer>().material = material;
        startCap.transform.parent = gameObject.transform;

        tube = MakeTube.GenerateTube(1, 1, 1, true);
        tube.GetComponent<MeshRenderer>().material = material;
        tube.transform.parent = gameObject.transform;

        endCap = MakeSphereHemisphere.GenerateHemisphere(1, 1, true, Quaternion.Euler(90, 0, 0));
        endCap.GetComponent<MeshRenderer>().material = material;
        endCap.transform.parent = gameObject.transform;

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
        float distanceToNext = (nextNode.transform.localPosition - currentNode.transform.localPosition).magnitude;

        yield return StartCoroutine(AnimateSpawn(currentNode.transform.localPosition, currentNode.transform.localRotation));
        yield return StartCoroutine(AnimateStraight(currentNode.transform.localPosition, currentNode.transform.localRotation, distanceToNext / 2.0f));
    }

    private IEnumerator AnimateCorner(int nodeIndex) {
        GameObject lastNode = transform.GetChild(nodeIndex - 1).gameObject;
        GameObject currentNode = transform.GetChild(nodeIndex).gameObject;
        GameObject nextNode = transform.GetChild(nodeIndex + 1).gameObject;
        float distanceToLast = (currentNode.transform.localPosition - lastNode.transform.localPosition).magnitude;
        float distanceToNext = (nextNode.transform.localPosition - currentNode.transform.localPosition).magnitude;

        float firstLegLength = distanceToLast / 2.0f;
        float secondLegLength = ((distanceToNext / 2.0f) - Size);

        Quaternion rotation = currentNode.transform.localRotation;
        rotation = rotation * Quaternion.Euler(0, -90, 0);

        Vector3 origin = currentNode.transform.localPosition;
        origin -= rotation * (Vector3.forward * (distanceToLast / 2.0f));

        yield return StartCoroutine(AnimateStraight(origin, rotation, (distanceToLast / 2.0f) - Size));

        yield return StartCoroutine(AnimateCorner(currentNode.transform.localPosition, currentNode.transform.localRotation));

        rotation = currentNode.transform.localRotation;
        origin = currentNode.transform.localPosition +
            (rotation * (Vector3.forward * Size));

        yield return StartCoroutine(AnimateStraight(origin, rotation, secondLegLength));
    }

    private IEnumerator AnimateEnd(int nodeIndex) {
        GameObject lastNode = transform.GetChild(nodeIndex - 1).gameObject;
        GameObject currentNode = transform.GetChild(nodeIndex).gameObject;
        float distanceToLast = (currentNode.transform.localPosition - lastNode.transform.localPosition).magnitude;

        Quaternion rotation = currentNode.transform.localRotation;
        Vector3 origin = currentNode.transform.localPosition;
        origin -= rotation * (Vector3.forward * (distanceToLast / 2.0f));

        yield return StartCoroutine(AnimateStraight(origin, rotation, distanceToLast / 2.0f));

        Instantiate(endCap, gameObject.transform);
    }

    private void ResetParts(Vector3 position, Quaternion rotation, bool useStart, bool useTube, bool useEnd) {
        startCap.SetActive(useStart);
        startCap.transform.localPosition = position;
        startCap.transform.localRotation = rotation;
        startCap.transform.localScale = Vector3.one * Size;

        tube.SetActive(useTube);
        tube.transform.localPosition = position;
        tube.transform.localRotation = rotation;
        tube.transform.localScale = Vector3.one * Size;

        endCap.SetActive(useEnd);
        endCap.transform.localPosition = position;
        endCap.transform.localRotation = rotation;
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

        Instantiate(startCap, gameObject.transform);
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
            endCap.transform.localPosition = pos;

            yield return null;
            t += Time.deltaTime;
        }

        scale.z = length;
        tube.transform.localScale = scale;

        Instantiate(tube, gameObject.transform);

        pos = start + (rotation * (Vector3.forward * scale.z));
        endCap.transform.localPosition = pos;
    }

    private IEnumerator AnimateCorner(Vector3 position, Quaternion rotation) {
        ResetParts(position, rotation, false, false, true);

        GameObject torus = MakeTorus.GenerateTube(Size, 1, true, 0, 0.1f);
        torus.GetComponent<MeshRenderer>().material = material;
        torus.transform.parent = gameObject.transform;

        Vector3 origin = position - (rotation * (Vector3.left * Size));

        torus.transform.localPosition = origin;
        torus.transform.localRotation = rotation * Quaternion.Euler(0, 0, 90) * Quaternion.Euler(-90, 0, 0);

        MeshFilter filter = torus.GetComponent<MeshFilter>();

        float angle = 0;
        float t = 0;
        Vector3 pivot = origin + (rotation * (Vector3.forward * Size));

        float cornerSpeed = Speed * Mathf.PI * Size * 0.5f;

        while (t < cornerSpeed) {
            angle = 90 * (t / cornerSpeed);
            MakeTorus.GenerateMesh(filter, Size, 1, true, 0, angle);

            endCap.transform.localRotation = torus.transform.localRotation * Quaternion.Euler(angle, 0, 0);
            endCap.transform.localPosition = pivot + (endCap.transform.localRotation * Vector3.up * Size);

            yield return null;
            t += Time.deltaTime;
        }

        MakeTorus.GenerateMesh(filter, Size, 1, true, 0, 90);
    }
}
