<?php

// Calculate ingredient requirements when making a number of pizzas for a number of guests.
class PizzaPi
{
    private int $slices_per_pizza = 8;
    private int $sauce_per_pizza_ml = 125;

    // Calculate the amount of dough to make (in grams) for the given count of pizzas and guests
    // @param $pizzas: the number of pizzas to make
    // @param $persons: the number of guests the pizzas should feed
    // @returns: Amount of dough to make in grams.
    public function calculateDoughRequirement(int $pizzas, int $persons)
    {
        return $pizzas * (($persons * 20) + 200);
    }

    // Calculate how many cans of Pizza Sauce to purchase for the given number of pizzas.
    // @param $pizzas: How many pizzas will be made.
    // @returns: The number of cans of sauce to buy (250ml each).
    public function calculateSauceRequirement(int $pizzas, int $sauce_can_ml)
    {
        return ($pizzas * $this->sauce_per_pizza_ml) / $sauce_can_ml;
    }

    // Calculate the number of cheese cubes required to top a pizza.
    // @param $cube_size: How big a cube of cheese is with a side length in centimeters
    // @param $thickness: How thick the pizza crust will be.
    // @param $diameter: The diameter of the pizza.
    // @returns: The number of cheese cubes to cover the pizza with.
    public function calculateCheeseCubeCoverage(int $cube_size, float $thickness, int $diameter)
    {
        return floor(($cube_size ** 3) / ($thickness * M_PI * $diameter));
    }

    // Calculate the number of left over slices if there are eight slices of pizza per pizza and
    // each guest takes the same number of slices (maximum where everyone gets the same)
    // @param $pizzas: How many pizzas.
    // @param $guests: How many guests there are eating pizza.
    // @returns: The number of left-over slices expected.
    public function calculateLeftOverSlices(int $pizzas, int $guests)
    {
        return ($this->slices_per_pizza * $pizzas) % $guests;
    }
}
