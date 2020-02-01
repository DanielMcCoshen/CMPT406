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

def room_exists(game_id):
    rooms.get_map().get(game_id, None) is not None

def validate_game(game_room, token):
    try:
        token_payload = jwt.decode(token, app.config['SECRET_KEY'], algorithms='HS256')
    except Exception:
       return invalid_token()

    if token_payload['role'] != 'game':
       return access_denied()
    elif not room_exists(game_room):
        return room_not_found()
    else:
        return None




