"""
Evaluate and score poker hands.
"""

SORT_RANK = 'A K Q J 109 8 7 6 5 4 3 2 '       # Ace above king
SORT_RANK_MINOR = 'K Q J 109 8 7 6 5 4 3 2 A '  # Ace below 2


def best_hands(hands):
    """
    Return the best hands from a list of hands.
    :param hands: A list of string representing hands. Strings are space delimited and each card is a rank followed by suit
    :returns: A list of the best rated hands. There can be multiple values if there are equivalent scores
    """
    scored_hands = {}
    for hand in hands:
        scored_hands[hand] = score_hand(hand)
    
    max_score = max(scored_hands.values())
    candidates = [hand for hand, score in scored_hands.items() if score == max_score]

    index = 0
    # Check for equivalent hands if scores are the same
    while index < len(candidates) - 1:
        result = compare_hands(candidates[index], candidates[index + 1])
        if result =='>':
            # A > B
            del candidates[index + 1]
            index = 0 # restart
        elif result == '<':
            # A < B
            del candidates[index]
            index = 0 # restart
        else:
            # A == B
            index += 1
    return candidates

def card_rank(card):
    """
    Return just the rank from a card
    :param card:The card evaluate
    :returns: Rank from the card (2-10, J, K, Q, A)
    """
    return card[0:-1]

# Return the suit of a card
def card_suit(card):
    """
    Return just the suit from a card
    :param card:The card evaluate
    :returns: Suit from the card (S, D, H, or C)
    """
    return card[-1:]

# This only works with Ace high
# Convert card to number 2=2, 10=10, K=13, A=14
def score_card_rank(card):
    """
    Return the rank of a card when the ace is above the king.
    :param card:The card to score. Should be Rank followed by suit.
    :returns: Score for the card a higher score is better.
    """
    return (28 - SORT_RANK.find(card)) // 2

# This only works with Ace low
# Convert card to number A=1, 2=2, 10=10, K=13
def score_card_rank_minor(card):
    """
    Return the rank of a card when the ace is below the 2.
    :param card:The card to score. Should be Rank followed by suit.
    :returns: Score for the card a higher score is better.
    """
    return (28 - SORT_RANK_MINOR.find(card)) // 2

def compare_hands(hand_a, hand_b):
    """
    Compare two hands 
    :param hand_a: string with cards in a hand separated by a space are rank followed by suit
    :param hand_b: string with cards in a hand separated by a space are rank followed by suit
    :returns: '<' if hand_a has the first low card, '>' if hand_b has the first low card, and '=' if they match all the way through
    """    
    rank_a = [score_card_rank(card_rank(value)) for value in hand_a.split(' ')]
    rank_a.sort(reverse=True)
    rank_b = [score_card_rank(card_rank(value)) for value in hand_b.split(' ')]
    rank_b.sort(reverse=True)
    for index, card_a in enumerate(rank_a):
        if card_a < rank_b[index]:
            return '<'
        if card_a > rank_b[index]:
            return '>'
        # otherwise equal, keep going.
    return '='

def score_hand(hand):
    """
    Calculate the score for a given card. Detecting if it is one of the best hands flowing 
    down to worst possible hand.
    :param hand: string with cards in a hand separated by a space are rank followed by suit
    :returns: Numeric score for the hand, larger is better.
    """    
    ranked = [score_card_rank(card_rank(card)) for card in hand.split(' ')]
    ranked.sort(reverse=True)
    min_score = 10000

    # Five of a Kind
    ret = five_of_a_kind(hand)
    if len(ret) > 0:
        return min_score + score_card_rank(ret[0])
    min_score -= 1000

    # Royal Flush
    # I guess we just let straight flush handle this.
    min_score -= 1_000

    # Straight Flush
    ret = straight_flush(hand)
    if len(ret) > 0:
        return min_score + score_card_rank(ret[0])
    min_score -= 1000

    # Four of a Kind
    ret = four_of_a_kind(hand)
    if len(ret) > 0:
        return min_score + score_card_rank(ret[0])
    min_score -= 1000

    # Full House
    ret = full_house(hand)
    if len(ret) > 0:
        return min_score + score_card_rank(ret[0])
    min_score -= 1_000

    # Flush
    ret = flush(hand)
    if len(ret) > 0:
        return min_score + score_card_rank(ret[0])
    min_score -= 1_000

    # Straight
    ret = straight(hand)
    if len(ret) > 0:
        return min_score + score_card_rank(ret[0])
    min_score -= 1000

    # Three of a Kind
    ret = three_of_a_kind(hand)
    if len(ret) > 0:
        return min_score + score_card_rank(ret[0])
    min_score -= 1000

    # Two Pair
    ret = two_pair(hand)
    if len(ret) > 0:
        return min_score + score_card_rank(ret[0])
    min_score -= 1000

    # One Pair
    ret = one_pair(hand)
    if len(ret) > 0:        
        return min_score + ranked[0]
    min_score -= 1000

    # High Card
    ret = high_card(hand)
    return min_score + score_card_rank(ret[0]) 

def full_house(hand):
    """
    Detect if a hand is a full hand
    :param hand: string with cards in a hand separated by a space are rank followed by suit
    :returns: list of card ranks, empty list if not a match
    """    
    cards = hand.split(' ')
    ranks = [card_rank(card) for card in cards]
    counts = {value: ranks.count(value) for value in ranks}
    if len(counts) != 2:
        return []
    # Should have a 3, 2
    three = [key for key in counts if counts[key] == 3][0]
    two = [key for key in counts if counts[key] == 2][0]
    cards_three = [card_rank(card) for card in cards if card_rank(card) == three]
    cards_two = [card_rank(card) for card in cards if card_rank(card) == two]
    cards_three.extend(cards_two)
    return cards_three

def flush(hand):
    """
    Detect if a hand is a flush
    :param hand: string with cards in a hand separated by a space are rank followed by suit
    :returns: list of cards, empty list if not a match
    """
    cards = hand.split(' ')
    suits = {card_suit(card) for card in cards}
    if len(suits) != 1:
        return []
    ret = [card_rank(card) for card in cards]
    ret.sort(reverse=True)
    return ret

def straight_helper(hand, ace_high):
    """
    Helper function to detect if a hand is a straight
    :param hand: string with cards in a hand separated by a space are rank followed by suit
    :ace_high: boolean should the ace be before the king or after the two
    :returns: list of cards, empty list if not a match
    """
    cards = hand.split(' ')
    ranks = [card_rank(card) for card in cards]
    if ace_high:
        sort_key = SORT_RANK
        ranks.sort(key=score_card_rank, reverse=True)
    else:
        sort_key = SORT_RANK_MINOR
        ranks.sort(key=score_card_rank_minor, reverse=True)
    previous = ranks[0]

    for index in range(1, len(ranks)):
        index_previous = sort_key.find(previous)
        index_current = sort_key.find(ranks[index])
        if index_current != (index_previous + 2):
            return []
        previous = ranks[index]
    return ranks

def straight(hand):
    """
    Detect if a hand is a straight
    :param hand: string with cards in a hand separated by a space are rank followed by suit
    :returns: list of cards, empty list if not a match
    """    
    ranks = straight_helper(hand, True)
    if len(ranks) > 0:
        return ranks
    return straight_helper(hand, False)

def straight_flush(hand):
    """
    Detect if a hand is a straight flush (acts as a straight and a flush)
    :param hand: string with cards in a hand separated by a space are rank followed by suit
    :returns: list of cards, empty list if not a match
    """
    as_straight = straight(hand)
    as_flush = flush(hand)
    if len(as_straight) > 0 and len(as_flush) > 0:
        return as_straight
    return []


def n_of_a_kind(hand, kind_count):
    """
    Helper function for 3, 4, or 5 of a kind detection
    :param hand: string with cards in a hand separated by a space are rank followed by suit
    :param kind_count: how many of a kind is expected, count must be exact 3 fails if seeking 2.
    :returns: list of cards, empty list if not a match
    """    
    cards = hand.split(' ')
    ranks = [card_rank(card) for card in cards]
    counts = {value: ranks.count(value) for value in ranks}
    matches = [key for key in counts if counts[key] == kind_count]
    if len(matches) != 1:
        return []
    return matches

def five_of_a_kind(hand):
    """
    Detect if a hand is five of a kind
    :param hand: string with cards in a hand separated by a space are rank followed by suit
    :returns: list of cards, empty list if not a match
    """    
    return n_of_a_kind(hand, 5)

def four_of_a_kind(hand):
    """
    Detect if a hand is a four of a kind
    :param hand: string with cards in a hand separated by a space are rank followed by suit
    :returns: list of cards, empty list if not a match
    """    
    return n_of_a_kind(hand, 4)

def three_of_a_kind(hand):
    """
    Detect if a hand is a three of a kind
    :param hand: string with cards in a hand separated by a space are rank followed by suit
    :returns: list of cards, empty list if not a match
    """    
    return n_of_a_kind(hand, 3)

def two_pair(hand):
    """
    Detect if a hand is has two pair
    :param hand: string with cards in a hand separated by a space are rank followed by suit
    :returns: list of cards, empty list if not a match
    """    
    cards = hand.split(' ')
    ranks = [card_rank(card) for card in cards]
    counts = {value: ranks.count(value) for value in ranks}
    matches = [key for key in counts if counts[key] == 2]
    if len(matches) != 2:
        return []
    matches.sort(reverse=True)
    return matches

def one_pair(hand):
    """
    Detect if a hand is has one pair
    :param hand: string with cards in a hand separated by a space are rank followed by suit
    :returns: list of cards, empty list if not a match
    """
    cards = hand.split(' ')
    ranks = [card_rank(card) for card in cards]
    counts = {value: ranks.count(value) for value in ranks}
    if len(counts) != 4:
        return []
    ret = [key for key in counts if counts[key] == 2]
    ret.sort(reverse=True)
    return ret


def high_card(hand):
    """
    Return the hand with the best card first.
    :param hand: string with cards in a hand separated by a space are rank followed by suit
    :returns: sorted list of card ranks.
    """    
    cards = hand.split(' ')
    ranks = [card_rank(card) for card in cards]
    ranks.sort(reverse=True)
    return ranks
