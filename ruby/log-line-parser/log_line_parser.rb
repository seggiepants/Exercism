class LogLineParser
  def initialize(line)
    @line = line
    index = line.index(':')
    @level = line[1, index - 2].downcase
    @message = line[index + 1,line.length - 1 - index].strip
  end

  def message
    @message
  end

  def log_level
    @level
  end

  def reformat
    "#{@message} (#{@level})"
  end
end
