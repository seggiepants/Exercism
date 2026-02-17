export const degreesOfSeparation = (familyTree, personA, personB) => {
  
  // find parents, children, and siblings of a given name (one degree of separation)
  let one_degree = (tree, name) => {
    let ret = []
    let treeKeys = Object.keys(tree)
    // children
    if (treeKeys.includes(name))
      ret.push(... tree[name])

    // Parents & siblings
    for(let key of treeKeys)
    {
      if (tree[key].includes(name))
      {
        ret.push(key)
        ret.push(...tree[key])
      }
    }
    // don't include name in the return set
    return [...new Set(ret.filter((entry) => entry !== name))]
  }

  // searched is none, new to search is one degree separation
  // if count doesn't change between rounds
  let familyA = []
  let newFamilyA = one_degree(familyTree, personA)
  let familyB = []
  let newFamilyB = one_degree(familyTree, personB)
  let distance = 0
  
  while (true)
  {
    // check if the family groups overlapped in the last round
    distance += 1
    familyA = [...familyA, ...newFamilyA]
    familyB = [...familyB, ...newFamilyB]
    if (familyA.includes(personB) && familyB.includes(personA))
      return distance

    // Get the next set of relatives from the new search set
    let nextFamilyA = []
    for(let person of newFamilyA)
    {
      let current = one_degree(familyTree, person)
      nextFamilyA.push(...current)

    }
    let nextFamilyB = []
    for(let person of newFamilyB)
    {
      let current = one_degree(familyTree, person)
      nextFamilyB.push(...current)
      
    }
    
    // set the new search list. Only include names we haven't searched yet.
    newFamilyA = nextFamilyA.filter((name) => !familyA.includes(name))
    newFamilyB = nextFamilyB.filter((name) => !familyB.includes(name))

    // if no new names they are not related
    if (newFamilyA.length + newFamilyB.length === 0)
      return -1
  }
}



