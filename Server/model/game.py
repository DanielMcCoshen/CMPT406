import random
import string
from datetime import datetime, timedelta

class option(object):
    def __init__(self, id, icon_path, cost):
        self._id = id
        self._icon = icon_path
        self._cost = cost
    
    def get_id(self):
        return self._id
    def get_icon(self):
        return self._icon
    def get_cost(self):
        return self._cost
    def to_json(self):
        return {
            "id": self._id,
            "icon": self._icon,
            "cost": self._cost
        }

class job(object):
    def __init__(self, options: list):
        self._options = [[option, 0] for option in options]
        self._id = None
        self._time_complete = None
        self._voters = []

    def set_id(self, id):
        self._id = id

    def get_id(self):
        return self._id

    def get_options(self):
        return [x[0] for x in self._options]

    def start(self, length: timedelta):
        self._time_complete = datetime.utcnow() + length
    
    def is_complete(self):
        if self._time_complete is None:
            return False
        else:
            return datetime.utcnow() >= self._time_complete

    def get_result(self):
        if self.is_complete():
            current = self._options[0]
            for x in self._options:
                if x[1] > current[1]:
                    current = x
            return current[0]
        else:
            return None

    def vote(self, option, user):
        if user in  self._voters:
            return None
        else: 
            for x in self._options:
                if x[0].get_id() == option:
                    x[1] = x[1] + 1
                    user.set_mischeif_points(user.get_mischeif_points() - x[0].get_cost())
                    self._voters.append(user)
            else:
                return None


    def to_json(self):
        complete = self.is_complete()
        if not complete:
            if self._time_complete is None:
                time_remaining = -1
            else:
                time_remaining = (self._time_complete - datetime.utcnow()).total_seconds()
        else:
            time_remaining = 0

        return {
            "options": [x[0].to_json() for x in self._options],
            "time_remaining": time_remaining
            }

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

class gameroom(object):
    def __init__(self, vote_time):
        self._id = ''.join(random.choices(string.ascii_letters + string.digits, k=5))
        self._current_job = None
        self._jobs = []
        self._users = {}
        self._next_job_id = 0
        self._complete_jobs = {}
        self._vote_len = vote_time

    def get_id(self):
        return self._id

    def get_current_job(self):
        if self._current_job is not None and self._current_job.is_complete():
            self._complete_jobs.update({self._current_job.get_id(): self._current_job})
            self.next_job()

        return self._current_job

    def add_job(self, new_job: job):
        new_job.set_id(self._next_job_id)
        self._next_job_id = self._next_job_id + 1
        if self._current_job is None:
            self._current_job = new_job
            new_job.start(self._vote_len)
        else:
            self._jobs.append(new_job)

    def next_job(self):
        if len(self._jobs) == 0:
            self._current_job = None
        else:
            self._current_job = self._jobs.pop(0)
            self._current_job.start(self._vote_len)
        
        return self.get_current_job()

    def add_user(self, user: user):
        if user.get_name() in self._users:
            raise KeyError
        self._users.update({user.get_name(): user})

    def get_all_users(self):
        return self._users.values()

    def get_user(self, user_name, default = None):
        return self._users.get(user_name, default)

    def get_result(self, job_id):
        job = self._complete_jobs.get(job_id, None)
        if job is None and self._current_job.get_id() == job_id:
            job = self._current_job
        elif job is None:
            for j in self._jobs:
                if j.get_id() == job_id:
                    job = j
                    break
            else: 
                raise RuntimeError("Job Does Not Exist")
        return job.get_result()

class rooms(object):
    _instance = None

    def __init__(self):
        raise RuntimeError("call get() instead")

    @classmethod
    def get_map(cls):
        if cls._instance is None:
            print("creating instance")
            cls._instance = {}
        return cls._instance

class optionlist(object):
    _instance = None
    
    def __init__(self):
        raise RuntimeError("call get() instead")

    @classmethod
    def get(cls):
        if cls._instance is None:
            print("populating options list")
            cls._instance = {
                # All
                0:  [
                        option(0, "Paved Holes", 0)
                    ],
                # N
                1:  [
                        option(32, "Ice Room", 0),
                        option(33, "Skinny Path", 0),
                        option(34, "Straight Islands", 0),
                        option(35, "Paved Hole", 0)
                    ],
                # E
                2:  [
                        option(27, "Curve", 0),
                        option(28, "Ice Room", 0),
                        option(29, "Skinny Path", 0),
                        option(30, "Straight Islands", 0),
                        option(31, "Paved Hole", 0)
                    ],
                # S
                3:  [
                        option(36, "Ice Room", 0),
                        option(37, "Skinny Path", 0),
                        option(38, "Straight Islands", 0),
                        option(39, "Paved Hole", 0)
                    ],
                # W
                4:  [
                        option(40, "Curve", 0),
                        option(41, "Ice Room", 0),
                        option(42, "Skinny Path", 0),
                        option(43, "Straight Islands", 0),
                        option(44, "Paved Hole", 0)
                    ],
                # N-E
                5:  [
                        option(1, "Curve Islands", 0),
                        option(2, "Ice Room", 0),
                        option(3, "Skinny Path", 0),
                        option(4, "Paved Hole", 0)
                    ],
                # S-E
                6:  [
                        option(5, "Curve Islands", 0),
                        option(6, "Ice Room", 0),
                        option(7, "Skinny Path", 0),
                        option(8, "Paved Hole", 0)
                    ],
                # W-S
                7:  [
                        option(14, "Curve Islans", 0),
                        option(15, "Ice Room", 0),
                        option(16, "Skinny Path", 0),
                        option(17, "Paved Hole", 0)
                    ],
                # W-N
                8:  [
                        option(9, "Corner Islands", 0),
                        option(10, "Curve Islands", 0),
                        option(11, "Ice Room", 0),
                        option(12, "Skinny Path", 0),
                        option(13, "Paved Hole", 0)
                    ],
                # N-S
                9:  [
                        option(18, "Ice Room", 0),
                        option(19, "Skinny Path", 0),
                        option(20, "Straight Islands", 0),
                        option(21, "Paved Hole", 0)
                    ],
                # W-E
                10: [
                        option(22, "Ice Room", 0),
                        option(23, "Ice Strips", 0),
                        option(24, "Skinny Path", 0),
                        option(25, "Straight Islands", 0),
                        option(26, "Paved Holes", 0)
                    ],
                # N-T
                11: [
                        option(46, "Paved Holes", 0)
                    ],
                # E-T
                12: [
                        option(45, "Paved Holes", 0)
                    ],
                # S-T
                13: [   
                        option(47, "Paved Holes", 0)
                    ],
                # W-T
                14: [
                        option(48, "Paved Holes", 0)
                    ]
            }
        return cls._instance
