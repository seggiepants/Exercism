"""
Determine the state of a Tic-Tac-Toe board.
"""

def gamestate(board):
    """
    Determine the state of a tic-tac-toe board. Check if there is
    a winner, a draw or if it is ongoing. Raise an error if we are in an
    invalid state.
    :param board: A list of three strings of three characters each filled with X, O or a space for an empty spot
    :returns: 'win', 'draw' 'ongoing' depending on board state
    :raises: Value error if board is in invalid state. Doesn't detect invalid markers on the board.
    """
    winner_x = is_winner(board, 'X')
    winner_o = is_winner(board, 'O')

    if winner_x and winner_o:
        raise ValueError('Impossible board: game should have ended after the game was won')
    
    if winner_x or winner_o:
        return 'win'
    
    count_x = sum(1 for row in board for index in range(len(row)) if row[index] == 'X')
    count_o = sum(1 for row in board for index in range(len(row)) if row[index] == 'O')
    if count_x > count_o + 1:
        raise ValueError('Wrong turn order: X went twice')

    if count_o > count_x:
        raise ValueError('Wrong turn order: O started')
    
    if count_x + count_o < 9:
        return 'ongoing'
    return 'draw'

def is_winner(board, player):
    """
    Check to see if a player has won in tic-tac-toe
    :param board: A list of three strings of three characters each filled with X, O or a space for an empty spot
    :param player: The player to check. Should be 'X', or 'O'
    :returns: True if the winner.
    """
    win_row = player * 3
    # horizontal and vertical
    for index in range(3):
        if board[index] == win_row:
            return True
        vertical = board[0][index] + board[1][index] + board[2][index]
        if vertical == win_row:
            return True
    
    # diagonals
    left_to_right = board[0][0] + board[1][1] + board[2][2]
    right_to_left = board[0][2] + board[1][1] + board[2][0]
    if win_row in (left_to_right, right_to_left):
        return True
    
    return False