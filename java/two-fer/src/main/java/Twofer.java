class Twofer {
    String twofer(String name) {
        // Put your code here.
    	if ((name == null) || (name.trim().length() == 0)) {
    		return "One for you, one for me.";
    	}
    	else {
    		return String.format("One for %s, one for me.", name);
    	}
    }
}
