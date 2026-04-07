"""
Calculate the best discount available for a basket full of books in a given 5 book series
2 of the same in series is 5%, 3 is 10%, 4 is 20%, and all five gets you a 25% discount.
"""

SINGLE_BOOK = 800

DISCOUNT = {
    2: 0.05,
    3: 0.10,
    4: 0.20,
    5: 0.25,
}

COMBINATIONS = {

    5: [[1, 2, 3, 4, 5 ]],
    4: [[1, 2, 3, 4],
        [1, 2, 3, 5],
        [1, 2, 4, 5],
        [1, 3, 4, 5],
        [2, 3, 4, 5]],
    3: [[1, 2, 3],
        [1, 2, 4],
        [1, 2, 5],
        [1, 3, 4],
        [1, 3, 5],
        [1, 4, 5],
        [2, 3, 4],
        [2, 3, 5],
        [2, 4, 5],
        [3, 4, 5]],
    2: [[1, 2],
        [1, 3],
        [1, 4],
        [1, 5],
        [2, 3],
        [2, 4],
        [2, 5],
        [3, 4],
        [3, 5],
        [4, 5]]
}

CACHE = {}

def total(basket):
    """
    Compute the total after discount for books in a given basket (list of book numbers)
    """
    books_grouped = {1: 0, 2: 0, 3: 0, 4: 0, 5: 0}
    for book_num in basket:
        books_grouped[book_num] += 1
         
    return calc_discount(books_grouped) # default cost


def calc_discount(basket):
    """
    Return the smallest total for possible discounts for books in a basket.
    """
    min_total = sum(basket.values()) * SINGLE_BOOK
    unique_books = len(basket)

    for group_size in range(2, 6):
        if unique_books >= group_size:
            amount = check_groups(basket, group_size)
            min_total = min(amount, min_total)
    
    return min_total

def check_groups(basket, group_size):
    """
    Check the if a basket has an available discount for books of a given group size
    """
    min_total = float(sum(basket.values()) * SINGLE_BOOK)

    if group_size in COMBINATIONS:
        available = []
        for combination in COMBINATIONS[group_size]:
            is_available = True
            for value in combination:
                if value not in basket.keys():
                    is_available = False
                    break
            if is_available:
                available.append(combination)
            
        if len(available) > 0:
            for combination in available:
                current = {**basket}
                for value in combination:
                    current[value] = current[value] - 1
                    if current[value] <= 0:
                        del current[value]

                discount = DISCOUNT.get(group_size, 0.0)
                group_total = group_size * SINGLE_BOOK * (1.00 - discount)

                # Don't bother with a recursive call unless we are smaller than the current minimum
                print(f"Group Total: {group_total}")
                print(f"Min Total: {min_total}")
                if len(current) > 0 and group_total < min_total:
                    group_total += calc_discount_memoize(current)
                
                min_total = min(group_total, min_total)
    
    return min_total

def calc_discount_memoize(basket):
    """
    Calculate the discount but used the memoized value if possible to reduce runtime.
    """
    # It doesn't matter if we have 2,2,3 or 2,3,3 the discount will be the same.
    # so sort the counts ascending. Then turn that into a string so it will be keyed
    # on value not reference. Then cache/retrieve it so we don't have to
    # do it a million times. I had __HUGE__ runtimes before I got this working.

    key = ','.join([str(count) for count in sorted(basket.values())])
    if key in CACHE:
        return CACHE[key]
    ret = calc_discount(basket)
    CACHE[key] = ret
    return ret
