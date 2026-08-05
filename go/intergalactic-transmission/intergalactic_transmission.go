// Intergalactic Transmission - Parity check exercise.
package intergalactictransmission

import "errors"

// Package a series of bytes so that they have a parity bit on bit 7.
// This will make the message size increase by ~ 1/7th + 1 bytes
// @param message: slice of bytes to encode with parity.
// @returns: slice of bytes with parity encoded into the last bit
func Transmit(message []byte) []byte {
	var result []byte = make([]byte, 0)
	var bits []byte = make([]byte, 16)
	var bitCount int = 0
	var written = 0
	for _, data := range message {
		written = 0
		for bitCount >= 7 || written < 8 {
			for bitCount < 7 {
				bits[bitCount] = (data & 0b10000000) >> 7
				bitCount++
				data = data << 1
				written++
			}
			var payload byte = 0b00000000
			var count1s byte = 0
			for i := range 7 {
				bitCount--
				if bits[bitCount] == 1 {
					count1s++
					payload |= 1 << i
				}
			}
			payload = payload << 1
			if count1s%2 == 1 {
				payload |= 1
			}
			result = append(result, payload)
			for written < 8 {
				bits[bitCount] = (data & 0b10000000) >> 7
				bitCount++
				data = data << 1
				written++
				if bitCount == 7 {
					break
				}
			}
		}

	}

	if bitCount > 0 {
		for bitCount < 7 {
			bits[bitCount] = 0
			bitCount++
		}
		var payload byte = 0b00000000
		var count1s byte = 0
		for i := range 7 {
			bitCount--
			if bits[bitCount] == 1 {
				count1s++
				payload |= 1 << i
			}
		}
		payload = payload << 1
		if count1s%2 == 1 {
			payload |= 1
		}
		result = append(result, payload)

	}
	return result
}

// Decode a parity encoded message (from the Encode function) returning it to the original message.
// @param message: slice of bytes with parity encoded in last bit
// @returns: decoded message as a slice of bytes or a parity error.
// @raises: parity error if the parity check fails on a byte.
func Decode(message []byte) ([]byte, error) {
	var result []byte = make([]byte, 0)

	var written int = 0
	var payload byte = 0
	var bitCount = 0

	for _, data := range message {
		// Check for parity error.
		count1s := 0
		var parity int = int(data & 0b1)
		for i := 1; i < 8; i++ {
			if data&(1<<i) != 0 {
				count1s++
			}
		}
		if count1s%2 != parity {
			return nil, errors.New("wrong parity")
		}

		written = 0
		for written < 7 {
			payload = (payload << 1) | ((data & 0b10000000) >> 7)
			data = data << 1
			written++
			bitCount++
			if bitCount == 8 {
				result = append(result, payload)
				payload = 0
				bitCount = 0
			}
		}
	}
	return result, nil
}
