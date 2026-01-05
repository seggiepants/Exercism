/// <reference path="./global.d.ts" />
// @ts-check


/**
 * Implement the functions needed to solve the exercise here.
 * Do not forget to export them so they are available for the
 * tests. Here an example of the syntax as reminder:
 * 
 * export function yourFunction(...) {
 *   ...
 * }
 */

/**
 * Check on status of lasagna
 * @param {Number} minutesRemaining 
 * @returns status as string
 */
export function cookingStatus(minutesRemaining)
{
    if (minutesRemaining == undefined || minutesRemaining == null || Number.isNaN(minutesRemaining))
        return 'You forgot to set the timer.'

    if (minutesRemaining <= 0)
        return 'Lasagna is done.'

    return 'Not done, please wait.'
}

/**
 * Calculate lasagna preparation time based on time per layer and given set of layers
 * @param {*} layers list of layers in the lasagna
 * @param {Number} perLayerMinutes how many minutes each layer takes to prepare
 * @returns {Number} preparation time in minutes.
 */
export function preparationTime(layers, perLayerMinutes = 2)
{
    return layers.length * perLayerMinutes
}

/**
 * Calculate the quantities of noodles and sauce required for a given lasagna layer set.
 * @param {*} ingredients a list of ingredients one per layer
 * @returns object with keys noodles and sauce filled with required noodle and sauce in grams and liters respectively
 */
export function quantities(ingredients)
{
    const noodleIncrement = 50;
    const sauceIncrement = 0.2;
    let noodleGrams = 0
    let sauceLiters = 0;
    for(let i = 0; i < ingredients.length; i++)
    {
        if (ingredients[i] == 'noodles')
            noodleGrams += noodleIncrement
        else if (ingredients[i] == 'sauce')
            sauceLiters += sauceIncrement;
    }
    return { noodles: noodleGrams, sauce: sauceLiters}
}


/**
 * Update my list of lasagna to include friends secret ingredient.
 * @param {Array<string>} friendsList friends list of lasagna ingredient last is the secret ingredient.
 * @param {Array<string>} myList my list of lasagna ingredients
 * @returns nothing
 */
export function addSecretIngredient(friendsList, myList)
{
    myList.push(friendsList.slice(-1)[0])
}

/**
 * 
 * @param {Object} recipe object where keys are ingredients and values are the amount of ingredient.
 * @param {Number} servings how many servings we want to make
 * @returns {Object} object with the same recipe but servings scaled up to desired amount.
 */
export function scaleRecipe(recipe, servings)
{
    let ret = new Object()
    let scaleFactor = servings / 2.0

    for(let key in recipe)
    {
        if (recipe.hasOwnProperty(key))
            ret[key] = recipe[key] * scaleFactor
    }

    return ret
}