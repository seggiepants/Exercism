=begin
Write your code for the 'Matrix' exercise in this file. Make the tests in
`matrix_test.rb` pass.

To get started with TDD, see the `README.md` file in your
`ruby/matrix` directory.
=end
class Matrix

    attr_accessor :rows
    attr_accessor :columns

    def initialize(matrix)
        # parse and save the matrix to class variable value
        # iteration 2 changes 
        # - use transpose it is a built-in-method to do what I want for columns
        # - I should use a &:to_i with map instead of a loop, gotta get used to ruby-isms.
        # iteration 3 changes
        # - Comments admitting to changes swiped from the community solutions.
        @rows = []
        @columns = [] 
        matrix.each_line {|row|
            @rows.push(row.chomp.split.map(&:to_i))
        }
        @columns = @rows.transpose
    end
end
