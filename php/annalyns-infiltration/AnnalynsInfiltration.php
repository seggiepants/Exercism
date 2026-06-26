<?php
// Annalyns Infiltration exercise. Shows of booleans and boolean operations.

class AnnalynsInfiltration
{
    // Can we make a fast attack
    // @param $is_knight_awake: Is the knight awake?
    // @returns: True if we can fast attack
    public function canFastAttack(bool $is_knight_awake)
    {
        return !$is_knight_awake;
    }

    // Can we spy and get any useful information
    // @param $is_knight_awake: Is the knight awake?
    // @param $is_archer_awake: Is the archer awake?
    // @param $is_prisoner_awake: Is the prisoner awake?
    // @returns: True if there is someone to spy on.
    public function canSpy(
        bool $is_knight_awake,
        bool $is_archer_awake,
        bool $is_prisoner_awake
    ) {
        return $is_knight_awake || $is_archer_awake || $is_prisoner_awake;
    }

    // Can we signal the prisoner with a bird call?
    // @param $is_archer_awake: Is the archer awake?
    // @param $is_prisoner_awake: Is the prisoner awake?
    // @returns: True if it is safe to signal with a bird call.
    public function canSignal(
        bool $is_archer_awake,
        bool $is_prisoner_awake
    ) {
        return $is_prisoner_awake && !$is_archer_awake;
    }

    // Can we liberat the prisoner
    // @param $is_knight_awake: Is the knight awake?
    // @param $is_archer_awake: Is the archer awake?
    // @param $is_prisoner_awake: Is the prisoner awake?
    // @param $is_dog_present: Did the hero bring her dog?
    // @returns: True if we can free the prisoner.
    public function canLiberate(
        bool $is_knight_awake,
        bool $is_archer_awake,
        bool $is_prisoner_awake,
        bool $is_dog_present
    ) {
        return ($is_dog_present && !$is_archer_awake) || ($is_prisoner_awake && !$is_archer_awake && !$is_knight_awake);
    }
}
