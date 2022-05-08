class AssemblyLine
  BASE_CARS_PER_HOUR = 221

  def initialize(speed)
    @speed = speed
  end

  def production_rate_per_hour
    (BASE_CARS_PER_HOUR * @speed * get_multiplier).to_f
  end

  def get_multiplier
    if @speed <= 4 && @speed >= 1
      1.0
    elsif @speed <= 8 && @speed >= 5
      0.9
    elsif @speed == 9
      0.8
    elsif @speed == 10
      0.77
    else
      raise "Value out of range."
    end
  end

  def working_items_per_minute
    (production_rate_per_hour / 60).to_i
  end
end
