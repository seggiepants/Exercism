class BoutiqueInventory
  def initialize(items)
    @items = items
  end

  def item_names
    @items.map { |item| item[:name] }.sort
  end

  def cheap
    @items.filter { |item|  item[:price] < 30.00}
  end

  def out_of_stock
    @items.filter { |item| item[:quantity_by_size] == {}}
  end

  def stock_for_item(name)
    ret = @items.filter { |item| item[:name] == name}.map{|item| item[:quantity_by_size]}
    if ret == {}
      return nil
    end
    ret[0]
  end

  def total_stock
    # get quantity by size nodes
    # filter out empty ones
    # split sub items by key, value
    # sum the values
    # sum the sub-totals from above.
    @items.map {|item| item[:quantity_by_size]}.filter{
      |item| item != {}}.map{
        |item| item.map{
          |key, value| value}.sum
        }.sum
  end

  private
  attr_reader :items
end
