// IOU REST API example (no real networking)
package restapi

import (
	"slices"
	"strings"
)

// Define the Rest API interface. You should not modify the code in this block.

type User struct {
	Name    string
	Owes    map[string]float64
	OwedBy  map[string]float64
	Balance float64
}

type GetUsersRequest struct {
	Users []string
}

type GetUsersResponse struct {
	Users []User
}

type AddUserRequest struct {
	User string
}

type AddUserResponse struct {
	User User
}

type AddIouRequest struct {
	Lender   string
	Borrower string
	Amount   float64
}

type AddIouResponse struct {
	Users []User
}

type RestApi interface {
	GetUsers(GetUsersRequest) GetUsersResponse
	AddUser(AddUserRequest) AddUserResponse
	AddIou(AddIouRequest) AddIouResponse
}

// Your code goes below here. Implement the RestApi interface.

type Api struct {
	Users []User
}

// Create a new API with the given database of users.
// @param database: Slice of Users
// @returns: Api struct using the given users
func NewApi(database []User) RestApi {
	return &Api{Users: database}
}

// Create a new User record with the given name
// @param name: The name of the new user.
// @returns: New user object with the name, zero balance, and empty owes, and owed by maps.
func NewUser(name string) *User {
	return &User{Name: name, Balance: 0, Owes: make(map[string]float64, 0), OwedBy: make(map[string]float64)}
}

// Get a slice of users matching names in the request.
// @param req: Slice of names to return
// @returns: GetUsersResponse which has a slice of users sorted by name.
func (a *Api) GetUsers(req GetUsersRequest) GetUsersResponse {
	users := make([]User, 0)
	for _, user := range a.Users {
		if slices.Contains(req.Users, user.Name) {
			users = append(users, user)
		}
	}
	slices.SortFunc(users, func(a User, b User) int {
		return strings.Compare(a.Name, b.Name)
	})
	return GetUsersResponse{Users: users}
}

// Add a user to the IOUdatabase.
// @param req: Add User Request - contains a user name
// @returns: AddUserResponse - contains a user struct
func (a *Api) AddUser(req AddUserRequest) AddUserResponse {
	response := AddUserResponse{User: *NewUser(req.User)}
	a.Users = append(a.Users, response.User)
	return response
}

// Add an IOU to the database
// @param req: Add IOU Request (Borrower, Lender, Amount)
// @returns: AddIouResponse which has the updated users involved, sorted by name.
func (a *Api) AddIou(req AddIouRequest) AddIouResponse {
	indexBorrower := slices.IndexFunc(a.Users, func(user User) bool { return user.Name == req.Borrower })
	indexLender := slices.IndexFunc(a.Users, func(user User) bool { return user.Name == req.Lender })
	if indexBorrower < 0 || indexLender < 0 {
		return AddIouResponse{Users: make([]User, 0)} // Can't borrow if no lender or borrower found.
	}
	borrower := a.Users[indexBorrower]
	lender := a.Users[indexLender]

	borrower.Owes[req.Lender] += req.Amount
	borrower.Balance -= req.Amount
	lender.OwedBy[req.Borrower] += req.Amount
	lender.Balance += req.Amount
	NormalizeUser(borrower, lender.Name)
	NormalizeUser(lender, borrower.Name)
	users := []User{borrower, lender}
	slices.SortFunc(users, func(a User, b User) int { return strings.Compare(a.Name, b.Name) })
	return AddIouResponse{Users: users}
}

// If a user owes and is owed by another user simplify the balance
// @param User: The user to normalize
// @param other: The name of the other user
func NormalizeUser(user User, other string) {
	credit := user.OwedBy[other]
	debit := user.Owes[other]
	if credit != 0 && debit != 0 {
		if credit == debit {
			delete(user.OwedBy, other)
			delete(user.Owes, other)
		} else if credit > debit {
			user.OwedBy[other] -= debit
			delete(user.Owes, other)
		} else { // credit < debit
			user.Owes[other] -= credit
			delete(user.OwedBy, other)
		}
	}
}
