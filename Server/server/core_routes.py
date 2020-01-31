from server import app
from flask_api import status
from flask import render_template, jsonify, request
from server.forms import position_form
from model import game_room, rooms, user
from datetime import datetime, timedelta
from server.helpers import room_not_found, invalid_token, access_denied, user_exists
import jwt

@app.route('/', methods=['GET'])
def index():
    return "Home Page", status.HTTP_200_OK

@app.route('/game', methods=['POST'])
def create_game():
    new_game = game_room()
    room_id = new_game.get_id()
    rooms.get_map().update({room_id: new_game})
    expiry = datetime.utcnow() + timedelta(hours=5) # expire this room in 5 hours, not sure if should be used
    game_token = jwt.encode({"role": "game", "exp": expiry}, app.config['SECRET_KEY'], algorithm='HS256').decode('utf8')

    res = {
        "room_id": room_id,
        "token": game_token
    }
    return jsonify(res), status.HTTP_201_CREATED

@app.route('/game/<game_id>/join', methods=['POST'])
def join_room(game_id):
    current_room = rooms.get_map().get(game_id, None)
    if current_room is None:
        return room_not_found()
    request_json = request.json
    user_name = request_json['user_name']

    try:
        current_room.add_user(user(name=user_name, colour=request_json['colour']))
    except KeyError:
        return user_exists()

    expiry = datetime.utcnow() + timedelta(hours=5) # expire this room in 5 hours, not sure if should be used
    user_token = jwt.encode({'role': 'user', 'name': user_name, 'exp': expiry}, app.config['SECRET_KEY'], algorithm='HS256').decode('utf8')
    res = {
        "token": user_token
    }
    return jsonify(res), status.HTTP_200_OK

@app.route('/game/<game_id>/jobs', methods=['POST'])
def create_job(game_id):
    res = {
        "job_id": 10
    }
    return jsonify(res), status.HTTP_201_CREATED

@app.route('/game/<game_id>/jobs', methods=['GET'])
def current_job(game_id):
    res = {
        "options": [
            {
                "id": 3,
                "icon": "room3.png",
                "cost": 20
            },
            {
                "id": 4,
                "icon": "room4.png",
                "cost": -1
            }
        ],
        "time_remaining": 9.54,
        "mischeif_points": 100
    }
    return jsonify(res), status.HTTP_200_OK

@app.route('/game/<game_id>/jobs/vote', methods=['POST'])
def submit_vote(game_id):
    res = {
        "mischeif_points": 200
    }
    return jsonify(res), status.HTTP_200_OK

@app.route('/game/<game_id>/jobs/<job_id>', methods=['GET'])
def get_results(game_id, job_id):
    res = {
        "complete": True,
        "result": 5
    }
    return jsonify(res), status.HTTP_200_OK

@app.route('/game/<game_id>/jobs/<job_id>', methods=['PUT'])
def change_priority(game_id, job_id):
    return "", status.HTTP_200_OK

@app.route('/game/<game_id>', methods=['DELETE'])
def close_room(game_id):
    token = request.headers.get('Authorization').replace("Bearer ", "")
    try:
        token_payload = jwt.decode(token, app.config['SECRET_KEY'], algorithms='HS256')
    except Exception:
       return invalid_token()

    if token_payload['role'] != 'game':
       return access_denied()
    if rooms.get_map().pop(game_id, None) is None:
        return room_not_found()
    else:
        return "", status.HTTP_200_OK
