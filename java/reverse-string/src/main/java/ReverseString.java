class ReverseString {

    String reverse(String inputString) {
        StringBuilder ret = new StringBuilder();
        for(char c : inputString.toCharArray()) {
        	ret.insert(0, c);
        }
        return ret.toString();
    }
  
}