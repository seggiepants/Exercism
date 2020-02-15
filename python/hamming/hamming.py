def distance(strand_a, strand_b):
    if len(strand_a) == len(strand_b):
        return sum(map(lambda charA, charB: 1 if charA != charB else 0, strand_a.upper(), strand_b.upper()))
    else:
        raise ValueError('Cannot compare strands of differing lengths.')
