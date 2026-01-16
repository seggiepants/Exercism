// @ts-check

/**
 * Implement the classes etc. that are needed to solve the
 * exercise in this file. Do not forget to export the entities
 * you defined so they are available for the tests.
 */

/**
 * Size object
 * @param {Number} width 
 * @param {Number} height 
 */
export function Size(width = 80, height = 60)
{
    this.width = width
    this.height = height
}

/**
 * Resize a size object
 * @param {Number} width
 * @param {Number} height
 */
Size.prototype.resize = function(width, height)
{
    this.width = width
    this.height = height
}

/**
 * Position of a point on a 2D grid
 * @param {Number} x 
 * @param {Number} y 
 */
export function Position(x = 0, y = 0)
{
    this.x = x
    this.y = y
}

/**
 * Move position to a new coordinate pair.
 * @param {Number} x 
 * @param {Number} y 
 */
Position.prototype.move = function(x, y)
{
    this.x = x
    this.y = y
}

export class ProgramWindow
{
    constructor()
    {
        this.screenSize = new Size(800, 600)
        this.size = new Size()
        this.position = new Position()
    }

    /**
     * Change the size of the window
     * @param {Size} newSize 
     */
    resize(newSize)
    {
        let maxWidth = this.screenSize.width - this.position.x
        let maxHeight = this.screenSize.height - this.position.y
        newSize.width = Math.max(1, Math.min(maxWidth, newSize.width))
        newSize.height = Math.max(1, Math.min(maxHeight, newSize.height))
        this.size.resize(newSize.width, newSize.height)
    }

    /**
     * Desired location of the window
     * @param {Position} newPosition 
     */
    move(newPosition)
    {
        let maxX = this.screenSize.width - this.size.width
        let maxY = this.screenSize.height - this.size.height
        newPosition.x = Math.max(0, Math.min(maxX, newPosition.x))
        newPosition.y = Math.max(0, Math.min(maxY, newPosition.y))
        this.position.move(newPosition.x, newPosition.y)
    }

}

/**
 * Move and resize a window to given defaults
 * @param {ProgramWindow} window 
 */
export function changeWindow(window)
{
    window.move(new Position(100, 150))
    window.resize(new Size(400, 300))
    return window
}