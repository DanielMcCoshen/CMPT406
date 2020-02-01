import random
import string



class user(object):
    def __init__(self, name, colour):
        self._name = name
        self._colour = colour
        self._mp = 0

    def get_mischeif_points(self):
        return self._mp

    def set_mischeif_points(self, mp):
        self._mp = mp

    def get_name(self):
        return self._name
    
    def get_colour(self):
        return self._colour

    def to_json(self):
        return {
            "name": self._name,
            "colour": self._colour,
            "mischeif_points": self._mp
        }

class game_room(object):
    def __init__(self):
        self._id = ''.join(random.choices(string.ascii_letters + string.digits, k=5))
        self._current_job = None
        self._jobs = []
        self._users = {}

    def get_id(self):
        return self._id

    def get_current_job(self):
        return self._current_job

    def add_job(self, new_job):
        if self._current_job is None:
            self._current_job = new_job
        else:
            self._jobs.append(new_job)

    def next_job(self):
        if not self._jobs:
            self._current_job = None
        else:
            self._current_job = self._jobs.pop(0)
        
        return self.get_current_job()

    def add_user(self, user: user):
        if user.get_name() in self._users:
            raise KeyError
        self._users.update({user.get_name(): user})

    def get_all_users(self):
        return self._users.values()

class rooms(object):
    _instance = None

    def __init__(self):
        raise RuntimeError("call get_list() instead")

    @classmethod
    def get_map(cls):
        if cls._instance is None:
            print("creating instance")
            cls._instance = {}
        return cls._instance