using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;
using JsonData;

public class RestaurantDialogflow : DialogflowAPIScript
{
    private GameObject foodItem;
  	private GameObject sideItem;
  	private GameObject drinkItem;
  	private Material drinkMat;

    public override void ProcessResult(string result) {
        ResponseBody content = (ResponseBody)JsonUtility.FromJson<ResponseBody>(result);
        Debug.Log(content.queryResult.fulfillmentText);
        if (content.queryResult.allRequiredParamsPresent) {
            UpdateOrder(result);
        }
        if (content.queryResult.action == "Order.Order-yes") {
            ResolveOrder();
        }
    }

    public void UpdateOrder(string json) {
  			int outputContextsIndex = json.IndexOf("outputContexts");
  			if (outputContextsIndex == -1) {
  					return;
  			}
  			int parametersIndex = json.IndexOf("parameters", outputContextsIndex);
  			int openBracketIndex = json.IndexOf("{", parametersIndex);
  			int closedBracketIndex = json.IndexOf("}", openBracketIndex);
  			json = json.Substring(openBracketIndex, closedBracketIndex - openBracketIndex + 1);
  			OrderParameters orderParams = (OrderParameters)JsonUtility.FromJson<OrderParameters>(json);
  			string food = orderParams.Food;
  			string side = orderParams.Side;
  			string drink = orderParams.Drink;
  			string drinkSize = orderParams.DrinkSize;

  			if (food == null || side == null || drink == null || drinkSize == null) {
  					return;
  			}

  			FoodPrefabs foodPrefabs = GameObject.FindWithTag("FoodPrefabs").GetComponent<FoodPrefabs>();

  			switch (food)
  			{
  					case "Cheeseburger":
  							foodItem = foodPrefabs.cheeseburger;
  							break;
  					case "Grilled Cheese Sandwich":
  							foodItem = foodPrefabs.grilledCheese;
  							break;
  					case "Chicken Nuggets":
  							foodItem = foodPrefabs.chickenNuggets;
  							break;
  					case "Chicken Tenders":
  							foodItem = foodPrefabs.chickenTenders;
  							break;
  					case "Chicken Sandwich":
  							foodItem = foodPrefabs.chickenSandwich;
  							break;
  					case "Hamburger":
  							foodItem = foodPrefabs.hamburger;
  							break;
  					case "Chicken Wrap":
  							foodItem = foodPrefabs.chickenWrap;
  							break;
  					case "Ham and Cheese Sandwich":
  							foodItem = foodPrefabs.hamAndCheese;
  							break;
  					default:
  							foodItem = foodPrefabs.cheeseburger;
  							Debug.Log("Failed to match food item to prefab.");
  							break;
  			}

  			switch (side)
  			{
  					case "Fries":
  							sideItem = foodPrefabs.frenchFries;
  							break;
  					case "Yogurt":
  							sideItem = foodPrefabs.yogurt;
  							break;
  					case "Salad":
  							sideItem = foodPrefabs.salad;
  							break;
  					default:
  							sideItem = foodPrefabs.frenchFries;
  							Debug.Log("Failed to match side item to prefab.");
  							break;
  			}

  			switch (drink)
  			{
  					case "Coke":
  							drinkMat = foodPrefabs.coke;
  							break;
  					case "Sprite":
  							drinkMat = foodPrefabs.sprite;
  							break;
  					case "Water":
  							drinkMat = foodPrefabs.water;
  							break;
  					case "Orange Juice":
  							drinkMat = foodPrefabs.orangeJuice;
  							break;
  					case "Lemonade":
  							drinkMat = foodPrefabs.lemonade;
  							break;
  					case "Apple Juice":
  							drinkMat = foodPrefabs.appleJuice;
  							break;
  					case "Milk":
  							drinkMat = foodPrefabs.milk;
  							break;
  					case "Chocolate Milk":
  							drinkMat = foodPrefabs.chocolateMilk;
  							break;
  					default:
  							drinkMat = foodPrefabs.water;
  							Debug.Log("Failed to match drink item to material.");
  							break;
  			}

  			switch (drinkSize)
  			{
  					case "Small":
  							drinkItem = foodPrefabs.smallCup;
  							break;
  					case "Medium":
  							drinkItem = foodPrefabs.mediumCup;
  							break;
  					case "Large":
  							drinkItem = foodPrefabs.largeCup;
  							break;
  					default:
  							drinkItem = foodPrefabs.smallCup;
  							Debug.Log("Failed to match drink size to prefab.");
  							break;
  			}
  	}

  	public void ResolveOrder() {
  			Transform foodSpawn = GameObject.FindWithTag("FoodSpawn").transform;
  			Transform sideSpawn = GameObject.FindWithTag("SideSpawn").transform;
  			Transform drinkSpawn = GameObject.FindWithTag("DrinkSpawn").transform;

  			Instantiate(foodItem, foodSpawn.position, Quaternion.Euler(-90f,0f,0f));
  			Instantiate(sideItem, sideSpawn.position, Quaternion.Euler(-90f,0f,0f));
  			GameObject drinkGameObject = Instantiate(drinkItem, drinkSpawn.position, Quaternion.Euler(-90f,0f,0f));
  			drinkGameObject.GetComponent<Cup>().liquidMesh.material = drinkMat;
  	}
}
