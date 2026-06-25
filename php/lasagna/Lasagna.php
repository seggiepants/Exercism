<?php

class Lasagna
{
    private $cook_time = 40;

    public function expectedCookTime()
    {
        return $this->cook_time;
    }

    public function remainingCookTime($elapsed_minutes)
    {
        return $this->cook_time - $elapsed_minutes;
    }

    public function totalPreparationTime($layers_to_prep)
    {
        return $layers_to_prep * 2;
    }

    public function totalElapsedTime($layers_to_prep, $elapsed_minutes)
    {
        return $this->totalPreparationTime($layers_to_prep) + $elapsed_minutes;
    }

    public function alarm()
    {
        return 'Ding!';
    }
}
