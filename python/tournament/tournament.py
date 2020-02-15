def tally(rows):
    # rows is a list of strings.
    # each string is a semicolon delimited line in the format home team, away team, result.

    # output should be in the form: team, MP, W, D, L, P
    teams = {}
    for row in rows:
        home, away, result = row.split(';')
        add_entry(teams, home, result)
        add_entry(teams, away, reverse_result(result))

    # Team = 30 characters, numbers are two digits while separators are ' | '

    # The trick is that we want points sorted descending and then by name
    # ascending. I got around this by making the key a tuple of points, and team name.
    # however we multiply the points by -1 to get the sort order right. If we
    # use reverse = True then team name won't be sorted correctly. Not a good generic
    # fix, but it works.
    results = ['Team                           | MP |  W |  D |  L |  P'] 
    for key in sorted(teams, key=(lambda key: (teams[key]['P'] * -1, key))):
        result = key.ljust(30) + ' | ' + str(teams[key]['MP']).rjust(2) + ' | '  + str(teams[key]['W']).rjust(2) + ' | '  + str(teams[key]['D']).rjust(2) + ' | '  + str(teams[key]['L']).rjust(2) + ' | '  + str(teams[key]['P']).rjust(2)
        results.append(result)

    return results

def add_entry(teams, team, result):
    # update values for a team in the dictionary.
    # if the team is not present then it will be added to the dictionary
    # The key is the team name, the value is another dictionary with columns
    # MP (matches played), W (Wins), D (Draw), L (Losses), P (Points0)
    if team not in teams:
        # Add team to list
        teams[team] = {'MP': 0, 'W': 0, 'D': 0, 'L': 0, 'P': 0}
    
    # Update the stats.
    teams[team]['MP'] += 1
    if result == 'win':
        teams[team]['W'] += 1
    elif result == 'loss':
        teams[team]['L'] += 1
    elif result == 'draw':
        teams[team]['D'] += 1
    
    teams[team]['P'] += score_result(result)

def reverse_result(result):
    # Swap win/loss.
    # used for populating the away team
    if result == 'win':
        return 'loss'
    elif result == 'loss':
        return 'win'
    else:
        return result

def score_result(result):
    # Return how many points we get for this match. Win=3, Loss=0, Draw=1
    if result == 'win':
        return 3
    elif result == 'draw':
        return 1
    else:
        # must have been a loss
        return 0
