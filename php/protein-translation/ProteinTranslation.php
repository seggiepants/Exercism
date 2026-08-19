<?php
// Protein Translation Exercise

declare(strict_types=1);

$GLOBALS['codonAminoAcid'] = array(
    "AUG" => "Methionine",
    "UUU" => "Phenylalanine",
    "UUC" => "Phenylalanine",
    "UUA" => "Leucine",
    "UUG" => "Leucine",
    "UCU" => "Serine",
    "UCC" => "Serine",
    "UCA" => "Serine",
    "UCG" => "Serine",
    "UAU" => "Tyrosine",
    "UAC" => "Tyrosine",
    "UGU" => "Cysteine",
    "UGC" => "Cysteine",
    "UGG" => "Tryptophan",
    "UAA" => "STOP",
    "UAG" => "STOP",
    "UGA" => "STOP",
);

class ProteinTranslation
{
    // From a given string of codon return an array of the amino acids produced. Stopping if you reach a stop codon.
    // @param $rna: codon list every three characters is a single codon. No spaces all one big string.
    // @returns: Array of Amino Acids that would be produced by the rna sequence.
    public function getProteins($rna)
    {
        global $codonAminoAcid;
        $result = array();        

        // If length is not divisible by three the last codon will be 
        // too short to be in the lookup array and you will get the 
        // Invalid argument exception.
        foreach(str_split($rna, 3) as $codon) {
            if (array_key_exists($codon, $codonAminoAcid)) {
                $aminoAcid = $codonAminoAcid[$codon];
                if ($aminoAcid == "STOP") {
                    break;
                }
                $result[] = $aminoAcid;
            } else {
                throw new InvalidArgumentException("Invalid codon: " . $codon);
            }
        }

        return $result;
    }
}
