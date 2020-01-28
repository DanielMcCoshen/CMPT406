import random
import string

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

class game_room(object):
    def __init__(self):
        self.id = ''.join(random.choices(string.ascii_letters + string.digits, k=5))
        self.current_job = None
        self.jobs = []
        self.users = []

    def get_id(self):
        return self.id

    def get_current_job(self):
        return self.current_job

    def add_job(self, new_job):
        if self.current_job is None:
            self.current_job = new_job
        else:
            self.jobs.append(new_job)

    def next_job(self):
        if not self.jobs:
            self.current_job = None
        else:
            self.current_job = self.jobs.pop(0)
        
        return self.get_current_job()
