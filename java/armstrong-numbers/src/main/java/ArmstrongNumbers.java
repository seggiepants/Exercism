class ArmstrongNumbers {

	boolean isArmstrongNumber(int numberToCheck) {

		int digit;
		int power = 0;
		int check = numberToCheck;
		int armstrong = 0;
		power = Integer.toString(Math.abs(numberToCheck)).trim().length();
		while ((check != 0) && (armstrong < numberToCheck)) {
			digit = check % 10;
			check = (check - digit) / 10;
			armstrong = armstrong + (int) Math.pow((double) digit, (double) power);
		}
		return (numberToCheck == armstrong);
	}

}
