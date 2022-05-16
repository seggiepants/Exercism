=begin
Write your code for the 'Word Count' exercise in this file. Make the tests in
`word_count_test.rb` pass.

To get started with TDD, see the `README.md` file in your
`ruby/word-count` directory.
=end
class Phrase

    attr_accessor :data
    
    def initialize(data)
        @data = data
    end

    def word_count
        ret = {}
        words = @data.split(/[^a-zA-Z0-9']+/, -1)
        for word in words
            key = filter_word(word)
            if key.length > 0
                if ret.has_key?(key)
                    ret[key] = ret[key] + 1
                else
                    ret[key] = 1
                end
            end    
        end
        ret
    end

    def filter_word(word)
        ret = word.downcase
        ret = ret.strip
        ret = ret.gsub /[^a-z0-9']/, ""
        if ret[0] == ret[-1] and ret[0] == "'"
            ret = ret[1..-2]
        end
        ret
    end

end