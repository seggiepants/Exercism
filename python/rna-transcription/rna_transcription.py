def to_rna(dna_strand):
    # Use a dictionary to translate one nucleotide to another.
    transcribe = {
        'G': 'C',
        'C': 'G',
        'T': 'A',
        'A': 'U',
    }
    
    # Loop over the dna strand building the result.
    # Throw an exception if you find a value not in the transcribe dictionary.
    result = ''
    for ch in dna_strand:
        if ch in transcribe:
            result += transcribe[ch]
        else:
            raise Exception('Invalid nucleotide.')
    return result
