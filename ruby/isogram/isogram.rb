=begin
Write your code for the 'Isogram' exercise in this file. Make the tests in
`isogram_test.rb` pass.

To get started with TDD, see the `README.md` file in your
`ruby/isogram` directory.
=end

require 'set'

class Isogram
    def self.isogram?(input)
        # I only care about digits and letters (lower-case only)
        value = input.downcase.scan(/[a-z0-9]/)
        # If the length of the text equals the same thing converted to a set, 
        # it is an isogram.
        # Iteration 2 - Could have used .uniq to get unique characters instead of a set
        value.length == value.to_set.length
    end
end