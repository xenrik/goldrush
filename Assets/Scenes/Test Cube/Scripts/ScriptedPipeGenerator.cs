using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedPipeGenerator : MonoBehaviour {

    public float Size;
    public float Speed;

    public Material Material;

    public GameObject PipeParent;

    public PipeNode StartingNode;
    public PipeNode FinalNode;

    private GameObject startCap;
    private GameObject tube;
    private GameObject endCap;

    private void Start() {
        startCap = MakeSphereHemisphere.GenerateHemisphere(1, 1, true, Quaternion.Euler(-90, 0, 0));
        startCap.GetComponent<MeshRenderer>().material = Material;
        startCap.transform.parent = PipeParent.transform;

        tube = MakeTube.GenerateTube(1, 1, 1, true);
        tube.GetComponent<MeshRenderer>().material = Material;
        tube.transform.parent = PipeParent.transform;

        endCap = MakeSphereHemisphere.GenerateHemisphere(1, 1, true, Quaternion.Euler(90, 0, 0));
        endCap.GetComponent<MeshRenderer>().material = Material;
        endCap.transform.parent = PipeParent.transform;

        ResetParts(Vector3.zero, Quaternion.identity, false, false, false);
        StartCoroutine(AnimatePipe());
    }

    private IEnumerator AnimatePipe() {
        PipeNode currentNode = StartingNode;
        while (currentNode != null) { 
            switch (currentNode.Type) {
            case PipeNode.NodeType.START:
                yield return StartCoroutine(AnimateStart(currentNode.gameObject, currentNode.NextNode.gameObject));
                break;

            case PipeNode.NodeType.CORNER:
                yield return StartCoroutine(AnimateCorner(currentNode.PreviousNode.gameObject, currentNode.gameObject, currentNode.NextNode.gameObject));
                break;

            case PipeNode.NodeType.END:
            default:
                yield return StartCoroutine(AnimateEnd(currentNode.PreviousNode.gameObject, currentNode.gameObject));
                break;
            }

            currentNode = currentNode.NextNode;
        }

        Destroy(startCap);
        Destroy(tube);
        Destroy(endCap);
    }

    private IEnumerator AnimateStart(GameObject currentNode, GameObject nextNode) {
        Vector3 position = PipeParent.transform.WorldToLocal(currentNode.transform.position);
        Quaternion rotation = PipeParent.transform.WorldToLocal(currentNode.transform.rotation);

        Vector3 nextRelativePosition = PipeParent.transform.WorldToLocal(nextNode.transform.position);

        float distanceToNext = (nextRelativePosition - position).magnitude;

        yield return StartCoroutine(AnimateSpawn(position, rotation));
        yield return StartCoroutine(AnimateStraight(position, rotation, distanceToNext / 2.0f));
    }

    private IEnumerator AnimateCorner(GameObject lastNode, GameObject currentNode, GameObject nextNode) {
        Vector3 position = PipeParent.transform.WorldToLocal(currentNode.transform.position);
        Quaternion rotation = PipeParent.transform.WorldToLocal(currentNode.transform.rotation);

        Vector3 nextRelativePosition = PipeParent.transform.WorldToLocal(nextNode.transform.position);
        Vector3 lastRelativePosition = PipeParent.transform.WorldToLocal(lastNode.transform.position);

        float distanceToLast = (position - lastRelativePosition).magnitude;
        float distanceToNext = (nextRelativePosition - position).magnitude;

        float firstLegLength = distanceToLast / 2.0f;
        float secondLegLength = ((distanceToNext / 2.0f) - Size);

        Quaternion tempRotation = rotation;
        tempRotation = tempRotation * Quaternion.Euler(0, -90, 0);

        Vector3 origin = position;
        origin -= tempRotation * (Vector3.forward * (distanceToLast / 2.0f));

        yield return StartCoroutine(AnimateStraight(origin, tempRotation, (distanceToLast / 2.0f) - Size));

        yield return StartCoroutine(AnimateCorner(position, rotation));

        tempRotation = rotation;
        origin = position +
            (tempRotation * (Vector3.forward * Size));

        yield return StartCoroutine(AnimateStraight(origin, tempRotation, secondLegLength));
    }

    private IEnumerator AnimateEnd(GameObject lastNode, GameObject currentNode) {
        Vector3 position = PipeParent.transform.WorldToLocal(currentNode.transform.position);
        Quaternion rotation = PipeParent.transform.WorldToLocal(currentNode.transform.rotation);

        Vector3 lastRelativePosition = PipeParent.transform.WorldToLocal(lastNode.transform.position);

        float distanceToLast = (position - lastRelativePosition).magnitude;

        Quaternion tempRotation = rotation;
        Vector3 origin = position;
        origin -= tempRotation * (Vector3.forward * (distanceToLast / 2.0f));

        yield return StartCoroutine(AnimateStraight(origin, tempRotation, distanceToLast / 2.0f));

        Instantiate(endCap, PipeParent.transform);
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

        Instantiate(startCap, PipeParent.transform);
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

        Instantiate(tube, PipeParent.transform);

        pos = start + (rotation * (Vector3.forward * scale.z));
        endCap.transform.localPosition = pos;
    }

    private IEnumerator AnimateCorner(Vector3 position, Quaternion rotation) {
        ResetParts(position, rotation, false, false, true);

        GameObject torus = MakeTorus.GenerateTube(Size, 1, true, 0, 0.1f);
        torus.GetComponent<MeshRenderer>().material = Material;
        torus.transform.parent = PipeParent.transform;

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
