=begin
Write your code for the 'Tournament' exercise in this file. Make the tests in
`tournament_test.rb` pass.

To get started with TDD, see the `README.md` file in your
`ruby/tournament` directory.
=end
class Row

    attr_accessor :matches_played, :wins, :draws, :losses, :points

    def initialize(matches_played = 0, wins = 0, draws = 0, losses = 0, points = 0)
        @matches_played = matches_played
        @wins = wins
        @draws = draws
        @losses = losses
        @points = points
    end

    def pretty_print(team)
        "%-30s | %2d | %2d | %2d | %2d | %2d\n" % [team, @matches_played, @wins, @draws, @losses, @points]
    end
end

class Tournament
    def self.parse_input(input)
        data = {}
        for row in input.split(/\n/, -1) do
            if row.strip.length > 0
                fields = row.split(/;/, -1)
                team_1 = fields[0]
                team_2 = fields[1]
                status = fields[2]

                if (!data.has_key?(team_1))
                    data[team_1] = Row.new
                end
                
                if (!data.has_key?(team_2))
                    data[team_2] = Row.new
                end

                data[team_1].matches_played += 1
                data[team_2].matches_played += 1
                if (status == "win")
                    data[team_1].wins += 1
                    data[team_1].points += 3
                    data[team_2].losses += 1
                elsif (status == "loss")
                    data[team_1].losses += 1
                    data[team_2].wins += 1
                    data[team_2].points += 3
                else # draw
                    data[team_1].draws += 1
                    data[team_1].points += 1
                    data[team_2].draws += 1
                    data[team_2].points += 1
                end
            end
        end
        data
    end

    def self.tally(input)
        header = "Team                           | MP |  W |  D |  L |  P\n"
        data = parse_input(input)
        ret = header
        for key, value in data.sort_by { |key, value| [-value.points, key] } do
            ret += value.pretty_print(key)
        end
        ret
    end
end