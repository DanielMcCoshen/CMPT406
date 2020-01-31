from server import app
from flask_api import status
from flask import render_template, jsonify, request
from server.forms import position_form
from model import game_room, rooms, user
from datetime import datetime, timedelta
from server.helpers import room_not_found, invalid_token, access_denied
import jwt


@app.route('/game/<game_id>/users', methods=['GET'])
def get_all_users(game_id):
    token = request.headers.get('Authorization').replace("Bearer ", "")
    try:
        token_payload = jwt.decode(token, app.config['SECRET_KEY'], algorithms='HS256')
    except Exception:
       return invalid_token()
    current_room = rooms.get_map().get(game_id, None)
    if current_room is None:
        return room_not_found()
    if token_payload['role'] != 'game':
        return access_denied()

    users_list = [x.to_json() for x in current_room.get_all_users()]

    res  = {
        "users": users_list
    }
    return jsonify(res)

@app.route('/game/<game_id>/users/<user_name>', methods=['GET'])
def get_user(game_id, user_name):
    res = {
        "name": "Billy Nonexistant",
        "colour": "#00FFFF",
        "mischeif_points": 300
    }
    return jsonify(res)
