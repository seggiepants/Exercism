//
// This is only a SKELETON file for the 'Tournament' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

const header = 'Team                           | MP |  W |  D |  L |  P'

export const tournamentTally = (data) => {
  let rows = data.split("\n").map((row) => row.split(";"))
  let tally = {}
  if (data.length > 0)
  {
    for(let row of rows) 
    {
      let [team1, team2, state] = row
      addTeam(tally, team1)
      addTeam(tally, team2)

      tally[team1]["MP"] += 1
      tally[team2]["MP"] += 1

      switch(state)
      {
        case 'win':
          tally[team1]["W"] += 1
          tally[team1]["P"] += 3

          tally[team2]["L"] += 1
          break;
        case "loss":
          tally[team1]["L"] += 1

          tally[team2]["W"] += 1
          tally[team2]["P"] += 3
          break;
        case "draw":
          tally[team1]["D"] += 1
          tally[team1]["P"] += 1

          tally[team2]["D"] += 1
          tally[team2]["P"] += 1
          break;
        default:
          throw new Error("Invalid game state")
      }
    }
  }
  let computed = []
  if (Object.keys(tally).length > 0)
    computed = [... Object.values(tally)]
  computed.sort((a, b) => a["P"] === b["P"] ? a["Team"].localeCompare(b["Team"]) : b["P"] - a["P"])


  return formatResults(computed)
};

const formatResults = (data) => {
  let ret = header
  for (let row of data)
  {
    ret += `\n${row.Team.padEnd(30)} | ${String(row.MP).padStart(2)} | ${String(row.W).padStart(2)} | ${String(row.D).padStart(2)} | ${String(row.L).padStart(2)} | ${String(row.P).padStart(2)}`
  }
  return ret
}

const addTeam = (data, teamName) => {
  if (!(teamName in data))
  {
    data[teamName] = {"Team": teamName, "MP": 0, "W": 0, "D": 0, "L": 0, "P": 0}
  }
}
