from server import app
from flask_api import status
from flask import render_template, jsonify, request
from server.forms import position_form

@app.route('/game/<game_id>/users', methods=['GET'])
def get_all_users(game_id):
    res  = {
        "users": [
            {
                "name": "Billy Nonexistant",
                "colour": "#00FFFF",
                "mischeif_points": 300
            },
            {
                "name": "John Test",
                "colour": "#FFFF00",
                "mischeif_points": 300
            }
        ]
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
