<?php

declare(strict_types=1);

// Can you make this data class immutable?

class Location {

    public private(set) int $column;
    public private(set) int $row;

    public function __construct(int $column, int $row)
    {
        $this->column = $column;
        $this->row = $row;    
    }

    public function __toString() {
        return sprintf("(%d,%d)", $this->column, $this->row);
    }
}
