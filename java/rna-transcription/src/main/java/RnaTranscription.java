class RnaTranscription {

    String transcribe(String dnaStrand) {
    	// I decided to try out the parallel processing stuff I saw other entries using previously.
    	return dnaStrand.codePoints()
			.parallel()
			.map(c -> MapNucleotide((char) c))
			.collect(StringBuilder::new, (sb, i) -> sb.append((char) i), StringBuilder::append).toString();
    }
    
    private char MapNucleotide(char c)
    {
		if ((c == 'G') || (c == 'g')) {
			return 'C';
		}
		else if ((c == 'C') || (c == 'c')) {
			return 'G';
		}
		else if ((c == 'T') || (c == 't')) {
			return 'A';
		}
		else if ((c == 'A') || (c == 'a')) {
			return 'U';        				
		}
		else {
			return c;
		}
	}

}
