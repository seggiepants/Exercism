=begin
Write your code for the 'Series' exercise in this file. Make the tests in
`series_test.rb` pass.

To get started with TDD, see the `README.md` file in your
`ruby/series` directory.
=end
class Series
    attr_accessor :data

    def initialize(input)
        @data = input
    end

    def slices(length)
        ret = []
        if length > @data.length
            raise ArgumentError.new("Slice too big.")
        end
        for i in 0..@data.length - length
            ret.append(@data[i..i+length - 1])
        end
        ret
    end
end
