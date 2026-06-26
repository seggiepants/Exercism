<?php
// Windowing System. An exercise to introduce classes
// Why do the tests not want me to have a type declared for the member variables?

class ProgramWindow
{
    public $x;
    public $y;
    public $width;
    public $height;

    // Construct a new Program Window instance with default values for size and position.
    function __construct() {
        $this->x = 0;
        $this->y = 0;
        $this->width = 800;
        $this->height = 600;
    }

    // Resize the window.
    // @param $size: A size object with the new width and height
    function resize(Size $size) {
        $this->width = $size->width;
        $this->height = $size->height;
    }

    // Move the window
    // @param $position: Move the window to the position given by this parameter.
    function move(Position $position) {
        $this->x = $position->x;
        $this->y = $position->y;
    }
}
