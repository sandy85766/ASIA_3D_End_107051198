using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("NPC 資料")]
    public NPCdata data;
    [Header("對話框")]
    public GameObject dialog;
    [Header("對話內容")]
    public Text textContent;
    [Header("對話者名稱")]
    public Text textName;
    [Header("對話間隔")]
    public float interval = 0.2f;

    // <summary>
    // 玩家是否進入感應區
    // <summary>
    public bool playerInArea;

    // 定義列舉 eumn ( 下拉式選單 - 只能選一個 )
    public enum NPCState
    {
        FirstDialog, Missioning, Finish
    }
    public NPCState state = NPCState.FirstDialog;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "格瑞")
        {
            playerInArea = true;
            StartCoroutine(Dialog());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "格瑞")
        {
            playerInArea = false;
            StopDialog();
        }
    }

    // <summary>
    // 停止對話
    // <summary>
    private void StopDialog()
    {
        dialog.SetActive(false);   // 關閉對話框
        StopAllCoroutines();       // 停止所有協程
    }


    private IEnumerator Dialog()
    {
       dialog.SetActive(true);
        // 清空文字
        textContent.text = "";

        textName.text = name;

        string dialogString = data.dialogA;

        switch (state)
        {
            case NPCState.FirstDialog:
                dialogString = data.dialogA;
                break;
            case NPCState.Missioning:
                dialogString = data.dialogB;
                break;
            case NPCState.Finish:
                dialogString = data.dialogC;
                break;
            default:
                break;
        }

        //字串的長度 dialougA.Length
        for (int i = 0; i < dialogString.Length; i++)
        {
            // print(data.dialougA[i]);
            // 文字 串聯
            textContent.text += dialogString[i] + "";
            yield return new WaitForSeconds(interval);
        }
    }
}
