class SimpleCalculator

  class UnsupportedOperation < StandardError
  end

  ALLOWED_OPERATIONS = ['+', '/', '*'].freeze

  def self.calculate(first_operand, second_operand, operation)
    if !first_operand.is_a?(Numeric) 
      raise ArgumentError.new('First Operand is not a number.')
    end
    if !second_operand.is_a?(Numeric)
      raise ArgumentError.new('Second Operand is not a number.')
    end

    result = 0
    if operation == '+'
      result = first_operand + second_operand
    elsif operation == '/'
      begin
        result = first_operand / second_operand
      rescue ZeroDivisionError
        return "Division by zero is not allowed."
      end
    elsif operation == '*'
      result = first_operand * second_operand
    else
      raise UnsupportedOperation.new("Operation (#{operation}) is not a supported operation")
    end 
    "#{first_operand} #{operation} #{second_operand} = #{result}"
  end
end
