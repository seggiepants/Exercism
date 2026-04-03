//
// This is only a SKELETON file for the 'Parallel Letter Frequency' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

import {Worker, isMainThread, parentPort, workerData } from 'node:worker_threads'

export function parallelLetterFrequency(texts) {
    return new Promise((resolve, reject) => {
      const worker = new Worker(__filename, { workerData: texts });
      worker.on('message', (message) => resolve(message));
      worker.on('error', reject);
      worker.on('exit', (code) => {
        if (code !== 0)
          reject(new Error(`Worker stopped with exit code ${code}`));
      })
    })
}


if (!isMainThread)
{
  let results = {}
  for(let i = 0; i < workerData.length; i++)
  {
    let message = workerData[i].replaceAll(/[ \.\r\n\t()0123456789/?;:'"`~!@#$%^&*,-]+/gi, "");
    //console.log(workerData[i], ' -> ', message, '\n')
    for(let ch of message)
    {
      let c = ch.toLowerCase()
      results[c] = results[c] + 1 || 1
    }
  }
  parentPort.postMessage(results)
}
