<?php
// Two Bucket Exercise
// I did base it on my GO version.

declare(strict_types=1);

// Class to hold the results of a solve call.
class Solution {
    public int $numberOfActions;
    public string $nameOfBucketWithDesiredLiters;
    public int $litersLeftInOtherBucket;

    public function __construct($numberOfActions, $nameOfBucketWithDesiredLiters, $litersLeftInOtherBucket) {
        $this->numberOfActions = $numberOfActions;
        $this->nameOfBucketWithDesiredLiters = $nameOfBucketWithDesiredLiters;
        $this->litersLeftInOtherBucket = $litersLeftInOtherBucket;
    }
}

// Two Bucket Exercise class wrapper.
class TwoBucket
{
    // Find an optimal way to get a desired goal value with two buckets of a given size
    // @param int $sizeBucketOne: The size of the first bucket
    // @param int $sizeBucketTwo: The size of the second bucket
    // @param int $goal: The amount we are trying to measure
    // @param string $startBucket: Start with this bucket filled ('one', or 'two')
    // @returns: The goal bucket, number of moves, and the other bucket as a solution object.
    // @raises: InvalidArgumentException if the goal is nonsensicle or a path to it could not be found.
    public function solve(int $sizeBucketOne, int $sizeBucketTwo, int $goal, string $startBucket) : Solution
    {
        if ($goal > max($sizeBucketOne, $sizeBucketTwo)) {
		    throw new InvalidArgumentException("Invalid goal - larger than maximum possible.");
        }
        if ($sizeBucketOne <= 0 || $sizeBucketTwo <= 0) {
            throw new InvalidArgumentException("Invalid bucket size.");
        }
        if ($goal <= 0) {
            throw new InvalidArgumentException("Invalid goal amount");
        }

        $bucketOneAmt = 0;
        $bucketTwoAmt = 0;
        switch ($startBucket) {
        case "one":
            $bucketOneAmt = $sizeBucketOne;
            break;
        case "two":
            $bucketTwoAmt = $sizeBucketTwo;
            break;
        default:
            throw new InvalidArgumentException("Invalid start bucket name");
        }
        $moveStack = array();
        $moveStack[] = [$bucketOneAmt, $bucketTwoAmt];

	    $result = $this->step($moveStack, $bucketOneAmt, $bucketTwoAmt, $startBucket, $sizeBucketOne, $sizeBucketTwo, $goal);
        if ($result->numberOfActions < 0) {
            throw new Exception("No solution found.");
        }
        return $result;
    }

    // Solve the two bucket problem recursively. Try every possible move then reevaluate from that state
    // until you hit the goal. You can have multiple ways to the goal so return one with the shortest sequence
    // @param array $moveStack: Slice of Bucket amount pairs. No use reevaluating a previous state
    // @param int $bucketOneAmt: How many liters bucket one currently contains
    // @param int $bucketTwoAmt: How many liters bucket two currently contains
    // @param int $originalBucket: What bucket did we start on for the illegal move checks
    // @param int $bucketOneMax: How many liters can bucket one hold
    // @param int $bucketTwoMax: How many liters can bucket two hold
    // @param int $goal: The number of liters we want to get to in one of the given buckets.
    // @returns: Solution class containing the bucket that ended up with the goal amount, number of moves,
    // and how much was in the remaining bucket returned as an object expected by the test.
    // On error moves will be -1
    // and goal_bucket will be n/a also other bucket will be 0.
    private function step($moveStack, $bucketOneAmt, $bucketTwoAmt, $originalBucket, $bucketOneMax, $bucketTwoMax, $goal) : Solution {
        // Base state, return on goal
        if ($bucketOneAmt == $goal) {
            return new Solution(count($moveStack), "one", $bucketTwoAmt);
        }

        if ($bucketTwoAmt == $goal) {
            return new Solution(count($moveStack), "two", $bucketOneAmt);
        }

        // Three possible actions:
        // - fill one or two
        // - pour one into two or two into one until other full or current empty
        // - empty one or two
        // Move is invalid if it leaves original_bucket = empty and other_bucket = filled
        $results = array();

        // Fill 1
        $illegalFill = $originalBucket == "two" && $bucketTwoAmt == 0;

        $wasVisited = array_find_key($moveStack, function($pair) use ($bucketOneMax, $bucketTwoAmt) {
            return $pair[0] == $bucketOneMax && $pair[1] == $bucketTwoAmt;
        }) !== null;

        if (!($illegalFill || $wasVisited || $bucketOneAmt == $bucketOneMax)) {
            $nextStack = $moveStack;
            $nextStack[] = array($bucketOneMax, $bucketTwoAmt);
            $result = $this->step($nextStack,
                $bucketOneMax,
                $bucketTwoAmt,
                $originalBucket,
                $bucketOneMax,
                $bucketTwoMax,
                $goal);
            if ($result->numberOfActions >= 0) {
                $results[] = $result;
            }
        }

        // Fill 2
        $illegalFill = $originalBucket == "one" && $bucketOneAmt == 0;
        $wasVisited = array_find_key($moveStack, function($pair) use($bucketOneAmt, $bucketTwoMax) {
            return $pair[0] == $bucketOneAmt && $pair[1] == $bucketTwoMax;
        }) !== null;
        if (!($illegalFill || $wasVisited || $bucketTwoAmt == $bucketTwoMax)) {
            $nextStack = $moveStack;
            $nextStack[] = array($bucketOneAmt, $bucketTwoMax);
            $result = $this->step($nextStack,
                $bucketOneAmt,
                $bucketTwoMax,
                $originalBucket,
                $bucketOneMax,
                $bucketTwoMax,
                $goal);
            if ($result->numberOfActions >= 0) {
                $results[] = $result;
            }
        }

        // Pour 1 to 2
        $amountToPour = min($bucketOneAmt, $bucketTwoMax - $bucketTwoAmt);
        $illegalPour = $originalBucket == "one" && $bucketOneAmt - $amountToPour == 0 && $bucketTwoAmt + $amountToPour == $bucketTwoMax;
        $wasVisited = array_find_key($moveStack, function($pair) use ($bucketOneAmt, $amountToPour, $bucketTwoAmt) {
            return $pair[0] == $bucketOneAmt - $amountToPour && $pair[1] == $bucketTwoAmt + $amountToPour;
        }) !== null;

        if (!($illegalPour || $wasVisited || $amountToPour <= 0)) {
            $nextStack = $moveStack;
            $nextStack[] = array($bucketOneAmt - $amountToPour, $bucketTwoAmt + $amountToPour);            
            $result = $this->step($nextStack, 
                $bucketOneAmt - $amountToPour,
                $bucketTwoAmt + $amountToPour,
                $originalBucket,
                $bucketOneMax,
                $bucketTwoMax,
                $goal);
            if ($result->numberOfActions >= 0) {
                $results[] = $result;
            }
        }

        // Pour 2 to 1
        $amountToPour = min($bucketTwoAmt, $bucketOneMax - $bucketOneAmt);
        $illegalPour = $originalBucket == "two" && $bucketTwoAmt - $amountToPour == 0 && $bucketOneAmt + $amountToPour == $bucketOneMax;
        $wasVisited = array_find_key($moveStack, function($pair) use ($bucketOneAmt, $amountToPour, $bucketTwoAmt) {
            return $pair[0] == $bucketOneAmt + $amountToPour && $pair[1] == $bucketTwoAmt - $amountToPour;
        }) !== null;

        if (!($illegalPour || $wasVisited || $amountToPour <= 0)) {
            $nextStack = $moveStack;
            $nextStack[] = array($bucketOneAmt + $amountToPour, $bucketTwoAmt - $amountToPour);
            $result = $this->step($nextStack,
                $bucketOneAmt + $amountToPour,
                $bucketTwoAmt - $amountToPour,
                $originalBucket,
                $bucketOneMax,
                $bucketTwoMax,
                $goal);
            if ($result->numberOfActions >= 0) {
                $results[] = $result;
            }
        }

        // Empty 1
        $illegalEmpty = ($originalBucket == "one" && $bucketTwoAmt == $bucketTwoMax);
        $wasVisited = array_find_key($moveStack, function($pair) use ($bucketTwoAmt) {
            return $pair[0] == 0 && $pair[1] == $bucketTwoAmt;
        }) !== null;

        if (!($illegalEmpty || $wasVisited || $bucketOneAmt == 0)) {
            $nextStack = $moveStack;
            $nextStack[] = array(0, $bucketTwoAmt);
            $result = $this->step($nextStack,
                0,
                $bucketTwoAmt,
                $originalBucket,
                $bucketOneMax,
                $bucketTwoMax,
                $goal);
            if ($result->numberOfActions >= 0) {
                $results[] = $result;
            }
        }

        // Empty 2
        $illegalEmpty = $originalBucket == "two" && $bucketOneAmt == $bucketOneMax;
        $wasVisited = array_find_key($moveStack, function($pair) use($bucketOneAmt) {
            return $pair[0] == $bucketOneAmt && $pair[1] == 0;
        }) !== null;

        if (!($illegalEmpty || $wasVisited || $bucketTwoAmt == 0)) {
            $nextStack = $moveStack;
            $nextStack[] = array($bucketOneAmt, 0);
            $result = $this->step($nextStack,
                $bucketOneAmt,
                0,
                $originalBucket,
                $bucketOneMax,
                $bucketTwoMax,
                $goal);
            if ($result->numberOfActions >= 0) {
                $results[] = $result;
            }
        }

        // Failure if we ran out of legal moves.
        if (count($results) == 0) {
            return new Solution(-1, "n/a", 0);
        }

        // Return the result with the shortest path. Don't care
        // how a tie is sorted as we only really want the length.
        usort($results, function($a, $b) {
            return $a->numberOfActions - $b->numberOfActions;
        });
        return $results[0];
    }

}
