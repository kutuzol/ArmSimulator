using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private Quaternion original1; // rotation 2� �������
    private Quaternion original2;
    private Transform Sphere;
    private Transform Finger1; 
    private Transform Finger2;
    
    void Start()
    {
        Sphere = GameObject.Find("Sphere").GetComponent<Transform>();
        Finger1 = GameObject.Find("Finger1").GetComponent<Transform>();
        Finger2 = GameObject.Find("Finger2").GetComponent<Transform>();
        Debug.Log(Finger1.position);
        //Debug.Log(Finger1.name);
        //Debug.Log(Finger2.name);
        rb = GetComponent<Rigidbody>(); // ���������� ������ ��� ������ ������� Thing
        original1= Finger1.rotation ; // �������� ��������� 2� �������
        original2 = Finger2.rotation;
    }
    
    void Update()
    {
       
        float dist = Vector3.Distance(Sphere.position, transform.position); // ����������� ���������� ����� Sphere � Thing
                                                                            
        // Debug.Log(dist);// ������ ���� ���������� �� ������� ������� ���������� ����� ���������

        if (Input.GetKey(KeyCode.E)) // ����� (�������) ������
            {
            // ������ "�������" ������
            Finger1.position = Finger1.position + new Vector3(0.03f, 0, 0);
            Vector3 relativePos1 = Finger1.position - transform.position;
            Finger1.rotation = Quaternion.LookRotation(relativePos1 * Time.deltaTime);
           
            Finger2.position = Finger2.position + new Vector3(-0.03f, 0, 0);
            Vector3 relativePos2 = Finger2.position - transform.position;
            Finger2.rotation = Quaternion.LookRotation(relativePos2*Time.deltaTime);

             rb.isKinematic = true; // ����� ������ �� �����, ����� �� ��� ����� � �������, ��������� ������

            if (dist <= 6)  // ���� ���������� ����� ������ � �������� < 6 ����� ����� ���
                {
                    transform.position = Sphere.position + new Vector3(0, 0, -3);
                    transform.rotation = Sphere.rotation;
                    transform.SetParent(Sphere); // ��� ������ ����������� "��������" ����, ��� ������� ��� ��������� ������, "��� ������ �����"
                }
            }
        if (Input.GetKey(KeyCode.Q)) // ��������� (���������) ������
        {
            transform.parent = null; // "����" ������������ ����� ���� � �������, ������ ��� �� ����
            rb.isKinematic = false; // ���������� ������� ��� ������ ����� ������
            // "���������" ������
            Finger1.position = Finger1.position + new Vector3(-0.03f, 0, 0);
            Finger1.rotation = original1;
            Finger2.position = Finger2.position + new Vector3(0.03f, 0, 0);
            Finger2.rotation = original2;
        }

    }
}
