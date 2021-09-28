using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public CarDataObject[] carData;
    public TextMeshProUGUI numberText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Image image;
    public TextMeshProUGUI MPS;
    public TextMeshProUGUI DPS;
    public TextMeshProUGUI buyPrice;
    public TextMeshProUGUI upgradePrice;
    public Button buyButton;
    public Button upgradeButton;

    private int numberOfCar;
    private int currentNumber;
    private CarDataObject currentCar;

    private int maxLevelOfCar;
    private int currentProgress;

    public Image fullWidth;
    public Image fireSpeedProgress;
    public Image earningProgress;
    public Image damageProgress;
    public Sprite lockImage;

    private float originalWidth;

    public SaveDataObject data;

    public void Quit()
    {
        if (currentNumber > currentProgress)
        {
            data.currentNumber = currentProgress;
        }
        else
        {
            data.currentNumber = currentNumber;
        }
        data.currentProgress = currentProgress;
    }

    private void Awake()
    {
        originalWidth = fullWidth.rectTransform.rect.width;
        maxLevelOfCar = 10; //currentProgress = 0;

        //data taken from save
        currentProgress = data.currentProgress;
        currentNumber = data.currentNumber;
        // 
        currentCar = carData[currentNumber];
        numberOfCar = carData.Length;

        buyButton.onClick.AddListener(() => buyCar());
        upgradeButton.onClick.AddListener(() => upgradeCar());

        gameObject.SetActive(false);
    }

    public void upgradeCar()
    {
        if (currentNumber > currentProgress)
            return;
        if (currentCar.Level >= maxLevelOfCar)
            return;
        int price = getCurrentPrice();
        int wallet = GameUI.UISystem.moneyText.GetComponent<MoneyText>().getMoney();
        if (wallet < price)
        {
            GameUI.UISystem.noMoneyText.GetComponent<NoMoneyText>().setText("Not enough money!");
            GameUI.UISystem.noMoneyText.GetComponent<NoMoneyText>().Activate();
            return;
        }
        GameUI.UISystem.moneyText.GetComponent<MoneyText>().minusMoney(price);
        currentCar.damagePS++;
        GameEventSystem.eventSystem.UpgradeDPS(currentNumber);
        currentCar.moneyPS++;
        GameEventSystem.eventSystem.UpgradeMPS(currentNumber, 1);
        currentCar.Level++;
        currentCar.upgradePrice = currentCar.upgradePrice * 2;
        SetData();
    }

    public int getDPS(int bullet)
    {
        if (bullet >= 0 && bullet < numberOfCar)
        {
            int dm = (int)carData[bullet].damagePS;
            return dm;
        }
        return 0;
    }

    public void buyCar()
    {
        if (currentNumber > currentProgress)
            return;
        bool result = GameUI.UISystem.Purchase.GetComponent<Purchase>().Buy();
        if (result)
        {
            currentCar.buyPrice = currentCar.buyPrice * 1.2f;
            SetData();
        }
    }

    public int getMPS(GameObject car)
    {
        for (int i = 0; i < carData.Length; i++)
        {
            if (car.GetComponent<SpriteRenderer>().sprite.name == carData[i].Image.name)
                return (int)carData[i].moneyPS;
        }
        return -1;
    }

    public int getCurrentPrice()
    {
        if (currentCar == null)
            return 0;
        if (currentNumber <= currentProgress)
        {
            return (int)currentCar.buyPrice;
        }
        else
        {
            return (int)carData[currentProgress].buyPrice;
        }
    }

    public int getCurrent()
    {
        if (currentNumber <= currentProgress)
        {
            return currentNumber;
        }
        else
        {
            return currentProgress;
        }
    }

    public void SetData()
    {
        if (currentNumber <= currentProgress)
        {
            numberText.text = (currentNumber + 1) + "/" + numberOfCar;
            nameText.text = currentCar.Name;
            levelText.text = "Level " + currentCar.Level;
            image.sprite = currentCar.Image;
            MPS.text = currentCar.moneyPS + "/sec";
            DPS.text = currentCar.damagePS + "/hit";
            buyPrice.text = currentCar.buyPrice.ToString();
            if (currentCar.Level >= maxLevelOfCar)
            {
                upgradePrice.text = "MAXED";
            }
            else
            {
                upgradePrice.text = currentCar.upgradePrice.ToString();
            }
            buyButton.interactable = true; upgradeButton.interactable = true;
            SetProgress();
            GameUI.UISystem.Purchase.GetComponent<Purchase>().SetSprite(currentCar.Image);
            GameUI.UISystem.priceText.GetComponent<TextMeshProUGUI>().text = "$" + currentCar.buyPrice;
        }
        else
        {
            numberText.text = (currentNumber + 1) + "/" + numberOfCar;
            nameText.text = "???";
            levelText.text = "???";
            image.sprite = lockImage;
            MPS.text = "???";
            DPS.text = "???";
            buyPrice.text = "???";
            upgradePrice.text = "???";
            buyButton.interactable = false; upgradeButton.interactable = false;
            SetProgressToZero();
        }
    }

    public void SwipeData(bool right)
    {
        if (right) //right button
        {
            if (currentNumber == numberOfCar - 1)
                return;
            currentNumber = currentNumber + 1;
            currentCar = carData[currentNumber];
            SetData();
        }
        else
        {
            if (currentNumber == 0)
                return;
            currentNumber = currentNumber - 1;
            currentCar = carData[currentNumber];
            SetData();
        }
        //if (currentNumber <= currentProgress)
        //{
        //    GameUI.UISystem.Purchase.GetComponent<Purchase>().SetSprite(currentCar.Image);
        //    GameUI.UISystem.priceText.GetComponent<TextMeshProUGUI>().text = "$" + currentCar.buyPrice;
        //}
    }

    public void SetProgress()
    {
        float temp = (float)currentCar.Level / (float)maxLevelOfCar;
        fireSpeedProgress.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalWidth * temp);
        earningProgress.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalWidth * temp);
        damageProgress.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalWidth * temp);
    }

    public void SetProgressToZero()
    {
        fireSpeedProgress.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        earningProgress.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        damageProgress.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
    }

    public void SetMaxCarProgress(int prg)
    {
        if (prg < 0 && prg >= numberOfCar)
            return;
        if (prg > currentProgress)
        {
            currentProgress = prg;
        }
    }
}
