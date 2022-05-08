class BirdCount
  def self.last_week
    [0, 2, 5, 3, 7, 8, 4]
  end

  def initialize(birds_per_day)
    @birds_per_day = birds_per_day
  end

  def yesterday
    # @birds_per_day[-2] is better
    @birds_per_day[@birds_per_day.length() - 2]
  end

  def total
    # @birds_per_day.sum is better
    total_birds = 0
    for birds in @birds_per_day do 
      total_birds = total_birds + birds
    end
    total_birds
  end

  def busy_days
    # @birds_per_day.count { |birds| birds >= 5 }
    busy_count = 0
    for birds in @birds_per_day do 
      if birds >= 5
        busy_count = busy_count + 1
      end
    end
    busy_count
  end

  def day_without_birds?
    # @birds_per_day.include?(0) is better
    for birds in @birds_per_day do
      if birds == 0 
        return true
      end
    end
    return false
  end
end
