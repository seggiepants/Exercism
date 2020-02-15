import sqlite3
import json

class RestAPI(object):
    def __init__(self, database=None):
        self.db = sqlite3.connect(':memory:')
        self.db.row_factory = sqlite3.Row
        self.init_db(database)

    def add_user(self, name):
        cursor = self.db.cursor()
        cursor.execute('INSERT INTO users(name) VALUES(?)', (name,))
        self.db.commit()
        user_id = self.get_user_id(cursor, name)
        return json.dumps(self.get_user(cursor, user_id))
    
    def get(self, url, payload=None):
        if url.lower() == '/users':
            cursor = self.db.cursor()
            if payload == None:
                # basic list of everything (sorted by name)                
                cursor.execute('SELECT id FROM users ORDER BY name')
                rows = cursor.fetchall()
                users = []
                for row in rows:
                    users.append(self.get_user(cursor, row['id']))
                return json.dumps({'users': users})
            else:
                # get a list of users, sorted by name but filtered to just
                # the user list passed in.
                parameters = json.loads(payload)
                if 'users' in parameters:
                    return self.get_user_list(parameters['users'])
                else:
                    raise Exception('Users not found in payload')
                    return None
        else:
            raise Exception('Not a recognized get request')
            return None
        return None 

    def get_user_list(self, names):
        users = []
        cursor = self.db.cursor()        
        for name in sorted(names):
            user_id = self.get_user_id(cursor, name)
            users.append(self.get_user(cursor, user_id))
        return json.dumps({'users': users})           

    def post(self, url, payload=None):
        if url.lower() == '/add':
            if payload == None:
                raise Exception('Add called without a name')
            else:
                parameters = json.loads(payload)
                if 'user' in parameters:
                    return self.add_user(parameters['user'])
                else:
                    return None
        elif url.lower() == '/iou':
            if payload == None:
                raise Exception('IOU called without parameters')
            else:
                parameters = json.loads(payload)
                if 'lender' in parameters:
                    lender = parameters['lender']
                else:
                    raise Exception('IOU called without a lender.')
                
                if 'borrower' in parameters:
                    borrower = parameters['borrower']
                else:
                    raise Exception('IOU called without a borrower.')

                if 'amount' in parameters:
                    amount = parameters['amount']
                else:
                    raise Exception('IOU called without an amount.')
                
                cursor = self.db.cursor()
                borrower_id = self.get_user_id(cursor, borrower)
                lender_id = self.get_user_id(cursor, lender)
                cursor.execute('INSERT INTO loans(lender_id, borrower_id, amount) VALUES(?, ?, ?)', (lender_id, borrower_id, amount))
                self.db.commit()
                users = [lender, borrower]
                return self.get_user_list(users)
        else:
            raise Exception('Not a supported post command.')
        
        return None

    def init_db(self, data=None):
        cursor = self.db.cursor()
        cursor.execute('''
        CREATE TABLE users(id INTEGER PRIMARY KEY, name TEXT)
        ''')
        self.db.commit()

        cursor.execute('''
        CREATE TABLE loans(lender_id INTEGER, borrower_id INTEGER, amount NUMERIC)
        ''')
        self.db.commit()

        if 'users' in data:
            # Add user records
            for user in data['users']:
                if 'name' in user:
                    cursor.execute('INSERT INTO users(name) VALUES(?)', (user['name'],))
            self.db.commit()

            # Add in loan records
            for user in data['users']:
                if 'name' in user and 'owes' in user:
                    # owed by is redundant, only need to insert for
                    # one or the other.
                    borrower = user['name']                    
                    borrower_id = self.get_user_id(cursor, borrower)
                    for lender, amount in user['owes'].items():
                        lender_id = self.get_user_id(cursor, lender)
                        cursor.execute('''
                        INSERT INTO loans(lender_id, borrower_id, amount) 
                        VALUES(?, ? , ?)
                        ''', (lender_id, borrower_id, amount))
                        
            self.db.commit()
    
    def get_user(self, cursor, id):
        user = {}
        name = ''
        cursor.execute('SELECT name FROM users WHERE id = ?', (id,))
        row = cursor.fetchone()
        if row == None:
            raise Exception(f'User with id = {id} not found in database.')
        else:
            name = row['name']
        user['name'] = name
        
        balance = 0
        
        # read the owes and owed by section
        cursor.execute('''
        SELECT 
            users.name AS name
            , sum(borrow.amount) AS borrow
            , sum(lend.amount) AS lend
        FROM
            users
            LEFT OUTER JOIN loans borrow ON borrow.lender_id = users.id AND borrow.borrower_id = ?
            LEFT OUTER JOIN loans lend ON lend.borrower_id = users.id AND lend.lender_id = ?
        WHERE
            borrow.borrower_id IS NOT NULL
            OR lend.lender_id IS NOT NULL
        GROUP BY
            users.name
        ORDER BY
            users.name
        ''', (id, id))
        rows = cursor.fetchall()
        owes = {}
        owed_by = {}
        for row in rows:
            if row['lend'] != None:
                lend = row['lend']
            else:
                lend = 0

            if row['borrow'] != None:
                borrow = row['borrow']
            else:
                borrow = 0

            if lend - borrow != 0:
                if lend > borrow:
                    owed_by[row['name']] = lend - borrow
                else:
                    owes[row['name']] = borrow - lend
                balance += (lend - borrow)
        
        user['owes'] = owes
        user['owed_by'] = owed_by

        user['balance'] = balance

        return user

    def get_user_id(self, cursor, name):
        id = 0
        cursor.execute('SELECT id FROM users WHERE name = ?', (name,))
        row = cursor.fetchone()
        if row == None:
            raise Exception(f'User: {name} not found in database.')
        else:
            id = row['id']
        return id
