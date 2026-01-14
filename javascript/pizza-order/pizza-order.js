/// <reference path="./global.d.ts" />
//
// @ts-check

/**
 * Determine the price of the pizza given the pizza and optional extras
 *
 * @param {Pizza} pizza name of the pizza to be made
 * @param {Extra[]} extras list of extras
 *
 * @returns {number} the price of the pizza
 */
export function pizzaPrice(pizza, ...extras) {
  if (extras.length > 0)
  {
    let extra = extras[0]
    let price = 0
    if (extra == 'ExtraSauce')
      price = 1
    else if (extra == 'ExtraToppings')
      price = 2

    return price + pizzaPrice(pizza, ...extras.slice(1))
  }
  else
  {
    switch(pizza)
    {
      case 'Margherita':
        return 7
      case 'Caprese':
        return 9
      case 'Formaggio':
        return 10
    }
  }
  return 0
}

/**
 * Calculate the price of the total order, given individual orders
 *
 * (HINT: For this exercise, you can take a look at the supplied "global.d.ts" file
 * for a more info about the type definitions used)
 *
 * @param {PizzaOrder[]} pizzaOrders a list of pizza orders
 * @returns {number} the price of the total order
 */
export function orderPrice(pizzaOrders) {
  return pizzaOrders.reduce((total, pizza) => { return total + pizzaPrice(pizza.pizza, ...pizza.extras)}, 0)
}
