package react

import "slices"

// Define reactor, cell and canceler types here.

// These types will implement the Reactor, Cell and Canceler interfaces, respectively.
type ReactorImplementation struct {
	computeCells *[]CellCompute
}

type CellInput struct {
	value   *int
	reactor *ReactorImplementation
}

func (c CellInput) Value() int {
	return *c.value
}

func (c CellInput) SetValue(newValue int) {
	oldValue := *c.value
	*c.value = newValue
	if oldValue != *c.value {
		c.reactor.Notify(&c)
	}
}

type CellCompute struct {
	subscribers  *[]*func(int)
	dependencies *[]*Cell
	oldValues    []int
	fn1          func(int) int
	fn2          func(int, int) int
	value        *int
}

func (c CellCompute) Value() int {
	oldValue := *c.value
	changed := false
	for i, value := range *c.dependencies {
		newValue := (*value).Value()
		if newValue != c.oldValues[i] {
			changed = true
			c.oldValues[i] = newValue
		}
	}
	if changed {
		if c.fn1 != nil && len(*c.dependencies) == 1 {
			one := (*(*c.dependencies)[0]).Value()
			*c.value = c.fn1(one)
		} else if c.fn2 != nil && len(*c.dependencies) == 2 {
			one := (*(*c.dependencies)[0]).Value()
			two := (*(*c.dependencies)[1]).Value()
			*c.value = c.fn2(one, two)
		}
		if oldValue != *c.value {
			for _, fn := range *c.subscribers {
				(*fn)(*c.value)
			}
		}
	}
	return *c.value
}

func (c CellCompute) AddCallback(callback func(int)) Canceler {
	if !slices.Contains(*c.subscribers, &callback) {
		*c.subscribers = append(*c.subscribers, &callback)
	}
	return CancelerImplementation{
		cell:     c,
		callback: &callback,
	}
}

func (c CellCompute) RemoveCallback(callback *func(int)) {
	i := slices.Index(*c.subscribers, callback)
	if i >= 0 {
		*c.subscribers = slices.Delete(*c.subscribers, i, i+1)
	}
}

type CancelerImplementation struct {
	cell     CellCompute
	callback *func(int)
}

func (c CancelerImplementation) Cancel() {
	c.cell.RemoveCallback(c.callback)
}

func New() Reactor {
	newCells := make([]CellCompute, 0)
	return ReactorImplementation{computeCells: &newCells}
}

func (r ReactorImplementation) CreateInput(initial int) InputCell {
	//subscribers := make([]func(int), 0)
	return InputCell(CellInput{value: &initial, reactor: &r})
}

func (r ReactorImplementation) CreateCompute1(dep Cell, compute func(int) int) ComputeCell {
	dependencies := make([]*Cell, 0)
	dependencies = append(dependencies, &dep)
	subscribers := make([]*func(int), 0)
	oldValues := make([]int, 1)
	oldValues[0] = dep.Value()
	var initial int = compute(dep.Value())
	newCell := CellCompute{subscribers: &subscribers,
		dependencies: &dependencies,
		fn1:          compute,
		fn2:          nil,
		value:        &initial,
		oldValues:    oldValues,
	}
	*r.computeCells = append(*r.computeCells, newCell)
	return newCell
}

func (r ReactorImplementation) CreateCompute2(dep1, dep2 Cell, compute func(int, int) int) ComputeCell {
	dependencies := make([]*Cell, 0)
	dependencies = append(dependencies, &dep1)
	dependencies = append(dependencies, &dep2)
	subscribers := make([]*func(int), 0)
	oldValues := make([]int, 2)
	oldValues[0] = dep1.Value()
	oldValues[1] = dep2.Value()
	var initial int = compute(dep1.Value(), dep2.Value())
	newCell := CellCompute{subscribers: &subscribers,
		dependencies: &dependencies,
		fn1:          nil,
		fn2:          compute,
		value:        &initial,
		oldValues:    oldValues,
	}
	*r.computeCells = append(*r.computeCells, newCell)
	return newCell
}

func (r ReactorImplementation) Notify(cell *CellInput) {
	for _, nextCell := range *r.computeCells {
		//if slices.Contains((*nextCell.dependencies), cell) {
		//for _, nextDependency := range *nextCell.dependencies {
		//if Cell(cell) == &nextDependency {
		nextCell.Value()
		//}
		//}
	}
}
