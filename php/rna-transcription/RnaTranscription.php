<?php
// RNA Transcription Exercise.

declare(strict_types=1);

$GLOBALS['Dna2Rna'] = array(
    "G" => "C",
    "C" => "G",
    "T" => "A",
    "A" => "U",
);

// For a given DNA nucleotide string return the equivalent RNA string.
// @param $dna: DNA nucleotide string to process.
// @returns: RNA complement to the DNA nucleotide string.
// @raises: Exception if something other than G, C, T, or A is in the DNA nucleotide string.
function toRna(string $dna): string
{
    global $Dna2Rna;
    return strtr($dna, $Dna2Rna);
}
