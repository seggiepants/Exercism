=begin
Write your code for the 'Resistor Color Duo' exercise in this file. Make the tests in
`resistor_color_duo_test.rb` pass.

To get started with TDD, see the `README.md` file in your
`ruby/resistor-color-duo` directory.
=end
class ResistorColorDuo
    @@color_codes = {
        "black" => 0, "brown" => 1, "red" => 2,
        "orange" => 3, "yellow" => 4, "green" => 5,
        "blue" => 6, "violet" => 7, "grey" => 8,
        "white" => 9}
    
    def self.value(colors)
        count = 0
        value = 0
        colors.each do |color|
            key = color.downcase
            count = count + 1
            if @@color_codes.member?(key)
                value = (value * 10) + @@color_codes[key]
            end
            if count >= 2
                break
            end
        end
        value
    end

end