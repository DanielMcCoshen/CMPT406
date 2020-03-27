import os

class config(object):
    SECRET_KEY=os.environ.get("SECRET_KEY") or ("totally_secure_key")
    VOTE_TIME=30
    VOTE_OPTIONS=2