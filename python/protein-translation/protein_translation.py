"""
Calculate a list of amino acids for a given codon strand.
"""

codon_amino_acid = {
    'AUG': 'Methionine',
    'UUU': 'Phenylalanine',
    'UUC': 'Phenylalanine',
    'UUA': 'Leucine', 
    'UUG': 'Leucine',
    'UCU': 'Serine',
    'UCC': 'Serine',
    'UCA': 'Serine',
    'UCG': 'Serine',
    'UAU': 'Tyrosine',
    'UAC': 'Tyrosine',
    'UGU': 'Cysteine',
    'UGC': 'Cysteine',
    'UGG': 'Tryptophan',
    'UAA': '',
    'UAG': '',
    'UGA': '',
}

def proteins(strand):
    """
    For the given codon strand calculate the amino acids it produces.
    """
    ret = []

    for index in range(0, len(strand), 3):
        key = strand[index:index + 3]
        if key not in codon_amino_acid:
            break   # bad key = stop I decided
        amino_acid = codon_amino_acid[key]
        if len(amino_acid) == 0:
            break
        ret.append(amino_acid)

    return ret 
    
