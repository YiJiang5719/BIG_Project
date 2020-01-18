using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 10f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    //用于调取DialogueManger的功能
    public Dialogue dialogue;

    public virtual void Interact()
    {
        //This method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }
    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);

            if (distance <= radius)
            {
                Interact();
                //找到DialogueManager之后启用对话框
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                hasInteracted = true;
            }
            else
            {
                Debug.Log("Hard to Reach");
                hasInteracted = true;
            }
        }
    }
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;

    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
