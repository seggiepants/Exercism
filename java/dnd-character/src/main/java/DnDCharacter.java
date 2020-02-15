class DnDCharacter {
	
	int strength;
	int dexterity;
	int constitution;
	int intelligence;
	int wisdom;
	int charisma;
	int hitpoints;
	
	/**
	 * Create a DnD style Character layout.
	 */
	DnDCharacter() {
		strength = ability();
		dexterity = ability();
		constitution = ability();
		intelligence = ability();
		wisdom = ability();
		charisma = ability();
		hitpoints = 10 + modifier(constitution);
	}
	
	/**
	 * Calculate a random ability.
	 * Virtually throw four die, then sum up the values excluding the roll with the smallest value.
	 * @return New random ability score.
	 */
	int ability() {
		int sum = 0;
		int minRoll = 7;
		
		for(int i = 0; i < 4; i++)
		{
			int roll = (int) (Math.random() * 6 + 1);
			if (roll < minRoll) {
				minRoll = roll;
			}
			sum = sum + roll;
		}
		sum = sum - minRoll;
		
		return sum;
	}

	/**
	 * Calculate Constitution modifier. Left open enough to use on any ability. Simply subtracts 10 then floor divides by 2.
	 * @param input - ability to calculate a modifier for.
	 * @return new Constitution (ability) modifier.
	 */
    int modifier(int input) {
        return Math.floorDiv((input - 10),  2);
    }
    
    // Ability getters and setters.
    
    int getStrength() {
    	return strength;
    }
    
    void setStrength(int value) {
    	strength = value;
    }
    
    int getDexterity() {
    	return dexterity;
    }
    
    void setDexterity(int value) {
    	dexterity = value;
    }
    
    int getConstitution() {
    	return constitution;
    }
    
    void setConstitution(int value) {
    	constitution = value;
    }
    
    int getIntelligence() {
    	return intelligence;
    }
    
    void setIntelligence(int value) {
    	intelligence = value;
    }
    
    int getWisdom() {
    	return wisdom;
    }
    
    void setWisdom(int value) {
    	wisdom = value;
    }
    
    int getCharisma() {
    	return charisma;
    }
    
    void setCharisma(int value) {
    	charisma = value;
    }
    
    int getHitpoints() {
    	return hitpoints;
    }
    
    void setHitpoints(int value) { 
    	hitpoints = value;
    }
}