using Xunit.Runner.Common;

public static class ProteinTranslation
{
    /*
        Codon|Amino Acid
        --|--
        AUG|Methionine
        UUU, UUC|Phenylalanine
        UUA, UUG|Leucine
        UCU, UCC, UCA, UCG|Serine
        UAU, UAC|Tyrosine
        UGU, UGC|Cysteine
        UGG|Tryptophan
        UAA, UAG, UGA|STOP
    */
    /// <summary>
    /// Lookup table to transform a codon to an amino acid. Several codons make the same
    /// amino acid so duplicate are ok.
    /// 
    /// Codon|Amino Acid
    /// --|--
    /// AUG|Methionine
    /// UUU, UUC|Phenylalanine
    /// UUA, UUG|Leucine
    /// UCU, UCC, UCA, UCG|Serine
    /// UAU, UAC|Tyrosine
    /// UGU, UGC|Cysteine
    /// UGG|Tryptophan
    /// UAA, UAG, UGA|STOP
    ///
    /// </summary>
    static Dictionary<string, string> codon2AminoAcid = new Dictionary<string, string>()
    {
        ["AUG"] = "Methionine",
        ["UUU"] = "Phenylalanine",
        ["UUC"] = "Phenylalanine",
        ["UUA"] = "Leucine",
        ["UUG"] = "Leucine",
        ["UCU"] = "Serine",
        ["UCC"] = "Serine",
        ["UCA"] = "Serine",
        ["UCG"] = "Serine",
        ["UAU"] = "Tyrosine",
        ["UAC"] = "Tyrosine",
        ["UGU"] = "Cysteine",
        ["UGC"] = "Cysteine",
        ["UGG"] = "Tryptophan",
        ["UAA"] = "STOP",
        ["UAG"] = "STOP",
        ["UGA"] = "STOP",
    };

    /// <summary>
    /// Generate a list of proteins given parameter which contains a list of codons. Stop if a STOP codon is found.
    /// </summary>
    /// <param name="strand">A string of three character codons to be processed to return a list of amino acid</param>
    /// <returns>A string array with one entry per amino acid found</returns>
    /// <exception cref="ArgumentException">Thrown when an unknown codon is found.</exception>
    public static string[] Proteins(string strand)
    {
        List<string> results = new();
        foreach (char[] chunk in strand.Chunk(3))
        {
            string codon = String.Join("", chunk);
            bool success = codon2AminoAcid.TryGetValue(codon, out string? aminoAcid);
            if (success && aminoAcid != null)
            {
                if (aminoAcid.Equals("STOP"))
                    break;
                results.Add(aminoAcid);
            }
            else
            {
                throw new ArgumentException($"Unknown Codon: \"{codon}\".");
            }
        }
        return results.ToArray<string>();
    }
}