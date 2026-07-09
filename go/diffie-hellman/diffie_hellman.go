// Diffie-Hellman-Merkle key generation exercise.
package diffiehellman

import (
	"math/big"
	"math/rand"
	"time"
)

// Diffie-Hellman-Merkle key exchange
// Private keys should be generated randomly.

// I want this at the module level so we don't reset the source with a similar value
// on each call. That would limit the randomness of the result.
var rnd *rand.Rand = rand.New(rand.NewSource(time.Now().UnixNano()))

// Calculate the Private Key
// @param p: prime number to generate with
// @returns: big.Int reference with the private key result.
func PrivateKey(p *big.Int) *big.Int {
	// Note you can't use p in the sub and rand calls as it will
	// mutate the original. That is why we have ret.
	var ret *big.Int = big.NewInt(0)
	ret.Set(p)
	ret.Sub(ret, big.NewInt(2))
	ret.Rand(rnd, ret)
	ret.Add(ret, big.NewInt(2))
	return ret
}

// Calculate the Public key.
// @param private: The Private Key
// @param p: The first prime number
// @param g: The second prime number.
// @returns: big.Int reference with the Public key.
func PublicKey(private, p *big.Int, g int64) *big.Int {
	var ret *big.Int = big.NewInt(0)
	ret.Exp(big.NewInt(g), private, p)
	return ret
}

// Return the pair of Private and Public keys for the given prime numbers.
// @param p: First Prime Number
// @param g: Second Prime Number
// @returns: A matched pair of encryption keys in private, public order.
func NewPair(p *big.Int, g int64) (*big.Int, *big.Int) {
	privateKey := PrivateKey(p)
	publicKey := PublicKey(privateKey, p, g)
	return privateKey, publicKey
}

// Return the Secret Key for the given private key (a), public key (b), and prime number (p)
// @param private1: Private Key of first Person
// @param public2: Public Key of the second person
// @param p: The prime number to mod with.
// @returns: New big.Int reference with the resulting value.
func SecretKey(private1, public2, p *big.Int) *big.Int {
	var ret *big.Int = big.NewInt(0)
	ret.Exp(public2, private1, p)
	return ret
}
