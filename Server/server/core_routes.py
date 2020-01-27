from server import app
from flask_api import status
from flask import render_template, jsonify, request
from server.forms import position_form

@app.route('/', methods=['GET'])
def index():
    return "Home Page", status.HTTP_200_OK

@app.route('/game', methods=['POST'])
def create_game():
    res = {
        "room_id": "ABC12",
        "token": "xxxxx.yyyyy.zzzzz"
    }
    return jsonify(res), status.HTTP_201_CREATED

@app.route('/game/<game_id>/join', methods=['POST'])
def join_room(game_id):
    res = {
        "token": "xxxxx.yyyyy.zzzzz"
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
    return "", status.HTTP_200_OK
