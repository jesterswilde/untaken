using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour {

    static Dictionary<string, Recipe> _recipeDict = new Dictionary<string, Recipe>();
    static RecipeManager t;

    public static Recipe WhatAmICooking(List<Food> _ingredients)
    {
        _ingredients.Sort((_a, _b) => (int)_a - (int)_b);
        string _key = Recipe.CreateKey(_ingredients);
        Debug.Log(_key); 
        Recipe _recipe;
        if (_recipeDict.TryGetValue(_key, out _recipe)){
            return _recipe; 
        }
        return null; 
    }
    void Awake()
    {
        t = this; 
    }
	void Start()
    {
        List<Recipe> _recipes = new List<Recipe>(GetComponentsInChildren<Recipe>());
        _recipes.ForEach((_recipe) => _recipeDict[_recipe.Key] = _recipe); 
    }
}
