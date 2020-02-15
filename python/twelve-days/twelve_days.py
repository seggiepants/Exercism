def recite(start_verse, end_verse):
    """
    Twelve Days of Christmas:
    Recites the lyrics of the song the Twelve Days of Christmas
    from the given start to end verse.

    Both verses should be between 1 and 12 inclusive and whole integer numbers.
    """
    assert (start_verse >= 1 and start_verse <= 12), 'start_verse should be between 1 and 12'
    assert (end_verse >= 1 and end_verse <= 12 and end_verse >= start_verse), 'end_verse should be between 1 and 12 and greater or equal to start_verse'

    ret = []
    days = {
        1: ('first', 'a Partridge in a Pear Tree'),
        2: ('second', 'two Turtle Doves'),
        3: ('third', 'three French Hens'),
        4: ('fourth', 'four Calling Birds'),
        5: ('fifth', 'five Gold Rings'),
        6: ('sixth', 'six Geese-a-Laying'),
        7: ('seventh', 'seven Swans-a-Swimming'),
        8: ('eighth', 'eight Maids-a-Milking'),
        9: ('ninth', 'nine Ladies Dancing'),
        10: ('tenth', 'ten Lords-a-Leaping'),
        11: ('eleventh', 'eleven Pipers Piping'),
        12: ('twelfth', 'twelve Drummers Drumming')}

    for verse in range(start_verse, end_verse + 1):
        day = days[verse][0]
        item = f'On the {day} day of Christmas my true love gave to me: '
        for counter in range(verse, 0, -1):
            if counter != verse:
                item += ', '

            if counter == 1 and verse > 1:                
                item += 'and '
            
            gift = days[counter][1]
            item += gift
        item += '.'
        ret.append(item)
    return ret

