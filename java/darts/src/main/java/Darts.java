class Darts {

	double x;
	double y;
    Darts(double x, double y) {
    	this.x = x;
    	this.y = y;
    }

    int score() {
    	double distance = Math.sqrt(this.x * this.x + this.y * this.y);
    	if (distance <= 1.0) {
    		return 10;
    	}
    	else if (distance <= 5.0) { 
    		return 5;
    	}
    	else if (distance <= 10.0) {
    		return 1;
    	}
    	else {
    		return 0;
    	}
    }

}
