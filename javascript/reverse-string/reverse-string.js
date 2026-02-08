//
// This is only a SKELETON file for the 'Reverse String' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const reverseString = (value) => {
  // graphemeClusters splitting from:  
  // https://www.javaspring.net/blog/how-can-i-tell-if-a-string-contains-multibyte-characters-in-javascript/

  // Segment into grapheme clusters  
  const segmenter = new Intl.Segmenter('en', { granularity: 'grapheme' });
  const graphemeClusters = Array.from(segmenter.segment(value)).map(s => s.segment);
  
  return graphemeClusters.reverse().join('')
};
