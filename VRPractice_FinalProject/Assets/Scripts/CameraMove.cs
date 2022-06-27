using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed;
    public float dist, height;

    void LateUpdate()
    {
        if(!GameManager.Instance.playerDead){
			float currY = Mathf.LerpAngle(this.transform.eulerAngles.y, target.eulerAngles.y, rotateSpeed * Time.deltaTime);
			Quaternion rot = Quaternion.Euler(0, currY, 0);
			
			this.transform.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);
			this.transform.LookAt(target);
		}
    }
}