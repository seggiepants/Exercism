def total(basket):
    cost = score(basket) # default cost

    for i in range(1, 32): # can skip 0 that would be no books
        pattern = bin(i)[2:].rjust(5, '0')
        pattern_ok = True
        unique = []
        remaining = basket.copy()

        for j in range(len(pattern)):
            if pattern[j] == '1':
                digit = j + 1
                if digit in remaining:
                    # add to the current unique list based on the pattern
                    unique.append(digit)
                    remaining.remove(digit)
                else:
                    # desired digit not found, can't make the desired pattern to check.
                    pattern_ok = False
                    break
        
        if pattern_ok:
            cost = min(cost, score(unique) + total(remaining))

    return cost

def score(basket):
    unique_items = []
    remaining = basket[:]
    for book in range(1, 6):
        if book in remaining and book not in unique_items:
            unique_items.append(book)
            remaining.remove(book)
    
    if len(unique_items) > 0:
        basket_size = len(unique_items)
    else:
        basket_size = 0

    if basket_size <= 1:
        multiplier = 1.00 # full price
    elif basket_size == 2:
        multiplier = 0.95 # 5% discount
    elif basket_size == 3:
        multiplier = 0.90 # 10% discount
    elif basket_size == 4:
        multiplier = 0.80 # 20% discount
    elif basket_size == 5:
        multiplier = 0.75 # 25% discount

    return int(round(800 * basket_size * multiplier)) + (800 * len(remaining))
