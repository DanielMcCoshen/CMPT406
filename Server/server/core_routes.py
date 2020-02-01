from server import app
from flask_api import status
from flask import render_template, jsonify, request
from server.forms import position_form
from model import game_room, rooms, user
from datetime import datetime, timedelta
from server.helpers import room_not_found, invalid_token, access_denied, user_exists, validate_game, room_exists
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
    if not room_exists(game_id):
        return room_not_found()

    current_room = rooms.get_map().get(game_id, None)
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
    token = request.headers.get('Authorization').replace("Bearer ", "")
    valid = validate_game(game_id, token)
    if valid is not None:
        return valid

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
    valid = validate_game(game_id, token)
    if valid is not None:
        return valid
    else:
        rooms.get_map().pop(game_id, None) 
        return "", status.HTTP_200_OK
