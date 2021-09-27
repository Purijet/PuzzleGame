using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public string puzzlename = "position";
    [Header("���ϸH��")]
    public GameObject[] fragment;

    private bool isMouseDown = false;
    public bool isClicked = false;
    private bool overlap = false;
    private Vector3 oldposition;
    private GameObject target = null;
    private GameObject mouse;

    private void Start()
    {
        // �H����l��m
        for (int i = 0; i < fragment.Length; i++)
        {
            fragment[i].transform.position = new Vector3(Random.Range(-7, 7), Random.Range(-3.5f, 3.5f), 0);
        }

        mouse = GameObject.Find("mouse");
    }

    private void Update()
    {
        // ��mouse�l�׸�۹��Ш�
        mouse.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        isMouseDown = Input.GetMouseButton(0);      // ���Х�����U�ɡAisMouse��true

        if (!isMouseDown && isClicked)              // item�Q��ʹL�{�����Щ�}
        {
            isClicked = false;
            if (!overlap) target.transform.position = oldposition;
        }
        // �bitem�Q��ʹL�{���O�Ҩ�l�׸��mouse���A�îɨ�P�w��O�_�P����node���X
        if (isClicked)
        {
            target.transform.position = transform.position;
            overlap = target.GetComponent<Item>().isChongHe;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isMouseDown && !isClicked && other.gameObject.tag == "puzzle")
        {
            isClicked = true;
            target = GameObject.Find(other.gameObject.name);
            oldposition = other.transform.position;
            puzzlename = "pos_" + other.gameObject.name[4];
        }
    }
}
