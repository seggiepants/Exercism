//
// This is only a SKELETON file for the 'Protein Translation' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

const CodonToAminoAcid = {  
  AUG: 'Methionine',
  UUU: 'Phenylalanine',
  UUC: 'Phenylalanine',
  UUA: 'Leucine', 
  UUG: 'Leucine',
  UCU: 'Serine',
  UCC: 'Serine',
  UCA: 'Serine',
  UCG: 'Serine',
  UAU: 'Tyrosine',
  UAC: 'Tyrosine',
  UGU: 'Cysteine',
  UGC: 'Cysteine',
  UGG: 'Tryptophan',
  UAA: 'STOP',
  UAG: 'STOP',
  UGA: 'STOP'
}

export const translate = (codons = '') => {
  let ret = []
  for(let i = 0; i < codons.length; i+= 3)
  {
    let key = codons.slice(i, i + 3)
    if (key in CodonToAminoAcid)
    {
      let value = CodonToAminoAcid[key]
      if (value === 'STOP')
        break;
      ret.push(value)
    }
    else
    {
      throw new Error('Invalid codon')
    }
  }
  return ret
};
