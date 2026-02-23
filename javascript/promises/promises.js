//
// This is only a SKELETON file for the 'Promises' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const promisify = (fn) => {
  let ret = (value) => {
    return new Promise((resolve, reject) => {
      fn(value, (err, result) => {
        if (err)
          reject(err)
        else
          resolve(result)
      })
    }) 
  }
  return ret
}

export const all = (promises) => {
  if (typeof promises === 'undefined')
    return Promise.resolve(undefined)
  if (!Array.isArray(promises) || promises.length === 0)
    return Promise.resolve([])

  let chain = Promise.resolve([])
  for(let i = 0; i < promises.length; i++)
  {
    chain = chain.then((value) => {      
      return promises[i].then((next) => [...value, next])
    })
  }
  return chain
}


export const allSettled = (promises) => {
  if (typeof promises === 'undefined')
    return Promise.resolve(undefined)
  if (!Array.isArray(promises) || promises.length === 0)
    return Promise.resolve([])

  let chain = Promise.resolve([])
  for(let i = 0; i < promises.length; i++)
  {
    chain = chain.then((value) => {      
      return promises[i].then((next) => [...value, next])
        .catch((reason) => {
          return [...value, reason]
        })      
    })
  }
  return chain
};

export const race = (promises) => {
  if (typeof promises === 'undefined')
    return Promise.resolve(undefined)
  
  if (!Array.isArray(promises) || promises.length === 0)
    return Promise.resolve([])

  return new Promise((resolve, reject) => {
    promises.forEach(promise => {
      Promise.resolve(promise)
        .then(resolve)
        .catch(reject);
    });
  });
};

export const any = (promises) => {
  if (typeof promises === 'undefined')
    return Promise.resolve(undefined)
  
  if (!Array.isArray(promises) || promises.length === 0)
    return Promise.resolve([])

  let errors = []

  return new Promise((resolve, reject) => {
    promises.forEach(promise => {
      Promise.resolve(promise)
        .then((value) => resolve(value))
        .catch((error) => {
          errors.push(error)
          if (errors.length === promises.length)
            reject(errors)
      })
    })
  })
}
