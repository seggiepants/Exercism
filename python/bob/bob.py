def response(hey_bob):
    message = hey_bob.strip()
    
    is_yelling = message == message.upper() and message != message.lower()
    is_question = message[-1:] == '?'
    is_nothing = len(message) == 0
    if is_question and is_yelling:
        return 'Calm down, I know what I\'m doing!'
    elif is_question:
        return 'Sure.'
    elif is_yelling:
        return 'Whoa, chill out!'
    elif is_nothing:
        return 'Fine. Be that way!'
    else:
        return 'Whatever.'
