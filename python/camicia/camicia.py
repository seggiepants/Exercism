"""
Simulate a game of Camicia
"""

def simulate_game(player_a, player_b):
    """
    Simulate a game of Camicia with two players having the given cards.
    :param player_a: A list of card values
    :param player_b: A list of card values
    :returns: Dictionary with keys for  status ("loop" or "finished"), cards (# cards played), and tricks (# tricks)
    """
    royalty = {
        'J': 1,
        'Q': 2,
        'K': 3,
        'A': 4,
    }

    game_status = ''
    cards_played = 0
    count_tricks = 0
    pile = []
    previous_states = []
    turn = 0
    has_moved = [0, 0]

    def collect_pile():
        """
        Collect the pile of cards add them to the player 
        who isn't the current turn. Increment number of tricks
        """
        nonlocal pile
        nonlocal count_tricks
        if 1-turn == 0:
            player_a.extend(pile)
        else:
            player_b.extend(pile)
        pile = []
        count_tricks += 1

    def is_finished():
        """Check if the game is finished due to one player having all the cards
        :returns: True if one player has all the cards (empty pile)
        """
        nonlocal game_status
        total_cards = len(player_a) + len(player_b) + len(pile)
        if len(player_a) == total_cards or len(player_b) == total_cards:
            game_status = 'finished'
            return True
        return False
    
    def no_card():
        """
        Handle condition where the current turn player had no cards to give.
        """
        collect_pile()
        is_finished()

    def update_state():
        """
        Update the list of previous game stats with the current state
        detecting if it is game over due to a loop (current state duplicates previous state)
        """
        nonlocal previous_states
        nonlocal game_status
        move = state_hash(player_a, player_b)
        if move in previous_states:
            game_status = 'loop'
        else:
            previous_states.append(move)

    previous_states.append(state_hash(player_a, player_b)) 
    while len(game_status) == 0:
        has_moved[turn] = 1
        card = next_card(turn, player_a, player_b)
        if len(card) == 0:
            no_card()            
        else:
            pile.append(card)
            cards_played += 1

        if card in royalty:
            penalty = royalty[card]
            recipient = turn
            turn  = 1 - turn
            while penalty > 0:
                has_moved[turn] = 1
                card = next_card(turn, player_a, player_b)
                if len(card) == 0:
                    no_card()
                    break
                pile.append(card)
                cards_played += 1
                penalty-= 1

                if card in royalty:
                    penalty = royalty[card]
                    recipient = turn
                    turn  = 1 - turn
        
            if penalty == 0:
                collect_pile()
                turn = 1 - recipient
        turn = 1 - turn
        if has_moved[0] == has_moved[1] and has_moved[0] != 0:
            update_state()
            has_moved[0] = 0
            has_moved[1] = 0
        
        is_finished()

    return {
        'status': game_status,
        'cards': cards_played,
        'tricks': count_tricks,
    }

def next_card(turn, player_a, player_b):
    """
    Get the next card for the current player
    :param turn: 0 for player_a, and 1 for player_b
    :param player_a: list of cards held by player_a
    :param player_b: list of cards held by player_b
    :returns: next card in the sequence or empty string if unavailable. Card will also be removed from playerA/B's set.
    """
    if turn == 0:
        if len(player_a) == 0:
            return ''
        return player_a.pop(0)
    if len(player_b) == 0:
        return ''
    return player_b.pop(0)
    
def state_hash(player_a, player_b):
    """
    Turn the current state of the game into a hash so we can detect duplicate states 
    and mark the game as a loop
    :param player_a: list of cards held by player_a
    :param player_b: list of cards held by player_b
    :returns: a string which is the hash of the game state. The value of number cards don't matter
    only the position
    """
    numbers = ['2', '3', '4', '5', '6', '7', '8', '9', '10']
    return ''.join(['N' if char in numbers else char for char in player_a]) + '|' + ''.join(['N' if char in numbers else char for char in player_b])
    
