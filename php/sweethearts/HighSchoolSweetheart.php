<?php

class HighSchoolSweetheart
{
    public function firstLetter(string $name): string
    {
        return mb_substr(trim($name), 0, 1);
    }

    public function initial(string $name): string
    {
        $initial = mb_strtoupper($this->firstLetter($name));
        return "$initial.";
    }

    public function initials(string $name): string
    {
        $parts = explode(' ', $name);
        for ($i = 0; $i < sizeof($parts); $i++) {
            $parts[$i] = $this->initial($parts[$i]);
        }
        return implode(" ", $parts);
    }

    public function pair(string $sweetheart_a, string $sweetheart_b): string
    {
        $pair_a = $this->initials($sweetheart_a);
        $pair_b = $this->initials($sweetheart_b);

        return <<<HEART
      ******       ******
    **      **   **      **
  **         ** **         **
 **            *            **
 **                         **
 **     $pair_a  +  $pair_b     **
  **                       **
    **                   **
      **               **
        **           **
          **       **
            **   **
              ***
               *
 HEART;
    }
}
