from server import app
from flask_api import status
from flask import render_template, jsonify, request
from server.forms import position_form
from model import game_room, rooms, user
from datetime import datetime, timedelta
import jwt

def room_not_found():
    res = {
        "error": "game not found"
    }
    return jsonify(res), status.HTTP_404_NOT_FOUND

def invalid_token():
    res = {
        "error": "Invalid Token"
    }
    return res, status.HTTP_403_FORBIDDEN

def access_denied():
    res = {
        "error": "Access Denied"
    }
    return res, status.HTTP_403_FORBIDDEN

def user_exists():
    res = {
        "error": "User Exists"
    }
    return res, status.HTTP_409_CONFLICT
