class Lasagna
{
    // TODO: define the 'ExpectedMinutesInOven()' method
    public int ExpectedMinutesInOven() {
        const int COOK_TIME = 40;
        return COOK_TIME;
    }

    // TODO: define the 'RemainingMinutesInOven()' method
    public int RemainingMinutesInOven(int minutes) {
        return ExpectedMinutesInOven() - minutes;
    }

    // TODO: define the 'PreparationTimeInMinutes()' method
    public int PreparationTimeInMinutes(int layers) {
        const int TIME_PER_LAYER = 2;
        return layers * TIME_PER_LAYER;
    }

    // TODO: define the 'ElapsedTimeInMinutes()' method
    public int ElapsedTimeInMinutes(int layers, int minutes) {
        return PreparationTimeInMinutes(layers) + minutes;
    }
    
}