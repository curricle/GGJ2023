using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance;
    //Delegates
    public static Action onExitButtonPressed;
    public static Action onRestartButtonPressed;

    public static Action<bool> onResumeButtonPressed;

    //bools
    private bool isInteractIconActive = false;

    //ints
    public int waitTime = 5;

    //GameObjects
    public GameObject itemSprite;
    public GameObject itemPickupAlert;
    public GameObject winScreen;
    public GameObject interactIcon;

    public GameObject pauseMenu;
    public GameObject inventoryContainer;
    public GameObject inventorySlot;
    public GameObject dialogueParent;

    public List<GameObject> listOfInventorySlots;

    //TextMeshProUGUI
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI alertText;
    public TextMeshProUGUI nameTag;
    public TextMeshProUGUI dialogueText;

   

    private void Awake() {
        InitializeInventory();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable() {
        GameManager.onGameWon += DisplayWinScreen;
        GameManager.onMissingIngredient += DisplayAlert;

        InteractHandler.canInteract += DisplayInteractAlert;

        PlayerActions.onGamePaused += DisplayPauseMenu;

        InventoryHandler.onQueryInventory += UpdateInventorySlotDisplay;
        InventoryHandler.onItemAddedToInventory += DisplayAlert;

        ExchangeItem.onSendSelf += UpdateDialogueDisplay;

    }

    private void OnDisable() {
        GameManager.onGameWon -= DisplayWinScreen;
        GameManager.onMissingIngredient -= DisplayAlert;

        InteractHandler.canInteract -= DisplayInteractAlert;

        PlayerActions.onGamePaused -= DisplayPauseMenu;
        
        InventoryHandler.onQueryInventory -= UpdateInventorySlotDisplay;
        InventoryHandler.onItemAddedToInventory -= DisplayAlert;

        ExchangeItem.onSendSelf -= UpdateDialogueDisplay;
    }

    private void DisplayAlert(Item item) {

        if(item) {
            itemSprite.GetComponent<Image>().sprite = item.itemSprite;
            itemSprite.SetActive(true);
            alertText.text = $"{item.itemName} acquired!";
            itemPickupAlert.SetActive(true);
        }
        
        if(item == null) {
            itemSprite.SetActive(false);
            alertText.text = "Something's missing!";
        }
    }

    private void DisplayWinScreen(int score) {
        itemPickupAlert.SetActive(false);
        scoreText.text = $"Score: {score.ToString()}";
        winScreen.SetActive(true);
    }

    private void DisplayInteractAlert(GameObject location, bool isActive) {
        interactIcon.SetActive(isActive);
        StartCoroutine(CloseAlert(itemPickupAlert));
    }

    public void OnRestartButtonPresed() {
        DestroyInventorySlots();
        InitializeInventory();
        onRestartButtonPressed?.Invoke();
    }

    public void OnExitButtonPresed() {
        onExitButtonPressed?.Invoke();
    }

    private void DisplayPauseMenu(bool isPaused) {
        pauseMenu.SetActive(isPaused);
    }

    public void OnResumeButtonPressed(bool isPaused) {
        onResumeButtonPressed?.Invoke(isPaused);
    }

    public void InitializeInventory() {
        for(int i = 0; i < 6; i++) {
            inventorySlot = Instantiate(inventorySlot, inventoryContainer.GetComponent<Transform>());
            inventorySlot.GetComponent<InventorySlotDisplayHandler>().slotID = i;
            listOfInventorySlots.Add(inventorySlot);
        }
    }

    public void UpdateInventorySlotDisplay(Inventory inventory) {
        for(int i = 0; i < inventory.inventoryItems.Count; i++) {
            listOfInventorySlots[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.inventoryItems[i].itemSprite;
            listOfInventorySlots[i].transform.GetChild(0).gameObject.SetActive(true);          
        }
    }

    public void DestroyInventorySlots() {
        listOfInventorySlots.Clear();
    }

    private void UpdateDialogueDisplay(GameObject speaker) {
        var speakerScript = speaker.GetComponent<ExchangeItem>();
        nameTag.text = speakerScript.selfName;
        if(!speakerScript.hasExchanged) {
            dialogueText.text = speakerScript.selfDialogue[0];
        }
        else {
            dialogueText.text = speakerScript.selfDialogue[1];
        }
        
        dialogueParent.SetActive(true);
    }

    IEnumerator CloseAlert(GameObject alertBox) {
        yield return new WaitForSeconds(waitTime);

        alertBox.SetActive(false);
    }

}
