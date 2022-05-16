=begin
Write your code for the 'Hamming' exercise in this file. Make the tests in
`hamming_test.rb` pass.

To get started with TDD, see the `README.md` file in your
`ruby/hamming` directory.
=end
class Hamming
    def self.compute(left_strand, right_strand)
        raise ArgumentError.new("Strand lengths are not the same") if left_strand.length != right_strand.length
        errors = 0
        for i in 0..left_strand.length-1
            if left_strand[i] != right_strand[i]
                errors = errors + 1
            end
        end
        errors
    end
end